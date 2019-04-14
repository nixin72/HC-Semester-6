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
		///     L'identifiant unique associ� a la demande d'inscription.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDInscription { get; set; }

		/// <summary>
		///     L'identifiant unique du dossier sessionnel associ� � la demande d'inscription.
		/// </summary>
		public int IDEtudiantSession { get; set; }

		/// <summary>
		///     L'identifiant unique du cour associ� la � demande d'inscription.
		/// </summary>
		public int IDCours { get; set; }

		/// <summary>
		///     L'identifiant unique du groupe associ� la � demande d'inscription.
		/// </summary>
		public int? IDGroupe { get; set; }

		/// <summary>
		///     L'identifiant unique de l'unit� organisationelle associ� � la demande d'inscription.
		/// </summary>
		public int IDUniteOrg { get; set; }

		/// <summary>
		///     <para>�tat de l''inscription:</para>
		///     <para>0 = Supprim�</para>
		///     <para>1 = R�ussi</para>
		///     <para>2 = En cours</para>
		///     <para>3 = En choix de cours</para>
		///     <para>4 = � faire</para>
		/// </summary>
		public byte? Etat { get; set; }

		/// <summary>
		///     <para>
		///         Le type de reconnaissance d''acquis de formation correspond � une �valuation formelle des acquis scolaires ou
		///         extrascolaires d''un candidat.
		///     </para>
		///     <para>CO = Cours complet</para>
		///     <para>EE = Extra-scol.�val.</para>
		///     <para>FM = Formation manquante</para>
		///     <para>RC = R�cup�ration de cours</para>
		///     <para>SO = Sans objet</para>
		/// </summary>
		[StringLength(2)]
		public string TypeRAF { get; set; }

		/// <summary>
		///     <para>
		///         La source de financement coll�giale repr�sente la source de financement d�clar�e par un �tablissement
		///         d'enseignement, pour une inscription � un cours suivi par un �l�ve.
		///     </para>
		///     <para>10 = Minist�re de l'�ducation : Le financement provient du minist�re de l'�ducation.</para>
		///     <para>11 = Financ� via priorit�s MELS.</para>
		///     <para>12 = Financ� via Env. reg. EQ.</para>
		///     <para>13 = Enveloppe r�gionale interordres.</para>
		///     <para>14 = Enveloppe temps partiel volet 2.</para>
		///     <para>15 = Danse-ballet financ� MELS.</para>
		///     <para>16 = M�tier d'arts financ� MELS.</para>
		///     <para>17 = Client�le Inuit financ�e MELS.</para>
		///     <para>
		///         30 = Emploi-Qu�bec - Temps complet : Le financement provient du minist�re dont Emploi-Qu�bec rel�ve pour un
		///         �l�ve inscrit dans un programme d�fini � temps complet (24 heures et plus).
		///     </para>
		///     <para>
		///         31 = Emploi-Qu�bec - Temps partiel : Le financement provient du minist�re dont Emploi-Qu�bec rel�ve pour un
		///         �l�ve inscrit dans un programme d�fini � temps partiel (moins de 24 heures).
		///     </para>
		///     <para>
		///         32 = Emploi-Qu�bec - Place-�l�ve : Le financement provient du minist�re dont Emploi-Qu�bec rel�ve pour
		///         place-�l�ve.
		///     </para>
		///     <para>40 = Autre minist�re que MEQ et EQ : Le financement provient d'un minist�re autre que MEQ et Emploi-Qu�bec.</para>
		///     <para>50 = Assum� par l'�l�ve : Le financement est enti�rement assum� par l'�l�ve.</para>
		///     <para>90 = Entreprise priv�e : Le financement provient d'une entreprise priv�e.</para>
		///     <para>00 = Sans financement : Sans financement.</para>
		/// </summary>
		[StringLength(2)]
		public string SourceFinancement { get; set; }

		/// <summary>
		///     La date et heure � laquelle la demande d'inscription a �t� cr��e.
		/// </summary>
		public DateTime DateHeureCreation { get; set; }

		/// <summary>
		///     Le dossier sessionnel associ� � la demande d'inscription
		/// </summary>
		[ForeignKey("IDEtudiantSession")]
		public virtual EtudiantSession EtudiantSession { get; set; }

		/// <summary>
		///     Le cour associ� � la demande d'inscription
		/// </summary>
		[ForeignKey("IDCours")]
		public virtual Cour Cour { get; set; }

		/// <summary>
		///     Le groupe associ� � la demande d'inscription
		/// </summary>
		[ForeignKey("IDGroupe")]
		public virtual Groupe Groupe { get; set; }

		/// <summary>
		///     L'unit� organisationelle associ� a la demande d'inscription
		/// </summary>
		[ForeignKey("IDUniteOrg")]
		public virtual UniteOrg UniteOrg { get; set; }
	}
}