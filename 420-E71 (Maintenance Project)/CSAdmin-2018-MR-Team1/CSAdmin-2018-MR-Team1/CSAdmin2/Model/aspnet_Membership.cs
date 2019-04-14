using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	[Table("NetMem.aspnet_Membership")]
	public class aspnet_Membership
	{
		protected int _ApplicationId { get; set; }
		protected int _UserId { get; set; }
		protected bool _IsApproved { get; set; }

		/// <summary>
		///     A unique identifier for the ApplicationId association.
		/// </summary>
		[Key]
		public int ApplicationId
		{
			get { return _ApplicationId; }
			set { _ApplicationId = value; }
		}

		/// <summary>
		///     The user id tied to the Membership
		/// </summary>
		public int UserId
		{
			get { return _UserId; }
			set { _UserId = value; }
		}

		/// <summary>
		///     The approval status of the membership
		/// </summary>
		public bool IsApproved
		{
			get { return _IsApproved; }
			set { _IsApproved = value; }
		}


		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format("{{ {0}, {1}, {2} }}",
					StringificationUtility.Stringify("ApplicationId", ApplicationId),
					StringificationUtility.Stringify("UserId", UserId),
					StringificationUtility.Stringify("IsApproved", IsApproved)
				);
		}
	}
}