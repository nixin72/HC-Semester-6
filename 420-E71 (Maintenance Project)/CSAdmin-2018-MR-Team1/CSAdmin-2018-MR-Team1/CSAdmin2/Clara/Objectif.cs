using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     Une comp�tance prise en charge par CLARA.
	/// </summary>
	[Table("ClaraObjectifs.Objectif")]
	public class Objectif
	{
		/// <summary>
		///     L'identifiant unique de cette comp�tance.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDObjectif { get; set; }

		/// <summary>
		///     Le num�ro de la comp�tance.
		/// </summary>
		[StringLength(4)]
		public string Numero { get; set; }

		/// <summary>
		///     Le nom de la comp�tance.
		/// </summary>
		[StringLength(255)]
		public string Nom { get; set; }

		/// <summary>
		///     Aucune documentation disponible.
		/// </summary>
		public virtual ICollection<FeuilleObjectifProgramme> FeuilleObjectifProgrammes { get; set; }
	}
}