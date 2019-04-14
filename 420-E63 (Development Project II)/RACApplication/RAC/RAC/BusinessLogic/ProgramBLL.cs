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
using RAC.RACModels;

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
                        Console.WriteLine(@"Error could not be logged: " + e);
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
            [TypeProgramme],
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
                                Name = Convert.ToString(drCurrent["Name"]) + " ("+ Convert.ToString(drCurrent["TypeProgramme"]) + ")", 
                                MinistryCode = Convert.ToString(drCurrent["Numero"]),
                                
                                
                                
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



        public static Program CreateRACProgramWithGenEds(int ProgramClaraId, int GenEdClaraId, int CourseProfileClaraId)
        {
            //First going to go to Clara again, get all the competencies required for the program,
            //Get the list from the RAC DB to link it to Program
            var ProgramMinistry =
                DbContext.Context.ProgramMinistryData.FirstOrDefault(x => x.MinistryId == ProgramClaraId);
            var newProgram = new Program();
            if (ProgramMinistry != null)
            {
                //Setting all existing programs using the code to null
                var existingPrograms = DbContext.Context.Program.Where(x => x.ProgramMinistryData.MinistryCode == ProgramMinistry.MinistryCode && x.DateExpired == null);
                foreach(var program in existingPrograms)
                {
                    program.DateExpired = DateTime.Now;
                    program.IsActive = false;
                }
               newProgram = new Program()
                {
                    IsActive = false, //TODO: Should maybe default this to false
                    ProgramMinistryDataId = ProgramMinistry.Id,
                    DateAdded = DateTime.Now
                    
                };
                DbContext.Context.Program.Add(newProgram);

                
            }

            DbContext.Context.SaveChanges();

            //Afterwards going to go to Clara again, and get all the courses by their ID, and once again link it Program in the RAC DB
            //Since we copied the ids over from Clara before we start, we just reference it by their ID
            //List<Int32> CourseIds = GetCourseMinistryIds(CourseProfileClaraId);
            List<Int32> ProgramCompetencyIds = GetCompetencyMinistryIds(ProgramClaraId);
            List<Int32> GenEdCompetencyIds = GetCompetencyMinistryIds(GenEdClaraId);

            //CourseIds.ForEach(delegate(int id)
            //{
            //    DbContext.Context.Course.Add(new Course
            //    {
            //        CourseMinistryDataId = id,
            //        ProgramId = newProgram.Id
            //    });
            //});
            ProgramCompetencyIds.ForEach(delegate (int id)
            {
                DbContext.Context.Competency.Add(new Competency
                {
                    CompetencyMinistryDataId = id,
                    ProgramId = newProgram.Id,
                    IsActive = false
                });
            });
            GenEdCompetencyIds.ForEach(delegate (int id)
            {
                DbContext.Context.Competency.Add(new Competency
                {
                    CompetencyMinistryDataId = id,
                    ProgramId = newProgram.Id,
                    IsActive = false
                });
            });
            DbContext.Context.SaveChanges();
            //replacing any existing programs if there are any
            
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
                //Setting all existing programs using the code to null
                var existingPrograms = DbContext.Context.Program.Where(x => x.ProgramMinistryData.MinistryCode == ProgramMinistry.MinistryCode && x.DateExpired == null);
                foreach (var program in existingPrograms)
                {
                    program.DateExpired = DateTime.Now;
                    program.IsActive = false;
                }
                newProgram = new Program()
                {
                    IsActive = false,
                    ProgramMinistryDataId = ProgramMinistry.Id,
                    DateAdded = DateTime.Now
                };
                DbContext.Context.Program.Add(newProgram);
            }

            DbContext.Context.SaveChanges();

            //Afterwards going to go to Clara again, and get all the courses by their ID, and once again link it Program in the RAC DB
            //Since we copied the ids over from Clara before we start, we just reference it by their ID

            //List<Int32> CourseIds = GetCourseMinistryIds(CourseProfileClaraId);
            List<Int32> ProgramCompetencyIds = GetCompetencyMinistryIds(ProgramClaraId);

            //CourseIds.ForEach(delegate (int id)
            //{
            //    DbContext.Context.Course.Add(new Course
            //    {
            //        CourseMinistryDataId = id,
            //        ProgramId = newProgram.Id
            //    });
            //});
            ProgramCompetencyIds.ForEach(delegate (int id)
            {
                DbContext.Context.Competency.Add(new Competency
                {
                    CompetencyMinistryDataId = id,
                    ProgramId = newProgram.Id,
                    IsActive = false
                });
            });
            DbContext.Context.SaveChanges();
            //replacing any existing programs if there are any

            //Getting the updated program after all the inserts and mappings have been done
            var mappedProgram = DbContext.Context.Program.Find(newProgram.Id);
            if (mappedProgram != null)
            {
                return mappedProgram;
            }
            //TODO run error handling on this condition
            return new Program();

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

        public static bool CheckProgramExists(string programCode)
        {
            return DbContext.Context.ProgramMinistryData.Any(x => x.MinistryCode == programCode.ToUpper());
        }

        public static List<Program> GetAllPrograms()
        {
            return DbContext.Context.Set<Program>().ToList();
        }

        public static Program GetProgram(int ProgramId)
        {
            return DbContext.Context.Program.Find(ProgramId);

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
        return false;
    }

        public static Program UpdateProgram(Program program)
        {
            var programFromDB = DbContext.Context.Program.Find(program.Id);

            //First setting all the competency elements in program from db to false
            foreach (var competencies in programFromDB.Competencies)
            {
                foreach (var cel in competencies.CompetencyElements)
                {
                    cel.DateExpired = DateTime.Now;
                    cel.ElementMinistryData.DateExpired = DateTime.Now;
                }
            }

            foreach (var competency in program.Competencies)
            {
                var programfromDBCompetency = programFromDB.Competencies.FirstOrDefault(x => x.Id == competency.Id);
                //Looking for all the current competency elements in the prgoram
                programfromDBCompetency.IsActive = competency.IsActive;
                foreach (var el in competency.CompetencyElements.Where(x=> x.Id != 0))
                {
                    var programFromDBElement = programfromDBCompetency?.CompetencyElements.FirstOrDefault(x => x.Id == el.Id);
                    //if the program is updating its competency elements, but it is used by a RAC Request
                    if (DbContext.Context.RACRequestCompetencyElement.Any(x=> x.CompetencyElementId == programFromDBElement.Id))
                    {
                        if (programFromDBElement?.ElementMinistryData.Description != el.ElementMinistryData.Description)
                        {
                            programFromDBElement.DateExpired = DateTime.Now;
                            programFromDBElement.ElementMinistryData.DateExpired = DateTime.Now;                          
                            ElementMinistryData newEl = new ElementMinistryData
                            {
                                Id = 0,
                                Description = el.ElementMinistryData.Description,
                                MinistryId = 0,
                                MinistryCode = "N/A",
                                DateAdded = DateTime.Now
                            };

                            DbContext.Context.ElementMinistryData.Add(newEl);
                            DbContext.Context.SaveChanges();
                            CompetencyElement newComp = new CompetencyElement
                            {
                                Id = 0,
                                DateAdded = DateTime.Now,
                                CompetencyId = programfromDBCompetency.Id,
                                ElementMinistryDataId = newEl.Id
                            };

                            DbContext.Context.CompetencyElement.Add(newComp);
                            DbContext.Context.SaveChanges();
                        }
                        else
                        {
                            programFromDBElement.DateExpired = null;
                        }
                            
                    } //else just update it
                    else
                    {
                        if (programFromDBElement.ElementMinistryData.Description == el.ElementMinistryData.Description)
                        {
                            programFromDBElement.DateExpired = null;
                            programFromDBElement.ElementMinistryData.DateExpired = null;
                        }
                        else
                        {
                            programFromDBElement.ElementMinistryData.Description = el.ElementMinistryData.Description;
                            programFromDBElement.DateExpired = null;
                            programFromDBElement.ElementMinistryData.DateExpired = null;
                        }

                    }
                    
                }
                //Adding new elements
                foreach (var el in competency.CompetencyElements.Where(x=> x.Id == 0))
                {
                    ElementMinistryData newEl = new ElementMinistryData
                    {
                        Id = 0,
                        Description = el.ElementMinistryData.Description,
                        MinistryId = 0,
                        MinistryCode = "N/A",
                        DateAdded = DateTime.Now
                    };

                    DbContext.Context.ElementMinistryData.Add(newEl);
                    DbContext.Context.SaveChanges();
                    CompetencyElement newComp = new CompetencyElement
                    {
                        DateAdded = DateTime.Now,
                        CompetencyId = programfromDBCompetency.Id,
                        ElementMinistryDataId = newEl.Id
                    };

                    programfromDBCompetency.CompetencyElements.Add(newComp);
                }
            }

            var elementDeleteList = new List<ElementMinistryData> ();
            //Afterwards clean up left over data
            foreach(var element in DbContext.Context.ElementMinistryData.Where(x=> x.DateExpired != null))
            {
                if (!DbContext.Context.RACRequestCompetencyElement.Any(x=> x.CompetencyElement.ElementMinistryDataId == element.Id))
                {
                    elementDeleteList.Add(element);
                }
                
            }

            var competencyElementDeleteList = new List<CompetencyElement>();
            foreach (var cel in DbContext.Context.CompetencyElement.Where(x => x.DateExpired != null))
            {
                if (!DbContext.Context.RACRequestCompetencyElement.Any(x => x.CompetencyElementId == cel.Id))
                {
                    competencyElementDeleteList.Add(cel);
                }

                if (cel.ElementMinistryData.DateExpired != null)
                {
                    if (!DbContext.Context.RACRequestCompetencyElement.Any(x => x.CompetencyElement.ElementMinistryDataId == cel.ElementMinistryDataId))
                    {
                        elementDeleteList.Add(cel.ElementMinistryData);
                    }
                }
               
                    

                
            }

            DbContext.Context.ElementMinistryData.RemoveRange(elementDeleteList);
            DbContext.Context.CompetencyElement.RemoveRange(competencyElementDeleteList);




                DbContext.Context.SaveChanges();
            return programFromDB;
            // ReSharper restore PossibleNullReferenceException
        }

        public static void ToggleArchive(int id, bool value)
        {
            Program prog = GetProgramById(id);
            prog.IsActive = value;
            UpdateProgram(prog);
        }
    }
}