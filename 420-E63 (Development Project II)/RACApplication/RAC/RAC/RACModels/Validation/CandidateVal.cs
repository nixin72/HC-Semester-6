using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using RAC.BusinessLogic;

namespace RAC.RACModels.Validation
{
    public class CandidateVal : UserVal
    {
        public string Street { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a province")]
        [Display(Name = "*Province")]
        public string Province { get; set; }

        [Display(Name = "*Method Of Contact")]
        [Required(ErrorMessage = "Method Of Contact is Required")]
        public PreferredMethodOfContact PreferredMethodOfContact {get; set;}

        [Display(Name = "*Program")]
        [Required(ErrorMessage = "Please Choose a Program")]
        public int ProgramId { get; set; }

		[Display(Name = "General Education Only")]
		public bool GenEdOnly { get; set; }

        public bool IsArchived { get; set; }


		public IList<SelectListItem> ProgramsOffered { get; set; }

        public IList<SelectListItem> GetProgramsOfferedDropDownList()
        {
            IEnumerable<Program> programs = DbContext.Context.Program;
            List<SelectListItem> programsOffered = new List<SelectListItem>();
            foreach (Program program in programs)
            {
                if (!program.ProgramMinistryData.Name.Equals("General Education"))
                {
                    programsOffered.Add(new SelectListItem()
                    {
                        Text = program.ProgramMinistryData.Name,
                        Value = program.Id.ToString()
                    });
                }
                
            }

            //Looping to put them in alphabetical order
            programsOffered = programsOffered.OrderBy(x => x.Text).ToList();

            return programsOffered;
        }

        public RACUser ToCandidate()
        {
            RACUser can = CandidateBLL.GetCandidateById(UserId);
            can.Id = UserId;
            can.FirstName = FirstName;
            can.LastName = LastName;
            can.Email = Email;
            can.HomePhone = HomePhone;
            can.WorkPhone = WorkPhone;
            can.UserType = UserType;
            can.Street = Street;
            can.City = City;
            can.Province = Province;
            can.Country = "Canada";
            can.PreferredMethodOfContact = (int)PreferredMethodOfContact;
            can.IsArchived = IsArchived;
            
            return can;
        }

        public static CandidateVal ToCandidateVal(RACUser candidate)
        {
            var val = new CandidateVal();
            try
            {
                val.UserId = candidate.Id;
                val.FirstName = candidate.FirstName;
                val.LastName = candidate.LastName;
                val.Email = candidate.Email;
                val.HomePhone = candidate.HomePhone;
                val.WorkPhone = candidate.WorkPhone;
                val.UserType = candidate.UserType;
                val.Street = candidate.Street;
                val.City = candidate.City;
                val.Province = candidate.Province;
                if (candidate.PreferredMethodOfContact != null)
                {
                    val.PreferredMethodOfContact = (PreferredMethodOfContact) candidate.PreferredMethodOfContact;
                }

                val.IsArchived = candidate.IsArchived;
            }
            catch
            {
                val.PreferredMethodOfContact = PreferredMethodOfContact.Email;
            }           

            return val;
        }
    }
}