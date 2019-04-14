using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest.Logic
{
	[TestClass]
	public class UserManagerTest
	{
		/*
		 * The purpose of this class is to test all of the methods inside the UserManager class. 
		 * Each method in the UserManager class has an inner class associated with it. 
		 * Each of these inner classes is marked as a test class, and contains methods for 
		 * different things to test for that method. 
		 * 
		 * This is a way of having tighter grouping of tests so that the class is more organized.         
		 */

		[TestClass] //
		class InsertNewStudentsFromClara_Test
		{
			/*
			 * This class will test the InsertNewStudentsFromClara method. 
			 * 
			 * It will test 
			 */
			[TestMethod]
			public void InsertNewStudentsFromClara_TEST()
			{
			}
		}

		[TestClass] //
		class DeactivateInactiveStudents_Test
		{
			/*
			 * This class will test the DeactivateInactiveStudents method. 
			 * 
			 * It will test 
			 */
			[TestMethod]
			public void DeactivateInactiveStudents_TEST()
			{
			}
		}

		[TestClass] //
		class RemoveCESStudents_Test
		{
			/*
			 * This class will test the RemoveCESStudents method. 
			 * 
			 * It will test 
			 */
			[TestMethod]
			public void RemoveCESStudents_TEST()
			{
			}
		}

		[TestClass] //
		class InsertAllNewFacultyFromClara_Test
		{
			/*
			 * This class will test the InsertAllNewFacultyFromClara method. 
			 * 
			 * It will test 
			 */
			[TestMethod]
			public void InsertAllNewFacultyFromClara_TEST()
			{
			}
		}

		[TestClass] //
		class RemoveFaculty_Test
		{
			/*
			 * This class will test the RemoveFaculty method. 
			 * 
			 * It will test 
			 */
			[TestMethod]
			public void RemoveFaculty_TEST()
			{
			}
		}

		[TestClass] //
		class ActiveStudentsOnly_Test
		{
			/*
			 * This class will test the ActiveStudentsOnly method. 
			 * 
			 * It will test 
			 */
			[TestMethod]
			public void ActiveStudentsOnly_TEST()
			{
			}
		}

		[TestClass] //
		class InnactiveStudentsOnly_Test
		{
			/*
			 * This class will test the InnactiveStudentsOnly method. 
			 * 
			 * It will test 
			 */
			[TestMethod]
			public void InnactiveStudentsOnly_TEST()
			{
			}
		}

		[TestClass] //Overload
		class StudentsInSemester_Test
		{
			/*
			 * This class will test the StudentsInSemester method. 
			 * 
			 * This method has two different signatures with different parameters. 
			 * One inner class exists for each of the different signatures with the same name. 
			 */
			[TestClass]
			class StudentsInSemester_int_CSAdminContext_Test
			{
			}

			[TestClass]
			class StudentsInSemester_EtudiantSession_int_Test
			{
			}
		}

		[TestClass] //Overload
		class CoordinatorsInSemester_Test
		{
			/*
			 * This class will test the CoordinatorsInSemester method. 
			 * 
			 * This method has two different signatures with different parameters. 
			 * One inner class exists for each of the different signatures with the same name. 
			 */
			class CoordinatorsInSemester_int_CSAdminContext
			{
			}

			class CoordinatorsInSemester_Coordonnateur_int
			{
			}
		}

		[TestClass] //Overload
		class TeachersInSemester_Test
		{
			/*
			 * This class will test the TeachersInSemester method. 
			 * 
			 * This method has two different signatures with different parameters. 
			 * One inner class exists for each of the different signatures with the same name. 
			 */

			[TestClass]
			class TeachersInSemester_int_CSAdminContext
			{
			}

			[TestClass]
			class TeachersInSemester_EmployeGroupe_int
			{
			}
		}

		[TestClass]
		class IncludesClaraStudent_Test
		{
			/*
			 * This class will test the IncludesClaraStudent method. 
			 * 
			 * It will test 
			 */
			[TestMethod]
			public void IncludesClaraStudent_TEST()
			{
			}
		}

		[TestClass]
		class IncludesClaraEmployee_Test
		{
			/*
			 * This class will test the IncludesClaraEmployee method. 
			 * 
			 * It will test 
			 */
			[TestMethod]
			public void IncludesClaraEmployee_TEST()
			{
			}
		}

		//[TestClass]
		//class SelectCesAndStRoles_Test
		//{
		//    /*
		//     * This class will test the SelectCesAndStRoles method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectCesAndStRoles_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectCesAndStRoles_Test
		//{
		//    /*
		//     * This class will test the SelectCesAndStRoles method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectCesAndStRoles_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectCesAndStRoles_Test
		//{
		//    /*
		//     * This class will test the SelectCesAndStRoles method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectCesAndStRoles_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectCesAndStRoles_Test
		//{
		//    /*
		//     * This class will test the SelectCesAndStRoles method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectCesAndStRoles_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectCesAndCoRoles_Test
		//{
		//    /*
		//     * This class will test the SelectCesAndCoRoles method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectCesAndCoRoles_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectCesAndTeRoles_Test
		//{
		//    /*
		//     * This class will test the SelectCesAndTeRoles method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectCesAndTeRoles_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectAllStudentUsers_Test
		//{
		//    /*
		//     * This class will test the SelectAllStudentUsers method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectAllStudentUsers_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectAllEmployeeUsers_Test
		//{
		//    /*
		//     * This class will test the SelectAllEmployeeUsers method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectAllEmployeeUsers_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectAllNonActiveStudents_Test
		//{
		//    /*
		//     * This class will test the SelectAllNonActiveStudents method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectAllNonActiveStudents_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectAllActiveStudents_Test
		//{
		//    /*
		//     * This class will test the SelectAllActiveStudents method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectAllActiveStudents_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectAllStudentsFromClaraForASemester_Test
		//{
		//    /*
		//     * This class will test the SelectAllStudentsFromClaraForASemester method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectAllStudentsFromClaraForASemester_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectAllEmployeeFromClaraForASemester_Test
		//{
		//    /*
		//     * This class will test the SelectAllEmployeeFromClaraForASemester method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectAllEmployeeFromClaraForASemester_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class SelectAllTeacherFromClaraForASemester_Test
		//{
		//    /*
		//     * This class will test the SelectAllTeacherFromClaraForASemester method. 
		//     * 
		//     * It will test 
		//     */
		//    [TestMethod]
		//    public void SelectAllTeacherFromClaraForASemester_TEST()
		//    {

		//    }
		//}

		//[TestClass]
		//class IsInByIDEtudiant_Test
		//{
		//    /*
		//     * This class will test the IsInByIDEtudiant method. 
		//     * 
		//     * This method has two different signatures with different parameters. 
		//     * One inner class exists for each of the different signatures with the same name. 
		//     */

		//    [TestClass]
		//    class IsInByIDEtudiant_StudentUser_Etudiant_TEST
		//    {

		//    }

		//    [TestClass]
		//    class IsInByIDEtudiant_Etudiant_StudentUser_TEST
		//    {

		//    }            
		//}

		//[TestClass]
		//class IsInByIDEmploye_Test
		//{
		//    /*
		//     * This class will test the IsInByIDEmploye method. 
		//     * 
		//     * This method has two different signatures with different parameters. 
		//     * One inner class exists for each of the different signatures with the same name. 
		//     */
		//    [TestClass]
		//    class IsInByIDEmpolye_EmployeeUser_Employe
		//    {

		//    }

		//    [TestClass]
		//    class IsInByIDEmpolye_Employe_EmployeeUser
		//    {

		//    }
		//}        
	}
}