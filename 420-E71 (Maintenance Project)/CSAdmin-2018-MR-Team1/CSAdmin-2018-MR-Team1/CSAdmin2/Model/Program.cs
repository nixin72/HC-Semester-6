using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     <para>A program known and managed by the system.</para>
	///     <para>
	///         The <see cref="IsActive" /> should anways be checked when bulk selecting to check if the program still
	///         exists.
	///     </para>
	/// </summary>
	[Table("Resources.Program")]
	public class Program
	{
		/// <summary>
		///     The maximum length of the name field.
		/// </summary>
		internal static readonly int _MaxLengthName;


		/// <summary>
		///     Initialises the static read only static constants.
		/// </summary>
		static Program()
		{
			_MaxLengthName = ValidationUtility.GetMaxLengthOfField(typeof(Program), "Name");
		}

		protected int _IDProgram { get; set; }
		protected string _Name { get; set; }
		protected int _IDEducationType { get; set; }
		protected bool _IsActive { get; set; }

		/// <summary>
		///     The id of the program.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDProgram
		{
			get { return _IDProgram; }
			set { _IDProgram = value; }
		}

		/// <summary>
		///     The name of the program.
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
		///     The id of the <see cref="Model.EducationType" /> this program is a part of.
		/// </summary>
		public int IDEducationType
		{
			get { return _IDEducationType; }
			set { _IDEducationType = value; }
		}

		/// <summary>
		///     Wether or not the program is still active.
		/// </summary>
		public bool IsActive
		{
			get { return _IsActive; }
			set { _IsActive = value; }
		}

		/// <summary>
		///     The <see cref="Model.EducationType" /> this program is part of.
		/// </summary>
		public virtual EducationType EducationType { get; set; }

		/// <summary>
		///     The list of iterations this program has gone through and their related clara id.
		/// </summary>
		/// <seealso cref="Model.ProgramVersion" />
		public virtual ICollection<ProgramVersion> ProgramVersions { get; set; }


		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1}, {2}, {3} }}",
					StringificationUtility.Stringify("IDProgram", IDProgram),
					StringificationUtility.Stringify("Name", Name),
					StringificationUtility.Stringify("IDEducationType", IDEducationType),
					StringificationUtility.Stringify("IsActive", IsActive)
				);
		}
	}
}