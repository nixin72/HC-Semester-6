using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     A group within CLARA.
	/// </summary>
	[Table("ClaraGroupes.Groupe")]
	public class Groupe
	{
		/// <summary>
		///     The groupe is open
		/// </summary>
		public const byte Open = 0;

		/// <summary>
		///     The groupe is closed, but may re-open again
		/// </summary>
		public const byte NotOpen = 1;

		/// <summary>
		///     The groupe is permanently closed
		/// </summary>
		public const byte Closed = 2;

		/// <summary>
		///     The group's unique id.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDGroupe { get; set; }

		/// <summary>
		///     The course id associated with the group.
		/// </summary>
		public int IDCours { get; set; }

		/// <summary>
		///     The competance id for the course, null if it's university related.
		/// </summary>
		public int IDDiscipline { get; set; }

		/// <summary>
		///     The organisational id associated with the group.
		/// </summary>
		public int IDUniteOrg { get; set; }

		/// <summary>
		///     No documentation available
		/// </summary>
		public int? IDCohorteFC { get; set; }

		/// <summary>
		///     The group number.
		/// </summary>
		public int Numero { get; set; }

		/// <summary>
		///     The group number used during evaluations, also used during transmissions.
		/// </summary>
		public int NumeroGroupeEvaluation { get; set; }

		/// <summary>
		///     The year semester associated with the group
		/// </summary>
		public short AnSession { get; set; }

		/// <summary>
		///     <para>The state of the group</para>
		///     <para>1 = Open</para>
		///     <para>2 = temporarly closed</para>
		///     <para>3 = Closed</para>
		/// </summary>
		public byte Etat { get; set; }

		/// <summary>
		///     The course associated with the group.
		/// </summary>
		/// <seealso cref="Clara.Cour" />
		[ForeignKey("IDCours")]
		public virtual Cour Cours { get; set; }

		/// <summary>
		///     The competance taught in the group.
		/// </summary>
		/// <seealso cref="Clara.Discipline" />
		[ForeignKey("IDDiscipline")]
		public virtual Discipline Discipline { get; set; }

		/// <summary>
		///     The organisational unit associated with the group.
		/// </summary>
		/// <seealso cref="Clara.UniteOrg" />
		[ForeignKey("IDUniteOrg")]
		public virtual UniteOrg UniteOrg { get; set; }

		/// <summary>
		///     The employee entries associated with the group.
		/// </summary>
		public virtual ICollection<EmployeGroupe> EmployeGroupes { get; set; }
	}
}