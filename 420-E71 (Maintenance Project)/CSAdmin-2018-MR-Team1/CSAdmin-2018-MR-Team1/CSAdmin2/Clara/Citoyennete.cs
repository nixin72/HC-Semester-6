using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     A student's citizenship from CLARA.
	/// </summary>
	[Table("ClaraEtudiants.Citoyennete")]
	public class Citoyennete
	{
		public const string CANADIAN_CITIZEN = "CC";
		public const string NATIVE = "IN";
		public const string NOT_AVAILABLE = "ND";
		public const string KNOWN_REFUGEE = "RE";
		public const string PERMANENT_RESIDANT = "RP";
		public const string TEMPORARY_RESIDENT = "RT";
		public const string NO_OBJECT = "SO";
		public const string WITHOUT_STATUS = "ST";
		public const string UNKNOWN_INFORMATION = "ZC";

		/// <summary>
		///     The unique id associated with the student's citizenship.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDCitoyennete { get; set; }

		/// <summary>
		///     The unique identifier of the student.
		/// </summary>
		public int IDEtudiant { get; set; }

		/// <summary>
		///     <para>
		///         Le statut l�gal au Canada indique si l''�l�ve est � citoyen canadien � ou, si ce n''est pas le cas, sous
		///         quelle condition il a obtenu le droit de r�sider pour un s�jour permanent ou temporaire au pays. Ce droit de
		///         r�sider est confin� par un document officiel d�cern�
		///     </para>
		///     <para>par le gouvernement f�d�ral, plus sp�cifiquement par les services reli�s � l''immigration.</para>
		///     <para>
		///         �galement, il permet d''identifier les �l�ves �trangers sans statut l�gal au Canada dont les �tudes
		///         dispens�es par un organisme scolaire du Qu�bec sont suivies en dehors du Canada; cette indication est fournie
		///         par la valeur � sans objet �.
		///     </para>
		///     <para>CC = Citoyen canadien</para>
		///     <para>IN = Indien</para>
		///     <para>ND = Non disponible</para>
		///     <para>RE = R�fugi� reconnu</para>
		///     <para>RP = R�sident permanent</para>
		///     <para>RT = R�sident temporaire</para>
		///     <para>SO = Sans objet</para>
		///     <para>ST = Aucun statut</para>
		///     <para>ZC = Information inconnue(valeur Clara)</para>
		/// </summary>
		[StringLength(2)]
		public string StatutLegal { get; set; }

		/// <summary>
		///     The id of the student's country of residance form clara.
		/// </summary>
		public int? IDPaysCitoyennete { get; set; }

		/// <summary>
		///     L'<see cref="Clara.Etudiant" /> associ� avec cet objet.
		/// </summary>
		[ForeignKey("IDEtudiant")]
		public virtual Etudiant Etudiant { get; set; }
	}
}