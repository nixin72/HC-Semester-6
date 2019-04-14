﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using RAC.BusinessLogic;
using System.Web.Mvc;

using RAC.Models;
namespace RAC.Validation
{
    public class CandidateVal : UserVal
    {
        public string Street { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a province")]
        [Display(Name = "*Province")]
        public string Province { get; set; }
        public string Country { get; set; }

        [Display(Name = "*Method Of Contact")]
        [Required(ErrorMessage = "Method Of Contact is Required")]
        public Models.PreferredMethodOfContact PreferredMethodOfContact {get; set;}

        [Display(Name = "*Program")]
        [Required(ErrorMessage = "Please Choose a Program")]
        public int ProgramId { get; set; }

		[Display(Name = "General Education Only")]
		public bool GenEdOnly { get; set; }

        public bool IsArchived { get; set; }


		public IList<SelectListItem> ProgramsOffered { get; set; }

        public IList<SelectListItem> GetProgramsOfferedDropDownList()
        {
            IEnumerable<Program> programs = DbContext.Context.Programs;
            List<SelectListItem> ProgramsOffered = new List<SelectListItem>();
            foreach (Program program in programs)
            {
                if (!program.ProgramMinistryData.Name.Equals("General Education"))
                {
                    ProgramsOffered.Add(new SelectListItem()
                    {
                        Text = program.ProgramMinistryData.Name,
                        Value = program.Id.ToString()
                    });
                }
                
            }

            //Looping to put them in alphabetical order
            ProgramsOffered = ProgramsOffered.OrderBy(x => x.Text).ToList();

            return ProgramsOffered;
        }

        public Candidate ToCandidate()
        {
            Candidate can = CandidateBLL.GetCandidateById(UserId);
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
            can.Country = Country;
            can.PreferedMethodOfContact = (int)PreferredMethodOfContact;
            can.IsGenEdOnly = GenEdOnly;
            can.IsArchived = IsArchived;
            
            return can;
        }

        public static CandidateVal ToCandidateVal(Candidate candidate)
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
                val.Country = candidate.Country;
                val.PreferredMethodOfContact = (PreferredMethodOfContact)candidate.PreferedMethodOfContact;
                val.GenEdOnly = candidate.IsGenEdOnly;
                val.IsArchived = candidate.IsArchived;
            }
            catch
            {
                val.PreferredMethodOfContact = PreferredMethodOfContact.Email;
                val.GenEdOnly = candidate.IsGenEdOnly;
            }           

            return val;
        }
    }
}