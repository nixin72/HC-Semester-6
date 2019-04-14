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
		///     Cette information sert de valeur par d�faut � la cr�ation des grilles de cours. Elle permet de cr�er des grilles
		///     plus rapidement. R�gle g�n�rale, la cat�gorie du cours demeure la m�me dans une grille de cours, mais
		///     l''utilisateur pourra la changer si d�sir�.
		/// </summary>
		public int IDCategorieCours { get; set; }

		/// <summary>
		///     <para>
		///         Num�ro unique du cours interne � Clara. Ce num�ro est destin� � permettre une codification interne/maison des
		///         cours SOBEC.
		///     </para>
		///     <para>
		///         Dans le cas d'un cours SOBEC ou SOBEC en attente d'approbation, cette zone peut contenir un alias, soit un
		///         num�ro de cours diff�rent du num�ro de cours SOBEC pour une utilisation strictement interne au coll�ge. Dans le
		///         cas d'un cours Clara, ce champ doit obligatoirement contenir la m�me valeur que la zone Numero.
		///     </para>
		///     <para>
		///         Si aucun alias diff�rent du num�ro de cours "normal" n'est n�cessaire, ce champ devrait contenir la m�me
		///         valeur que le champ Numero.
		///     </para>
		///     <para>
		///         Il ne faut pas confondre le num�ro alias avec le num�ro de cours minist�re (SOBEC). Le num�ro de cours
		///         minist�re ne peut �tre utilis� que pour un cours Clara qui repr�sente un cours SOBEC. Il peut arriver que
		///         plusieurs cours Clara r�f�rent au m�me cours SOBEC(par exemple les cours d'�ducation physique). Un alias sert
		///         uniquement � cr�er une codification interne des cours d�finis � SOBEC.
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
		///         Num�ro du cours. Ce num�ro de cours permet de faire r�f�rence au cours SOBEC si un num�ro de cours alias
		///         diff�rent du num�ro de cours officiel a �t� sp�cifi�.
		///     </para>
		///     <para>
		///         Pour un cours SOBEC ou SOBEC en attente d'importation, cette zone contient le num�ro de cours SOBEC. Pour un
		///         cours Clara, cette zone contient toujours le num�ro de cours Clara.
		///     </para>
		///     <para>
		///         Ce num�ro de cours sera utilis� entre autres lors des transmissions au Minist�re et affich� sur le bulletin
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
		///     L'objet de type <see cref="CategorieCour" /> associ� avec ce cours
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
		///     La liste des groupe associ� avec ce cours.
		/// </summary>
		public virtual ICollection<Groupe> Groupes { get; set; }

		/// <summary>
		///     La liste des inscriptions associ� � ce cour.
		/// </summary>
		public virtual ICollection<Inscription> Inscriptions { get; set; }
	}
}