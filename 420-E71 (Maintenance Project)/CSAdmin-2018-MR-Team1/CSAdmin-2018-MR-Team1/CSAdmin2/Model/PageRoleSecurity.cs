using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     Used to specifiy which roles can access a specific page within a system, may be obselete.
	/// </summary>
	[Table("Applications.PageRoleSecurity")]
	// [Obsolete("This might be obselete as the content of the entity is inconsistent and isn't used by the only system that used it, the CSAdmin System")]
	public class PageRoleSecurity
	{
		/// <summary>
		///     The maximum length of the <see cref="Role" /> field.
		/// </summary>
		internal static readonly int _MaxLengthRole;

		/// <summary>
		///     The maximum length of the page <see cref="Name" /> field.
		/// </summary>
		internal static readonly int _MaxLengthPageName;


		/// <summary>
		///     Initialises the static read only static constants.
		/// </summary>
		static PageRoleSecurity()
		{
			_MaxLengthRole = ValidationUtility.GetMaxLengthOfField(typeof(PageRoleSecurity), "Role");
			_MaxLengthPageName = ValidationUtility.GetMaxLengthOfField(typeof(PageRoleSecurity), "PageName");
		}


		protected int _IDApplication { get; set; }
		protected string _Role { get; set; }
		protected string _PageName { get; set; }


		/// <summary>
		///     The id of the application that contains the page.
		/// </summary>
		[Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDApplication
		{
			get { return _IDApplication; }
			set { _IDApplication = value; }
		}

		/// <summary>
		///     The name of the role that can access the page.
		/// </summary>
		[Key, Column(Order = 1), StringLength(50)]
		public string Role
		{
			get { return _Role; }
			set
			{
				ValidationUtility.ValidateRequiredString(value, _MaxLengthRole, "Role");
				_Role = value;
			}
		}


		/// <summary>
		///     The name of the page that the role can access within the application.
		/// </summary>
		[Key, Column(Order = 2), StringLength(200)]
		public string PageName
		{
			get { return _PageName; }
			set
			{
				ValidationUtility.ValidateRequiredString(value, _MaxLengthPageName, "Page Name");
				_PageName = value;
			}
		}

		/// <summary>
		///     The application <see cref="Model.Application" /> object related to this associative entity.
		/// </summary>
		public virtual Application Application { get; set; }


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
					StringificationUtility.Stringify("Role", Role),
					StringificationUtility.Stringify("PageName", PageName)
				);
		}
	}
}