using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CSAdmin2.Model
{
	/// <summary>
	///     A utility class for all sort of tasks related to the validation of objects within this namespace.
	/// </summary>
	internal static class ValidationUtility
	{
		/// <summary>
		///     Extracts the maximum length value of a StringLengthAttibute for a specific field within the specified type.
		/// </summary>
		/// <param name="type">The object type containing the field.</param>
		/// <param name="name">The name of the field you are extracting the maximum length from.</param>
		/// <returns>The maximum length of the string</returns>
		internal static int GetMaxLengthOfField(Type type, string fieldName)
		{
			MemberInfo[] mi = type.GetMember(fieldName);
			return ((StringLengthAttribute) Attribute.GetCustomAttribute(mi[0], typeof(StringLengthAttribute))).MaximumLength;
		}


		/// <summary>
		///     <para>Validates a string by checking if it's empty, whitespace or too long.</para>
		///     <para>Throws a validation exception the string is invalid.</para>
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="maxLenght">The max length of the string.</param>
		/// <param name="name">The name used to generate the error messages.</param>
		/// <exception cref="CSAdminValidationException">When the string is invalid.</exception>
		internal static void ValidateRequiredString(string value, int maxLenght, string name)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new CSAdminValidationException(string.Format("The {0} cannot be null or empty.", name.ToLower()));
			}

			ValidateStringLength(value, maxLenght, name);
		}

		/// <summary>
		///     <para>Validates a string by checking if it's too long.</para>
		///     <para>Throws a validation exception the string is too long.</para>
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="maxLength">The max length of the string.</param>
		/// <param name="name">The name used to generate the error message.</param>
		/// <exception cref="CSAdminValidationException">When the string is invalid.</exception>
		internal static void ValidateStringLength(string value, int maxLength, string name)
		{
			if (value != null)
			{
				if (value.Length > maxLength)
				{
					throw new CSAdminValidationException(string.Format("The {0} is too long {1}, it cannot be longer than {2}",
						name.ToLower(), value.Length, maxLength));
				}
			}
		}
	}

	/// <summary>
	///     An exception type used to identify validation problems with the entities used by the csadmin system.
	/// </summary>
	public class CSAdminValidationException : Exception
	{
		internal CSAdminValidationException(string message) : base(message)
		{
		}
	}
}