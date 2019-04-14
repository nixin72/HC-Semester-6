using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAdmin2.Model
{
	/// <summary>
	///     The settings used by csadmin, There is only one entry in the database at a time, column names are used as keys.
	/// </summary>
	[Table("Applications.Settings")]
	public class Setting
	{
		protected short _CurrentYearSemester { get; set; }
		protected DateTime _SemseterEndDate { get; set; }

		/// <summary>
		///     <para>
		///         A numeric representation of the current semester. It is formatted the same way clara formats the AnSession
		///         columns.
		///     </para>
		///     <para>format: value = (CurrentYear * 10) + (1 = Winter | 2 = Summer | 3 = Fall)</para>
		///     <para>eg: 20183 = Fall 2018 | 19982 = Summer 1998</para>
		/// </summary>
		[Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]
		public short CurrentYearSemester
		{
			get { return _CurrentYearSemester; }
			set { _CurrentYearSemester = value; }
		}

		/// <summary>
		///     The end date of the current semester.
		/// </summary>
		[Column(Order = 1, TypeName = "date")]
		public DateTime SemesterEndDate
		{
			get { return _SemseterEndDate; }
			set { _SemseterEndDate = value; }
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
					StringificationUtility.Stringify("CurrentYearSemester", CurrentYearSemester),
					StringificationUtility.Stringify("SemesterEndDate", SemesterEndDate)
				);
		}
	}
}