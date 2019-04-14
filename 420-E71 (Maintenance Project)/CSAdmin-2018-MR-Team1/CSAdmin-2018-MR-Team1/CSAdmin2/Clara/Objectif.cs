using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     Une compétance prise en charge par CLARA.
	/// </summary>
	[Table("ClaraObjectifs.Objectif")]
	public class Objectif
	{
		/// <summary>
		///     L'identifiant unique de cette compétance.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDObjectif { get; set; }

		/// <summary>
		///     Le numéro de la compétance.
		/// </summary>
		[StringLength(4)]
		public string Numero { get; set; }

		/// <summary>
		///     Le nom de la compétance.
		/// </summary>
		[StringLength(255)]
		public string Nom { get; set; }

		/// <summary>
		///     Aucune documentation disponible.
		/// </summary>
		public virtual ICollection<FeuilleObjectifProgramme> FeuilleObjectifProgrammes { get; set; }
	}
}