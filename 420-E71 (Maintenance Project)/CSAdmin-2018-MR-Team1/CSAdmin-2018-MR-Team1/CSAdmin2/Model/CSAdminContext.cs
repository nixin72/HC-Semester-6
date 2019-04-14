using System.Data.Entity;
using System.Diagnostics;
using CSAdmin2.Clara;

namespace CSAdmin2.Model
{
	/// <summary>
	///     The entity framework context used to fetch and update data within the csadmin system.
	/// </summary>
	public class CSAdminContext : DbContext
	{
		/// <summary>
		///     Creates a new entity framework database context for the csadmin database.
		/// </summary>
		public CSAdminContext() : base("name=CSAdminContext")
		{
#if DEBUG
			//This debug only code is used to debug and figure out the calls made to the database
			Database.Log = s => Debug.WriteLine(s);
#endif
		}

		#region csadmin

		/// <summary>
		///     The applications that can authentificate using the system.
		/// </summary>
		public virtual DbSet<Application> Applications { get; set; }

		/// <summary>
		///     The list of associative entities that identifies which roles can authentificate to an application.
		/// </summary>
		public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

		/// <summary>
		///     The list of roles that a user can have.
		/// </summary>
		public virtual DbSet<Role> Roles { get; set; }

		/// <summary>
		///     The list of associative entities that are used to assign roles to a csadmin user.
		/// </summary>
		public virtual DbSet<UserRole> UserRoles { get; set; }

		/// <summary>
		///     The list of countries known by the system.
		/// </summary>
		public virtual DbSet<Country> Countries { get; set; }

		/// <summary>
		///     The list of managed education types.
		/// </summary>
		public virtual DbSet<EducationType> EducationTypes { get; set; }

		/// <summary>
		///     The list of languages that can be managed within the system.
		/// </summary>
		public virtual DbSet<Language> Languages { get; set; }

		/// <summary>
		///     The list of programs managed by the system.
		/// </summary>
		public virtual DbSet<Program> Programs { get; set; }

		/// <summary>
		///     The list of associative entities that relate program revisions to their clara entry.
		/// </summary>
		public virtual DbSet<ProgramVersion> ProgramVersions { get; set; }

		/// <summary>
		///     The list of known canadian provinces and their accornyms.
		/// </summary>
		public virtual DbSet<Province> Provinces { get; set; }

		/// <summary>
		///     The list associative entities that relate known state and provinces to their respective country.
		/// </summary>
		public virtual DbSet<ProvinceState> ProvinceStates { get; set; }

		/// <summary>
		///     A list of associative entity that relates csadmin users to the clara users using the idemploye from the
		///     employes.employe table within clara.
		/// </summary>
		public virtual DbSet<EmployeeUser> EmployeeUsers { get; set; }

		/// <summary>
		///     A list of associative entity that relates csadmin users to the clara users using the idetudiant from the
		///     etidiants.etudiant table within clara.
		/// </summary>
		public virtual DbSet<StudentUser> StudentUsers { get; set; }

		/// <summary>
		///     The list of known and registered users within the CSAdmin system.
		/// </summary>
		public virtual DbSet<User> Users { get; set; }

		/// <summary>
		///     <para>A list that specifies which roles can access specific pages.</para>
		///     <para>May be depricated as the only system it serves doesn't use it anymore.</para>
		/// </summary>
		//[Obsolete("The only system who uses this, CSAdmin, doesn't use it anymore in it's code and it's content are inconsistent. The entity might be deprecated.")]
		public virtual DbSet<PageRoleSecurity> PageRoleSecurities { get; set; }

		/// <summary>
		///     The system settings used by csadmin.
		/// </summary>
		public virtual DbSet<Setting> Settings { get; set; }

		/// <summary>
		///     The security questions used by the system.
		/// </summary>
		public virtual DbSet<SecurityQuestion> SecurityQuestions { get; set; }

		#endregion

		#region Clara

		/// <summary>
		///     Contient la liste des cours.
		/// </summary>
		public virtual DbSet<Cour> Cours { get; set; }

		/// <summary>
		///     Liste des coordonnateurs.
		/// </summary>
		public virtual DbSet<Coordonnateur> Coordonnateurs { get; set; }

		/// <summary>
		///     Liste des employés.
		/// </summary>
		public virtual DbSet<Employe> Employes { get; set; }

		/// <summary>
		///     ETC maximal pour les statut d'engagements temps partiel (type de calcul de l'ETC = 2).
		/// </summary>
		public virtual DbSet<EmployeStatutEngagement> EmployeStatutEngagements { get; set; }

		/// <summary>
		///     Contient les données de citoyenneté des étudiants.
		/// </summary>
		public virtual DbSet<Citoyennete> Citoyennetes { get; set; }

		/// <summary>
		///     Liste des etudiants
		/// </summary>
		public virtual DbSet<Etudiant> Etudiants { get; set; }

		/// <summary>
		///     L'entité EtudiantSession contient la liste des dossiers sessions attribués à l'étudiant.
		/// </summary>
		public virtual DbSet<EtudiantSession> EtudiantSessions { get; set; }

		/// <summary>
		///     No documentation available as of now, but i suspect it's relaed to the schedule making process
		/// </summary>
		public virtual DbSet<Grille> Grilles { get; set; }

		/// <summary>
		///     Cette table contient les liens entre les groupes et les enseignants.
		/// </summary>
		public virtual DbSet<EmployeGroupe> EmployeGroupes { get; set; }

		/// <summary>
		///     Links groups to courses
		/// </summary>
		public virtual DbSet<Groupe> Groupes { get; set; }

		/// <summary>
		///     Links students to groups
		/// </summary>
		public virtual DbSet<Inscription> Inscriptions { get; set; }

		/// <summary>
		///     No documentation as of yet
		/// </summary>
		public virtual DbSet<BrancheObjectifProgramme> BrancheObjectifProgrammes { get; set; }

		/// <summary>
		///     No documentation as of yet
		/// </summary>
		public virtual DbSet<FeuilleObjectifProgramme> FeuilleObjectifProgrammes { get; set; }

		/// <summary>
		///     No documentation as of yet
		/// </summary>
		public virtual DbSet<Objectif> Objectifs { get; set; }

		/// <summary>
		///     The list of programs managed by clara
		/// </summary>
		public virtual DbSet<Programme> Programmes { get; set; }

		/// <summary>
		///     Contient les différentes catégories de cours du système
		/// </summary>
		public virtual DbSet<CategorieCour> CategorieCours { get; set; }

		/// <summary>
		///     The list of departements managed by clara
		/// </summary>
		public virtual DbSet<Departement> Departements { get; set; }

		/// <summary>
		///     The list of competencies managed by clara
		/// </summary>
		public virtual DbSet<Discipline> Disciplines { get; set; }

		/// <summary>
		///     No documentation as of yet
		/// </summary>
		public virtual DbSet<StatutEngagement> StatutEngagements { get; set; }

		/// <summary>
		///     Liste des unités organisationnelles
		/// </summary>
		public virtual DbSet<UniteOrg> UniteOrgs { get; set; }

		public virtual DbSet<aspnet_Membership> AspnetMemberships { get; set; }

		#endregion

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			#region csadmin

			modelBuilder.Entity<aspnet_Membership>();

			modelBuilder.Entity<Application>()
				.Property(e => e.Code)
				.IsFixedLength()
				.IsUnicode(true);

			modelBuilder.Entity<Application>()
				.Property(e => e.Description)
				.IsUnicode(true);

			modelBuilder.Entity<Application>()
				.HasMany(e => e.ApplicationRoles)
				.WithRequired(e => e.Application)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Application>()
				.HasMany(e => e.PageRoleSecurities)
				.WithRequired(e => e.Application)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Role>()
				.Property(e => e.Code)
				.IsFixedLength()
				.IsUnicode(true);

			modelBuilder.Entity<Role>()
				.Property(e => e.Description)
				.IsUnicode(true);

			modelBuilder.Entity<Role>()
				.HasMany(e => e.ApplicationRoles)
				.WithRequired(e => e.Role)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Role>()
				.HasMany(e => e.UserRoles)
				.WithRequired(e => e.Role)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Country>()
				.Property(e => e.Name)
				.IsUnicode(true);

			modelBuilder.Entity<Country>()
				.HasMany(e => e.ProvinceStates)
				.WithRequired(e => e.Country)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<EducationType>()
				.Property(e => e.Name)
				.IsUnicode(true);

			modelBuilder.Entity<EducationType>()
				.HasMany(e => e.Programs)
				.WithRequired(e => e.EducationType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Language>()
				.Property(e => e.Name)
				.IsUnicode(true);

			modelBuilder.Entity<Program>()
				.Property(e => e.Name)
				.IsUnicode(true);

			modelBuilder.Entity<Program>()
				.HasMany(e => e.ProgramVersions)
				.WithRequired(e => e.Program)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Province>()
				.Property(e => e.Text)
				.IsUnicode(true);

			modelBuilder.Entity<Province>()
				.Property(e => e.Abbreviation)
				.IsFixedLength()
				.IsUnicode(true);

			modelBuilder.Entity<ProvinceState>()
				.Property(e => e.Name)
				.IsUnicode(true);

			modelBuilder.Entity<User>()
				.Property(e => e.LastName)
				.IsUnicode(true);

			modelBuilder.Entity<User>()
				.Property(e => e.FirstName)
				.IsUnicode(true);

			modelBuilder.Entity<User>()
				.Property(e => e.Username)
				.IsUnicode(true);

			modelBuilder.Entity<User>()
				.HasMany(e => e.UserRoles)
				.WithRequired(e => e.User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasOptional(e => e.EmployeeUser)
				.WithRequired(e => e.User);

			modelBuilder.Entity<User>()
				.HasOptional(e => e.StudentUser)
				.WithRequired(e => e.User);

			modelBuilder.Entity<PageRoleSecurity>()
				.Property(e => e.Role)
				.IsUnicode(true);

			modelBuilder.Entity<PageRoleSecurity>()
				.Property(e => e.PageName)
				.IsUnicode(true);

			modelBuilder.Entity<SecurityQuestion>()
				.Property(e => e.Text)
				.IsUnicode(true);

			#endregion

			#region Clara

			modelBuilder.Entity<Cour>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<Cour>()
				.Property(e => e.TitreLong)
				.IsUnicode(false);

			modelBuilder.Entity<Cour>()
				.Property(e => e.NumeroOfficiel)
				.IsUnicode(false);

			modelBuilder.Entity<Cour>()
				.Property(e => e.TitreCourt)
				.IsUnicode(false);

			modelBuilder.Entity<Cour>()
				.Property(e => e.TitreCourtOfficiel)
				.IsUnicode(false);

			modelBuilder.Entity<Cour>()
				.Property(e => e.TitreMoyen)
				.IsUnicode(false);

			modelBuilder.Entity<Cour>()
				.Property(e => e.TitreMoyenTraduit)
				.IsUnicode(false);

			modelBuilder.Entity<Cour>()
				.Property(e => e.LangueOrigine)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Employe>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<Employe>()
				.Property(e => e.Nom)
				.IsUnicode(false);

			modelBuilder.Entity<Employe>()
				.Property(e => e.Prenom)
				.IsUnicode(false);

			modelBuilder.Entity<Employe>()
				.Property(e => e.NomPrenom)
				.IsUnicode(false);

			modelBuilder.Entity<Citoyennete>()
				.Property(e => e.StatutLegal)
				.IsUnicode(false);

			modelBuilder.Entity<Etudiant>()
				.Property(e => e.Numero7)
				.IsUnicode(false);

			modelBuilder.Entity<Etudiant>()
				.Property(e => e.Nom)
				.IsUnicode(false);

			modelBuilder.Entity<Etudiant>()
				.Property(e => e.Prenom)
				.IsUnicode(false);

			modelBuilder.Entity<Etudiant>()
				.Property(e => e.Sexe)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<EtudiantSession>()
				.Property(e => e.TypeFrequentation)
				.IsUnicode(false);

			modelBuilder.Entity<Grille>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<Grille>()
				.Property(e => e.Titre)
				.IsUnicode(false);

			modelBuilder.Entity<Inscription>()
				.Property(e => e.TypeRAF)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Inscription>()
				.Property(e => e.SourceFinancement)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Objectif>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<Objectif>()
				.Property(e => e.Nom)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.TitreCourt)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.TitreMoyen)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.TitreLong)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.TitreCourtTraduit)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.TitreMoyenTraduit)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.TitreTraduit)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.TitreLongTraduit)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.TitreLongOfficiel)
				.IsUnicode(false);

			modelBuilder.Entity<Programme>()
				.Property(e => e.TypeProgramme)
				.IsUnicode(false);

			modelBuilder.Entity<CategorieCour>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<CategorieCour>()
				.Property(e => e.Titre)
				.IsUnicode(false);

			modelBuilder.Entity<Departement>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<Departement>()
				.Property(e => e.TitreLong)
				.IsUnicode(false);

			modelBuilder.Entity<Discipline>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<StatutEngagement>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<StatutEngagement>()
				.Property(e => e.TitreCourt)
				.IsUnicode(false);

			modelBuilder.Entity<StatutEngagement>()
				.Property(e => e.TitreLong)
				.IsUnicode(false);

			modelBuilder.Entity<UniteOrg>()
				.Property(e => e.Numero)
				.IsUnicode(false);

			modelBuilder.Entity<UniteOrg>()
				.Property(e => e.TitreLong)
				.IsUnicode(false);

			#endregion
		}
	}
}