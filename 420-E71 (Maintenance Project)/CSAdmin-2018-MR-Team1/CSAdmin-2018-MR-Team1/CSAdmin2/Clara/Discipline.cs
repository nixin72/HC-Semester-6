using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     A competency from CLARA.
	/// </summary>
	[Table("ClaraReference.Discipline")]
	public class Discipline
	{
		/// <summary>
		///     The unique identifyer for the competency.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDDiscipline { get; set; }

		/// <summary>
		///     The id of the departement associated with this competency.
		/// </summary>
		public int IDDepartement { get; set; }

		/// <summary>
		///     The number used to represent the competency.
		/// </summary>
		[StringLength(10)]
		public string Numero { get; set; }

		/// <summary>
		///     The number used to represent the competency.
		/// </summary>
		public bool IndicateurActif { get; set; }

		/// <summary>
		///     The medium length title of the competency
		/// </summary>
		[StringLength(100)]
		public string TitreMoyen { get; set; }

		/// <summary>
		///     Le département associé avec cette discipline.
		/// </summary>
		[ForeignKey("IDDepartement")]
		public virtual Departement Departement { get; set; }

		/// <summary>
		///     la liste des cours qui enseignent cette discipline.
		/// </summary>
		public virtual ICollection<Groupe> Groupes { get; set; }
	}
}