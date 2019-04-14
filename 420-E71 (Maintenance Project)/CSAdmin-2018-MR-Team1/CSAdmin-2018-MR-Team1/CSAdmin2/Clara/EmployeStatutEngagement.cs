using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Clara
{
	/// <summary>
	///     ETC maximal pour les statut d'engagements temps partiel (type de calcul de l'ETC = 2).
	/// </summary>
	[Table("ClaraEmployes.EmployeStatutEngagement")]
	public class EmployeStatutEngagement
	{
		/// <summary>
		///     L'identifiant unique pour cet objet.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDEmployeStatutEngagement { get; set; }

		/// <summary>
		///     L'employe li� a cet objet.
		/// </summary>
		public int IDEmploye { get; set; }

		/// <summary>
		///     L'ann�e-session o� a debut� cet objet.
		/// </summary>
		public short? AnSessionDebut { get; set; }

		/// <summary>
		///     L'ann�e-session o� a finit cet objet.
		/// </summary>
		public short? AnSessionFin { get; set; }

		/// <summary>
		///     L'identifiant unique du statut d'engagement associ� a cet objet.
		/// </summary>
		public int? IDStatutEngagement { get; set; }

		/// <summary>
		///     L'employ� associ� a cet objet
		/// </summary>
		[ForeignKey("IDEmploye")]
		public virtual Employe Employe { get; set; }

		/// <summary>
		///     Le statut d'engagement associ� a cet objet
		/// </summary>
		[ForeignKey("IDStatutEngagement")]
		public virtual StatutEngagement StatutEngagement { get; set; }
	}
}