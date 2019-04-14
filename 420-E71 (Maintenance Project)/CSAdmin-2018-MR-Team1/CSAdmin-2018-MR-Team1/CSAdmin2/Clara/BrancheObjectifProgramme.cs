using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     No documentation as of yet
	/// </summary>
	[Table("ClaraObjectifs.BrancheObjectifProgramme")]
	public class BrancheObjectifProgramme
	{
		/// <summary>
		///     No documentation as of yet
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDBrancheObjectifProgramme { get; set; }


		/// <summary>
		///     La branche parente de cette branche
		/// </summary>
		public int? IDBrancheObjectifProgrammeParent { get; set; }


		/// <summary>
		///     The program associated with the program
		/// </summary>
		public int IDProgramme { get; set; }


		/// <summary>
		///     The <see cref="clara.Programme" /> associated with the branch.
		/// </summary>
		[ForeignKey("IDProgramme")]
		public virtual Programme Programme { get; set; }

		/// <summary>
		///     No documentation available.
		/// </summary>
		[ForeignKey("IDBrancheObjectifProgrammeParent")]
		public virtual BrancheObjectifProgramme BrancheParente { get; set; }

		/// <summary>
		///     No documentation available.
		/// </summary>
		public virtual ICollection<FeuilleObjectifProgramme> FeuilleObjectifProgrammes { get; set; }
	}
}