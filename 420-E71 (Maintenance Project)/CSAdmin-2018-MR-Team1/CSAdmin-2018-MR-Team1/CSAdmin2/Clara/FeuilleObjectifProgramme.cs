using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     No documentation available.
	/// </summary>
	[Table("ClaraObjectifs.FeuilleObjectifProgramme")]
	public class FeuilleObjectifProgramme
	{
		/// <summary>
		///     No documentation available.
		/// </summary>
		[Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDObjectif { get; set; }

		/// <summary>
		///     No documentation available.
		/// </summary>
		[Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDBrancheObjectifProgramme { get; set; }

		/// <summary>
		///     No documentation available.
		/// </summary>
		[Key, Column(Order = 2)]
		public bool IndicateurOptionnel { get; set; }

		/// <summary>
		///     No documentation available.
		/// </summary>
		[ForeignKey("IDBrancheObjectifProgramme")]
		public virtual BrancheObjectifProgramme BrancheObjectifProgramme { get; set; }

		/// <summary>
		///     No documentation available
		/// </summary>
		[ForeignKey("IDObjectif")]
		public virtual Objectif Objectif { get; set; }
	}
}