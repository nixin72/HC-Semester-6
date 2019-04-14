using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     An engagement status type from clara CLARA.
	/// </summary>
	[Table("ClaraReference.StatutEngagement")]
	public class StatutEngagement
	{
		/// <summary>
		///     The unique id associated with the engagement status type.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDStatutEngagement { get; set; }

		/// <summary>
		///     The number associated with the engagement status type.
		/// </summary>
		[StringLength(10)]
		public string Numero { get; set; }

		/// <summary>
		///     The short title of the of engagement status type.
		/// </summary>
		[StringLength(20)]
		public string TitreCourt { get; set; }

		/// <summary>
		///     The long title of the of engagement status type.
		/// </summary>
		[StringLength(255)]
		public string TitreLong { get; set; }

		/// <summary>
		///     The medium title of the engagement status type.
		/// </summary>
		[StringLength(100)]
		public string TitreMoyen { get; set; }

		/// <summary>
		///     No documentation available.
		/// </summary>
		public byte TypeCalculETC { get; set; }

		/// <summary>
		///     The list of <see cref="Clara.EmployeStatutEngagement" /> associated with this engagement status type.
		/// </summary>
		public virtual ICollection<EmployeStatutEngagement> EmployeStatutEngagements { get; set; }
	}
}