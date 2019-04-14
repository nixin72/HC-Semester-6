using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     Un program pris en charge par CLARA.
	/// </summary>
	[Table("ClaraProgrammes.Programme")]
	public class Programme
	{
		/// <summary>
		///     L'identifient unique du programme
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDProgramme { get; set; }

		/// <summary>
		///     No documentation available
		/// </summary>
		public int? IDTypeSanction { get; set; }

		/// <summary>
		///     The number associated with the program
		/// </summary>
		[StringLength(5)]
		public string Numero { get; set; }

		/// <summary>
		///     The short title for the program,
		/// </summary>
		[StringLength(26)]
		public string TitreCourt { get; set; }

		/// <summary>
		///     The medium length title for the program.
		/// </summary>
		[StringLength(53)]
		public string TitreMoyen { get; set; }

		/// <summary>
		///     The long title for the program.
		/// </summary>
		[StringLength(255)]
		public string TitreLong { get; set; }

		/// <summary>
		///     The short title for the program,
		/// </summary>
		[StringLength(26)]
		public string TitreCourtTraduit { get; set; }

		/// <summary>
		///     The medium length title for the program.
		/// </summary>
		[StringLength(53)]
		public string TitreMoyenTraduit { get; set; }

		/// <summary>
		///     The translated title of the program.
		/// </summary>
		[StringLength(100)]
		public string TitreTraduit { get; set; }

		/// <summary>
		///     The long title for the program.
		/// </summary>
		[StringLength(255)]
		public string TitreLongTraduit { get; set; }

		/// <summary>
		///     Le titre long officiel de ce programme
		/// </summary>
		[StringLength(255)]
		public string TitreLongOfficiel { get; set; }

		/// <summary>
		///     <para>The program Type:</para>
		///     <para>AEC or DEC</para>
		///     <para>For unifersity:</para>
		///     <para>1er cycle :</para>
		///     <para>BC = Baccalaur�at(Bachelor's degree)</para>
		///     <para>PC = Programme court(Short Program)</para>
		///     <para>CT = Certificat(Certificate)</para>
		///     <para>EL = �tudiant libre(Autonomous Student)</para>
		///     <para>SS = Summer School</para>
		///     <para>SE = S�jour d��tude(Study Stay)</para>
		///     <para>Second Cycle :</para>
		///     <para>MM = Ma�trise avec m�moire(Master with memory)</para>
		///     <para>MP = Ma�trise avec projet(Master with project)</para>
		///     <para>DE = Dipl�me d��tudes Sup�rieures Sp�cialis�es(Diploma of specialised superior studies)</para>
		///     <para>PC = Programme court(Short Program)</para>
		///     <para>CI = Cursus imbriqu�(Nested curriculum)</para>
		///     <para>Thrid cycle :</para>
		///     <para>DC = Doctorat(doctorate)</para>
		///     <para>DP = Post doctorat(post doctorate)</para>
		///     <para>CO = Cotutelle</para>
		/// </summary>
		[StringLength(5)]
		public string TypeProgramme { get; set; }

		/// <summary>
		///     Indicates if the program is provided within the college.
		/// </summary>
		public bool IndicateurLocal { get; set; }

		/// <summary>
		///     the year version of the program.
		/// </summary>
		public short AnneeVersion { get; set; }

		/// <summary>
		///     The parent program to the program
		/// </summary>
		public int? IDProgrammeSuperieur { get; set; }

		/// <summary>
		///     The formation type provided by the program defined by SOBEC.
		/// </summary>
		public int IDTypeFormation { get; set; }

		/// <summary>
		///     The parent program to the program
		/// </summary>
		[ForeignKey("IDProgrammeSuperieur")]
		public virtual Programme ProgrammeSuperieur { get; set; }

		/// <summary>
		///     The list of programs that require this program
		/// </summary>
		public virtual ICollection<Programme> ProgramInferieurs { get; set; }

		/// <summary>
		///     Aucune documentation disponible.
		/// </summary>
		public virtual ICollection<BrancheObjectifProgramme> BranchesObjectifProgrammes { get; set; }

		/// <summary>
		///     La liste des dossier sessionnel associ� au programme.
		/// </summary>
		public virtual ICollection<EtudiantSession> EtudiantSessions { get; set; }

		/// <summary>
		///     La liste des grille horaires associ� au programme.
		/// </summary>
		public virtual ICollection<Grille> Grilles { get; set; }
	}
}