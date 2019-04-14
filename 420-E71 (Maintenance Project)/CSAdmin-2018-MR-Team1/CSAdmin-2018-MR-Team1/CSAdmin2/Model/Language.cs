using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     A known language known by the system
	/// </summary>
	[Table("Resources.Language")]
	public class Language
	{
		/// <summary>
		///     The maximum length of the <see cref="Name" /> field.
		/// </summary>
		internal static readonly int _MaxLengthName;

		/// <summary>
		///     Initialises the static read only static constants.
		/// </summary>
		static Language()
		{
			_MaxLengthName = ValidationUtility.GetMaxLengthOfField(typeof(Language), "Name");
		}

		protected int _LanguageID { get; set; }
		protected string _Name { get; set; }
		protected bool? _IsDefault { get; set; }

		/// <summary>
		///     The id for the language
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LanguageID
		{
			get { return _LanguageID; }
			set { _LanguageID = value; }
		}

		/// <summary>
		///     The name of the language
		/// </summary>
		[Column("Language"), StringLength(255)]
		public string Name
		{
			get { return _Name; }
			set
			{
				ValidationUtility.ValidateStringLength(value, _MaxLengthName, "Name");
				_Name = value;
			}
		}

		/// <summary>
		///     Wether or not the language is the default language within the CSAdmin system.
		/// </summary>
		public bool? IsDefault
		{
			get { return _IsDefault; }
			set { _IsDefault = value; }
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
					StringificationUtility.Stringify("LanguageID", LanguageID),
					StringificationUtility.Stringify("Name", Name),
					StringificationUtility.Stringify("IsDefault", IsDefault)
				);
		}
	}
}