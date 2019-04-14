using CSAdmin2.Clara;
using CSAdmin2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAdmin2.Logic
{
    public static class UserManager
    {
	    public static int SelectNumActivateStudents(int anSession, CSAdminContext context = null)
	    {
		    Constants.Initialise(ref context);

		    var sql = @"SELECT COUNT(es.IDEtudiant) 'Number of Students'
						FROM ClaraEtudiants.EtudiantSession es
						LEFT JOIN Resources.ProgramVersion pv ON pv.IDProgramClara = es.IDProgramme
						LEFT JOIN Resources.Program p ON pv.IDProgram = p.IDProgram 
						WHERE (es.AnSession = 20181 OR es.AnSession IS NULL) 
						AND (es.Etat > 0 OR es.Etat IS NULL)
						AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
						AND (es.IDEtudiant IN (SELECT su.IDEtudiant
						FROM Users.StudentUser su
						JOIN Users.[User] u ON u.IDUser = su.IDUser
						JOIN Applications.UserRole ur ON ur.IDUser = u.IDUser
						JOIN Applications.Role r ON r.IDRole = ur.IDRole
						WHERE u.IsActive = 0 OR (r.Code = 'ST' AND ur.IsActive = 0)) OR es.IDEtudiant IS NULL);";

		    return context.Database.SqlQuery<int>(sql, new SqlParameter("@AnSession", anSession)).First();
	    }

	    public static int SelectNumNewCSAdminStudents(int anSession, CSAdminContext context = null)
	    {
		    Constants.Initialise(ref context);

		    var sql = @"SELECT COUNT(es.IDEtudiant) 'Number of Students'
						FROM ClaraEtudiants.EtudiantSession es
						LEFT JOIN Resources.ProgramVersion pv ON pv.IDProgramClara = es.IDProgramme
						LEFT JOIN Resources.Program p ON pv.IDProgram = p.IDProgram 
						WHERE (es.AnSession = 20172 OR es.AnSession IS NULL) 
						AND (es.Etat > 0 OR es.Etat IS NULL)
						AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
						AND (es.IDEtudiant NOT IN (SELECT IDEtudiant FROM Users.StudentUser) OR es.IDEtudiant IS NULL);";

		    return context.Database.SqlQuery<int>(sql, new SqlParameter("@AnSession", anSession)).First();
	    }

	    public static int NumAddCESStudents(int anSession, CSAdminContext context = null)
	    {
		    Constants.Initialise(ref context);

		    int rowsAffected = 0;


		    var parameter = new SqlParameter("@AnSession", anSession);

		    var sql = @"SELECT COUNT(es.IDEtudiant) 'Number of Students'
						FROM ClaraEtudiants.EtudiantSession es
						LEFT JOIN Resources.ProgramVersion pv ON pv.IDProgramClara = es.IDProgramme
						LEFT JOIN Resources.Program p ON pv.IDProgram = p.IDProgram 
						WHERE (es.AnSession = @AnSession OR es.AnSession IS NULL) 
						AND (es.Etat > 0 OR es.Etat IS NULL)
						AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
						AND (es.IDEtudiant NOT IN (SELECT IDEtudiant FROM CES.Users.Student) OR es.IDEtudiant IS NULL);";

		    rowsAffected = context.Database.SqlQuery<int>(sql, new SqlParameter("@AnSession", anSession)).First();



		    return rowsAffected;
	    }

		/// <summary>
		/// Adds Students from clara that are not in CSAdmin
		/// </summary>
		/// <param name="anSession">The an session for which to search students for.</param>
		/// <param name="context">The <see cref="CSAdminContext"/> to use for the operation if the underlying logic already has one open.</param>
		public static int InsertNewStudentsFromClara(int anSession, CSAdminContext context = null)
        {
            Constants.Initialise(ref context);

            int rowsAffected = 0;

            //Selects Students from studentUser
            IEnumerable<StudentUser> csadminStudent = context.StudentUsers.ToArray();
            IEnumerable<StudentUser> csadminNonActiveStudent = csadminStudent.InnactiveStudentsOnly();
            IEnumerable<Etudiant> claraStudent = StudentsInSemester(anSession, context).ToArray();


            //Logic to add students that are not in CSAdmin yet
            foreach (Etudiant i in claraStudent)
            {
                if (!csadminStudent.IncludesClaraStudent(i.IDEtudiant))
                {
                    User tempUser = new User
                    {
                        LastName = i.Nom,
                        FirstName = i.Prenom,
                        Username = i.Numero7,
                        IsActive = true
                    };
                    context.Users.Add(tempUser);
                    rowsAffected += context.SaveChanges();

                    //Insert to StudentUser
                    context.StudentUsers.Add(new StudentUser
                    {
                        IDUser = tempUser.IDUser,
                        IDEtudiant = i.IDEtudiant
                    });

                    //Insert to UserRole
                    context.UserRoles.Add(new UserRole
                    {
                        IDUser = tempUser.IDUser,
                        IDRole = Constants.CES_STUDENT_ROLE_ID,
                        IsActive = true
                    });
                }
            }

            //Logic to activate Students
            foreach (Etudiant i in claraStudent)
            {
                foreach (StudentUser j in csadminNonActiveStudent)
                {
                    //If CSAdmin students are in clara
                    if (i.IDEtudiant == j.IDEtudiant)
                    {
                        j.User.IsActive = true;
                        //Goes through each UserRole and activates them
                        foreach (UserRole ur in j.User.UserRoles)
                        {
                            ur.IsActive = true;
                        }
                    }
                }
            }

            //TODO: Logic to insert new ces students uspInsertNewCESStudents


            rowsAffected += context.SaveChanges();
            return rowsAffected;
        }

        /// <summary>
        /// Deactivates any students within csadmin who aren't taking part in the specified semester
        /// </summary>
        /// <param name="anSession"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int DeactivateInactiveStudents(short anSession, CSAdminContext context = null)
        {
            // just straight up using a merge statement because it's much faster and efficient
            // by like a good 5 seconds
            Constants.Initialise(ref context);

            // deactivates the student
            int affected = context.Database.ExecuteSqlCommand(
                $@"merge into [users].[user] u
					using (
						select
							su.[IDUser]
						from(
							select distinct
								es1.[IDEtudiant]
							from
								[ClaraEtudiants].[EtudiantSession] es1
							where
								es1.[AnSession] = {anSession} and
								not(
									es1.Etat = {(byte)EtudiantSession.ACTIVE} and
									es1.[IDUniteOrg] in ({Constants.UNIT_ORG_ID_CONTINUED_EDUCATION}, {Constants.UNIT_ORG_ID_REGULAR_EDUCTAION})
								)
						) es2
						join[Users].[StudentUser]
						su
						   on es2.[IDEtudiant] = su.[IDEtudiant]
					) su
						on u.[IDUser] = su.[IDUser]
				when matched then
					update SET
						u.isactive = 0;");

            //deactivates the student's roles
            affected += context.Database.ExecuteSqlCommand(
                $@"merge into [Applications].[UserRole] ur
					using (
						select
							su.[IDUser]
						from (
							select distinct
								es1.[IDEtudiant]
							from
								[ClaraEtudiants].[EtudiantSession] es1
							where
								es1.[AnSession] = {anSession} and
								not(
									es1.Etat = {(byte)EtudiantSession.ACTIVE} and
									es1.[IDUniteOrg] in ({Constants.UNIT_ORG_ID_CONTINUED_EDUCATION},{Constants.UNIT_ORG_ID_REGULAR_EDUCTAION})
								)
						) es2
						join [Users].[StudentUser] su
							on es2.IDEtudiant = su.IDEtudiant
					) su
						on ur.[IDUser] = su.[IDUser]
				when matched then
					update SET
						ur.isactive = 0;"
            );
            return affected;
        }

	    public static int NumRemoveCESStudents(int anSession, CSAdminContext context = null)
	    {
			Constants.Initialise(ref context);

			var sql = @"SELECT COUNT(IDEtudiant) 'Number of Students'
						FROM CES.Users.student s
						WHERE IDEtudiant NOT IN
						(
						SELECT DISTINCT es.IDEtudiant
						FROM ClaraEtudiants.EtudiantSession es
						JOIN ClaraProgrammes.Programme p	ON p.IDProgramme = es.IDProgramme
						WHERE (es.AnSession = @AnSession OR es.AnSession IS NULL)                  
						AND (es.Etat > 0 OR es.Etat IS NULL)
						);";
		    //sql.Parameters.AddWithValue("@AnSession", anSession);
			return context.Database.SqlQuery<int>(sql, new SqlParameter("@AnSession", anSession)).First();

			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//int rowsAffected = 0;

			//IEnumerable<Etudiant> claraStudent = context.EtudiantSessions.StudentsInSemester(anSession);
			//IEnumerable<StudentUser> csadminActiveStudent = context.StudentUsers.ActiveStudentsOnly();

			////TODO: redo Logic for deactivateStudents
			//foreach (Etudiant e in claraStudent)
			//{
			//	foreach (StudentUser su in csadminActiveStudent)
			//	{
			//		if (e.IDEtudiant != su.IDEtudiant)
			//		{
			//			rowsAffected++;
			//		}
			//	}
			//}

			//return rowsAffected;
		}

        public static int RemoveCESStudents(int anSession, CSAdminContext context = null)
        {
            Constants.Initialise(ref context);

            int rowsAffected = 0;

            IEnumerable<Etudiant> claraStudent = context.EtudiantSessions.StudentsInSemester(anSession);
            IEnumerable<StudentUser> csadminActiveStudent = context.StudentUsers.ActiveStudentsOnly();

            //TODO: redo Logic for deactivateStudents
            foreach (Etudiant e in claraStudent)
            {
                foreach (StudentUser su in csadminActiveStudent)
                {
                    if (e.IDEtudiant != su.IDEtudiant)
                    {
                        foreach (UserRole ur in su.User.UserRoles)
                        {
                            ur.IsActive = false;
                        }
                        su.User.IsActive = false;
                    }
                }
            }


            //TODO: Logic for deleteCESStudents (Needs CES in the context)


            return rowsAffected;
        }

		public static int NumAllNewFacultyFromClara(int anSession, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);

			int rowsAffected = 0;

			IEnumerable<EmployeeUser> csAdminEmployees = context.EmployeeUsers.ToArray().Distinct();
			IEnumerable<Employe> claraCoordinators = CoordinatorsInSemester(anSession, context).ToArray().Distinct();
			IEnumerable<Employe> claraTeacher = TeachersInSemester(anSession, context).ToArray().Distinct();

			foreach (Employe e in claraTeacher)
			{
				if (csAdminEmployees.IncludesClaraEmployee(e.IDEmploye))
				{
					rowsAffected++;
				}
			}
			return rowsAffected;
		}

	    public static int NumAllNewCoordinatorsFromClara(int anSession, CSAdminContext context = null)
	    {
		    Constants.Initialise(ref context);

		    string sql = @"SELECT count(DISTINCT c.IDEmploye) 'Number of Coordinators'
							FROM ClaraEmployes.Coordonnateur c
							JOIN ClaraEmployes.Employe e ON e.IDEmploye = c.IDEmploye
							AND(c.AnSessionDebut IS NULL OR c.AnSessionDebut <= @AnSession)
							AND(c.AnSessionFin IS NULL OR c.AnSessionFin >= @AnSession)
							AND(c.IDUniteOrg = 235 OR c.IDUniteOrg = 525)
							AND(
								--Check if they exist in CSAdmin at all
							c.IDEmploye NOT IN(SELECT eu.IDEmploye
								FROM Users.EmployeeUser eu
								)
							--check if they exist in CSAdmin but not as a coordinator
								OR c.IDEmploye NOT IN(
								SELECT eu.IDEmploye
								FROM Users.EmployeeUser eu
							JOIN Applications.UserRole ur           ON ur.IDUser = eu.IDUser
							JOIN Applications.Role r                ON r.IDRole = ur.IDRole
							JOIN Applications.ApplicationRole ar    ON ar.IDRole = r.IDRole
							JOIN Applications.Application a         ON a.IDApplication = ar.IDApplication
							WHERE a.Code = 'CES'
							AND r.Code = 'CO'
							)
							);";

		    return context.Database.SqlQuery<int>(sql, new SqlParameter("@AnSession", anSession)).First();


			//int rowsAffected = 0;


			//IEnumerable<EmployeeUser> csAdminEmployees = context.EmployeeUsers.ToArray();
			//IEnumerable<Employe> claraCoordinators = CoordinatorsInSemester(anSession, context).ToArray();
			//IEnumerable<Employe> claraTeacher = TeachersInSemester(anSession, context).ToArray();

			//foreach (Employe e in claraCoordinators)
			//{
			// if (!csAdminEmployees.IncludesClaraEmployee(e.IDEmploye))
			// {
			//  rowsAffected++;
			// }
			//}
			//return rowsAffected;
		}

		public static int InsertAllNewFacultyFromClara(int anSession, CSAdminContext context = null)
        {
            Constants.Initialise(ref context);

            int rowsAffected = 0;


            IEnumerable<EmployeeUser> csAdminEmployees = context.EmployeeUsers.ToArray();
            IEnumerable<Employe> claraCoordinators = CoordinatorsInSemester(anSession, context).ToArray();
            IEnumerable<Employe> claraTeacher = TeachersInSemester(anSession, context).ToArray();

            foreach (Employe e in claraCoordinators)
            {
                if (!csAdminEmployees.IncludesClaraEmployee(e.IDEmploye))
                {
                    User tempUser = new User
                    {
                        LastName = e.Nom,
                        FirstName = e.Prenom,
                        Username = RoleManager.CreateUserName(e.Prenom, e.Nom),
                        IsActive = true
                    };
                    context.Users.Add(tempUser);
                    rowsAffected += context.SaveChanges();

                    context.EmployeeUsers.Add(new EmployeeUser
                    {
                        IDUser = tempUser.IDUser,
                        IDEmploye = e.IDEmploye
                    });

                    context.UserRoles.Add(new UserRole
                    {
                        IDUser = tempUser.IDUser,
                        IDRole = Constants.CES_COORDONATOR_ROLE_ID,
                        IsActive = true
                    });
                }
            }

            foreach (Employe e in claraTeacher)
            {
                if (csAdminEmployees.IncludesClaraEmployee(e.IDEmploye))
                {
                    User tempUser = new User
                    {
                        LastName = e.Nom,
                        FirstName = e.Prenom,
                        Username = RoleManager.CreateUserName(e.Prenom, e.Nom),
                        IsActive = true
                    };
                    context.Users.Add(tempUser);
                    rowsAffected += context.SaveChanges();

                    context.EmployeeUsers.Add(new EmployeeUser
                    {
                        IDUser = tempUser.IDUser,
                        IDEmploye = e.IDEmploye
                    });

                    context.UserRoles.Add(new UserRole
                    {
                        IDUser = tempUser.IDUser,
                        IDRole = Constants.CES_TEACHER_ROLE_ID,
                        IsActive = true
                    });
                }
            }

            //TODO: logic for InsertNewCESTeachersFromClara

            //unless you want an id you don't need to save 15 times a second - gui
            rowsAffected += context.SaveChanges();
            return rowsAffected;
        }

        public static int RemoveFaculty(int anSession, CSAdminContext context = null)
        {
            Constants.Initialise(ref context);

            IEnumerable<EmployeeUser> csAdminEmployees = context.EmployeeUsers.ToArray();
            IEnumerable<Employe> claraCoordinators = CoordinatorsInSemester(anSession, context).ToArray();

            int rowsAffected = 0;

            foreach (EmployeeUser eu in csAdminEmployees) {
                if (!claraCoordinators.IncludesCSAdminEmployee(eu.IDEmploye)) {
                    foreach (UserRole ur in eu.User.UserRoles)
                    {
                        ur.IsActive = false;
                    }
                    eu.User.IsActive = false;
                }
            }

            rowsAffected += context.SaveChanges();
            return rowsAffected;
        }

	    public static int NumRemoveCoordinators(int anSession, CSAdminContext context = null)
	    {
		    Constants.Initialise(ref context);

		    string sql = @"SELECT count(u.IDUser) 'Number of Coordinators'
							FROM Users.EmployeeUser eu
								JOIN Applications.UserRole ur			ON ur.IDUser = eu.IDUser
								JOIN Applications.Role r				ON r.IDRole = ur.IDRole
								JOIN Applications.ApplicationRole ar	ON ar.IDRole = r.IDRole
								JOIN Applications.Application a			ON a.IDApplication = ar.IDApplication
								JOIN [Users].[User] u					ON eu.IDUser = u.IDUser
							WHERE ur.IsActive = 1
								AND r.Code = 'CO'
								AND eu.IDEmploye NOT IN 
								(
								SELECT DISTINCT c.IDEmploye 
								FROM ClaraEmployes.Coordonnateur c
									JOIN ClaraEmployes.Employe e			ON e.IDEmploye = c.IDEmploye
									AND (c.AnSessionDebut IS NULL OR c.AnSessionDebut <= @AnSession)
									AND (c.AnSessionFin IS NULL OR c.AnSessionFin >= @AnSession)
									AND c.IDUniteOrg = 235
								);";

		    return context.Database.SqlQuery<int>(sql, new SqlParameter("@AnSession", anSession)).First();

			//IEnumerable<EmployeeUser> csAdminEmployees = context.EmployeeUsers.ToArray();
			//IEnumerable<Employe> claraCoordinators = CoordinatorsInSemester(anSession, context).ToArray();

			//int rowsAffected = 0;

			//foreach (EmployeeUser eu in csAdminEmployees)
			//{
			// if (!claraCoordinators.IncludesCSAdminEmployee(eu.IDEmploye))
			// {
			//  rowsAffected++;
			// }
			//}
			//return rowsAffected;
		}

		public static List<User> SelectAllUsernames(string SortExpression, string SearchLastName, string SearchFirstName, string SearchUsername, CSAdminContext context = null)
        {
            if (SortExpression == null)
                SortExpression = "Name";

            Constants.Initialise(ref context);

            List<User> adUsers = new List<User>();
            List<User> users = new List<User>();

            if (SearchLastName != null)
            {
                users = getAllUsersByLastName(SearchLastName).ToList();
            }
            if (SearchFirstName != null)
            {
                users = getAllUsersByFirstName(SearchFirstName).ToList();
            }
            if (SearchUsername != null)
            {
                users = getAllUsersByUsername(SearchUsername).ToList();
            }
            else {
                users = getAllUsers().ToList();
            }

            //var results = Auth.getPrincipalSearcher().FindAll();

            //foreach (UserPrincipalEx p in results)
            //{
            //    adUsers.Add(new User
            //    {
            //        FirstName = p.GivenName,
            //        LastName = p.Surname,
            //        Username = p.MailNickName
            //    });
            //}

            //foreach (User aduUser in adUsers)
            //{
            //    User tempUser = new User();

            //    foreach (User user in users)
            //    {

            //        if (aduUser.FirstName == user.FirstName &&
            //            aduUser.LastName == user.LastName &&
            //            aduUser.Username == user.Username)
            //        {
            //            users.Add(aduUser);
            //        }
            //    }
            //}

            if (SortExpression.EndsWith("DESC"))
            {
                SortExpression.Replace("DESC", "").Trim();

                if (SortExpression == "Name")
                {
                    users = users.OrderByDescending(x => x.LastName + x.FirstName).ToList();
                }

                if (SortExpression == "Username")
                {
                    users = users.OrderByDescending(x => x.Username).ToList();
                }

            }
            else {
                if (SortExpression == "Name" || (SortExpression == null || SortExpression.Length == 0))
                {
                    users = users.OrderBy(x => x.LastName + x.FirstName).ToList();
                }

                if (SortExpression == "Username")
                {
                    users = users.OrderBy(x => x.Username).ToList();
                }
            }

            return users;

        }

        public static string SelectAdUsername(string username, string firstName, string lastName)
        {
            string name;

            if (username.Any(c => char.IsDigit(c)))
                name = username;
            else
                name = lastName + " " + firstName;


            var results = Auth.getPrincipalSearcher().FindAll();
            foreach (UserPrincipal p in results)
            {
                if (p.Name.ToString().Equals(name))
                        return p.SamAccountName;                 
            }
            name = "";
            return name;
        }

        public static List<User> SelectUsersNotInAD(string SortExpression, string SearchLastName, string SearchFirstName, string SearchUsername, CSAdminContext context = null) {
            Constants.Initialise(ref context);

            if (SortExpression == null)
                SortExpression = "Name";

            List<User> users = new List<User>();
            List<User> adUsers = new List<User>();
            List<User> nonadUsers = new List<User>();

            if (SearchLastName != null)
            {
                users = getAllUsersByLastName(SearchLastName).ToList();
            }
            if (SearchFirstName != null)
            {
                users = getAllUsersByFirstName(SearchFirstName).ToList();
            }
            if (SearchUsername != null)
            {
                users = getAllUsersByUsername(SearchUsername).ToList();
            }
            else
            {
                users = getAllUsers().ToList();
            }

            var results = Auth.getPrincipalSearcher().FindAll();

            foreach (UserPrincipal p in results)
            {
                adUsers.Add(new User
                {
                    FirstName = p.GivenName,
                    LastName = p.Surname,
                    Username = p.SamAccountName
                });
            }
            bool flag = false;
            foreach (User u in users)
            {
                flag = false;
                foreach(User au in adUsers)
                {
                    if (au.Username == u.Username)
                        flag = true;
                }
                if (!flag)
                    nonadUsers.Add(u);
              
            }

            if (SortExpression.EndsWith("DESC"))
            {
                SortExpression.Replace("DESC", "").Trim();

                if (SortExpression == "Name")
                {
                    users = nonadUsers.OrderByDescending(x => x.LastName + x.FirstName).ToList();
                }

                if (SortExpression == "Username")
                {
                    users = nonadUsers.OrderByDescending(x => x.Username).ToList();
                }
            }
            else
            {
                if (SortExpression == "Name" || (SortExpression == null || SortExpression.Length == 0))
                {
                    users = nonadUsers.OrderBy(x => x.LastName + x.FirstName).ToList();
                }

                if (SortExpression == "Username")
                {
                    users = nonadUsers.OrderBy(x => x.Username).ToList();
                }
            }
            return users;
        }
        
        public static List<User> SelectAllTeachers(int AnSession, CSAdminContext context = null) {
            Constants.Initialise(ref context);

            List<User> users = new List<User>();

            IEnumerable<Employe> employes = 
            (
                ((
                    from Employe e in context.Employes
                    join EmployeGroupe eg in context.EmployeGroupes
                        on e.IDEmploye equals eg.IDEmploye
                    join Groupe g in context.Groupes
                        on eg.IDGroupe equals g.IDGroupe
                    where
                        (g.AnSession == AnSession) &&
                        (e.Etat == 1)
                    select e
                ).Distinct()).Union
                ((
                    from Employe e in context.Employes
                    join EmployeeUser eu in context.EmployeeUsers
                        on e.IDEmploye equals eu.IDEmploye
                    join User u in context.Users
                        on eu.IDUser equals u.IDUser
                    where
                        (e.Etat == 1)
                    select e
                ).Distinct()).OrderBy(e => e.Nom)
            );

            foreach (Employe e in employes) {
                users.Add(
                    new User() {
                        IDUser = e.IDEmploye,
                        LastName = e.Nom,
                        FirstName = e.Prenom,
                    }
                );
            }


            return users;
        }

        public static List<User> SelectDuplicateUsernames(string SortExpression, string SearchLastName, string SearchFirstName, string SearchUsername, CSAdminContext context = null)
        {
            Constants.Initialise(ref context);

            List<User> adUsers = new List<User>();
            List<User> users = new List<User>();
            List<User> dupUsers = new List<User>();

            if (SortExpression == null)
                SortExpression = "all";

            if (SearchLastName != null)
            {
                users = getAllUsersByLastName(SearchLastName).ToList();
            }
            if (SearchFirstName != null)
            {
                users = getAllUsersByFirstName(SearchFirstName).ToList();
            }
            if (SearchUsername != null)
            {
                users = getAllUsersByUsername(SearchUsername).ToList();
            }
            else
            {
                users = getAllUsers().ToList();
            }

            //var results = Auth.getPrincipalSearcher().FindAll();

            //foreach (UserPrincipalEx p in results)
            //{
            //    adUsers.Add(new User
            //    {
            //        FirstName = p.GivenName,
            //        LastName = p.Surname,
            //        Username = p.MailNickName
            //    });
            //}

            //foreach (User aduUser in adUsers)
            //{
            //    User tempUser = new User();

            //    foreach (User user in users)
            //    {

            //        if (aduUser.FirstName == user.FirstName &&
            //            aduUser.LastName == user.LastName &&
            //            aduUser.Username == user.Username)
            //        {
            //            users.Add(aduUser);
            //        }
            //    }
            //}
            users = users.OrderByDescending(x => x.LastName + x.FirstName).ToList();

            List<string> usernames = new List<string>();
            foreach (User user in users) {
                if (!usernames.Contains(user.Username) && user.Username !="Unknown")
                    usernames.Add(user.Username);
            }

            foreach(string name in usernames)
            {
                List<User> dupes = users.FindAll(x => x.Username == name);
                if (dupes.Count > 1)
                {
                    foreach(User u in dupes)
                    {
                        dupUsers.Add(u);
                    }
                }
                
            }


            if (SortExpression.EndsWith("DESC"))
            {
                SortExpression.Replace("DESC", "").Trim();

                if (SortExpression == "Name")
                {
                    dupUsers = dupUsers.OrderByDescending(x => x.LastName + x.FirstName).ToList();
                }

                if (SortExpression == "Username")
                {
                    dupUsers = dupUsers.OrderByDescending(x => x.Username).ToList();
                }

            }
            else
            {
                if (SortExpression == "Name" || (SortExpression == null || SortExpression.Length == 0))
                {
                    dupUsers = dupUsers.OrderBy(x => x.LastName + x.FirstName).ToList();
                }

                if (SortExpression == "Username")
                {
                    dupUsers = dupUsers.OrderBy(x => x.Username).ToList();
                }
            }

            return dupUsers;
        }

        public static List<User> SelectADByLastName(string SearchLastName, int IDUser, bool isActive, CSAdminContext context = null)
        {
            Constants.Initialise(ref context);


            List<User> adUsers = new List<User>();

            var results = Auth.getPrincipalSearcher().FindAll();

            foreach (UserPrincipalEx p in results)
            {
                adUsers.Add(new User
                {
                    FirstName = p.GivenName,
                    LastName = p.Surname,
                    Username = p.MailNickName
                });
            }

            return adUsers.OrderBy(u => u.LastName + u.FirstName).ToList();
        }

        public static bool CheckDuplicateUsername(int IDUser, string Username, CSAdminContext context = null) {
            return (
                from User u in context.Users
                where IDUser != u.IDUser && Username == u.Username
                select u
                ).ToList().Count > 1;
        }

        //TODO: No longer needs RAC delete and to be determined for Alumni delete.
        public static void DeletePendingUsers(CSAdminContext context = null) {
            Constants.Initialise(ref context);

            context.AspnetMemberships.RemoveRange(
                    from aspnet_Membership m in context.AspnetMemberships
                    where m.IsApproved == false
                    select m
                );
        }

        #region Queries


        /// <summary>
        /// Gets all active students
        /// </summary>
        /// <param name="studentUsers">The list of student users to use for the check</param>
        /// <returns>The list of active students</returns>
        internal static IEnumerable<StudentUser> ActiveStudentsOnly(this IEnumerable<StudentUser> studentUsers)
		{
			return studentUsers.Where(su => su.User.IsActive);
		}

		/// <summary>
		/// Gets all inactive students
		/// </summary>
		/// <param name="studentUsers">The list of student users to use for the check</param>
		/// <returns>The list of inactive students</returns>
		internal static IEnumerable<StudentUser> InnactiveStudentsOnly(this IEnumerable<StudentUser> studentUsers)
		{
			return studentUsers.Where(su => !su.User.IsActive);
		}

	    internal static IEnumerable<Etudiant> InnactiveEtudiantsOnly(this IEnumerable<Etudiant> studentUsers)
	    {
		    return studentUsers.Where(su => !su.CSAdminStudentUser.IsActive);
	    }

		/// <summary>
		/// Gets the list of students for a semester
		/// </summary>
		/// <param name="anSession">The semester to look for</param>
		/// <param name="context">The csadmin context to use if one doesn't already exists </param>
		/// <returns>The list of students for a semester</returns>
		internal static IEnumerable<Etudiant> StudentsInSemester(int anSession, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return StudentsInSemester(context.EtudiantSessions, anSession);
		}
		/// <summary>
		/// Gets the list of students for a semester
		/// </summary>
		/// <param name="semesterRecords">The records to use for the semester</param>
		/// <param name="anSession">The session to look for</param>
		/// <returns>The list of students for a semester</returns>
		private static IEnumerable<Etudiant> StudentsInSemester(this IEnumerable<EtudiantSession> semesterRecords, int anSession, CSAdminContext context = null)
		{
			return
				from EtudiantSession es in semesterRecords
				where
					(es.Etat == EtudiantSession.ACTIVE) &&
					(es.AnSession == anSession) &&
					(
						(es.IDUniteOrg == Constants.UNIT_ORG_ID_REGULAR_EDUCTAION) ||
						(es.IDUniteOrg == Constants.UNIT_ORG_ID_CONTINUED_EDUCATION)
					)
				select es.Etudiant;
		}

		/// <summary>
		/// Gets the list of students for a given semester
		/// </summary>
		/// <param name="anSession">The semester to look for</param>
		/// <param name="context">The csadmin context to use if one already exists</param>
		/// <returns>The list of students for a given semester</returns>
		internal static IEnumerable<Employe> CoordinatorsInSemester(int anSession, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return CoordinatorsInSemester(context.Coordonnateurs, anSession);
		}
		/// <summary>
		/// Gets the list of students for a given semester
		/// </summary>
		/// <param name="coordonateurs">The list of coordinators to look through</param>
		/// <param name="anSession">The semester to look for</param>
		/// <returns>The list of students for a given semester</returns>
		internal static IEnumerable<Employe> CoordinatorsInSemester(this IEnumerable<Coordonnateur> coordonateurs, int anSession)
		{
			// NOTE: ya gotta be careful about that logic you should encapsulate your clauses
			return
				from Coordonnateur c in coordonateurs
				where
					(c.Employe != null) &&
					(
						(c.AnSessionDebut == null) ||
						(c.AnSessionDebut <= anSession)
					) &&
					(
						(c.AnSessionFin == null) ||
						(c.AnSessionFin >= anSession)
					) &&
					(c.IDUniteOrg == Constants.UNIT_ORG_ID_REGULAR_EDUCTAION)
				select c.Employe;
		}

		/// <summary>
		/// Gets the list of teachers for a given semester
		/// </summary>
		/// <param name="anSession">The semester to look for</param>
		/// <param name="context">The csadmin context to use if one already exists</param>
		/// <returns>The teachers </returns>
		internal static IEnumerable<Employe> TeachersInSemester(int anSession, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return TeachersInSemester(context.EmployeGroupes, anSession);
		}
		/// <summary>
		/// Gets the list of teachers for a given semester
		/// </summary>
		/// <param name="employeGroupes">The list of employee groupe records to look for</param>
		/// <param name="anSession">The semester to look for</param>
		/// <returns>The list of teachers for a given semester</returns>
		private static IEnumerable<Employe> TeachersInSemester(this IEnumerable<EmployeGroupe> employeGroupes, int anSession)
		{
			//return employees who meet the folowing criterias
			//has a groupe that is marked as 
			return (
				from EmployeGroupe eg in employeGroupes
				where
					(eg.Groupe.Etat == Groupe.NotOpen) &&
					(eg.Groupe.AnSession == anSession)
				select eg.Employe)
				.Distinct();
		}

        public static IEnumerable<User> getAllUsers(CSAdminContext context = null) {
            Constants.Initialise(ref context);
            return
                from User u in context.Users
                select u;
        }

        public static IEnumerable<User> getAllUsersByFirstName(string key, CSAdminContext context = null)
        {
            Constants.Initialise(ref context);
            return
                from User u in context.Users
                where u.FirstName.Contains(key)
                select u;
        }

        public static IEnumerable<User> getAllUsersByLastName(string key, CSAdminContext context = null)
        {
            Constants.Initialise(ref context);
            return
                from User u in context.Users
                where u.LastName.Contains(key)
                select u;
        }

        public static IEnumerable<User> getAllUsersByUsername(string key, CSAdminContext context = null)
        {
            Constants.Initialise(ref context);
            return
                from User u in context.Users
                where u.Username.Contains(key)
                select u;
        }

        #endregion

        #region utility
        // NOTE: Killed the utility section and changed it to the extentions section becasue it makes it more reusable - GUILLAUME

        /// <summary>
        /// Checks if a list of student users contains a specific employee using the claraid
        /// </summary>
        /// <param name="list">The list to check through</param>
        /// <param name="idEmploye">The clara id of the etudiant</param>
        /// <returns>wether the collection contains a user with the specified claraid</returns>
        internal static bool IncludesClaraStudent(this IEnumerable<StudentUser> list, int idEtudiant)
		{
			// NOTE: you should have just used link instead of fetching all students one by one, also use the id and turn it into an extention method
			// using count because it simplifies the query by a lot and it's more efficient
			return list.Count(etu => etu.IDEtudiant == idEtudiant) > 0;
			//foreach (StudentUser su in list) {
			//    if (su.IDEtudiant == item.IDEtudiant) {
			//        return true;
			//    }
			//}
			//return false;
		}

		/// <summary>
		/// Checks if a list of employee users contains a specific employee using the claraid
		/// </summary>
		/// <param name="list">The list to check through</param>
		/// <param name="idEmploye">The clara id of the employee</param>
		/// <returns>if the collection contains a user with the specified claraid</returns>
		internal static bool IncludesClaraEmployee(this IEnumerable<EmployeeUser> list, int idEmploye) 
		{
			//same comments as for the method over this one
			return list.Count(etu => etu.IDEmploye == idEmploye) > 0;
			//foreach (EmployeeUser eu in list)
			//{
			//	if (eu.IDEmploye == IDEmploye)
			//	{
			//		return true;
			//	}
			//}
			//return false;
		}

        internal static bool IncludesCSAdminEmployee(this IEnumerable<Employe> list, int idEmploye)
        {
            //same comments as for the method over this one
            return list.Count(etu => etu.IDEmploye == idEmploye) > 0;
        }
		#endregion
		

		//TODO: Implement functionality for UpdateUsername
		public static void UpdateUsername(int IDUser, string Username, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			User editUser = context.Users.FirstOrDefault(u => u.IDUser == IDUser);
			editUser.Username = Username;
			context.Users.AddOrUpdate(editUser);
			context.SaveChanges();
		}

	}
}
