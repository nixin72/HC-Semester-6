using System;
using System.Data.SqlClient;
using System.Linq;
using CSAdmin2.Model;

namespace CSAdmin2.Logic
{
	public class SemesterManager
	{
		//Old files that this class is being build up from:
		//-Semester.vb - OBJECT
		//-SemesterManager.vb - Business Logic (Code behind has more business logic than this it seems...)
		//-SemesterDB.vb - Database Access
		//-UpdateSemester.aspx.vb - Code Behind
		//NOTE: It seems that all the backend code for update semester is done, the UpdateSemester.aspx.vb is all that's left

		/// <summary>
		///     Selects the current settings for the csadmin system.
		/// </summary>
		/// <param name="context">The csadmin context used for selecting the data within the system.</param>
		/// <returns>The setting of the csadmin system.</returns>
		public static Setting SelectSettings(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			// get first settings row in settings table.
			Setting settings = context.Settings.First();
			// make sure it's current session yera exists in clara.
			if (!AnSessionExists(settings.CurrentYearSemester, context))
			{
				settings = null;
			}

			return settings;
		}

		/// <summary>
		///     Checks if an assessment session exists in clara.
		/// </summary>
		/// <param name="anSession">The AnSession value to check for in clara.</param>
		/// <param name="context">
		///     The <see cref="CSAdminContext" /> to use for the query if the underlying logic already have one
		///     open.
		/// </param>
		/// <returns>Wether or not the session exists in clara.</returns>
		private static bool AnSessionExists(int yearSemester, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.Groupes.Any(group => group.AnSession == yearSemester);
		}

		/// <summary>
		///     Updates the current semester settings within the database.
		/// </summary>
		/// <param name="newYearSemester">The an session for the current semester.</param>
		/// <param name="theEndDate">The end date of the semester.</param>
		/// <param name="context">A CSAdminContext object used for queries that already have one openned.</param>
		/// <returns>The number of rows updated, by default it should be one.</returns>
		/// <exception cref="ArgumentException">If the newYearSemester doesn't exists in clara.</exception>
		public static int UpdateCurrentSemester(short newYearSemester, DateTime theEndDate, CSAdminContext context = null)
		{
			int updated = -1;
			// updates current semester. returns true or false depending on success.
			if (AnSessionExists(newYearSemester, context))
			{
				// initialize context by using the optional final parameter it's less of a back pain that way.

				context = null;
				Constants.Initialise(ref context);
				Setting obj = context.Settings.First();
				if (obj != null)
				{
					// Note: removed the try catch to help with consistency and enforce validation on the lowest levels.
					obj.SemesterEndDate = theEndDate;
					updated = context.SaveChanges();
				}
			}
			else
			{
				// throws an exception to notify that the current semester doesn't exists in clara
				throw new ArgumentException($"The passed in semester {newYearSemester} does not exist in clara");
			}

			return updated;
		}

		public static DateTime? GetSemesterEndDate(int newYearSemester, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			// get first settings row in settings table.
			Setting settings = context.Settings.Single(e => e.CurrentYearSemester == newYearSemester);

			DateTime semesterEndDate = settings.SemesterEndDate;

			// make sure it's current session yera exists in clara.
			if (!AnSessionExists(settings.CurrentYearSemester, context))
			{
				settings = null;
			}

			return settings.SemesterEndDate;
		}


		//TODO: The following vb to c# methods might be replace by Guilaume's all-in-one super query.

		public static int SelectNumDeactivateStudents(int yearSemester, CSAdminContext context = null)
		{
			//uspSelectNumDelCESStudents
			//uspSelectNumDeactivateStudents

			Constants.Initialise(ref context);

			var sql = @"SELECT COUNT(su.IDUser) 'Number of Students'
						FROM Users.StudentUser su
							JOIN Applications.UserRole ur ON ur.IDUser = su.IDUser
							JOIN Applications.Role r ON r.IDRole = ur.IDRole
							JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
							JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
							JOIN [Users].[User] u ON su.IDUser = u.IDUser
						WHERE ur.IsActive = 1
							AND r.Code = 'ST'
							AND su.IDEtudiant NOT IN(SELECT DISTINCT es.IDEtudiant
						FROM ClaraEtudiants.EtudiantSession es
							JOIN ClaraEtudiants.Etudiant e ON es.IDEtudiant = e.IDEtudiant
							WHERE es.etat = 1 --This gets active students for this semester
							AND es.AnSession = @AnSession --current semester (must be changed in procedure)
							AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
							);";
			//sql.Parameters.AddWithValue("@AnSession", anSession);
			return context.Database.SqlQuery<int>(sql, new SqlParameter("@AnSession", yearSemester)).First();

			//TO CODE BACKEND FOR

			//	Shared Function SelectNumDeactivateStudents(ByVal AnSession As Integer) As Integer
			//         Dim numDeactivateStudents As Integer = 0

			//         Dim conn As New SqlConnection
			//         conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
			//         Dim cmd As New SqlCommand("uspSelectNumDeactivateStudents", conn)
			//         cmd.Parameters.AddWithValue("@AnSession", AnSession)
			//         cmd.CommandType = CommandType.StoredProcedure
			//         Dim rdr As SqlDataReader

			//         conn.Open()
			//         Try

			//             rdr = cmd.ExecuteReader()
			//             rdr.Read()
			//             If rdr.HasRows Then
			//                 Do

			//                     If Not IsDBNull(rdr("Number of Students")) Then
			//                         numDeactivateStudents += CInt(rdr("Number of Students").ToString())
			//                     End If

			//                 Loop While rdr.Read()
			//             End If
			//             rdr.Close()
			//         Catch ex As Exception
			//             Throw ex
			//         Finally
			//             conn.Close()
			//         End Try

			//         Return numDeactivateStudents
			//     End Function
		}

		//This is for Course Evaluation System apparently
		public static int SelectNumDelStudents(int yearSemester)
		{
			return UserManager.NumRemoveCESStudents(yearSemester);
			//TO CODE BACKEND FOR

			//	Shared Function SelectNumDelStudents(ByVal AnSession As Integer) As Integer
			//          Dim numDelStudents As Integer = 0

			//          Dim conn As New SqlConnection
			//          conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
			//          Dim cmd As New SqlCommand("uspSelectNumDelCESStudents", conn)
			//          cmd.Parameters.AddWithValue("@AnSession", AnSession)
			//          cmd.CommandType = CommandType.StoredProcedure
			//          Dim rdr As SqlDataReader

			//          conn.Open()
			//          Try

			//              rdr = cmd.ExecuteReader()
			//              rdr.Read()
			//              If rdr.HasRows Then
			//                  Do

			//                      If Not IsDBNull(rdr("Number of Students")) Then
			//                          numDelStudents += CInt(rdr("Number of Students").ToString())
			//                      End If

			//                  Loop While rdr.Read()
			//              End If
			//              rdr.Close()
			//          Catch ex As Exception
			//              Throw ex
			//          Finally
			//              conn.Close()
			//          End Try

			//          Return numDelStudents
			//      End Function
		}

		public static int SelectNumNewFaculty(int yearSemester, CSAdminContext context = null)
		{
			//TO CODE BACKEND FOR
			//return UserManager.NumAllNewFacultyFromClara(yearSemester);

			Constants.Initialise(ref context);

			var sql = @"SELECT COUNT(*) FROM (
						SELECT DISTINCT eg.IDEmploye
						FROM ClaraGroupes.EmployeGroupe eg
							JOIN ClaraGroupes.Groupe g			  ON eg.IDGroupe = g.IDGroupe
							JOIN ClaraEmployes.Employe e		  ON e.IDEmploye = eg.IDEmploye
							JOIN ClaraInscriptions.Inscription i ON i.IDGroupe = g.IDGroupe
							JOIN ClaraReference.UniteOrg uo	  ON uo.IDUniteOrg = i.IDUniteOrg
						WHERE g.etat = 1 --This gets active teachers for this semester
							AND g.AnSession = 20171
							AND (uo.IDUniteOrg = 235 OR uo.IDUniteOrg = 525)
							AND e.IDEmploye NOT IN	(
													SELECT t.IDEmploye
													FROM CES.Users.Teacher t
													)
													) src;";

			return context.Database.SqlQuery<int>(sql, yearSemester).First();
		}


		public static int SelectNumNewCoordinators(int yearSemester, CSAdminContext context = null)
		{
			//  --Count new coordinators
			//begin transaction
			//	--select * FROM[CLARA].[Clara].[ReportClient].[Coordonnateur]
			//        c where 
			//	--c.IdEmploye not in (select eu.[IDEmploye] from[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Users].[EmployeeUser] eu where eu.IdUser in (select[IdUser] from [CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[UserRole]));

			//  select count(*) from[CLARA].[Clara].[ReportClient].[Coordonnateur]
			//        c where c.IdEmploye not in (select[IdEmploye] from[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[UserRole]
			//        ur
			//join[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[Role]
			//        r on ur.IdRole = R.IdRole and R.Code = 'CO'
			//  join[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Users].[EmployeeUser]
			//        eu on ur.IDUser = eu.IdUser)


			//TO CODE BACKEND FOR
			return UserManager.NumAllNewCoordinatorsFromClara(yearSemester);

			//	Shared Function SelectNumNewCoordinators(ByVal AnSession As Integer) As Integer
			//         Dim numNewCoordinators As Integer = 0

			//         Dim conn As New SqlConnection
			//         conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
			//         Dim cmd As New SqlCommand("uspSelectNumNewProgramCoordinators", conn)
			//         cmd.Parameters.AddWithValue("@AnSession", AnSession)
			//         cmd.CommandType = CommandType.StoredProcedure
			//         Dim rdr As SqlDataReader

			//         conn.Open()
			//         Try

			//             rdr = cmd.ExecuteReader()
			//             rdr.Read()
			//             If rdr.HasRows Then
			//                 Do

			//                     If Not IsDBNull(rdr("Number of Coordinators")) Then
			//                         numNewCoordinators += CInt(rdr("Number of Coordinators").ToString())
			//                     End If

			//                 Loop While rdr.Read()
			//             End If
			//             rdr.Close()
			//         Catch ex As Exception
			//             Throw ex
			//         Finally
			//             conn.Close()
			//         End Try

			//         Return numNewCoordinators
			//     End Function


//            --Setting all coordinators to inactive
//  --Update[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[UserRole] set IsActive = IsActive where
// --IdRole = (Select r.IdRole from[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[Role]
//        r where r.Code = 'CO');    

//  -- Setting all coordinators that are in Clara to active
//  --select* from[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[UserRole]
//        where IdUser in   
//  --(    
//  --SELECT EU.[IDUser]
//  --FROM[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Users].[EmployeeUser] eu join [CLARA].[Clara].[ReportClient].[Coordonnateur] c on eu.[IDEmploye] = c.IDEmploye
//  --where AnSessionFin is null) and IdRole = (Select r.IdRole from[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[Role]
//        r where r.Code = 'CO');


//  --Count new coordinators
//begin transaction
//	--select count(*) FROM[CLARA].[Clara].[ReportClient].[Coordonnateur]
//        c where 
//	--c.IdEmploye not in (select eu.[IDEmploye] from[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Users].[EmployeeUser] eu where eu.IdUser in (select[IdUser] from [CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[UserRole]));

//  select count(*) from[CLARA].[Clara].[ReportClient].[Coordonnateur]
//        c where c.IdEmploye not in (select[IdEmploye] from[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[UserRole]
//        ur
//join[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[Role]
//        r on ur.IdRole = R.IdRole and R.Code = 'CO'
//  join[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Users].[EmployeeUser]
//        eu on ur.IDUser = eu.IdUser)


//rollback transaction;

//  --Count coordinators to delete
//  begin transaction
//  --Select* FROM[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Users].[EmployeeUser]
//        eu where eu.IdEmploye not in 
//  --(select[IDEmploye] from[CLARA].[Clara].[ReportClient].[Coordonnateur]) and eu.IdUser in (  Select* from [CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[UserRole] ur
//  --join[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[Role] r on ur.IdRole = r.IdRole and r.Code = 'CO');

//  --(select* from[CLARA].[Clara].[ReportClient].[Coordonnateur])
//  select* from[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[UserRole]
//        ur
//join[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Applications].[Role]
//        r on ur.IdRole = R.IdRole and R.Code = 'CO'
//  join[CSAdmin_MAINT_TEAM_1_AHA_TEST].[Users].[EmployeeUser]
//        eu on ur.IDUser = eu.IdUser and eu.IDEmploye not in (select[IdEmploye] from[CLARA].[Clara].[ReportClient].[Coordonnateur]);

//  rollback transaction;
		}

		//public static int updateSemesterFaculty(CSAdminContext context = null)
		//{
		//	//Constants.Initialise(ref context);
		//	//string sql = @"";
		//	//return context.Database.SqlQuery<int>(sql).First();
		//}

		public static int updateSemesterStudents(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			string sql = @"begin transaction
							update users.[User] set
								isactive = 0
							where
								iduser in (select distinct iduser from Applications.UserRole where idrole = 1);

							update Applications.UserRole set IsActive = 0 where idrole = 1;

							create table #tempStudents (
								action nvarchar(10),
								iduser int not null,
								idetudiant int not null
							)

							merge into users.[user] old
								using (
									SELECT
										DISTINCT es.[IDEtudiant] as [IDEtudiant],
										e.numero7 as [username],
										e.Nom as [lastName],
										e.Prenom as [firstName],
										st.iduser as iduser
									FROM
										ClaraEtudiants.EtudiantSession es
											JOIN ClaraEtudiants.Etudiant e ON es.IDEtudiant = e.IDEtudiant
											full outer join [Users].[studentuser] st on e.IDEtudiant = st.IDEtudiant
									WHERE
										es.etat = 1 AND --This gets active students for this semester
										es.AnSession = (select top 1 [CurrentYearSemester] from [Applications].[Settings]) 
									AND
										(
											es.IDUniteOrg = 235 OR -- REGULAR EDUCATION
											es.IDUniteOrg = 525 -- CONTINUED EDUCATION
										) --and e.idetudiant not in (select idetudiant from [users].[studentuser])
									) new
									on old.iduser = new.iduser
							when not matched then
								insert (username, lastname, firstname, isactive)
									values (new.username, new.lastname, new.firstname, 1)
							when matched then
								update set isactive = 1
							OUTPUT $action as [Action], inserted.iduser, new.idetudiant
							into #tempStudents (action, iduser, idetudiant);

							insert into [Users].[StudentUser] (IDUser,IDEtudiant)
								select iduser, idetudiant
								from #tempStudents
								where [Action] = 'INSERT';

							merge into [applications].[userrole] old 
								using #tempStudents new
									on old.idrole = 1 and
									   old.iduser = new.iduser
							when matched then
								update set isactive = 1
							when not matched then
								insert (iduser,idrole,isActive)
									values (new.iduser,1,1);

							drop table #tempStudents;

						rollback transaction";
			return context.Database.SqlQuery<int>(sql).First();
		}

		public static int SelectNumDelCoordinators(int yearSemester)
		{
			//TO CODE BACKEND FOR
			return UserManager.NumRemoveCoordinators(yearSemester);

			//	Shared Function SelectNumDelCoordinators(ByVal AnSession As Integer) As Integer
			//         Dim numDelCoordinators As Integer = 0

			//         Dim conn As New SqlConnection
			//         conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
			//         Dim cmd As New SqlCommand("uspSelectNumDelCESCoordinators", conn)
			//         cmd.Parameters.AddWithValue("@AnSession", AnSession)
			//         cmd.CommandType = CommandType.StoredProcedure
			//         Dim rdr As SqlDataReader

			//         conn.Open()
			//         Try

			//             rdr = cmd.ExecuteReader()
			//             rdr.Read()
			//             If rdr.HasRows Then
			//                 Do

			//                     If Not IsDBNull(rdr("Number of Coordinators")) Then
			//                         numDelCoordinators += CInt(rdr("Number of Coordinators").ToString())
			//                     End If

			//                 Loop While rdr.Read()
			//             End If
			//             rdr.Close()
			//         Catch ex As Exception
			//             Throw ex
			//         Finally
			//             conn.Close()
			//         End Try

			//         Return numDelCoordinators
			//     End Function
		}
	}
}