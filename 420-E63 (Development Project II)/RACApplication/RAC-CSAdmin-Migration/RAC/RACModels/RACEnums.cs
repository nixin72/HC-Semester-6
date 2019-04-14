using System.ComponentModel.DataAnnotations;

namespace RAC.RACModels
{
    public enum Messages
    {
        CANDIDATE_CONFIRM_EMAIL = 0,
        NEW_CANDIDATE_ACCOUNT = 1,
        NEW_RAC_STARTED = 2,
        NOTIFY_CONTENT_SPECIALIST = 3,
        RETURN_SPECIALIST_EVALUATION = 4,
        FORGOT_PASSWORD = 5,
        RAC_PROGRAM_CHANGE = 6,
        RAC_REQUEST_SUBMITTED = 7,
        CANDIDATE_ACCOUNT_DELETED = 8,
        RAC_ADVISOR_UPLOAD_TO_CANDIDATE = 9
    };

    public enum userType
    {
        Candidate = 0,
        ContentSpecialist = 1,
        RACAdvisor = 2
    }

    public enum RACStatus
    {
        [Display(Name = "Started")] Started = 0,
        [Display(Name = "Submitted")] Submitted = 1,
        [Display(Name = "Specialist Review")] SpecialistReview = 2,
        [Display(Name = "Completed")] Completed = 3
    }

    public enum provinces
    {
        [Display(Name = "Quebec")] QC = 0,
        [Display(Name = "Ontario")] ON = 1,
        [Display(Name = "Alberta")] AB = 2,
        [Display(Name = "British Columbia")] BC = 3,
        [Display(Name = "Manitoba")] MB = 4,
        [Display(Name = "New Brunswick")] NB = 5,

        [Display(Name = "Newfoundland and Labrador")]
        NL = 6,
        [Display(Name = "Nova Scotia")] NS = 7,

        [Display(Name = "Nortwest Territories")]
        NT = 8,
        [Display(Name = "Nunavut")] NU = 9,

        [Display(Name = "Prince Edward Island")]
        PE = 10,
        [Display(Name = "Saskatchewan")] SK = 11,
        [Display(Name = "Yukon")] YT = 12
    }




    public enum SelfEvaluationAnswer
    {
        [Display(Name = "Not Answered")] NotAnswered = 0,
        [Display(Name = "I can do this")] CanDoThis = 1,

        [Display(Name = "I can somewhat do this")]
        CanSomewhatDoThis = 2,
        [Display(Name = "I cannot do this")] CannotDoThis = 3,
    }

    public static class EnumToString{
        public static string GetSelfEvaluationAnswer(SelfEvaluationAnswer ans)
        {

            string returned = "";
            switch(ans)
            {
                case SelfEvaluationAnswer.NotAnswered:
                    returned = "Not Answered";
                    break;
                case SelfEvaluationAnswer.CanDoThis:
                    returned = "I can do this";
                    break;
                case SelfEvaluationAnswer.CanSomewhatDoThis:
                    returned = "I can somewhat do this";
                    break;
                case SelfEvaluationAnswer.CannotDoThis:
                    returned = "I cannot do this";
                    break;
            }
            return returned;
        }
    }

    public enum EmailNotificationErr {
        [Display(Name = "Error. Something went wrong on our end.")]
        DatabaseErr = 2,
        [Display(Name = "Error. The email confirmation cannot be sent within 10 minutes of the last one.")]
        RequestTooSoon = 1,
        [Display(Name = "Success")]
        Success = 0,
        [Display(Name = "Error. The parameters entered did not match the database.")]
        DatabasConsistencyError = 3,
        [Display(Name = "Error. The email failed to send.")]
        EmailSendFailed = 4,
        [Display(Name = "Error. Input was invalid.")]
        InvalidInput = 5,
    }
    public enum ERRORS
    {
        //Create Account Errors
        SUCCESS = 0,

        MISSING_INPUT_FIRST_NAME,
        INPUT_TOO_LONG_FIRST_NAME,
        INVALID_CHARACTER_FIRST_NAME,

        MISSING_INPUT_LAST_NAME,
        INPUT_TOO_LONG_LAST_NAME,
        INVALID_CHARACTER_LAST_NAME,

        MISSING_INPUT_EMAIL,
        INPUT_TOO_LONG_EMAIL,
        INVALID_PATTERN_EMAIL,
        INVALID_CHARACTER_EMAIL,

        INVALID_FIRST_NAME_NULL,
        INVALID_LAST_NAME_NULL,
        INVALID_EMAIL_NULL,
        INVALID_HOME_PHONE_NULL,
        INVALID_PASSWORD_NULL,

        INPUT_TOO_SHORT_PHONE,
        INPUT_TOO_LONG_PHONE,
        INVALID_CHARACTER_PHONE,

        //Account Retrieval Errors
        MISSING_REQUIRED_USERID,
        MISSING_REQUIRED_FIRSTNAME,
        MISSING_REQUIRED_LASTNAME,
        MISSING_REQUIRED_EMAIL,
        MISSING_REQUIRED_HOMEPHONE,
        INVALID_USERTYPE,

        PASSWORD_NOT_EMPTY,
        SALT_NOT_EMPTY,

        USERID_RETRIEVAL_MISMATCH,
        FIRSTNAME_RETRIEVAL_MISMATCH,
        LASTNAME_RETRIEVAL_MISMATCH,
        EMAIL_RETRIEVAL_MISMATCH,
        HOMEPHONE_RETRIEVAL_MISMATCH,
        WORKPHONE_RETRIEVAL_MISMATCH,
        USERTYPE_RETRIEVAL_MISMATCH,
        STREET_RETRIEVAL_MISMATCH,
        CITY_RETRIEVAL_MISMATCH,
        PROVINCE_RETRIEVAL_MISMATCH,
        COUNTRY_RETRIEVAL_MISMATCH,

        // UPLOAD DOCUMENTS BLL ERRORS
        DOCUMENT_IS_NULL,
        NO_DOCUMENT_WITH_ID,
        DOCUMENT_NOT_IN_LIST,
        DOCUMENT_REMOVAL_FAIL,

        //File Reading Errors
        FILE_NOT_FOUND

    }

    public enum PreferredMethodOfContact
    {
        [Display(Name = "Email")]
        Email = 0,
        [Display(Name = "Home Phone")]
        HomePhone = 1,
        [Display(Name = "Work Phone")]
        WorkPhone = 2     
    }
    /* These are based off of the values from Clara, we need to confirm with Richard to see
     if it is ok to hardcode these values */
    public enum CourseType
    {
        [Display(Name="Undetermined")]
        ND = 1,
        [Display(Name="Gen-Ed Common to all Programs")]
        GC = 2,
        [Display(Name="Gen-Ed Program Specific")]
        GP = 3,
        [Display(Name="Complementary")]
        GM = 4,
        [Display(Name="Program Specific or Concentration")]
        SP = 5,
        [Display(Name= "Mise à niveau / Upgrade")]
        MN = 6,
        [Display(Name="University Prerequisites")]
        PU = 7,
        [Display(Name="Out of Program")]
        HP = 8,
        [Display(Name="Non Requis / Not Required")]
        NR = 9
        
    }

}