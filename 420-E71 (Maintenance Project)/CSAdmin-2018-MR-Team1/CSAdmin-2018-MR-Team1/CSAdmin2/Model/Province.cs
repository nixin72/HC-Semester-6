using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     A known province managed by the csadmin system.
	/// </summary>
	[Table("Resources.Province")]
	public class Province
	{
		/// <summary>
		///     The maximum length of the <see cref="Text" /> field.
		/// </summary>
		internal static readonly int _MaxLengthText;

		/// <summary>
		///     The maximum length of the <see cref="Abbreviation" /> field.
		/// </summary>
		internal static readonly int _MaxLengthAbbreviation;


		/// <summary>
		///     Initialises the static read only static constants.
		/// </summary>
		static Province()
		{
			_MaxLengthText = ValidationUtility.GetMaxLengthOfField(typeof(Province), "Text");
			_MaxLengthAbbreviation = ValidationUtility.GetMaxLengthOfField(typeof(Province), "Abbreviation");
		}

		protected int _IDProvince { get; set; }
		protected string _Text { get; set; }
		protected string _Abbreviation { get; set; }

		/// <summary>
		///     The id of the province.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDProvince
		{
			get { return _IDProvince; }
			set { _IDProvince = value; }
		}

		/// <summary>
		///     The name of the province.
		/// </summary>
		[Required, StringLength(100)]
		public string Text
		{
			get { return _Text; }
			set
			{
				ValidationUtility.ValidateRequiredString(value, _MaxLengthText, "Text");
				_Text = value;
			}
		}

		/// <summary>
		///     The abbreviation used for the province.
		/// </summary>
		[Required, StringLength(2)]
		public string Abbreviation
		{
			get { return _Abbreviation; }
			set
			{
				ValidationUtility.ValidateRequiredString(value, _MaxLengthAbbreviation, "Abbreviation");
				_Abbreviation = value;
			}
		}

		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1}, {2} }}",
					StringificationUtility.Stringify("IDProvince", IDProvince),
					StringificationUtility.Stringify("Text", Text),
					StringificationUtility.Stringify("Abbreviation", Abbreviation)
				);
		}
	}
}