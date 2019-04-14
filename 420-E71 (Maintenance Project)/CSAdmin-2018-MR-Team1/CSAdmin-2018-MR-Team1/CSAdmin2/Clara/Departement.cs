using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     A department from CLARA.
	/// </summary>
	[Table("ClaraReference.Departement")]
	public class Departement
	{
		/// <summary>
		///     The unique identifier for the department.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDDepartement { get; set; }

		/// <summary>
		///     The number associated with the department.
		/// </summary>
		[StringLength(10)]
		public string Numero { get; set; }

		/// <summary>
		///     The medium length title of the department.
		/// </summary>
		[StringLength(100)]
		public string TitreMoyen { get; set; }

		/// <summary>
		///     The long title of the department.
		/// </summary>
		[StringLength(255)]
		public string TitreLong { get; set; }

		/// <summary>
		///     Wether the department is active.
		/// </summary>
		public bool IndicateurActif { get; set; }

		/// <summary>
		///     The list of coordinators associated with the department.
		/// </summary>
		public virtual ICollection<Coordonnateur> Coordonnateurs { get; set; }

		/// <summary>
		///     The list of competencies related to the department
		/// </summary>
		public virtual ICollection<Discipline> Discipline { get; set; }
	}
}