using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RAC.RACModels;

namespace RAC.BusinessLogic
{
    public class RACRequestBLL
    {
        public ERRORS StartRacRequest()
        {
            return ERRORS.SUCCESS;
        }

        public static RACRequest SaveRACRequest(Candidate user)
        {
            RACRequest racRequestFromDb = DbContext.Context.RACRequest.FirstOrDefault(x => x.Id == user.RACRequest.Id);

            if (racRequestFromDb != null)
            {
                racRequestFromDb = CopyAnswersFromForm(racRequestFromDb, user.RACRequest);
                DbContext.Context.SaveChanges();
                return racRequestFromDb;
            }

            return null;
        }

        public static RACRequest SubmitRACRequest(Candidate user)
        {
            RACRequest racRequestFromDb = DbContext.Context.RACRequest.FirstOrDefault(x => x.Id == user.RACRequest.Id);

            if (racRequestFromDb != null)
            {
                racRequestFromDb = CopyAnswersFromForm(racRequestFromDb, user.RACRequest);
                racRequestFromDb.Status = (int) RACStatus.Submitted;
                racRequestFromDb.SubmissionDate = DateTime.Now;
                DbContext.Context.SaveChanges();

                //Sending notification to RAC Advisor
                Notification.CreateNotification(user.Id, Messages.RAC_REQUEST_SUBMITTED, (int)userType.RACAdvisor);
                return racRequestFromDb;
            }

            return null;
        }

        private static RACRequest CopyAnswersFromForm(RACRequest racRequestFromDb, RACRequest racRequestFromForm)
        {
            ICollection<RACRequestCompetency>
                racRequestCompetenciesFromForm = racRequestFromForm.RACRequestCompetencies;
            ICollection<RACRequestCompetency> racRequestCompetenciesFromDb = racRequestFromDb.RACRequestCompetencies;
            var competenciesIndex = 0;

            //Looping through the RAC Request from the DB, and mapping the ids to user's updated values
            //TODO compare they entered to Enums, Make sure that the RACRequest and it's competencies belong to the user. Do input checks
            if (racRequestCompetenciesFromForm.Count == racRequestCompetenciesFromDb.Count)
            {
                while (competenciesIndex < racRequestCompetenciesFromDb.Count)
                {
                    RACRequestCompetency racRequestCompetencyFromForm =
                        racRequestCompetenciesFromForm.ElementAt(competenciesIndex);
                    RACRequestCompetency racRequestCompetencyFromDb =
                        racRequestCompetenciesFromDb.ElementAt(competenciesIndex);
                    if (racRequestCompetencyFromForm.RACRequestCompetencyElements.Count ==
                        racRequestCompetencyFromDb.RACRequestCompetencyElements.Count)
                    {
                        var competencyElementIndex = 0;
                        while (competencyElementIndex < racRequestCompetencyFromDb.RACRequestCompetencyElements.Count)
                        {
                            RACRequestCompetencyElement competencyElementFromForm = racRequestCompetenciesFromForm
                                .ElementAt(competenciesIndex).RACRequestCompetencyElements.FirstOrDefault(x =>
                                    x.Id == racRequestCompetencyFromDb.RACRequestCompetencyElements
                                        .ElementAt(competencyElementIndex).Id);
                            if (competencyElementFromForm != null)
                            {
                                racRequestFromDb.RACRequestCompetencies.ElementAt(competenciesIndex)
                                        .RACRequestCompetencyElements.ElementAt(competencyElementIndex).Answer =
                                    Enum.IsDefined(typeof(SelfEvaluationAnswer), competencyElementFromForm.Answer)
                                        ? competencyElementFromForm.Answer
                                        : 0;
                            }

                            competencyElementIndex++;
                        }
                    }

                    if (racRequestCompetencyFromForm.CompetencyComments.Count ==
                        racRequestCompetencyFromDb.CompetencyComments.Count)
                    {
                        var competencyCommentIndex = 0;
                        //Loop for saving list of comments
                        while (competencyCommentIndex < racRequestCompetencyFromDb.CompetencyComments.Count)
                        {
                            CompetencyComment competencyCommentFromFormEntry =
                                racRequestCompetencyFromForm.CompetencyComments.FirstOrDefault(x =>
                                    x.Id == racRequestCompetencyFromDb.CompetencyComments
                                        .ElementAt(competencyCommentIndex).Id);
                            if (competencyCommentFromFormEntry != null)
                            {
                                // Following Null Reference can not occur naturally
                                // ReSharper disable PossibleNullReferenceException
                                racRequestCompetenciesFromDb.ElementAtOrDefault(competenciesIndex)                                    
                                        .CompetencyComments.ElementAtOrDefault(competencyCommentIndex).Comment =
                                    competencyCommentFromFormEntry.Comment?.Replace("\n", "<br/>")
                                        .Replace("\r", "") ?? "";
                                // ReSharper restore PossibleNullReferenceException
                            }

                            competencyCommentIndex++;
                        }
                    }

                    competenciesIndex++;
                }
            }

            return racRequestFromDb;
        }

        public void ChangeRACStatusSubmitted(RACRequest currRac)
        {
            currRac.Status = 1;
            DbContext.Context.Entry(currRac).State = EntityState.Modified;
            DbContext.Context.SaveChanges();
        }

        public static RACRequest GetRACByCandidate(int candidateId)
        {
            RACRequest foundRac;
            IEnumerable<RACRequest> matchingRac =
                DbContext.Context.RACRequest.Where(r => r.Candidate.Id == candidateId);

            try
            {
                foundRac = matchingRac.First();
            }
            catch
            {
                foundRac = null;
            }

            return foundRac;
        }

        public List<RACRequest> GetAllRacRequests()
        {
            return DbContext.Context.RACRequest.ToList();
        }

        public static RACRequest ChangeProgram(Candidate candidate, int programId, bool IsGenEdOnly)
        {
            
                foreach (RACRequestCompetency comp in candidate.RACRequest.RACRequestCompetencies)
                {
                    DbContext.Context.CompetencyComment.RemoveRange(comp.CompetencyComments);
                    DbContext.Context.RACRequestCompetencyElement.RemoveRange(comp.RACRequestCompetencyElements);
                }

                DbContext.Context.RACRequestCompetency.RemoveRange(candidate.RACRequest.RACRequestCompetencies);

                RACRequest rac = DbContext.Context.RACRequest.First(c => c.Id == candidate.RACRequest.Id);
                rac.StartDate = DateTime.Now;
                rac.IsGenEdOnly = IsGenEdOnly;
                rac.ProgramId = programId;
                rac.Status = (int) RACStatus.Started;
                rac.SubmissionDate = null;
                rac.Program = ProgramBLL.GetProgramById(programId);

                foreach (Competency comp in IsGenEdOnly == false
                    ? rac.Program.Competencies
                    : rac.Program.Competencies.Where(x => x.CompetencyMinistryData.IsGenEd))
                {
                    var rComp = new RACRequestCompetency
                    {
                        Competency = comp,
                        RACRequestId = rac.Id,
                        RACRequest = rac,
                        CompetencyId = comp.Id,
                        CompetencyComments = new List<CompetencyComment>(),
                        RACRequestCompetencyElements = new List<RACRequestCompetencyElement>()
                    };

                    var competencyComment = new CompetencyComment
                    {
                        Id = -1,
                        Comment = "",
                        UserId = candidate.Id,
                        RACRequestCompetencyId = comp.Id,
                        RACRequestCompetency = rComp
                    };

                    rComp.CompetencyComments.Add(competencyComment);

                    foreach (CompetencyElement el in comp.CompetencyElements.Where(x => x.DateExpired == null))
                    {
                        rComp.RACRequestCompetencyElements.Add(new RACRequestCompetencyElement
                        {
                            Answer = 0,
                            CompetencyElement = el,
                            CompetencyElementId = el.Id,
                            RACRequestCompetency = rComp,
                            RACRequestCompetencyId = rComp.Id
                        });
                    }

                    DbContext.Context.RACRequestCompetency.Add(rComp);
                    DbContext.Context.RACRequestCompetencyElement.AddRange(rComp.RACRequestCompetencyElements);
                }

                DbContext.Context.SaveChanges();

                return rac;
            
        }

        public static bool IsManditoryCommentsFilled(Candidate user)
        {
            /*
             * This controller method confirms that positive answers have an assoicated comment.
             *
             * Parameters:
             * Candidate user - The user object of the client making the request.
             * Returns:
             * A JsonResult that represents the success/failure Boolean.
             */


            foreach (RACRequestCompetency racRequestRacRequestCompetency in user.RACRequest.RACRequestCompetencies)
            {
                foreach (RACRequestCompetencyElement racRequestCompetencyElementse in racRequestRacRequestCompetency.RACRequestCompetencyElements)
                {
                    // If Answer is **not** negative...
                    if (racRequestCompetencyElementse.Answer != (int) SelfEvaluationAnswer.CannotDoThis) 
                    {
                        // ...and **not** unanswered
                        if (racRequestCompetencyElementse.Answer != (int) SelfEvaluationAnswer.NotAnswered)
                        {
                            foreach (CompetencyComment competencyComment in racRequestRacRequestCompetency.CompetencyComments)
                            {
                                if (string.IsNullOrWhiteSpace(competencyComment.Comment))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }


        public static bool IsAllAnswersFilled(Candidate user)
        {
            foreach (RACRequestCompetency racRequestRacRequestCompetency in user.RACRequest.RACRequestCompetencies)
            {
                foreach (RACRequestCompetencyElement racRequestCompetencyElement in racRequestRacRequestCompetency.RACRequestCompetencyElements)
                {
                    if (racRequestCompetencyElement.Answer == (int) SelfEvaluationAnswer.NotAnswered)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}