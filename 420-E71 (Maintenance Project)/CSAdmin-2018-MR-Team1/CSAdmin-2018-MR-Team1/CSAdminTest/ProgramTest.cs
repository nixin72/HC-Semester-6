using System.Collections.Generic;
using System.Linq;
using CSAdmin2.Clara;
using CSAdmin2.Logic;
using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class ProgramTest
	{
		/// <summary>
		///     Testing basic setters and getters for Program. Using valid values
		/// </summary>
		[TestMethod]
		public void ProgramTest_ConstructorHappyPath()
		{
			Program p = new Program
			{
				IDProgram = 420,
				Name = "Name",
				IDEducationType = 12,
				IsActive = false
			};

			Assert.AreEqual(420, p.IDProgram, "Test failed. Expected value for IDProgram was 420. Actual Value: " + p.IDProgram);
			Assert.AreEqual("Name", p.Name, "Test failed. Expected value for Name was 'Name' Actual Value: " + p.Name);
			Assert.AreEqual(12, p.IDEducationType,
				"Test failed. Expected value for IDEducationType was 12. Actual Value: " + p.IDEducationType);
			Assert.AreEqual(false, p.IsActive, "Test failed. Expected value for IsActive was false Actual Value: " + p.IsActive);
		}

		/// <summary>
		///     Testing required Name attribute
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' that has a value of null was inappropriately allowed")]
		public void ProgramTest_NullName()
		{
			Program p = new Program
			{
				Name = Constants.NullString
			};
		}

		/// <summary>
		///     Testing Name length validation of more than 50 characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' that has a string length of 50+ was inappropriately allowed")]
		public void ProgramTest_NameTooLong()
		{
			Program p = new Program
			{
				Name = Constants.LongString.Substring(0, 55)
			};
		}

		/// <summary>
		///     Testing Name length validation of more than 50 whitespace characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' that has a whitespace string length of 50+ was inappropriately allowed")]
		public void ProgramTest_NameTooLongWhitespace()
		{
			Program p = new Program
			{
				Name = Constants.LongWhiteSpaceString.Substring(0, 55)
			};
		}


		/// <summary>
		///     Testing SelectCegepPrograms for returning a list of Programmes
		/// </summary>
		[TestMethod]
		//[ExpectedException(typeof(NullReferenceException),"The list returned for select cegep programs was empty")]
		public void ProgramTest_SelectCegepPrograms()
		{
			List<Programme> cegepPrograms = ProgramManager.SelectCegepPrograms().ToList();
			Assert.IsNotNull(cegepPrograms, "Test failed. Expected list of Programmes to be a list that is not null");
			Assert.IsTrue(cegepPrograms.Count > 0,
				"Test failed. Expected list of Programmes to be a list of items greater than 0");
			Assert.IsNotNull(cegepPrograms[0].Numero,
				"Test failed. Expected numero for first entry of list of programmes to not be null");
			Assert.IsNotNull(cegepPrograms[0].TitreMoyen,
				"Test failed. Expected titreMoyen for first entry of list of programmes to not be null");
			Assert.IsNotNull(cegepPrograms[0].IDProgramme,
				"Test failed. Expected IDProgramme for first entry of list of programmes to not be null");
		}
	}
}