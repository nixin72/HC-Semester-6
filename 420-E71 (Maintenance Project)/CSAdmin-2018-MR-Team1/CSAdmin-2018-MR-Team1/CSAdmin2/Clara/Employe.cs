using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CSAdmin2.Model;

namespace CSAdmin2.Clara
{
	public enum EtatEmploye : byte
	{
		Active = 1,
		Inactive = 1
	}

	/// <summary>
	///     An employee within CLARA
	/// </summary>
	[Table("ClaraEmployes.Employe")]
	public class Employe
	{
		/// <summary>
		///     The unique identifier for this employee
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDEmploye { get; set; }

		/// <summary>
		///     <para>The employee's number used to login into the systems.</para>
		///     <para>by default it's the same as their HR number</para>
		/// </summary>
		[StringLength(10)]
		public string Numero { get; set; }

		/// <summary>
		///     The employee's last name
		/// </summary>
		[StringLength(60)]
		public string Nom { get; set; }

		/// <summary>
		///     The employee's first name
		/// </summary>
		[StringLength(60)]
		public string Prenom { get; set; }

		/// <summary>
		///     A more formal version of the employee's name formatted like so: "<see cref="Employe.Nom" />,
		///     <see cref="Employe.Prenom" />"
		/// </summary>
		[StringLength(123)]
		public string NomPrenom { get; set; }

		/// <summary>
		///     <para>The employee's current state:</para>
		///     <para>1=Active</para>
		///     <para>255=Inactive</para>
		/// </summary>
		public byte Etat { get; set; }

		/// <summary>
		///     The employee's coordonator roles
		/// </summary>
		public virtual ICollection<Coordonnateur> RoleDeCoordonnateurs { get; set; }

		/// <summary>
		///     The list of <see cref="EmployeGroupe" /> associated with the employee
		/// </summary>
		public virtual ICollection<EmployeGroupe> EmployeGroupes { get; set; }

		/// <summary>
		///     The <see cref="EmployeeUser" /> entry related to the student in csadmin
		/// </summary>
        
        [NotMapped]
		public EmployeeUser CSAdminUser {
            get
            {
                CSAdminContext context = new CSAdminContext();
                EmployeeUser user = context.EmployeeUsers.DefaultIfEmpty(null).FirstOrDefault( u => u.IDEmploye == IDEmploye);
                context.Dispose();
                return user;
            }
        }

		/// <summary>
		///     Fetches the list of <see cref="Groupe" />s associated with the employee
		/// </summary>
		/// <returns>The groups associated with the entity.</returns>
		public IEnumerable<Groupe> Groupes()
		{
			return EmployeGroupes.Select(eg => eg.Groupe);
		}
	}
}