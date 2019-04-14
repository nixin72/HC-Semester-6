using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     The state of an inscription
	/// </summary>
	public enum EtatInscription : byte
	{
		/// <summary>
		/// The inscription is deleted
		/// </summary>
	}

	/// <summary>
	///     Une demande d'inscription pour un cour prise en charge par CLARA.
	/// </summary>
	[Table("ClaraInscriptions.Inscription")]
	public class Inscription
	{
		/// <summary>
		///     The inscription has been deleted
		/// </summary>
		public const byte DELETED = 0;

		/// <summary>
		///     The inscription has passed
		/// </summary>
		public const byte PASSED = 1;

		/// <summary>
		///     The inscription is ongoing
		/// </summary>
		public const byte ONGOING = 2;

		/// <summary>
		///     The inscription is available in the student's course choices
		/// </summary>
		public const byte IN_COURSE_CHOICES = 3;

		/// <summary>
		///     The inscriptions is in the list of courses to do for the student's diploma
		/// </summary>
		public const byte TO_DO = 4;

		/// <summary>
		///     L'identifiant unique associé a la demande d'inscription.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDInscription { get; set; }

		/// <summary>
		///     L'identifiant unique du dossier sessionnel associé à la demande d'inscription.
		/// </summary>
		public int IDEtudiantSession { get; set; }

		/// <summary>
		///     L'identifiant unique du cour associé la à demande d'inscription.
		/// </summary>
		public int IDCours { get; set; }

		/// <summary>
		///     L'identifiant unique du groupe associé la à demande d'inscription.
		/// </summary>
		public int? IDGroupe { get; set; }

		/// <summary>
		///     L'identifiant unique de l'unité organisationelle associé à la demande d'inscription.
		/// </summary>
		public int IDUniteOrg { get; set; }

		/// <summary>
		///     <para>État de l''inscription:</para>
		///     <para>0 = Supprimé</para>
		///     <para>1 = Réussi</para>
		///     <para>2 = En cours</para>
		///     <para>3 = En choix de cours</para>
		///     <para>4 = À faire</para>
		/// </summary>
		public byte? Etat { get; set; }

		/// <summary>
		///     <para>
		///         Le type de reconnaissance d''acquis de formation correspond à une évaluation formelle des acquis scolaires ou
		///         extrascolaires d''un candidat.
		///     </para>
		///     <para>CO = Cours complet</para>
		///     <para>EE = Extra-scol.éval.</para>
		///     <para>FM = Formation manquante</para>
		///     <para>RC = Récupération de cours</para>
		///     <para>SO = Sans objet</para>
		/// </summary>
		[StringLength(2)]
		public string TypeRAF { get; set; }

		/// <summary>
		///     <para>
		///         La source de financement collégiale représente la source de financement déclarée par un établissement
		///         d'enseignement, pour une inscription à un cours suivi par un élève.
		///     </para>
		///     <para>10 = Ministère de l'éducation : Le financement provient du ministère de l'Éducation.</para>
		///     <para>11 = Financé via priorités MELS.</para>
		///     <para>12 = Financé via Env. reg. EQ.</para>
		///     <para>13 = Enveloppe régionale interordres.</para>
		///     <para>14 = Enveloppe temps partiel volet 2.</para>
		///     <para>15 = Danse-ballet financé MELS.</para>
		///     <para>16 = Métier d'arts financé MELS.</para>
		///     <para>17 = Clientèle Inuit financée MELS.</para>
		///     <para>
		///         30 = Emploi-Québec - Temps complet : Le financement provient du ministère dont Emploi-Québec relève pour un
		///         élève inscrit dans un programme défini à temps complet (24 heures et plus).
		///     </para>
		///     <para>
		///         31 = Emploi-Québec - Temps partiel : Le financement provient du ministère dont Emploi-Québec relève pour un
		///         élève inscrit dans un programme défini à temps partiel (moins de 24 heures).
		///     </para>
		///     <para>
		///         32 = Emploi-Québec - Place-élève : Le financement provient du ministère dont Emploi-Québec relève pour
		///         place-élève.
		///     </para>
		///     <para>40 = Autre ministère que MEQ et EQ : Le financement provient d'un ministère autre que MEQ et Emploi-Québec.</para>
		///     <para>50 = Assumé par l'élève : Le financement est entièrement assumé par l'élève.</para>
		///     <para>90 = Entreprise privée : Le financement provient d'une entreprise privée.</para>
		///     <para>00 = Sans financement : Sans financement.</para>
		/// </summary>
		[StringLength(2)]
		public string SourceFinancement { get; set; }

		/// <summary>
		///     La date et heure à laquelle la demande d'inscription a été créée.
		/// </summary>
		public DateTime DateHeureCreation { get; set; }

		/// <summary>
		///     Le dossier sessionnel associé à la demande d'inscription
		/// </summary>
		[ForeignKey("IDEtudiantSession")]
		public virtual EtudiantSession EtudiantSession { get; set; }

		/// <summary>
		///     Le cour associé à la demande d'inscription
		/// </summary>
		[ForeignKey("IDCours")]
		public virtual Cour Cour { get; set; }

		/// <summary>
		///     Le groupe associé à la demande d'inscription
		/// </summary>
		[ForeignKey("IDGroupe")]
		public virtual Groupe Groupe { get; set; }

		/// <summary>
		///     L'unité organisationelle associé a la demande d'inscription
		/// </summary>
		[ForeignKey("IDUniteOrg")]
		public virtual UniteOrg UniteOrg { get; set; }
	}
}