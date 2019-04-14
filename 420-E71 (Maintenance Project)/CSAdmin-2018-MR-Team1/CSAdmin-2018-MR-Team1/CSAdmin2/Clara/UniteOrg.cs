using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     An organisational unit from CLARA.
	/// </summary>
	[Table("ClaraReference.UniteOrg")]
	public class UniteOrg
	{
		/// <summary>
		///     The unique identifier associated with the organisational unit.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDUniteOrg { get; set; }

		/// <summary>
		///     <para>The number associated with the organisational unit.</para>
		///     <para>
		///         If the number contains 8 numbers ([6 numbers for the organisation]-[academic service id]), the unit isn't
		///         local
		///     </para>
		/// </summary>
		[StringLength(10)]
		public string Numero { get; set; }

		/// <summary>
		///     The long title of the organisational unit.
		/// </summary>
		[StringLength(255)]
		public string TitreLong { get; set; }

		/// <summary>
		///     The list of coordonators associated with the organisational unit.
		/// </summary>
		public virtual ICollection<Coordonnateur> Coordonnateurs { get; set; }

		/// <summary>
		///     The list of groups associated with the organisational unit.
		/// </summary>
		public virtual ICollection<Groupe> Groupes { get; set; }

		/// <summary>
		///     The list of sessional records related to the organisational unit.
		/// </summary>
		public virtual ICollection<EtudiantSession> EtudiantSession { get; set; }

		/// <summary>
		///     The list of schedules related to the organisational unit.
		/// </summary>
		public virtual ICollection<Grille> Grilles { get; set; }

		/// <summary>
		///     The list of course inscriptions related to the organisational unit.
		/// </summary>
		public virtual ICollection<Inscription> Inscriptions { get; set; }
	}
}