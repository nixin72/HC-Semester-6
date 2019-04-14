using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     Represents a known education type managed by the system
	/// </summary>
	[Table("Resources.EducationType")]
	public class EducationType
	{
		/// <summary>
		///     The maximum length of the <see cref="Name" /> field.
		/// </summary>
		internal static readonly int _MaxLengthName;

		/// <summary>
		///     Initialises the readonly static constants.
		/// </summary>
		static EducationType()
		{
			_MaxLengthName = ValidationUtility.GetMaxLengthOfField(typeof(EducationType), "Name");
		}

		protected int _IDEducationType { get; set; }
		protected string _Name { get; set; }

		/// <summary>
		///     The id of the education type
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDEducationType
		{
			get { return _IDEducationType; }
			set { _IDEducationType = value; }
		}

		/// <summary>
		///     The name of the education type
		/// </summary>
		[Required, StringLength(50)]
		public string Name
		{
			get { return _Name; }
			set
			{
				ValidationUtility.ValidateRequiredString(value, _MaxLengthName, "Name");
				_Name = value;
			}
		}

		/// <summary>
		///     The programs related to this education types
		/// </summary>
		public virtual ICollection<Program> Programs { get; set; }


		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1} }}",
					StringificationUtility.Stringify("IDEducationType", IDEducationType),
					StringificationUtility.Stringify("Name", Name)
				);
		}
	}
}