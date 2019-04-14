using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     A security question managed by the csadmin system.
	/// </summary>
	[Table("Resources.SecurityQuestions")]
	public class SecurityQuestion
	{
		/// <summary>
		///     The maximum length of the <see cref="Text" /> field.
		/// </summary>
		internal static readonly int _MaxLengthText;

		/// <summary>
		///     Initialises the static read only static constants.
		/// </summary>
		static SecurityQuestion()
		{
			_MaxLengthText = ValidationUtility.GetMaxLengthOfField(typeof(SecurityQuestion), "Text");
		}

		protected int _IDSecurityQuestion { get; set; }
		protected string _Text { get; set; }


		/// <summary>
		///     The id of the security question.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDSecurityQuestion
		{
			get { return _IDSecurityQuestion; }
			set { _IDSecurityQuestion = value; }
		}

		/// <summary>
		///     The text of the security question
		/// </summary>
		[StringLength(256)]
		public string Text
		{
			get { return _Text; }
			set
			{
				ValidationUtility.ValidateStringLength(value, _MaxLengthText, "Text");
				_Text = value;
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
					"{{ {0}, {1} }}",
					StringificationUtility.Stringify("IDSecurityQuestion", IDSecurityQuestion),
					StringificationUtility.Stringify("Text", Text)
				);
		}
	}
}