using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     An associative entity used to associate users with specific roles.
	/// </summary>
	/// <seealso cref="Model.User" />
	/// <seealso cref="Model.Role" />
	[Table("Applications.UserRole")]
	public class UserRole
	{
		protected int _IDUserRole { get; set; }
		protected int _IDUser { get; set; }
		protected int _IDRole { get; set; }
		protected bool _IsActive { get; set; }

		/// <summary>
		///     The id of the user role associative entity.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDUserRole
		{
			get { return _IDUserRole; }
			set { _IDUserRole = value; }
		}

		/// <summary>
		///     The id of the user who has access to this role.
		/// </summary>
		public int IDUser
		{
			get { return _IDUser; }
			set { _IDUser = value; }
		}

		/// <summary>
		///     The id of the role the user has access to.
		/// </summary>
		public int IDRole
		{
			get { return _IDRole; }
			set { _IDRole = value; }
		}

		/// <summary>
		///     Wether or not the user still has access to the role.
		/// </summary>
		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}

		/// <summary>
		///     The <see cref="Model.Role" /> object related to this entity.
		/// </summary>
		public virtual Role Role { get; set; }

		/// <summary>
		///     The <see cref="Model.User" /> object related to this entity.
		/// </summary>
		public virtual User User { get; set; }

		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1}, {2}, {3} }}",
					StringificationUtility.Stringify("IDUser", IDUserRole),
					StringificationUtility.Stringify("IDUser", IDUser),
					StringificationUtility.Stringify("IDRole", IDRole),
					StringificationUtility.Stringify("IsActive", IsActive)
				);
		}
	}
}