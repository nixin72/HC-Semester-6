using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAC.Models;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.Helpers;
using RAC.RACModels;
using RAC.Validation;

namespace RAC.BusinessLogic
{
    public class ProgramBLL
    {
        private const string CLARA_CONNECTION = "Clara";
        public static Program GetProgramById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            Program foundProgram = DbContext.Context.Program.First(p => p.Id == id);
            return foundProgram;
        }

        public static IList<SelectListItem> GetProgramsOffered()
        {
            var val = new RegisterViewModel();
            return val.GetProgramsOfferedDropDownList();
        }

        public static bool CheckProgramHasGenEds(int id)
        {
            /*
             * The method is called from a controller determining whether or not the program the user selected contains a gen-ed or not
             *
             * See also:
             * GetProgramById(int id)
             * enableDisableGenEd() - ~/Scripts/register.js
             *
             * Parameters:
             * int id = The id of the program that is being checked if it contains gen-eds
             *
             * Returns:
             * Boolean, confirming whether or not the program being selected contains gen-eds 
             * 
             */

            Program programSelected = DbContext.Context.Program.FirstOrDefault(p => p.Id == id);
            var containsGenEds = false;
            Competency genEdPrograms = programSelected?.Competencies.FirstOrDefault(x => x.CompetencyMinistryData.IsGenEd);

            if (genEdPrograms != null)
            {
                containsGenEds =  true;
            }

            return containsGenEds;
        }

        public static List<ProgramResultsViewModel> GetProgramsFromClara(string programNumero)
        {
            List<ProgramResultsViewModel> ProgramResults = new List<ProgramResultsViewModel>();
            string sql = @" 

        SELECT [idprogramme], 
        [numero], 
        Isnull([titrelongtraduit], [titrelong])     AS ""Name"", 
        CASE 
         WHEN titrelongtraduit IS NOT NULL THEN[titrelong] 
         ELSE 'N/A' 
        END                                         AS ""French Name"", 
        [typeprogramme], 
        [anneeversion], 
        (SELECT Count(g.idgrille) 
        FROM   [CLARA].[Clara].[ReportClient].[Grille] g 
        WHERE  g.idprogramme = main.[idprogramme]) AS ""Program Profiles"", 
        (SELECT Count(*) 
        FROM   [CLARA].[Clara].[ReportClient].[Programme] p 
               INNER JOIN [CLARA].[Clara].[ReportClient].[BrancheObjectifProgramme] bop 
                       ON bop.[idprogramme] = p.[idprogramme] 
               INNER JOIN [CLARA].[Clara].[ReportClient].[FeuilleObjectifProgramme] fop 
                       ON fop.[idbrancheobjectifprogramme] = 
                          bop.[idbrancheobjectifprogramme] 
               INNER JOIN [CLARA].[Clara].[ReportClient].[ObjectifENG] ob 
                       ON ob.[idobjectif] = fop.[idobjectif] 
            WHERE  p.idprogramme = main.[idprogramme]) AS ""Competencies"" 
            FROM   [CLARA].[Clara].[ReportClient].[Programme] main 
            WHERE  main.[numero] = @ProgramNumero ";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProgramNumero", programNumero);

                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "Programme");
                    
                    DataTable tblProgramme;
                    tblProgramme = result.Tables["Programme"];
                    foreach (DataRow drCurrent in tblProgramme.Rows)
                    {
                        ProgramResultsViewModel p = new ProgramResultsViewModel();
                        p.ClaraId = Convert.ToInt32(drCurrent["IDProgramme"]);
                        p.ClaraCode = Convert.ToString(drCurrent["Numero"]);
                        p.ProgramNameFromClaraEN = Convert.ToString(drCurrent["Name"]);
                        p.ProgramNameFromClaraFR = Convert.ToString(drCurrent["French Name"]);
                        p.Year = Convert.ToInt32(drCurrent["AnneeVersion"]);
                        p.TypeOfProgram = Convert.ToString(drCurrent["TypeProgramme"]);
                        p.NumberOfCompetencies = Convert.ToInt32(drCurrent["Competencies"]);
                        p.NumberOfCourseProfiles = Convert.ToInt32(drCurrent["Program Profiles"]);
                        ProgramResults.Add(p);
                    }
                }
            }

            return ProgramResults;
        }

        public static List<CompetenciesViewModel> GetProgramCompetenciesFromClara(int ProgramId)
        {
            List<CompetenciesViewModel> CompetenciesResults = new List<CompetenciesViewModel>();
            string sql = @"SELECT ob.[numero], 
        (CASE 
           WHEN ob.[langueorigine] = 'FR' THEN Isnull(ob.[nomtraduit], [nom]) 
           ELSE Isnull(ob.[nom], ob.[nomtraduit]) 
         END ) AS ""Description"" 
            FROM[CLARA].[Clara].[ReportClient].[Programme] p
                INNER JOIN[CLARA].[Clara].[ReportClient].[BrancheObjectifProgramme] bop
                ON bop.[idprogramme] = p.[idprogramme]
            INNER JOIN[CLARA].[Clara].[ReportClient].[FeuilleObjectifProgramme]
            fop
                ON fop.[idbrancheobjectifprogramme] = 
                bop.[idbrancheobjectifprogramme]
            INNER JOIN[CLARA].[Clara].[ReportClient].[ObjectifENG]
            ob
                ON ob.[idobjectif] = fop.[idobjectif]
            WHERE  p.idprogramme = @ProgramID;";
            bool quit = false;
            int loopcount = 0;
            while (!quit)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@ProgramID", ProgramId);

                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                        {

                            DataSet result = new DataSet();
                            adapter.SelectCommand = command;
                            adapter.Fill(result, "Competencies");

                            DataTable tblCompetencies;
                            tblCompetencies = result.Tables["Competencies"];
                            foreach (DataRow drCurrent in tblCompetencies.Rows)
                            {
                                var c = new CompetenciesViewModel();

                                c.CompetencyCode = Convert.ToString(drCurrent["Numero"]);
                                c.CompetencyDescription = Convert.ToString(drCurrent["Description"]);
                                CompetenciesResults.Add(c);
                            }
                            quit = true;
                            
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    try
                    {
                        using (StreamWriter file = new StreamWriter(HttpContext.Current.Server.MapPath("err.log"), true))
                        {
                            file.WriteLine(ex.ToString());
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Error could not be logged: " + e.ToString());
                    }
                }
                loopcount++;
                if (loopcount > 3)
                {
                    quit = true;
                }
                    
            }

            return CompetenciesResults;

        }

        public static List<CompetenciesViewModel> GetGenEdCompetenciesFromClara(int ProgramId)
        {
            List<CompetenciesViewModel> CompetenciesResults = new List<CompetenciesViewModel>();
            string sql = @"SELECT ob.[numero], 
        (CASE 
           WHEN ob.[langueorigine] = 'FR' THEN Isnull(ob.[nomtraduit], [nom])
           ELSE Isnull(ob.[nom], ob.[nomtraduit]) 
         END ) AS ""Description"" 
            FROM[CLARA].[Clara].[ReportClient].[Programme] p
                INNER JOIN[CLARA].[Clara].[ReportClient].[BrancheObjectifProgramme] bop
                ON bop.[idprogramme] = p.[idprogramme]
            INNER JOIN[CLARA].[Clara].[ReportClient].[FeuilleObjectifProgramme]
            fop
                ON fop.[idbrancheobjectifprogramme] = 
                bop.[idbrancheobjectifprogramme]
            INNER JOIN[CLARA].[Clara].[ReportClient].[ObjectifENG]
            ob
                ON ob.[idobjectif] = fop.[idobjectif]
            WHERE  p.idprogramme = @ProgramID;  ";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProgramID", ProgramId);

                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "Competencies");

                    DataTable tblProgramme;
                    tblProgramme = result.Tables["Competencies"];
                    foreach (DataRow drCurrent in tblProgramme.Rows)
                    {
                        var c = new CompetenciesViewModel();

                        c.CompetencyCode = Convert.ToString(drCurrent["Numero"]);
                        c.CompetencyDescription = Convert.ToString(drCurrent["Description"]);
                        CompetenciesResults.Add(c);
                    }

                    return CompetenciesResults;
                }
            }
        }

        public static List<CourseProfileViewModel> GetCourseProfileFromClara(int ProgramId)
        {
            List<CourseProfileViewModel> CourseProfileList = new List<CourseProfileViewModel>();
            string sql = @"
        SELECT [IDGrille]
        ,[Numero]
        ,[Titre]
        FROM [CLARA].[Clara].[ReportClient].[Grille]

        where IdProgramme = @ProgramID;";
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProgramID", ProgramId);

                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "CourseProfiles");

                    DataTable tblCourseProfile;
                    tblCourseProfile = result.Tables["CourseProfiles"];
                    foreach (DataRow drCurrent in tblCourseProfile.Rows)
                    {
                        var c = new CourseProfileViewModel();

                        c.CourseProfileClaraId = Convert.ToInt32(drCurrent["IDGrille"]);
                        c.CourseProfileCode = Convert.ToString(drCurrent["Numero"]);
                        c.ProfileName = Convert.ToString(drCurrent["Titre"]);
                        CourseProfileList.Add(c);
                    }
                }
            }

            return CourseProfileList;
                    
        }

        public static List<CourseViewModel> GetCoursesFromClara(int ProfileId)
        {
            List<CourseViewModel> CourseList = new List<CourseViewModel>();
            string sql = @"SELECT CASE 
         WHEN [langueorigine] = 'FR' THEN Isnull([titrelongtraduit], [titrelong] 
                                          ) 
         ELSE Isnull([titrelong], [titrelongtraduit]) 
       END                AS 'Course Name', 
       [numero]           AS 'CourseCode', 
       [idcategoriecours] AS 'Type of course' 
       FROM   [CLARA].[Clara].[ReportClient].[Cours] p 
       WHERE  p.idcours IN(SELECT [idcours] 
                    FROM   [CLARA].[Clara].[ReportClient].[CoursGrille] 
                    WHERE  idgrille = @ProfileId) ";
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProfileId", ProfileId);

                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "Courses");

                    DataTable tblCourse;
                    tblCourse = result.Tables["Courses"];
                    foreach (DataRow drCurrent in tblCourse.Rows)
                    {
                        var c = new CourseViewModel();

                        c.CourseCode = Convert.ToString(drCurrent["CourseCode"]);
                        c.CourseName = Convert.ToString(drCurrent["Course Name"]);
                        c.CourseType = (CourseType)Convert.ToInt32(drCurrent["Type of Course"]);
                        CourseList.Add(c);
                    }
                }
            }

            return CourseList;

        }

        public static List<CourseViewModel> GetCoursesFromClaraNoGenEds(int ProfileId)
        {
            List<CourseViewModel> CourseList = new List<CourseViewModel>();
            string sql = @"SELECT CASE 
         WHEN [langueorigine] = 'FR' THEN Isnull([titrelongtraduit], [titrelong] 
                                          ) 
         ELSE Isnull([titrelong], [titrelongtraduit]) 
       END                AS 'Course Name', 
       [numero]   AS 'CourseCode', 
       [idcategoriecours] AS 'Type of course' 
       FROM   [CLARA].[Clara].[ReportClient].[Cours] p 
        WHERE  p.idcours IN(SELECT [idcours] 
                    FROM   [CLARA].[Clara].[ReportClient].[CoursGrille]
                    WHERE  idgrille = @ProfileId) 
					and [IdCategorieCours] = @ProgramSpecific";
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProfileId", ProfileId);
                command.Parameters.AddWithValue("@ProgramSpecific", (int) CourseType.SP);
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "Courses");

                    DataTable tblCourse;
                    tblCourse = result.Tables["Courses"];
                    foreach (DataRow drCurrent in tblCourse.Rows)
                    {
                        var c = new CourseViewModel();

                        c.CourseCode = Convert.ToString(drCurrent["CourseCode"]);
                        c.CourseName = Convert.ToString(drCurrent["Course Name"]);
                        c.CourseType = (CourseType)Convert.ToInt32(drCurrent["Type of Course"]);
                        CourseList.Add(c);
                    }
                }
            }

            return CourseList;

        }



        public static void CopyProgramFromClara(int ProgramClaraId, int ProgramProfileId)
        {
            var exists = DbContext.Context.ProgramMinistryData.Any(x => x.MinistryId == ProgramClaraId);
            //If exists copy over, if not don't
            if (exists != true)
            {
                string sql = @"SELECT [idprogramme], 
            [numero], 
            (SELECT [titre] 
            FROM   [CLARA].[Clara].[ReportClient].[Grille] 
            WHERE  idgrille = @ProgramProfileID) AS ""Name""
                FROM[CLARA].[Clara].[ReportClient].[Programme]
                WHERE idprogramme = @ProgramID ";
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ProgramID", ProgramClaraId);
                    command.Parameters.AddWithValue("@ProgramProfileID", ProgramProfileId);
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                    {

                        DataSet result = new DataSet();
                        adapter.SelectCommand = command;
                        adapter.Fill(result, "Program");

                        DataTable tblProgramme;
                        tblProgramme = result.Tables["Program"];
                        //Should only have one result, but we are looping anyways
                        foreach (DataRow drCurrent in tblProgramme.Rows)
                        {
                            var p = new ProgramMinistryData
                            {
                                MinistryId = Convert.ToInt32(drCurrent["IdProgramme"]),
                                Name = Convert.ToString(drCurrent["Name"]),
                                MinistryCode = Convert.ToString(drCurrent["Numero"])

                            };
                            DbContext.Context.ProgramMinistryData.Add(p);

                        }

                        DbContext.Context.SaveChanges();
                    }
                }
            }
        }

        public static void CopyCompetenciesFromClara(int ProgramClaraId, bool IsGenEd)
        {
            var databaseName = ConfigurationManager.AppSettings["RACDatabase"];
            string sql = @"SELECT ob.[idobjectif], 
       ob.[numero], 
       CASE 
         WHEN ob.[langueorigine] = 'AN' THEN Isnull(ob.[nom], ob.[nomtraduit]) 
         ELSE Isnull(ob.[nomtraduit], ob.[nom]) 
       END AS ""Description""
            FROM[CLARA].[Clara].[ReportClient].[Programme] p
                INNER JOIN[CLARA].[Clara].[ReportClient].[BrancheObjectifProgramme] bop
                ON bop.[idprogramme] = p.[idprogramme]
            INNER JOIN[CLARA].[Clara].[ReportClient].[FeuilleObjectifProgramme]
            fop
                ON fop.[idbrancheobjectifprogramme] = 
                bop.[idbrancheobjectifprogramme]
            INNER JOIN[CLARA].[Clara].[ReportClient].[ObjectifENG]
            ob
                ON ob.[idobjectif] = fop.[idobjectif]
            WHERE  p.idprogramme = @ProgramID
            AND ob.[idobjectif] NOT IN (SELECT[ministryid]
            FROM
                [" + databaseName + "].[dbo].[competencyministrydata]) ";
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProgramID", ProgramClaraId);

                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "Competencies");

                    DataTable tblProgramme;
                    tblProgramme = result.Tables["Competencies"];
                    //Should only have one result, but we are looping anyways
                    foreach (DataRow drCurrent in tblProgramme.Rows)
                    {
                        var c = new CompetencyMinistryData
                        {
                            MinistryId = Convert.ToInt32(drCurrent["IdObjectif"]),
                            Description = Convert.ToString(drCurrent["Description"]),
                            MinistryCode = Convert.ToString(drCurrent["Numero"]),
                            IsGenEd = IsGenEd
                            
                        };
                        DbContext.Context.CompetencyMinistryData.Add(c);

                    }

                    DbContext.Context.SaveChanges();
                }
            }
        }

        public static void CopyCoursesFromClara(int CourseProfileClaraId)
        {
            var databaseName = ConfigurationManager.AppSettings["RACDatabase"];
            string sql = @"SELECT p.[idcours], 
       CASE 
         WHEN [langueorigine] = 'FR' THEN Isnull([titrelongtraduit], [titrelong] 
                                          ) 
         ELSE Isnull([titrelong], [titrelongtraduit]) 
       END                AS 'Course Name', 
       [numero]           AS 'CourseCode', 
       [idcategoriecours] AS 'Type of course' 
    FROM   [CLARA].[Clara].[ReportClient].[Cours] p 
    WHERE  p.idcours IN(SELECT [idcours] 
                    FROM   [CLARA].[Clara].[ReportClient].[CoursGrille] 
                    WHERE  idgrille = @ProfileID) 
       AND p.[idcours] NOT IN (SELECT [ministryid] 
                               FROM   [" + databaseName + "].[dbo].[courseministrydata]) ";
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProfileID", CourseProfileClaraId);

                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "Courses");

                    DataTable tblCourse;
                    tblCourse = result.Tables["Courses"];
                    foreach (DataRow drCurrent in tblCourse.Rows)
                    {
                        var c = new CourseMinistryData
                        {
                            MinistryId = Convert.ToInt32(drCurrent["IDCours"]),
                            MinistryCode = Convert.ToString(drCurrent["CourseCode"]),
                            CourseType = Convert.ToInt32(drCurrent["Type of course"]),
                            Name = Convert.ToString(drCurrent["Course Name"])
                        };
                        DbContext.Context.CourseMinistryData.Add(c);

                    }

                    DbContext.Context.SaveChanges();
                }
            }
        }
        public static void CopyCoursesFromClaraNoGenEds(int CourseProfileClaraId)
        {
            var databaseName = ConfigurationManager.AppSettings["RACDatabase"];
            string sql = @"SELECT p.[idcours], 
       CASE 
         WHEN [langueorigine] = 'FR' THEN Isnull([titrelongtraduit], [titrelong] 
                                          ) 
         ELSE Isnull([titrelong], [titrelongtraduit]) 
       END                AS 'Course Name', 
       [numero]           AS 'CourseCode', 
       [idcategoriecours] AS 'Type of course' 
        FROM   [CLARA].[Clara].[ReportClient].[Cours] p 
        WHERE  p.idcours IN(SELECT [idcours] 
                    FROM   [CLARA].[Clara].[ReportClient].[CoursGrille] 
                    WHERE  idgrille = @ProfileID) 
       AND p.[idcours] NOT IN (SELECT [ministryid] 
                               FROM   [" + databaseName + "].[dbo].[courseministrydata]) AND p.[idcategoriecours] = @ProgramSpecific";
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProfileID", CourseProfileClaraId);
                command.Parameters.AddWithValue("@ProgramSpecific", (int) CourseType.SP);
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "Courses");

                    DataTable tblCourse;
                    tblCourse = result.Tables["Courses"];
                    foreach (DataRow drCurrent in tblCourse.Rows)
                    {
                        var c = new CourseMinistryData
                        {
                            MinistryId = Convert.ToInt32(drCurrent["IDCours"]),
                            MinistryCode = Convert.ToString(drCurrent["CourseCode"]),
                            CourseType = Convert.ToInt32(drCurrent["Type of course"]),
                            Name = Convert.ToString(drCurrent["Course Name"])
                        };
                        DbContext.Context.CourseMinistryData.Add(c);

                    }

                    DbContext.Context.SaveChanges();
                }
            }
        }

        public static Program CreateRACProgramWithGenEds(int ProgramClaraId, int GenEdClaraId, int CourseProfileClaraId)
        {
            //First going to go to Clara again, get all the competencies required for the program,
            //Get the list from the RAC DB to link it to Program
            var ProgramMinistry =
                DbContext.Context.ProgramMinistryData.FirstOrDefault(x => x.MinistryId == ProgramClaraId);
            var newProgram = new Program();
            if (ProgramMinistry != null)
            {
               newProgram = new Program()
                {
                    IsActive = true, //TODO: Should maybe default this to false
                    ProgramMinistryDataId = ProgramMinistry.Id,
                    DateAdded = DateTime.Now
                    
                };
                DbContext.Context.Program.Add(newProgram);
            }

            DbContext.Context.SaveChanges();

            //Afterwards going to go to Clara again, and get all the courses by their ID, and once again link it Program in the RAC DB
            //Since we copied the ids over from Clara before we start, we just reference it by their ID
            List<Int32> CourseIds = GetCourseMinistryIds(CourseProfileClaraId);
            List<Int32> ProgramCompetencyIds = GetCompetencyMinistryIds(ProgramClaraId);
            List<Int32> GenEdCompetencyIds = GetCompetencyMinistryIds(GenEdClaraId);

            CourseIds.ForEach(delegate(int id)
            {
                DbContext.Context.Course.Add(new Course
                {
                    CourseMinistryDataId = id,
                    ProgramId = newProgram.Id
                });
            });
            ProgramCompetencyIds.ForEach(delegate (int id)
            {
                DbContext.Context.Competency.Add(new Competency
                {
                    CompetencyMinistryDataId = id,
                    ProgramId = newProgram.Id
                });
            });
            GenEdCompetencyIds.ForEach(delegate (int id)
            {
                DbContext.Context.Competency.Add(new Competency
                {
                    CompetencyMinistryDataId = id,
                    ProgramId = newProgram.Id
                });
            });
            DbContext.Context.SaveChanges();

            //Getting the updated program after all the inserts and mappings have been done
            var mappedProgram = DbContext.Context.Program.Find(newProgram.Id);
            if (mappedProgram != null)
            {
                return mappedProgram;
            }
            //TODO run error handling on this condition
            return new Program();

        }

        public static Program CreateRACProgramWithoutGenEds(int ProgramClaraId, int CourseProfileClaraId)
        {
            //First going to go to Clara again, get all the competencies required for the program,
            //Get the list from the RAC DB to link it to Program
            var ProgramMinistry =
                DbContext.Context.ProgramMinistryData.FirstOrDefault(x => x.MinistryId == ProgramClaraId);
            var newProgram = new Program();
            if (ProgramMinistry != null)
            {
                newProgram = new Program()
                {
                    IsActive = true, //TODO: Should maybe default this to false
                    ProgramMinistryDataId = ProgramMinistry.Id,
                    DateAdded = DateTime.Now
                };
                DbContext.Context.Program.Add(newProgram);
            }

            DbContext.Context.SaveChanges();

            //Afterwards going to go to Clara again, and get all the courses by their ID, and once again link it Program in the RAC DB
            //Since we copied the ids over from Clara before we start, we just reference it by their ID
            List<Int32> CourseIds = GetCourseMinistryIds(CourseProfileClaraId);
            List<Int32> ProgramCompetencyIds = GetCompetencyMinistryIds(ProgramClaraId);

            CourseIds.ForEach(delegate (int id)
            {
                DbContext.Context.Course.Add(new Course
                {
                    CourseMinistryDataId = id,
                    ProgramId = newProgram.Id
                });
            });
            ProgramCompetencyIds.ForEach(delegate (int id)
            {
                DbContext.Context.Competency.Add(new Competency
                {
                    CompetencyMinistryDataId = id,
                    ProgramId = newProgram.Id
                });
            });
            DbContext.Context.SaveChanges();

            //Getting the updated program after all the inserts and mappings have been done
            var mappedProgram = DbContext.Context.Program.Find(newProgram.Id);
            if (mappedProgram != null)
            {
                return mappedProgram;
            }
            //TODO run error handling on this condition
            return new Program();

        }

        public static List<Int32> GetCourseMinistryIds(int ProgramProfileClaraId)
        {
            List<Int32> CourseMinistryIds = new List<Int32>();
            string sql = @"SELECT [idcours] 
            FROM   [CLARA].[Clara].[ReportClient].[CoursGrille] 
            WHERE  idgrille = @ProfileID ";
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProfileID", ProgramProfileClaraId);

                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "Courses");

                    DataTable tblCourse;
                    tblCourse = result.Tables["Courses"];
                    foreach (DataRow drCurrent in tblCourse.Rows)
                    {
                        var courseIdFromClara = (Convert.ToInt32(drCurrent["IDCours"]));
                        var course = DbContext.Context.CourseMinistryData.FirstOrDefault(x => x.MinistryId == courseIdFromClara);
                        if (course != null)
                        {
                            CourseMinistryIds.Add(course.Id);
                        }
                    }

                }
            }
            return CourseMinistryIds;
        }

        public static List<Int32> GetCompetencyMinistryIds(int ProgramClaraId)
        {
            List<Int32> CompetencyMinistryIds = new List<Int32>();
            string sql = @"SELECT ob.[idobjectif] 
        FROM   [CLARA].[Clara].[ReportClient].[Programme] p 
        INNER JOIN [CLARA].[Clara].[ReportClient].[BrancheObjectifProgramme] bop 
               ON bop.[idprogramme] = p.[idprogramme] 
        INNER JOIN [CLARA].[Clara].[ReportClient].[FeuilleObjectifProgramme] fop 
               ON fop.[idbrancheobjectifprogramme] = 
                  bop.[idbrancheobjectifprogramme] 
        INNER JOIN [CLARA].[Clara].[ReportClient].[ObjectifENG] ob 
               ON ob.[idobjectif] = fop.[idobjectif] 
        WHERE  p.idprogramme = @ProgramID";
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[CLARA_CONNECTION].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProgramID", ProgramClaraId);

                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {

                    DataSet result = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(result, "Competencies");

                    DataTable tblCourse;
                    tblCourse = result.Tables["Competencies"];
                    foreach (DataRow drCurrent in tblCourse.Rows)
                    {
                        var competencyIdFromClara = (Convert.ToInt32(drCurrent["IdObjectif"]));
                        var competency = DbContext.Context.CompetencyMinistryData.FirstOrDefault(x => x.MinistryId == competencyIdFromClara);
                        if (competency != null)
                        {
                            CompetencyMinistryIds.Add(competency.Id);
                        }
                    }

                }
            }

            return CompetencyMinistryIds;
        }

        public static List<Program> GetAllPrograms()
        {
            return DbContext.Context.Set<Program>().ToList();
        }

        public static Program GetProgram(int ProgramId)
        {
            return DbContext.Context.Program.Find(ProgramId);

        }
        public static List<CompetencyElement> GetCompetencyElements(int CompetencyId)
        {
            return DbContext.Context.CompetencyElement.Where(x => x.CompetencyId == CompetencyId).ToList();
        }

        public static Competency GetCompetency(int CompetencyId)
        {
            return DbContext.Context.Competency.Find(CompetencyId);
        }

        public static List<ElementMinistryData> GetAllElementsNotAlreadyInCompetency(int id)
        {
            var ElementsLinkedToCompetency = DbContext.Context.CompetencyElement.Where(x => x.CompetencyId == id);
            var NotElementMinistryData = DbContext.Context.ElementMinistryData.Where(x => ElementsLinkedToCompetency.All(y => y.ElementMinistryDataId != x.Id) && x.DateExpired == null).OrderBy(y => y.Description).ToList();

            //Afterwards getting all the elements that were set inactive for competency elements
            var CompetencyElementsInactive = DbContext.Context.CompetencyElement.Where(x=> x.CompetencyId == id && x.ElementMinistryData.DateExpired == null && x.DateExpired != null).OrderBy(x=> x.Id).ToList();
            for(int i = 0; i < CompetencyElementsInactive.Count(); i++)
            {
                NotElementMinistryData.Add(CompetencyElementsInactive.ElementAt(i).ElementMinistryData);
            }
            return NotElementMinistryData.OrderBy(x=> x.Description).ToList();
        }

        public static List<ElementMinistryData> GetAllElements()
        {
            return DbContext.Context.ElementMinistryData.Where(x => x.DateExpired == null).OrderBy(x => x.Description).ToList();
        }

        public static ElementMinistryData GetSpecificElement(int id)
        {
            return DbContext.Context.ElementMinistryData.Find(id);
        }

        public static ElementViewModel MapToEditViewModel(ElementMinistryData el)
        {
            return new ElementViewModel()
            {
                Id = el.Id,
                Description = el.Description
            };
        }

        public static void UpdateSpecificElement(ElementMinistryData element)
        {
            var IsUsed = DbContext.Context.RACRequestCompetencyElement.Any(x => x.CompetencyElement.ElementMinistryDataId == element.Id);
            if (IsUsed)
            {
                var elementFromDB = DbContext.Context.ElementMinistryData.Find(element.Id);
                elementFromDB.DateExpired = DateTime.Now;
                DbContext.Context.ElementMinistryData.Add(new ElementMinistryData
                {
                    Description = element.Description,
                    DateAdded = DateTime.Now,
                    DateExpired = null,
                    MinistryCode = "N/A",
                    MinistryId = 0
                });
            }
            else
            {
                var elementFromDB = DbContext.Context.ElementMinistryData.Find(element.Id);
                elementFromDB.Description = element.Description;
            }

            DbContext.Context.SaveChanges();
        }

        public static void DeleteSpecificElement(ElementMinistryData element)
        {
            if (element != null)
            {
                var IsUsed = DbContext.Context.RACRequestCompetencyElement.Any(x => x.CompetencyElement.ElementMinistryDataId == element.Id);
                if (IsUsed)
                {
                    var elementFromDB = DbContext.Context.ElementMinistryData.Find(element.Id);
                    elementFromDB.DateExpired = DateTime.Now;
                }
                else
                {
                    DbContext.Context.ElementMinistryData.Remove(DbContext.Context.ElementMinistryData.Find(element.Id));
                }

                DbContext.Context.SaveChanges();
            }

        }

        public static void UpdateCompetencyElements(Competency competency)
        {
            var ExistingCompetencyElements = new List<CompetencyElement>();
            //Removing all the ones from this list that are passed in from competency
            //DbContext.Context.CompetencyElement.RemoveRange(DbContext.Context.CompetencyElement.Where(x => x.CompetencyId == competency.Id));
            //First getting Competency From Database
            var CompetencyFromDatabase = DbContext.Context.Competency.Find(competency.Id);
            //Gets all the ones that weren't passed in from the form
            //var TEST = CompetencyFromDatabase.CompetencyElements.Where(x => !competency.CompetencyElements.Any(y => y.CompetencyId == x.CompetencyId && y.ElementMinistryDataId == x.ElementMinistryDataId)).ToList();
            
            var CopyOfCompetencyElementsFromDatabase = CompetencyFromDatabase.CompetencyElements;
            //Setting all the ones that aren't passed in to expired
            foreach (var ce in CompetencyFromDatabase.CompetencyElements.Where(x=> !competency.CompetencyElements.Any(y=> y.CompetencyId == x.CompetencyId && y.ElementMinistryDataId == x.ElementMinistryDataId)))
            {
                //Need the if statement so that we dont overwrite the date expired for existing inactive elements
                if (ce.DateExpired == null)
                {
                    ce.DateExpired = DateTime.Now;
                }
            }

            //Checking ones that currently exist in the database, will be re-enable them
            foreach (var ce in CompetencyFromDatabase.CompetencyElements.Where(x => competency.CompetencyElements.Any(y => y.CompetencyId == x.CompetencyId && y.ElementMinistryDataId == x.ElementMinistryDataId) && x.DateExpired != null))
            {
                ce.DateExpired = null;
            }
            //var TEST2 = competency.CompetencyElements.Where(x => !CompetencyFromDatabase.CompetencyElements.Any(y => y.CompetencyId == x.CompetencyId && y.ElementMinistryDataId == x.ElementMinistryDataId)).ToList();
            //After setting the new ones to active, remove from list and check which ones need to be added, add those to the list
            foreach (var ce in competency.CompetencyElements.Where(x => !CompetencyFromDatabase.CompetencyElements.Any(y => y.CompetencyId == x.CompetencyId && y.ElementMinistryDataId == x.ElementMinistryDataId)))
            {
                DbContext.Context.CompetencyElement.Add(new CompetencyElement
                {
                    CompetencyId = competency.Id,
                    ElementMinistryDataId = ce.ElementMinistryDataId,
                    DateAdded = DateTime.Now
                }); 
            }

            //After adding those to list, go through all the inactive ones, and check if they have concurrency issues and if they dont remove them.
            var DeleteList = new List<CompetencyElement>();
            foreach (var ce in CompetencyFromDatabase.CompetencyElements.Where(x => !competency.CompetencyElements.Any(y => y.CompetencyId == x.CompetencyId && y.ElementMinistryDataId == x.ElementMinistryDataId) && x.DateExpired != null))
            {
                if (!DbContext.Context.RACRequestCompetencyElement.Any(x=> x.CompetencyElementId == ce.Id))
                {
                    DeleteList.Add(ce);
                }
            }
            DbContext.Context.CompetencyElement.RemoveRange(DeleteList);
            DbContext.Context.SaveChanges();
           
            
        }

        public static void AddCompetencyElement(string desc)
        {
            DbContext.Context.ElementMinistryData.Add(new ElementMinistryData
            {
                Description = desc,
                MinistryCode = "N/A",
                MinistryId = 0,
                DateAdded = DateTime.Now
            });
            DbContext.Context.SaveChanges();
        }

        public static Course GetCourseById(int id)
        {
            return DbContext.Context.Course.Find(id);
        }

       

        public static List<Competency> GetAllCompetenciesNotAlreadyInCourse(int programid, int CourseId)
        {            
            var CompetenciesInProgram = DbContext.Context.Competency.Where(x => x.ProgramId == programid).ToList();
            var CompetenciesInCourse = DbContext.Context.CourseCompetency.Where(x => x.CourseId == CourseId).ToList();
            //TODO: Turn this into a LAMBDA Expression after
            for (int i = 0; i < CompetenciesInCourse.Count(); i++)
            {
                CompetenciesInProgram.Remove(CompetenciesInCourse.ElementAt(i).Competency);
            }

            

            CompetenciesInProgram.OrderByDescending(x => x.CompetencyMinistryData.MinistryCode);
            return CompetenciesInProgram;
        }

        public static void UpdateCourseCompetencies(Course course)
        {
            DbContext.Context.CourseCompetency.RemoveRange(DbContext.Context.CourseCompetency.Where(x => x.CourseId == course.Id));
            foreach (CourseCompetency cc in course.CourseCompetencies)
            {
                DbContext.Context.CourseCompetency.Add(new CourseCompetency {
                    CourseId = cc.CourseId,
                    CompetencyId = cc.CompetencyId
                });
            }
            DbContext.Context.SaveChanges();
        }        
        

        public static void DeleteProgram(int id)
        {
            //If program exists
            var program = DbContext.Context.Program.Find(id);
            if (program != null)
            {
                //Check if it is being used. 
                if (IsProgramUsed(program))
                {
                    program.DateExpired = DateTime.Now;
                }
                else
                {
                   foreach (var competency in program.Competencies)
                   {                        
                       DbContext.Context.CompetencyElement.RemoveRange(competency.CompetencyElements);
                   }

                    DbContext.Context.Competency.RemoveRange(program.Competencies);
                    DbContext.Context.Course.RemoveRange(DbContext.Context.Course.Where(x => x.ProgramId == program.Id));
                    DbContext.Context.Program.Remove(program);
                }

                DbContext.Context.SaveChanges();
            }

        }

        private static bool IsProgramUsed(Program program)
        {
            if (DbContext.Context.RACRequest.Any(x => x.ProgramId == program.Id))
            {
                return true;
            }

            if (DbContext.Context.RACRequestCompetency.Any(x => x.Competency.ProgramId == program.Id))
            {
                return true;
            }

            if (DbContext.Context.RACRequestCompetencyElement.Any(x =>
                x.RACRequestCompetency.Competency.ProgramId == program.Id))
            {
                return true;
            }

            if (DbContext.Context.CourseCompetency.Any(x => x.Course.ProgramId == program.Id))
            { 
                return true;
            }
        return false;
    }

        public static void UpdateProgram(Program program)
        {
            var programFromDB = DbContext.Context.Program.Find(program.Id);
            programFromDB.IsActive = program.IsActive;
            DbContext.Context.SaveChanges();
        }
    }
}