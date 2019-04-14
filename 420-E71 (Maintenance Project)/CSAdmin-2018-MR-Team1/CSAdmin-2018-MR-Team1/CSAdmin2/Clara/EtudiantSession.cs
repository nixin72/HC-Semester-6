using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     A semester record within CLARA.
	/// </summary>
	[Table("ClaraEtudiants.EtudiantSession")]
	public class EtudiantSession
	{
		/// <summary>
		///     The semester report is deleted
		/// </summary>
		public const byte DELETED = 0;

		/// <summary>
		///     The semester report is active
		/// </summary>
		public const byte ACTIVE = 1;

		/// <summary>
		///     Used for part-time frequentations
		/// </summary>
		public const string PART_TIME_FREQUENTATION = "TPA";

		/// <summary>
		///     used for full-time frequentations
		/// </summary>
		public const string FULL_TIME_FREQUENTATION = "TPL";

		/// <summary>
		///     Used for coop only frequentations
		/// </summary>
		public const string COOP_FREQUENTATION = "ATE";

		/// <summary>
		///     User when a student does not frequent the school during the semester
		/// </summary>
		public const string NO_FREQUENTATION = "SO";

		/// <summary>
		///     The id of the semester record.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDEtudiantSession { get; set; }

		/// <summary>
		///     The student id associated with the student.
		/// </summary>
		public int IDEtudiant { get; set; }

		/// <summary>
		///     The program id associated with the record.
		/// </summary>
		/// <see cref="Clara.Programme" />
		/// <see cref="Programme" />
		public int IDProgramme { get; set; }

		/// <summary>
		///     The organisational unit id associated with the record.
		/// </summary>
		/// <see cref="Clara.UniteOrg" />
		/// <see cref="UniteOrg" />
		public int IDUniteOrg { get; set; }

		/// <summary>
		///     Aucune documentation disponible a ce moment.
		/// </summary>
		public int IDAdmission { get; set; }

		/// <summary>
		///     The record's year session associated with the student
		/// </summary>
		public short AnSession { get; set; }

		/// <summary>
		///     <para>Indicates the state of the record</para>
		///     <para>It may be deleted or active</para>
		/// </summary>
		/// <seealso cref="EtatEtudiantSession" />
		public byte Etat { get; set; }

		/// <summary>
		///     SPE (Session dans le Programme d'Étude) de l'étudiant à cette session.
		/// </summary>
		public byte SPE { get; set; }


		/// <summary>
		///     <para>
		///         The frequention type indicates the amount of time a students spends on his studies during a given semester or
		///         year at school
		///     </para>
		///     <para>TPA = Part-time, see <see cref="PART_TIME_FREQUENTATION" /></para>
		///     <para>TPL = Full-time, see <seealso cref="FULL_TIME_FREQUENTATION" /></para>
		///     <para>
		///         ATE = Only co-op (Alternance Travail Étude) stages are associated with this semester record, see
		///         <see cref="COOP_FREQUENTATION" />
		///     </para>
		///     <para>
		///         SO = No inscriptions and Co-Op stages associated with this semester record, see
		///         <see cref="NO_FREQUENTATION" />
		///     </para>
		/// </summary>
		/// <seealso cref="PART_TIME_FREQUENTATION" />
		/// <seealso cref="FULL_TIME_FREQUENTATION" />
		/// <seealso cref="COOP_FREQUENTATION" />
		/// <seealso cref="NO_FREQUENTATION" />
		public string TypeFrequentation { get; set; }

		/// <summary>
		///     No documentation available.
		/// </summary>
		public int? IDCohorteFC { get; set; }

		/// <summary>
		///     The record's creation date.
		/// </summary>
		public DateTime DateHeureCreation { get; set; }

		/// <summary>
		///     The student associated with this student
		/// </summary>
		/// <seealso cref="Clara.Etudiant" />
		[ForeignKey("IDEtudiant")]
		public virtual Etudiant Etudiant { get; set; }

		/// <summary>
		///     The program associated associated with the semester record
		/// </summary>
		/// <seealso cref="Clara.Programme" />
		[ForeignKey("IDProgramme")]
		public virtual Programme Programme { get; set; }

		/// <summary>
		///     The organisational unit associated with the semester record
		/// </summary>
		/// <seealso cref="Clara.UniteOrg" />
		[ForeignKey("IDUniteOrg")]
		public virtual UniteOrg UniteOrg { get; set; }

		/// <summary>
		///     The course inscriptions associated with the semester record
		/// </summary>
		/// <seealso cref="Clara.Inscription" />
		public virtual ICollection<Inscription> Inscriptions { get; set; }

		/// <summary>
		///     Gets all past and ongoing groups for the students
		/// </summary>
		/// <returns>The past and ongoing groups for this semester record</returns>
		/// <seealso cref="Clara.Groupe" />
		public IEnumerable<Groupe> GetGroups()
		{
			return
				from Inscription i in Inscriptions
				where
					i.Etat == Inscription.PASSED ||
					i.Etat == Inscription.ONGOING
				select
					i.Groupe;
		}
	}
}