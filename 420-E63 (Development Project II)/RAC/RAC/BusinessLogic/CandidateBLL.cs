using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using RAC.RACModels;

namespace RAC.BusinessLogic
{
    public static class CandidateBLL
    {
        /*
         * Contains all business logic code for handling candidate information
         */
        public static List<ERRORS> Register(Candidate newCandidate, int racProgramId, string baseUrl = "")
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
                    FirstName = CapitalizeFirstLetter(newCandidate.FirstName),
                    LastName = CapitalizeFirstLetter(newCandidate.LastName),
                    Email = newCandidate.Email,
                    HomePhone = newCandidate.HomePhone,
                    WorkPhone = newCandidate.WorkPhone,
                    UserType = (int) userType.Candidate,
                    City = newCandidate.City,
                    Street = newCandidate.Street,
                    Country = newCandidate.Country,
                    DateRegistered = newCandidate.DateRegistered,
                    Province = GetProvinceEnumString(newCandidate.Province),
                    PreferredMethodOfContact = newCandidate.PreferredMethodOfContact,
                    IsArchived = newCandidate.IsArchived,
                    RegistrationIP = newCandidate.RegistrationIP,
                    IsDeleted = newCandidate.IsDeleted,
                    IsPrivacyPolicyAccepted = newCandidate.IsPrivacyPolicyAccepted,
                    RACRequest = new RACRequest
                    {
                        Id = -1,
                        StartDate = DateTime.Now,
                        Status = (int)RACStatus.Started,
                        ProgramId = racProgramId,
                        UploadedDocuments = null,
                        IsGenEdOnly = false,
                        Program = ProgramBLL.GetProgramById(racProgramId)
                    }
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
                DbContext.Context.User.Add(c);
                DbContext.Context.SaveChanges();

                //TODO: Add serever side form field validation
                //If they choose gen-ed only will only select gen-ed competencies in the program
                foreach (Competency rrc in c.RACRequest.IsGenEdOnly == false
                    ? c.RACRequest.Program.Competencies : c.RACRequest.Program.Competencies.Where(x => x.CompetencyMinistryData.IsGenEd))
                {
                    var RACRequestCompetency = new RACRequestCompetency();
                    RACRequestCompetency.Id = -1;
                    var competencyComment = new CompetencyComment
                    {
                        Id = -1,
                        Comment = "",
                        UserId = c.Id,
                        RACRequestCompetencyId = RACRequestCompetency.Id,
                        RACRequestCompetency = RACRequestCompetency
                    };
                    RACRequestCompetency.CompetencyComments.Add(competencyComment);
                    foreach (CompetencyElement rrce in rrc.CompetencyElements)
                    {
                        var racRequestCompetencyElements = new RACRequestCompetencyElement()
                        {
                            Answer = -1,
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
                                 
                string racAdvisor = WebConfigurationManager.AppSettings.Get("RacAdvisorEmail"); // set to my email for now
                string to = racAdvisor;


                
                string message = "Hello Alain, <br /><br /> You are receiving this email to notify you that "
                    + c.FirstName +" "+c.LastName+ " has create a new account in the RAC system. Sign in to view their details";


                int ret = Notification.sendEmailWithMessage(to, message, "New RAC Account Created"); 
                if (ret == -1)
                {
                    //error sending email
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
                    DbContext.Context.User.Remove(c);
                    DbContext.Context.SaveChanges();
                }
            }

            return errors;
        }
        public static int getCandidateIdByEmail(string email) {
            return ((Candidate)DbContext.Context.User.First(u => u.Email == email)).Id;
        }
        public static List<ERRORS> CheckCandidateRequiredInformation(Candidate newCandidate)
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

            return errors;
        }

        public static User GetUser(string email)
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
            User foundCandidate;
            IEnumerable<User> matchingUsers = 
                DbContext.Context.User.Where(u => u.Email.Equals(email));

            try
            {
                foundCandidate = matchingUsers.First();
            }
            catch
            {
                foundCandidate = null;
            }

            return foundCandidate;
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

            var user = (Candidate)GetUser(email);
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

        public static void DeleteCandidate(int id)
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
                var user = (Candidate)DbContext.Context.User.First(u => u.Id == id);
                user.IsDeleted = true;
                //Sending notification
                Notification.CreateNotification(user.Id, Messages.CANDIDATE_ACCOUNT_DELETED);
                DbContext.Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public static void ArchiveCandidate(int id)
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
                var user = (Candidate)DbContext.Context.User.First(u => u.Id == id);
                user.IsArchived = true;
                DbContext.Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public static bool UpdateCandidate(Candidate candidate)
        {
            try
            {
                var context = new RacModelContainer();
                Candidate can = GetCandidateById(candidate.Id);
                can.FirstName = CapitalizeFirstLetter(candidate.FirstName);
                can.LastName = CapitalizeFirstLetter(candidate.LastName);
                can.HomePhone = candidate.HomePhone;
                can.WorkPhone = candidate.WorkPhone;
                can.PreferredMethodOfContact = candidate.PreferredMethodOfContact;
                can.Street = candidate.Street;
                can.City = candidate.City;
                can.Province = candidate.Province;
                can.Country = candidate.Country;

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        } 

        public static Candidate ChangeProgram(Candidate candidate, int programId, bool isGenEdOnly)
        {
            try
            {
                candidate.RACRequest = RACRequestBLL.ChangeProgram(candidate, programId, isGenEdOnly);
                //Sending notification
                Notification.CreateNotification(candidate.Id, Messages.RAC_PROGRAM_CHANGE);
                DbContext.Context.SaveChanges();
                return candidate;
            }
            catch
            {
                return candidate;
            }            
        }

        public static List<Candidate> GetAllCandidates()
        {
            List<User> users = DbContext.Context.User.Where(c => c.UserType == (int) userType.Candidate).ToList();
            List<Candidate> candidates = new List<Candidate>();

            foreach (User user in users)
            {
                candidates.Add((Candidate)user);
            }

            return candidates;
        }

        public static Candidate GetCandidateById(int id)
        {
            return (Candidate) DbContext.Context.User.First(u => u.Id == id);
        }

        public static List<NotificationsViewModel> GetCandidateNotifications(int id)
        {
            IQueryable<RACNotification> notificationsList = DbContext.Context.RACNotification.Where(r => 
                    r.CandidateId == id && r.NotificationType == (int) Messages.RAC_ADVISOR_UPLOAD_TO_CANDIDATE);

            List<RACNotification> notifications = notificationsList.ToList();
            List<NotificationsViewModel> notificationsVm = MapNotificationsToViewModels(notifications);
            return notificationsVm;

        }
        public static CandidateHomeViewModel MapToHomeViewModel(User user, CandidateHomeViewModel cvm)
        {
            if (user == null || cvm == null)
            {
                return null;
            }

            cvm.Id = user.Id;
            cvm.Email = user.Email;
            cvm.FirstName = user.FirstName;
            cvm.LastName = user.LastName;
            cvm.UserType = user.UserType;
            
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
                    Candidate = notificatonInstance.Candidate,
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
                    throw new ArgumentException($"{nameof(name)} cannot be empty.", nameof(name));
                default:
                    return name.First().ToString().ToUpper() + name.Substring(1);
            }
        }

        public static Candidate GetCandidateByUserType()
        {
            var user = (User)HttpContext.Current.Session["User"];
            Candidate currentCandidate;

            if (user == null)
            {
                currentCandidate = null;
            }
            else
            {
                if (user.UserType == (int)userType.Candidate)
                {
                    currentCandidate = (Candidate)user;
                }
                else
                {
                    currentCandidate = (Candidate)HttpContext.Current.Session["Candidate"];
                }
            }

            return currentCandidate;

        }
    }
}