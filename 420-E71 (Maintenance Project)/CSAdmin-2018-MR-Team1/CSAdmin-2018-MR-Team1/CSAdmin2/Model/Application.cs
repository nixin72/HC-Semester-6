using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CSAdmin2.Model
{
	/// <summary>
	///     A known application managed by the CSAdmin system.
	/// </summary>
	[Table("Applications.Application")]
	public class Application
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
		///     Initialises the readonly static constants.
		/// </summary>
		static Application()
		{
			_MaxLengthCode = ValidationUtility.GetMaxLengthOfField(typeof(Application), "Code");
			_MaxLengthDescription = ValidationUtility.GetMaxLengthOfField(typeof(Application), "Description");
		}


		protected int _IDApplication { get; set; }
		protected string _Code { get; set; }
		protected string _Description { get; set; }


		/// <summary>
		///     The unique identifier for the application.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDApplication
		{
			get { return _IDApplication; }
			set { _IDApplication = value; }
		}

		/// <summary>
		///     The code that identifies the application.
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
		///     A short description of the application.
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
		///     The list of roles associated with this application.
		/// </summary>
		public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }

		/// <summary>
		///     A list of associative objects that relate specific pages to specific roles within the application.
		/// </summary>
		public virtual ICollection<PageRoleSecurity> PageRoleSecurities { get; set; }

		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1}, {2} }}",
					StringificationUtility.Stringify("IDApplication", IDApplication),
					StringificationUtility.Stringify("Code", Code),
					StringificationUtility.Stringify("Description", Description)
				);
		}

		/// <summary>
		///     fetches the roles associated with the applications
		/// </summary>
		/// <param name="activeRolesonly">wether the role associations should be active or not, defaulted to true</param>
		/// <returns>The application's roles</returns>
		public IEnumerable<Role> Roles(bool activeRolesOnly = false)
		{
			//making multiple returns becasue efficiency is great
			if (activeRolesOnly)
			{
				return ApplicationRoles.Where(ar => ar.IsActive).Select(ar => ar.Role);
			}

			return ApplicationRoles.Select(ar => ar.Role);
		}
	}
}