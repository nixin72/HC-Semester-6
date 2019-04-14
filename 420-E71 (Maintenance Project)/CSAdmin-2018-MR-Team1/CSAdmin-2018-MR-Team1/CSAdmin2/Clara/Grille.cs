using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     A grid from CLARA, likely to be related to schedules
	/// </summary>
	[Table("ClaraGrilles.Grille")]
	public class Grille
	{
		/// <summary>
		///     The grid is active.
		/// </summary>
		public const byte ACTIVE = 1;

		/// <summary>
		///     The grid is being developed.
		/// </summary>
		public const byte IN_DEVELOPMENT = 2;

		/// <summary>
		///     The grid is currently deactivated.
		/// </summary>
		public const byte DEACTIVATED = 255;

		/// <summary>
		///     The unique identifier for the grid.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDGrille { get; set; }

		/// <summary>
		///     The grid's assopciated number.
		/// </summary>
		[StringLength(20)]
		public string Numero { get; set; }

		/// <summary>
		///     The title of the grid
		/// </summary>
		[StringLength(255)]
		public string Titre { get; set; }

		/// <summary>
		///     <para>The state of the grid.</para>
		///     <para>1 = <see cref="ACTIVE" /></para>
		///     <para>
		///         2 = <see cref="IN_DEVELOPMENT" />: Indicates that the grid is in development by the college. An in
		///         development grid is considered to be "active" within clara. No students can apply for it during it's
		///         development.
		///     </para>
		///     <para>255 = <see cref="DEACTIVATED" /></para>
		/// </summary>
		public byte Etat { get; set; }

		/// <summary>
		///     The unique identifier of the program associated with the grid
		/// </summary>
		public int IDProgramme { get; set; }

		/// <summary>
		///     The id of the organisational unit associated with this grid
		/// </summary>
		public int IDUniteOrg { get; set; }

		/// <summary>
		///     The semester at which the grid starts.
		/// </summary>
		public short? AnSessionDebug { get; set; }

		/// <summary>
		///     The semester at which the grid ends.
		/// </summary>
		public short? AnSessionFin { get; set; }

		/// <summary>
		///     No documentation available.
		/// </summary>
		public int IDTypeSanction { get; set; }

		/// <summary>
		///     The program associated with this grid.
		/// </summary>
		[ForeignKey("IDProgramme")]
		public virtual Programme Programme { get; set; }

		/// <summary>
		///     The organisational unit associated with the grid
		/// </summary>
		[ForeignKey("IDUniteOrg")]
		public virtual UniteOrg UniteOrg { get; set; }
	}
}