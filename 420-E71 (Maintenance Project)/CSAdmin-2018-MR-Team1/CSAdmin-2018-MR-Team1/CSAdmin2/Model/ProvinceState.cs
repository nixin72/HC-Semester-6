using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     An associative entity that relates known states and provinces to their related county.
	/// </summary>
	/// <seealso cref="Model.Province" />
	/// <seealso cref="Model.Country" />
	[Table("Resources.ProvinceState")]
	public class ProvinceState
	{
		/// <summary>
		///     The maximum length of the <see cref="Name" /> field.
		/// </summary>
		internal static readonly int _MaxLengthName;


		/// <summary>
		///     Initialises the static read only static constants.
		/// </summary>
		static ProvinceState()
		{
			_MaxLengthName = ValidationUtility.GetMaxLengthOfField(typeof(ProvinceState), "Name");
		}

		protected int _IDProvinceState { get; set; }
		protected string _Name { get; set; }
		protected int _IDCountry { get; set; }


		/// <summary>
		///     The id of the state or province.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDProvinceState
		{
			get { return _IDProvinceState; }
			set { _IDProvinceState = value; }
		}

		/// <summary>
		///     The name of the state or province.
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
		///     The id of the <see cref="Model.Country" /> related to this sate or province.
		/// </summary>
		public int IDCountry
		{
			get { return _IDCountry; }
			set { _IDCountry = value; }
		}

		/// <summary>
		///     The <see cref="Model.Country" /> object associated with this state or province.
		/// </summary>
		public virtual Country Country { get; set; }


		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1}, {2} }}",
					StringificationUtility.Stringify("IDProvinceState", IDProvinceState),
					StringificationUtility.Stringify("Name", Name),
					StringificationUtility.Stringify("IDCountry", IDCountry)
				);
		}
	}
}