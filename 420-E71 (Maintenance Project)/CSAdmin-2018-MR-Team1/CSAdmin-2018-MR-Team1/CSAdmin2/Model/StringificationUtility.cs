using System;

namespace CSAdmin2.Model
{
	/// <summary>
	///     A utility used to standardise the stringification of specific field types
	/// </summary>
	internal static class StringificationUtility
	{
		/// <summary>
		///     Serialises an object for serialisation purposes.
		/// </summary>
		/// <param name="name">The name of the field.</param>
		/// <param name="value">The value of the field.</param>
		/// <returns>The stringified field name and value.</returns>
		internal static string Stringify(string name, object value)
		{
			string ret = $"\"{name}\" : ";
			if (value == null)
			{
				ret += "null";
			}
			else if (value is string)
			{
				ret += $"\"{value}\"";
			}
			else if (value is DateTime)
			{
				ret += $"\"{((DateTime) value).Ticks}\"";
			}
			else
			{
				ret += value.ToString();
			}

			return ret;
		}
	}
}