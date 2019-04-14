using CSAdmin2.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;

namespace CSAdmin2.Model
{

	/// <summary>
	/// A user managed by the CSAdmin system.
	/// </summary>
	[Table("Users.User")]
	public partial class User
	{

		/// <summary>
		/// The maximum length of the <see cref="LastName"/> field.
		/// </summary>
		internal static readonly int _MaxLengthLastName;

		/// <summary>
		/// The maximum length of the <see cref="FirstName"/> field.
		/// </summary>
		internal static readonly int _MaxLengthFirstName;

		/// <summary>
		/// The maximum length of the <see cref="Username"/> field.
		/// </summary>
		public static readonly int _MaxLengthUsername;


		/// <summary>
		/// Initialises the static read only static constants.
		/// </summary>
		static User()
		{
			_MaxLengthLastName = ValidationUtility.GetMaxLengthOfField(typeof(User), "LastName");
			_MaxLengthFirstName = ValidationUtility.GetMaxLengthOfField(typeof(User), "FirstName");
			_MaxLengthUsername = ValidationUtility.GetMaxLengthOfField(typeof(User), "Username");
		}

		protected int _IDUser { get; set; }
		protected string _LastName { get; set; }
		protected string _FirstName { get; set; }
		protected string _Username { get; set; }
		protected bool _IsActive { get; set; }


		/// <summary>
		/// The Id of the user.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDUser
		{
			get { return _IDUser; }
			set { _IDUser = value; }
		}

		/// <summary>
		/// The last name of the user.
		/// </summary>
		[StringLength(60)]
		public string LastName
		{
			get { return _LastName; }
			set
			{
				ValidationUtility.ValidateStringLength(value, _MaxLengthLastName, "LastName");
				_LastName = value;
			}
		}

		/// <summary>
		/// The First name of the user
		/// </summary>
		[StringLength(60)]
		public string FirstName
		{
			get { return _FirstName; }
			set
			{
				ValidationUtility.ValidateStringLength(value, _MaxLengthFirstName, "FirstName");
				_FirstName = value;
			}
		}

		/// <summary>
		/// The username of the user
		/// </summary>
		[Required, StringLength(60)]
		public string Username
		{
			get { return _Username; }
			set
			{
				ValidationUtility.ValidateRequiredString(value, _MaxLengthUsername, "Username");
				_Username = value;
			}
		}

		/// <summary>
		/// Wether or not the user is still active, only active user can authentificate using the system
		/// </summary>
		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}



		/// <summary>
		/// <para>The <see cref="Model.EmployeeUser"/> object related to this user.</para>
		/// <para>If this exists this user is an employee in CLARA.</para>
		/// </summary>
		public virtual EmployeeUser EmployeeUser { get; set; }

		/// <summary>
		/// <para>The <see cref="Model.StudentUser"/> object related to this user.</para>
		/// <para>If this exists this user is a student in CLARA.</para>
		/// </summary>
		public virtual StudentUser StudentUser { get; set; }

		/// <summary>
		/// The list of roles this user has access to in the form of a <see cref="UserRole"/> associative entity
		/// </summary>
		public virtual ICollection<UserRole> UserRoles { get; set; }


		/// <summary>
		/// Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1}, {2}, {3}, {4} }}",
					StringificationUtility.Stringify("IDUser", IDUser),
					StringificationUtility.Stringify("LastName", LastName),
					StringificationUtility.Stringify("FirstName", FirstName),
					StringificationUtility.Stringify("Username", Username),
					StringificationUtility.Stringify("IsActive", IsActive)
				);
		}

		/// <summary>
		/// Fetches all the roles for the user
		/// </summary>
		/// <param name="activeRolesOnly">Wether to only fetch roles that are active, defaulted to false</param>
		/// <returns>The user's role</returns>
		public IEnumerable<Role> Roles(bool activeRolesOnly = false)
		{
			if (activeRolesOnly)
			{
				return UserRoles.Where(ur => ur.IsActive).Select(ur => ur.Role);
			}
			else
			{
				return UserRoles.Select(ur => ur.Role);
			}
		}

        public string getAdUsername()
        {
            return UserManager.SelectAdUsername(this.Username, this.FirstName, this.LastName);
        }
	}
}
