using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using RAC.RACModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using RAC.Models;

namespace RAC.BusinessLogic
{
    public static class CandidateBLL
    {
        /*
         * Contains all business logic code for handling candidate information
         */
        public static List<ERRORS> Register(RACUser newCandidate, int racProgramId, string baseUrl = "")
        {
            /*
             * The method accepts a Candidate object, program ID, and URL. An ID is created for the new user based on the next 
             * available ID in the database. Once the account is created,
             * a verification email is sent to the candidate to verify their account, and an email is sent to the RAC advisor to 
             * let them know a new account is created. 
             *
             * See also:
             * AccountController -> Register()
             *
             * Parameters:
             * Candidate newCandidate = The candidate object for the new candidate
             * int RACProgramId = The program ID that the candidate has chosen to apply for in RAC
             * String baseUrl = The URL used to send an email notification too.
             * 
             * 
             * Returns:
             * A list of possible errors. Contains SUCCESS if there are no errors that occurred.
             * 
             */

            List<ERRORS> errors = CheckCandidateRequiredInformation(newCandidate);
            if (errors.Count > 0)
            {
                return errors;
            }

            Candidate c = null;
            try
            {
                /*
                 * create candidate object
                 */
                c = new Candidate
                {
                    RACRequest = new RACRequest
                    {
                        Id = -1,
                        StartDate = DateTime.Now,
                        Status = (int)RACStatus.Started,
                        ProgramId = racProgramId,
                        UploadedDocuments = null,
                        IsGenEdOnly = false,
                        Program = ProgramBLL.GetProgramById(racProgramId)
                    },
                    CSAdminId = newCandidate.CSAdminId,
                    CompetencyComments = new List<CompetencyComment>()
                };

                /*
                 * Creating the RAC Request for the candidate. 
                 * The uploaded documents will be null since they have not had the chance to upload a document yet.
                 * The RAC Request will be saved, along with all fields that need to be filled out. (Defaulted to null). 
                 */
                //IEnumerable<User> users = DbContext.Context.User.Where(u => u.UserType == (int) userType.Candidate);
                //int userId;
                //if (users.Count() != 0)
                //{
                //    userId = users.Max(i => i.Id) + 1;
                //}

                //Checking if there they choose to select general education only
                DbContext.Context.Candidate.Add(c);
                DbContext.Context.SaveChanges();

                //TODO: Add serever side form field validation
                //If they choose gen-ed only will only select gen-ed competencies in the program
                foreach (Competency rrc in c.RACRequest.IsGenEdOnly == false
                    ? c.RACRequest.Program.Competencies.Where(x=> x.IsActive) : c.RACRequest.Program.Competencies.Where(x => x.CompetencyMinistryData.IsGenEd && x.IsActive))
                {
                    var RACRequestCompetency = new RACRequestCompetency();
                    RACRequestCompetency.Id = -1;
                    var competencyComment = new CompetencyComment
                    {
                        Id = -1,
                        Comment = "",
                        UserId = c.Id,
                        RACRequestCompetencyId = RACRequestCompetency.Id,
                        RACRequestCompetency = RACRequestCompetency,
                        Candidate = c
                    };
                    RACRequestCompetency.CompetencyComments.Add(competencyComment);
                    foreach (CompetencyElement rrce in rrc.CompetencyElements.Where(x => x.DateExpired == null))
                    {
                        var racRequestCompetencyElements = new RACRequestCompetencyElement()
                        {
                            Answer = 0,
                            CompetencyElement = rrce,
                            RACRequestCompetencyId = RACRequestCompetency.Id,
                            RACRequestCompetency = RACRequestCompetency,
                            CompetencyElementId = rrce.Id,
                            Id = -1,
                        };

                        RACRequestCompetency.RACRequestCompetencyElements.Add(racRequestCompetencyElements);
                    }

                    RACRequestCompetency.Competency = rrc;
                    RACRequestCompetency.CompetencyId = rrc.Id;
                    RACRequestCompetency.RACRequest = c.RACRequest;
                    RACRequestCompetency.RACRequestId = c.RACRequest.Id;
                    c.RACRequest.RACRequestCompetencies.Add(RACRequestCompetency);
                }

                DbContext.Context.SaveChanges();
                errors.Add(ERRORS.SUCCESS);

                /*
                 * Send email for RAC Advisor to know about the new account creation.
                 */
                                 
                string to = WebConfigurationManager.AppSettings.Get("RacAdvisorEmail"); // set to my email for now
                string message = "Hello Alain, <br /><br /> You are receiving this email to notify you that "
                    + newCandidate.FirstName + " " + newCandidate.LastName + " has create a new account in the RAC system. Sign in to view their details";


                int ret = Notification.sendEmailWithMessage(to, message, "New RAC Account Created");
                if (ret == -1)
                {
                   errors.Add(ERRORS.EMAIL_COULD_NOT_SEND);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                if (c != null)
                {
                    if (c.RACRequest != null)
                    {
                        DbContext.Context.RACRequest.Remove(c.RACRequest);
                    }
                    DbContext.Context.Candidate.Remove(c);
                    DbContext.Context.SaveChanges();
                }
            }

            return errors;
        }

        //DONE
        public static Candidate GetCandidateProfile(int id)
        {
            Candidate searchCan = new Candidate();
            try
            {
                searchCan = DbContext.Context.Candidate.Where(c => c.Id == id).First();
            }
            catch
            {
                searchCan = null;
            }
            return searchCan;
        }

        //DONE
        public static int getCandidateIdByEmail(string email)
        {
            ApplicationUser membershipUser = GetMembershipUserByEmail(email);
            return ((Candidate)DbContext.Context.Candidate.First(u => u.CSAdminId.Equals(membershipUser.Id))).Id;
        }

        //DONE
        public static List<ERRORS> CheckCandidateRequiredInformation(RACUser newCandidate)
        {
            /*
             * The method takes in the new candidate's information and verifies that all of the required information is properly entered. 
             *
             * See also:
             * Register(Candidate newCandidate, int RACProgramId, string baseUrl="")
             *
             * Parameters:
             * Candidate newCandidate = The candidate object for the new candidate
             *
             * Returns:
             * A list of possible errors. The list is empty if there are no errors. 
             * 
             */
            var errors = new List<ERRORS>();

            if (newCandidate.FirstName == null)
            {
                errors.Add(ERRORS.INVALID_FIRST_NAME_NULL);
            }

            if (newCandidate.LastName == null)
            {
                errors.Add(ERRORS.INVALID_LAST_NAME_NULL);
            }

            if (newCandidate.HomePhone == null)
            {
                errors.Add(ERRORS.INVALID_HOME_PHONE_NULL);
            }

            if (newCandidate.Email == null)
            {
                errors.Add(ERRORS.INVALID_EMAIL_NULL);
            }

            if (IsExistingEmail(newCandidate.Email))
            {
                errors.Add(ERRORS.EMAIL_ALREADY_IN_USE);
            }

            return errors;
        }

        //DONE
        public static RACUser MapUserInformation(ApplicationUser membershipUser)
        {

            var user = new RACUser()
            {
                CSAdminId = membershipUser.Id,
                FirstName = membershipUser.FirstName,
                LastName = membershipUser.LastName,
                Email = membershipUser.Email,
                HomePhone = membershipUser.HomePhone,
                WorkPhone = membershipUser.WorkPhone,
                City = membershipUser.City,
                Street = membershipUser.Street,
                Province = membershipUser.Province,
                Country = membershipUser.Country,
                IsDeleted = membershipUser.IsDeleted,
                IsArchived = membershipUser.IsArchived,
                IsPrivacyPolicyAccepted = membershipUser.IsPrivacyPolicyAccepted,
                DateRegistered = membershipUser.DateRegistered,
                RegistrationIP = membershipUser.RegistrationIP,
                UserType = (int)userType.Candidate,
                PreferredMethodOfContact = membershipUser.PreferredMethodOfContact,
                CandidateProfile = GetCandidateProfile(getCandidateIdByEmail(membershipUser.Email))
            };

            return user;
        }

        public static ApplicationUser GetMembershipUserById(string CSAdminId)
        {
            UserManager<ApplicationUser> userManager;
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser membershipUser = userManager.FindById(CSAdminId);
            return membershipUser;
        }

        public static ApplicationUser GetMembershipUserByEmail(String email)
        {
            UserManager<ApplicationUser> userManager;
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user = userManager.FindByEmail(email);
            userManager.Dispose();
            return user;
        }
        public static RACUser GetUserByEmail(string email)
        {
            /*
             * The method will search for a user by email address. 
             *
             * See also:
             * IsExistingEmail(string email)
             * AccountController -> Login()
             * Models -> Validation -> UserVal()
             *
             * Parameters:
             * String email = The email of the candidate that is to be searched for.
             *
             * Returns:
             * A User object. The object will be null if not found. 
             * 
             */




            ApplicationUser membershipUser = GetMembershipUserByEmail(email);
            Candidate foundCandidate;
            IEnumerable<Candidate> matchingUsers =
                DbContext.Context.Candidate.Where(u => u.CSAdminId.Equals(membershipUser.Id));

            RACUser racUser = new RACUser();
            try
            {
                foundCandidate = matchingUsers.First();
                racUser = MapUserInformation(membershipUser);
            }
            catch
            {
                racUser = null;
            }

            return racUser;
        }

        public static bool IsExistingEmail(string email)
        {
            /*
             * The method will check to see if the email passed in is already registered in the database.  
             *
             * See also:
             * AccountController -> CheckUniqueEmail()
             *
             * Parameters:
             * String email = The email of the candidate that is to be searched for.
             *
             * Returns:
             * True of False based on if the email exists or not 
             * 
             */

            var user = (RACUser)GetUserByEmail(email);
            return !(user == null || user.IsDeleted);
        }

        public static string GetProvinceEnumString(string enumChosen)
        {
            /*
             * The method take in the chosen province and get the proper string equivalent. This is needed because some provinces have spaces in the
             * name and ENUMs don't support spaces so we need to convert the string.
             *
             * See also:
             * Register()
             *
             * Parameters:
             * String enumChosen = The enum that has been chosen.
             *
             * Returns:
             * String that is the proper string value for the province. 
             * 
             */
            string retVal = null;
            if (enumChosen != null)
            {
                try
                {
                    retVal = Enum.GetName(typeof(provinces), int.Parse(enumChosen));
                }
                catch
                {
                    retVal = enumChosen;
                }
            }

            return retVal;
        }

        //[Currently borken]
        public static void DeleteCandidate(RACUser usr)
        {
            /*
             * The method will call the EF functionality to set the IsDeleted flag to true.
             * This will prevent the user from being able to log in to that account, and will allow the email to be reused. 
             *
             * See also:
             * None
             *
             * Parameters:
             * id: The id of the user to be deleted
             *
             * Returns:
             * void
             *
             */

            try
            {


                usr.IsDeleted = true;
                UpdateCandidate(usr);
                //user.IsDeleted = true;
                //Sending notification
                Notification.CreateNotification(usr.CandidateProfile.Id, Messages.CANDIDATE_ACCOUNT_DELETED, (int)userType.RACAdvisor);
                DbContext.Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        //[Currently borken]
        public static void ArchiveCandidate(RACUser usr)
        {
            /*
             * The method will call the EF functionality to set the IsArchived flag to true.
             * This will prevent the user from being able to modify anything in their account. 
             *
             * See also:
             * None
             *
             * Parameters:
             * id: The id of the user to be archived
             *
             * Returns:
             * void
             *
             */

            try
            {
                usr.IsArchived = true;
                UpdateCandidate(usr);

                var user = (Candidate)DbContext.Context.Candidate.First(u => u.Id == usr.CandidateProfile.Id);
                //user.IsArchived = true;
                DbContext.Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
        public static void ArchiveCandidate(int id)
        {
           // overload
            try
            {
                RACUser can = CandidateBLL.GetCandidateById(id);
                ArchiveCandidate(can);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
        //[Currently Broken]
        public static bool UpdateCandidate(RACUser candidate)
        {


            UserManager<ApplicationUser> userManager;
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManager.FindByEmail(candidate.Email);

            user.FirstName = CapitalizeFirstLetter(candidate.FirstName);
            user.LastName = CapitalizeFirstLetter(candidate.LastName);
            user.HomePhone = candidate.HomePhone;
            user.IsArchived = candidate.IsArchived;
            user.IsDeleted = candidate.IsDeleted;
            user.WorkPhone = candidate.WorkPhone;
            user.PreferredMethodOfContact = candidate.PreferredMethodOfContact;
            user.Street = candidate.Street;
            user.City = candidate.City;
            user.Province = candidate.Province;
            user.Country = candidate.Country;

            
            userManager.Update(user);
            var sessionUser = MapUserInformation(user);

            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                HttpContext.Current.Session["User"] = sessionUser;
            }

            return true;

        }

        public static Candidate ChangeProgram(Candidate candidate, int programId, bool isGenEdOnly)
        {
            
                candidate.RACRequest = RACRequestBLL.ChangeProgram(candidate, programId, isGenEdOnly);
                //Sending notification
                Notification.CreateNotification(candidate.Id, Messages.RAC_PROGRAM_CHANGE, (int)userType.RACAdvisor);
                DbContext.Context.SaveChanges();
                return candidate;
           
        }

        public static List<RACUser> GetAllCandidates()
        {
            List<Candidate> users = DbContext.Context.Candidate.ToList();
            List<RACUser> candidates = new List<RACUser>();

            foreach (Candidate user in users)
            {
                candidates.Add(MapUserInformation(GetMembershipUserById(user.CSAdminId)));
            }

            return candidates;
        }

        public static RACUser GetCandidateById(int id)
        {
            RACUser user = new RACUser();
            try
            {
                var candidate = DbContext.Context.Candidate.Where(u => u.Id == id).First();
                ApplicationUser membershipUser = GetMembershipUserById(candidate.CSAdminId);
                user = MapUserInformation(membershipUser);
            }
            catch (Exception e)
            {
                user = null;
            }

            return user;
        }

        public static List<NotificationsViewModel> GetCandidateNotifications(int id)
        {
            IQueryable<RACNotification> notificationsList = DbContext.Context.RACNotification.Where(r => 
                    r.CandidateId == id && r.UserType == (int) userType.Candidate);

            List<RACNotification> notifications = notificationsList.ToList();
            List<NotificationsViewModel> notificationsVm = MapNotificationsToViewModels(notifications);
            return notificationsVm;

        }
        public static CandidateHomeViewModel MapToHomeViewModel(RACUser user, CandidateHomeViewModel cvm)
        {
            if (user == null || cvm == null)
            {
                return null;
            }

            return cvm;
        }
        private static List<NotificationsViewModel> MapNotificationsToViewModels(IEnumerable<RACNotification> notification)
        {
            var notifications = new List<NotificationsViewModel>();
            foreach (RACNotification notificatonInstance in notification)
            {
                var description = "";

                switch ((Messages)notificatonInstance.NotificationType)
                {
                    case Messages.RAC_ADVISOR_UPLOAD_TO_CANDIDATE:
                        description = "A new document has been uploaded to your account!";
                        break;
                }


                notifications.Add(new NotificationsViewModel
                {
                    Id = notificatonInstance.Id,
                    Candidate = CandidateBLL.MapUserInformation(CandidateBLL.GetMembershipUserById(notificatonInstance.Candidate.CSAdminId)),
                    Description = description,
                    Date = notificatonInstance.Time,
                    NotificationType = notificatonInstance.NotificationType
                });
            }

            //Sorting by date now
            notifications = notifications.OrderByDescending(x => x.Date).ToList();
            return notifications;
        }
        private static string CapitalizeFirstLetter(string name)
        {
            switch (name)
            {
                case null:
                    throw new ArgumentNullException(nameof(name));
                case "":
                    throw new ArgumentException($@"{nameof(name)} cannot be empty.", nameof(name));
                default:
                    return name.First().ToString().ToUpper() + name.Substring(1);
            }
        }

        public static RACUser GetCandidateByUserType()
        {
            var user = (RACUser)HttpContext.Current.Session["User"];
            RACUser currentCandidate;

            if (user == null)
            {
                currentCandidate = null;
            }
            else
            {
                if (user.UserType == (int)userType.Candidate)
                {
                    currentCandidate = user;
                }
                else
                {
                    currentCandidate = (RACUser)HttpContext.Current.Session["Candidate"];
                }
            }

            return currentCandidate;

        }
    }
}