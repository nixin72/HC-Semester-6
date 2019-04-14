using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     Un coordonnateur pris en charge par clara
	/// </summary>
	[Table("ClaraEmployes.Coordonnateur")]
	public class Coordonnateur
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDCoordonnateur { get; set; }

		/// <summary>
		///     L'identifiant unique de l'employ� associ� avec ce coordonnateur.
		/// </summary>
		public int IDEmploye { get; set; }

		/// <summary>
		///     L'identifiant unique du departement associ� avec ce coordonnateur.
		/// </summary>
		public int? IDDepartement { get; set; }

		/// <summary>
		///     L'identifiant unique de l'unit� independante associ� avec ce coordonnateur.
		/// </summary>
		public int? IDUniteOrg { get; set; }

		/// <summary>
		///     Le numero de la session o� le coordonnateur a debut� son role.
		/// </summary>
		public short? AnSessionDebut { get; set; }

		/// <summary>
		///     Le numero de la session o� le coordonnateur a terminer son role.
		/// </summary>
		public short? AnSessionFin { get; set; }


		/// <summary>
		///     L'objet de type <see cref="Clara.Employe" /> associ� avec le coordonnateur.
		/// </summary>
		[ForeignKey("IDEmploye")]
		public virtual Employe Employe { get; set; }

		/// <summary>
		///     l'objet de type <see cref="Clara.Departement" /> associ� avec le coordonateur.
		/// </summary>
		[ForeignKey("IDDepartement")]
		public virtual Departement Departement { get; set; }

		/// <summary>
		///     L'objet de type <see cref="Clara.UniteOrg" /> associ� avec le coordonnateur.
		/// </summary>
		[ForeignKey("IDUniteOrg")]
		public virtual UniteOrg UniteOrg { get; set; }
	}
}