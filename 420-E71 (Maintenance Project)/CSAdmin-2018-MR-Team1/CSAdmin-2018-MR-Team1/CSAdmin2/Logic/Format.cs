using System;

namespace CSAdmin2.Logic
{
	public class Format
	{
		public static string FormatDate(DateTime EvalDate)
		{
			//stop reserving memory for no reason.
			return EvalDate.ToString("yyyy-MMM-dd");
		}

		/// <summary>
		///     Method that formats year and semester. eg. YearSemester: 20121 Formatted: Winter 2012
		/// </summary>
		/// <param name="YearSemester"></param>
		/// <returns></returns>
		public static string FormatSemester(short YearSemester)
		{
			//holy sweet potatoes this needed some optimisation and clean up - GUI
			string res;
			switch (YearSemester % 10)
			{
				case 1:
					res = $"Winter ";
					break;
				case 2:
					res = $"Summer ";
					break;
				case 3:
					res = $"Fall ";
					break;
				default:
					return null;
			}

			//because integer division is awesome
			res += (YearSemester / 10).ToString();

			return res;
		}

		/// <summary>
		///     Method that formats first and last name. eg. FirstName: Renee LastName: Ghattas Formatted: Ghattas, Renee
		/// </summary>
		/// <param name="FirstName"></param>
		/// <param name="LastName"></param>
		/// <returns></returns>
		public static string FormatLastFirst(string FirstName, string LastName)
		{
			//because interpolated strings are badass - GUI
			return $"{LastName}, {FirstName}";
		}

		/// <summary>
		///     Method that formats first and last name. eg. FirstName: Renee LastName: Ghattas Formatted: Renee Ghattas
		/// </summary>
		/// <param name="FirstName"></param>
		/// <param name="LastName"></param>
		/// <returns></returns>
		public static string FormatFirstLast(string FirstName, string LastName)
		{
			//same reason as before - GUI
			return $"{FirstName} {LastName}";
		}
	}
}