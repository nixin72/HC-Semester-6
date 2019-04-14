using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CSAdmin2.Model
{
	/// <summary>
	///     A role used to categorise and identify users.
	/// </summary>
	[Table("Applications.Role")]
	public class Role
	{
		/// <summary>
		///     The maximum length of the <see cref="Code" /> field.
		/// </summary>
		internal static readonly int _MaxLengthCode;

		/// <summary>
		///     The maximum length of the <see cref="Description" /> field.
		/// </summary>
		internal static readonly int _MaxLengthDescription;

		/// <summary>
		///     Initialises the static read only static constants.
		/// </summary>
		static Role()
		{
			_MaxLengthCode = ValidationUtility.GetMaxLengthOfField(typeof(Role), "Code");
			_MaxLengthDescription = ValidationUtility.GetMaxLengthOfField(typeof(Role), "Description");
		}

		protected int _IDRole { get; set; }
		protected string _Code { get; set; }
		protected string _Description { get; set; }

		/// <summary>
		///     The id for the role.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDRole
		{
			get { return _IDRole; }
			set { _IDRole = value; }
		}

		/// <summary>
		///     The code used for the role.
		/// </summary>
		[Required, StringLength(3)]
		public string Code
		{
			get { return _Code; }
			set
			{
				ValidationUtility.ValidateRequiredString(value, _MaxLengthCode, "Code");
				_Code = value;
			}
		}

		/// <summary>
		///     A short description of the role.
		/// </summary>
		[Required, StringLength(50)]
		public string Description
		{
			get { return _Description; }
			set
			{
				ValidationUtility.ValidateRequiredString(value, _MaxLengthDescription, "Description");
				_Description = value;
			}
		}


		/// <summary>
		///     The list of applications this role has access to in the form of an <see cref="Model.ApplicationRole" /> associative
		///     entity.
		/// </summary>
		public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }

		/// <summary>
		///     The list of users who have access to this role in the form of a <see cref="Model.UserRole" /> associative entity.
		/// </summary>
		public virtual ICollection<UserRole> UserRoles { get; set; }


		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1}, {2} }}",
					StringificationUtility.Stringify("IDRole", IDRole),
					StringificationUtility.Stringify("Code", Code),
					StringificationUtility.Stringify("Description", Description)
				);
		}

		/// <summary>
		///     fetches the users associated with the role
		/// </summary>
		/// <param name="activeUserRolesOnly">Wether to only get users who's roles active, defaulted to false</param>
		/// <returns>The role's users</returns>
		public IEnumerable<User> Users(bool activeUserRolesOnly = false)
		{
			if (activeUserRolesOnly)
			{
				return UserRoles.Where(ur => ur.IsActive).Select(ur => ur.User);
			}

			return UserRoles.Select(ur => ur.User);
		}

		/// <summary>
		///     fetches the applications associated with the role
		/// </summary>
		/// <param name="activeApplicationRolesOnly">
		///     Wether to only get applications who's role associations are active, defaulted
		///     to false
		/// </param>
		/// <returns>The role's applications</returns>
		public IEnumerable<Application> Applications(bool activeApplicationRolesOnly = false)
		{
			if (activeApplicationRolesOnly)
			{
				return ApplicationRoles.Where(ar => ar.IsActive).Select(ar => ar.Application);
			}

			return ApplicationRoles.Select(ar => ar.Application);
		}
	}
}