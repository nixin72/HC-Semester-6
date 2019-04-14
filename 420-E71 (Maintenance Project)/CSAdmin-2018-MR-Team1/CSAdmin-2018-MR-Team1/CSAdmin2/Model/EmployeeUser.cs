using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     An associative entity that relates csadmin users to employees within CLARA.
	/// </summary>
	[Table("Users.EmployeeUser")]
	public class EmployeeUser
	{
		protected int _IDUser { get; set; }
		protected int _IDEmploye { get; set; }

		/// <summary>
		///     The CSAdmin user id related to this user.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDUser
		{
			get { return _IDUser; }
			set { _IDUser = value; }
		}

		/// <summary>
		///     The CLARA employee id related to this user.
		/// </summary>
		public int IDEmploye
		{
			get { return _IDEmploye; }
			set { _IDEmploye = value; }
		}

		/// <summary>
		///     The CSAdmin <see cref="Model.User" /> object associated with this user.
		/// </summary>
		[ForeignKey("IDUser")]
		public virtual User User { get; set; }


		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1} }}",
					StringificationUtility.Stringify("IDUser", IDUser),
					StringificationUtility.Stringify("IDEmploye", IDEmploye)
				);
		}
	}
}