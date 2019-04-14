using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     A simple class used to represent a country within csadmin.
	/// </summary>
	[Table("Resources.Country")]
	public class Country
	{
		/// <summary>
		///     The maximum length of the <see cref="Name" /> field.
		/// </summary>
		internal static readonly int _MaxLengthName;

		/// <summary>
		///     Initialises the static read only static constants.
		/// </summary>
		static Country()
		{
			_MaxLengthName = ValidationUtility.GetMaxLengthOfField(typeof(Country), "Name");
		}

		protected string _Name { get; set; }
		protected int _IDCountry { get; set; }

		/// <summary>
		///     The unique id representing the country.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDCountry
		{
			get { return _IDCountry; }
			set { _IDCountry = value; }
		}

		/// <summary>
		///     The name of the country.
		/// </summary>
		[Required, StringLength(100)]
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
		///     A list of states or provinces that are part of this country.
		/// </summary>
		public virtual ICollection<ProvinceState> ProvinceStates { get; set; }

		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1} }}",
					StringificationUtility.Stringify("IDCountry", IDCountry),
					StringificationUtility.Stringify("Name", Name)
				);
		}
	}
}