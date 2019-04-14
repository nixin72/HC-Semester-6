using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     Un cour pris en charge par clara.
	/// </summary>
	[Table("ClaraBanqueCours.Cours")]
	public class Cour
	{
		/// <summary>
		///     Identifiant unique du cours.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDCours { get; set; }

		/// <summary>
		///     Cette information sert de valeur par défaut à la création des grilles de cours. Elle permet de créer des grilles
		///     plus rapidement. Règle générale, la catégorie du cours demeure la même dans une grille de cours, mais
		///     l''utilisateur pourra la changer si désiré.
		/// </summary>
		public int IDCategorieCours { get; set; }

		/// <summary>
		///     <para>
		///         Numéro unique du cours interne à Clara. Ce numéro est destiné à permettre une codification interne/maison des
		///         cours SOBEC.
		///     </para>
		///     <para>
		///         Dans le cas d'un cours SOBEC ou SOBEC en attente d'approbation, cette zone peut contenir un alias, soit un
		///         numéro de cours différent du numéro de cours SOBEC pour une utilisation strictement interne au collège. Dans le
		///         cas d'un cours Clara, ce champ doit obligatoirement contenir la même valeur que la zone Numero.
		///     </para>
		///     <para>
		///         Si aucun alias différent du numéro de cours "normal" n'est nécessaire, ce champ devrait contenir la même
		///         valeur que le champ Numero.
		///     </para>
		///     <para>
		///         Il ne faut pas confondre le numéro alias avec le numéro de cours ministère (SOBEC). Le numéro de cours
		///         ministère ne peut être utilisé que pour un cours Clara qui représente un cours SOBEC. Il peut arriver que
		///         plusieurs cours Clara réfèrent au même cours SOBEC(par exemple les cours d'éducation physique). Un alias sert
		///         uniquement à créer une codification interne des cours définis à SOBEC.
		///     </para>
		/// </summary>
		[StringLength(10)]
		public string Numero { get; set; }

		/// <summary>
		///     Titre long du cours.
		/// </summary>
		[StringLength(255)]
		public string TitreLong { get; set; }

		/// <summary>
		///     <para>
		///         Numéro du cours. Ce numéro de cours permet de faire référence au cours SOBEC si un numéro de cours alias
		///         différent du numéro de cours officiel a été spécifié.
		///     </para>
		///     <para>
		///         Pour un cours SOBEC ou SOBEC en attente d'importation, cette zone contient le numéro de cours SOBEC. Pour un
		///         cours Clara, cette zone contient toujours le numéro de cours Clara.
		///     </para>
		///     <para>
		///         Ce numéro de cours sera utilisé entre autres lors des transmissions au Ministère et affiché sur le bulletin
		///         officiel.
		///     </para>
		/// </summary>
		[StringLength(10)]
		public string NumeroOfficiel { get; set; }

		/// <summary>
		///     Titre court du cours.
		/// </summary>
		[StringLength(29)]
		public string TitreCourt { get; set; }

		/// <summary>
		///     Titre court officiel pour un cours Sobec.
		/// </summary>
		[StringLength(29)]
		public string TitreCourtOfficiel { get; set; }

		/// <summary>
		///     Titre moyen du cours.
		/// </summary>
		[StringLength(60)]
		public string TitreMoyen { get; set; }

		/// <summary>
		///     Titre moyen traduit du cours.
		/// </summary>
		[StringLength(60)]
		public string TitreMoyenTraduit { get; set; }

		/// <summary>
		///     Langue d'origine du cours (FR ou AN).
		/// </summary>
		[StringLength(2)]
		public string LangueOrigine { get; set; }

		/// <summary>
		///     The ponderation for the theory portion of the course
		/// </summary>
		public float PonderationTheo { get; set; }

		/// <summary>
		///     The ponderation for the lab portion of the course
		/// </summary>
		public float PonderationLab { get; set; }

		/// <summary>
		///     The long translated name of the course
		/// </summary>
		[StringLength(255)]
		public string TitreLongTraduit { get; set; }

		/// <summary>
		///     L'objet de type <see cref="CategorieCour" /> associé avec ce cours
		/// </summary>
		[ForeignKey("IDCategorieCours")]
		public virtual CategorieCour CategorieCour { get; set; }

		/// <summary>
		///     The id of the course in the ministery's registers
		/// </summary>
		public int? IDCoursMinistere { get; set; }

		/// <summary>
		///     Wether the course is taught in the college
		/// </summary>
		public bool IndicateurLocal { get; set; }

		/// <summary>
		///     La liste des groupe associé avec ce cours.
		/// </summary>
		public virtual ICollection<Groupe> Groupes { get; set; }

		/// <summary>
		///     La liste des inscriptions associé à ce cour.
		/// </summary>
		public virtual ICollection<Inscription> Inscriptions { get; set; }
	}
}