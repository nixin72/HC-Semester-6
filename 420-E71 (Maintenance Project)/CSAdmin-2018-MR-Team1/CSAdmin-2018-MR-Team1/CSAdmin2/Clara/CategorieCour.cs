using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     A course category from clara
	/// </summary>
	[Table("ClaraReference.CategorieCours")]
	public class CategorieCour
	{
		/// <summary>
		///     The unique identifyer for the course category.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDCategorieCours { get; set; }

		/// <summary>
		///     The number associated with this course category.
		/// </summary>
		[StringLength(3)]
		public string Numero { get; set; }

		/// <summary>
		///     The title of the category.
		/// </summary>
		[StringLength(50)]
		public string Titre { get; set; }

		/// <summary>
		///     The courses who are part of this category.
		/// </summary>
		public virtual ICollection<Cour> Cours { get; set; }
	}
}