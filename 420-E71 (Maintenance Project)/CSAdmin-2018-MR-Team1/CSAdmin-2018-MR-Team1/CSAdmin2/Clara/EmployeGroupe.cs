using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     An group and employe association from CLARA
	/// </summary>
	[Table("ClaraGroupes.EmployeGroupe")]
	public class EmployeGroupe
	{
		/// <summary>
		///     The associative entity's id
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDEmployeGroupe { get; set; }

		/// <summary>
		///     The id of the employee.
		/// </summary>
		public int IDEmploye { get; set; }

		/// <summary>
		///     The employee id of the group.
		/// </summary>
		public int IDGroupe { get; set; }

		/// <summary>
		///     The employee associated with the entity
		/// </summary>
		[ForeignKey("IDEmploye")]
		public virtual Employe Employe { get; set; }

		/// <summary>
		///     The groupe associated with the entity.
		/// </summary>
		[ForeignKey("IDGroupe")]
		public virtual Groupe Groupe { get; set; }
	}
}