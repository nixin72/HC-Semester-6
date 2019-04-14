using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     An iteration of known program managed within the system.
	/// </summary>
	[Table("Resources.ProgramVersion")]
	public class ProgramVersion
	{
		protected int _IDProgramVersion { get; set; }
		protected int _IDProgram { get; set; }
		protected int _IDProgramClara { get; set; }

		/// <summary>
		///     The id of the program iteration
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IDProgramVersion
		{
			get { return _IDProgramVersion; }
			set { _IDProgramVersion = value; }
		}

		/// <summary>
		///     The id of the <see cref="Model.Program" /> associated with this iteration
		/// </summary>
		public int IDProgram
		{
			get { return _IDProgram; }
			set { _IDProgram = value; }
		}

		/// <summary>
		///     The program id of the program iteration within clara.
		/// </summary>
		public int IDProgramClara
		{
			get { return _IDProgramClara; }
			set { _IDProgramClara = value; }
		}

		/// <summary>
		///     The <see cref="Model.Program" /> object associated with this program iteration.
		/// </summary>
		public virtual Program Program { get; set; }


		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1}, {2} }}",
					StringificationUtility.Stringify("IDProgramVersion", IDProgramVersion),
					StringificationUtility.Stringify("IDProgram", IDProgram),
					StringificationUtility.Stringify("IDProgramClara", IDProgramClara)
				);
		}
	}
}