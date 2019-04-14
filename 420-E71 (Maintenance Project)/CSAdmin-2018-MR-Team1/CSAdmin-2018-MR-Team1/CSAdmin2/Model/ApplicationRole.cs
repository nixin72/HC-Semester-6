using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     <para>An associative entity that relates a role to an application.</para>
	///     <para>The <see cref="IsActive" /> variable should always be checked to see if the entity is still active.</para>
	/// </summary>
	[Table("Applications.ApplicationRole")]
	public class ApplicationRole
	{
		protected int _IDApplicationRole { get; set; }
		protected int _IDApplication { get; set; }
		protected int _IDRole { get; set; }
		protected bool _IsActive { get; set; }

		/// <summary>
		///     A unique identifier for the applicationrole association.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDApplicationRole
		{
			get { return _IDApplicationRole; }
			set { _IDApplicationRole = value; }
		}

		/// <summary>
		///     The id of the application this entity is realred with.
		/// </summary>
		public int IDApplication
		{
			get { return _IDApplication; }
			set { _IDApplication = value; }
		}

		/// <summary>
		///     The id of the role this entity is related with.
		/// </summary>
		public int IDRole
		{
			get { return _IDRole; }
			set { _IDRole = value; }
		}

		/// <summary>
		///     Wether or not this entity is active, if it isn't it should never be counted as existing.
		/// </summary>
		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}

		/// <summary>
		///     The application related to this entity.
		/// </summary>
		public virtual Application Application { get; set; }

		/// <summary>
		///     The role related to this entity.
		/// </summary>
		public virtual Role Role { get; set; }

		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format("{{ {0}, {1}, {2}, {3} }}",
					StringificationUtility.Stringify("IDApplicationRole", IDApplicationRole),
					StringificationUtility.Stringify("IDApplication", IDApplication),
					StringificationUtility.Stringify("IDRole", IDRole),
					StringificationUtility.Stringify("IsActive", IsActive)
				);
		}
	}
}