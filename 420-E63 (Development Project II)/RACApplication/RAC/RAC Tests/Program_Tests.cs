using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RAC.BusinessLogic;
using RAC.RACModels;




namespace RAC_Tests
{
    [TestClass]
    public class Program_Tests
    {
        // TODO ProgramBLL should be made static
        private readonly ProgramBLL _p = new ProgramBLL();

        [TestMethod]
        public void GetProgramById_Success()
        {
            Program[] programs = DbContext.Context.Program.ToArray();

            foreach (Program program in programs)
            {
                Assert.AreEqual(program, ProgramBLL.GetProgramById(program.Id),
                    "PROGRAM_DOES_NOT_EXIST: Program name " + program.ProgramMinistryData.Name + " could not be retrieved from the database.");
            }
        }

        [TestMethod]
        public void GetProgramById_Fail()
        {
            int fakeId = DbContext.Context.Program.Count() + 1;

            Assert.AreEqual(null, ProgramBLL.GetProgramById(0),
                "PROGRAM_DOES_NOT_EXIST: Program Id 0 returned a valid program.");

            try
            {
                ProgramBLL.GetProgramById(fakeId);
                Assert.Fail("PROGRAM_EXISTS: Method returned non-existant Program.");
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(0, 0);
            }
        }

        [TestMethod]
        public void GetProgramsOffered()
        {
            IList<SelectListItem> offeredPrograms = ProgramBLL.GetProgramsOffered();
            
            Assert.IsNotNull(offeredPrograms,
                "NO_PROGRAMS_OFFERED: No offered programs were returned from the database.");
        }

        [TestMethod]
        public void CheckProgramHasGenEds_Success()
        {
            Assert.IsTrue(ProgramBLL.CheckProgramHasGenEds(1), 
                "NO_GEN_EDS: General Education program contained no gen. eds.");
        }

        [TestMethod]
        public void CheckProgramHasGenEds_Fail()
        {
            Assert.IsFalse(ProgramBLL.CheckProgramHasGenEds(0),
                "HAS_GEN_ED: Non-existant program returned gen. eds.");
        }
    }
}
