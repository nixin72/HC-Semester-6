using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSAdmin2.Clara;

namespace CSAdmin2.Model
{
	/// <summary>
	///     An associative entity that relates csadmin users to students within CALRA.
	/// </summary>
	[Table("Users.StudentUser")]
	public class StudentUser
	{
		protected int _IDUser { get; set; }
		protected int _IDEtudiant { get; set; }

		/// <summary>
		///     The CSAdmin user id related to this user.
		/// </summary>
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IDUser
		{
			get { return _IDUser; }
			set { _IDUser = value; }
		}

		/// <summary>
		///     The CLARA student id related to this user.
		/// </summary>
		public int IDEtudiant
		{
			get { return _IDEtudiant; }
			set { _IDEtudiant = value; }
		}

		/// <summary>
		///     The CSAdmin <see cref="Model.User" /> object associated with this user.
		/// </summary>
		public virtual User User { get; set; }

		/// <summary>
		///     Turns the object into a json string.
		/// </summary>
		/// <returns>A json string representing the object.</returns>
		public override string ToString()
		{
			return
				string.Format(
					"{{ {0}, {1} }}",
					StringificationUtility.Stringify("IDUser", IDUser),
					StringificationUtility.Stringify("IDEtudiant", IDEtudiant)
				);
		}

		/// <summary>
		///     The clara entry for this student, if it exists
		/// </summary>
		[ForeignKey("IDEtudiant")]
		public virtual Etudiant ClaraEtudiant { get; set; }
	}
}