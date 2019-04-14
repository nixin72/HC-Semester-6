using System;
using System.Linq;
using CSAdmin2.Model;

namespace CSAdmin2.Logic
{
	public class Test
	{
#if DEBUG
		public static void Main(string[] args)
		{
			CSAdminContext context = new CSAdminContext();

			var applications = context.Applications.ToList();
			var applicationRoles = context.ApplicationRoles.ToList();
			var countries = context.Countries.ToList();
			var educationTypes = context.EducationTypes.ToList();
			var employeeUsers = context.EmployeeUsers.ToList();
			var languages = context.Languages.ToList();
			var pageRoleSecurities = context.PageRoleSecurities.ToList();
			var programs = context.Programs.ToList();
			var programVersions = context.ProgramVersions.ToList();
			var provinces = context.Provinces.ToList();
			var provinceStates = context.ProvinceStates.ToList();
			var roles = context.Roles.ToList();
			var securityQuestions = context.SecurityQuestions.ToList();
			var settings = context.Settings.ToList();
			var studentUsers = context.StudentUsers.ToList();
			var users = context.Users.ToList();
			var userRoles = context.UserRoles.ToList();


			var brancheObjectifProgrammes = context.BrancheObjectifProgrammes.ToList();
			var categorieCours = context.CategorieCours.ToList();
			var citoyennetes = context.Citoyennetes.ToList();
			var coordonateurs = context.Coordonnateurs.ToList();
			var cours = context.Cours.ToList();
			var departements = context.Departements.ToList();
			var disciplines = context.Disciplines.ToList();
			var employes = context.Disciplines.ToList();
			var employeGroupes = context.EmployeGroupes.ToList();
			var employeStatutEngagements = context.EmployeStatutEngagements.ToList();
			var etudiants = context.Etudiants.ToList();
			var etudiantSessions = context.EtudiantSessions.ToList();
			var feuilleObjectifProgrammes = context.FeuilleObjectifProgrammes.ToList();
			var grilles = context.Grilles.ToList();
			var groupes = context.Groupes.ToList();
			//var inscriptions = context.Inscriptions.ToList(); // blocking it for now as it won't allow 32 bit versions to run as they run out of the 2GB memory the can access.
			var objectifs = context.Objectifs.ToList();
			var programmes = context.Programmes.ToList();
			var statutEngagements = context.StatutEngagements.ToList();
			var unitOrgs = context.UniteOrgs.ToList();
			Console.WriteLine("Put a breakpoint here");
		}
#endif
	}
}