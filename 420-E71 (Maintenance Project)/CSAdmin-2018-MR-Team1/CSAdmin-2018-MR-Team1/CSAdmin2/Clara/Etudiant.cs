using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CSAdmin2.Model;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     A student within CLARA
	/// </summary>
	[Table("ClaraEtudiants.Etudiant")]
	public class Etudiant
	{
		/// <summary>
		///     The unique identifier for the student.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ForeignKey("CSAdminStudentUser")]
		public int IDEtudiant { get; set; }

		/// <summary>
		///     <para>The 7 digit number used as the student's number.</para>
		///     <para>Do not use this number as a unique identifier, it isn't unique, use <see cref="Etudiant.IDEtudiant" />.</para>
		/// </summary>
		[StringLength(7)]
		public string Numero7 { get; set; }

		/// <summary>
		///     The lastname of the student.
		/// </summary>
		[StringLength(40)]
		public string Nom { get; set; }


		/// <summary>
		///     The first name of the student.
		/// </summary>
		[StringLength(30)]
		public string Prenom { get; set; }

		/// <summary>
		///     The studen't birth date.
		/// </summary>
		public DateTime DateNaissance { get; set; }

		/// <summary>
		///     <para>The student's gender.</para>
		///     <para>M = Male</para>
		///     <para>F = Female</para>
		/// </summary>
		[StringLength(1)]
		public string Sexe { get; set; }

		/// <summary>
		///     The student's citizenship(s)
		/// </summary>
		public virtual ICollection<Citoyennete> Citoyentes { get; set; }

		/// <summary>
		///     The student's essional records
		/// </summary>
		public virtual ICollection<EtudiantSession> EtudiantSessions { get; set; }

		/// <summary>
		///     The <see cref="StudentUser" /> entry related to the student in csadmin
		/// </summary>
		public virtual User CSAdminStudentUser { get; set; }

		/// <summary>
		///     Fetches the groups associated with a given student during a semester.
		/// </summary>
		/// <param name="semester"></param>
		/// <returns></returns>
		public virtual IEnumerable<Groupe> GetCoursesForSemester(short semester)
		{
			EtudiantSession sess =
				EtudiantSessions.FirstOrDefault(es => es.AnSession == semester && es.Etat == EtudiantSession.ACTIVE);
			if (sess == null)
			{
				throw new InvalidOperationException(
					$"The student id ({IDEtudiant}) has no semester record during the {semester} semester");
			}

			return sess.GetGroups();
		}
	}
}