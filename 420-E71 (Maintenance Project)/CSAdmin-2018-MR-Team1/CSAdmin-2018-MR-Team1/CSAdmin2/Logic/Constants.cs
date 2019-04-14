using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CSAdmin2.Model;

namespace CSAdmin2.Logic
{
	public static class Constants
	{
		// user roles
		internal const string STUDENT_ROLE_CODE = "ST";
		internal const string TEACHER_ROLE_CODE = "TE";
		internal const string COORDONATOR_ROLE_CODE = "CO";

		// application codes
		internal const string CES_APP_CODE = "CES";
		internal const string CSADMIN_APP_CODE = "CSA";

		// connection strings
		internal const string ADConnextionString = "DC1.cegep-heritage.qc.ca";

		// object constants
		internal readonly static int CES_STUDENT_ROLE_ID;
		internal readonly static int CES_COORDONATOR_ROLE_ID;
		internal readonly static int CES_TEACHER_ROLE_ID;

		//Unit org ids
		internal const int UNIT_ORG_ID_REGULAR_EDUCTAION = 235;
		internal const int UNIT_ORG_ID_CONTINUED_EDUCATION = 525;

		static Constants()
		{
			CSAdminContext context = new CSAdminContext();
			CES_STUDENT_ROLE_ID =
			(
				from ApplicationRole ur in context.ApplicationRoles
				where
					ur.Application.Code == CES_APP_CODE &&
					ur.Role.Code == STUDENT_ROLE_CODE
				select
					ur.Role.IDRole
			).First();

			CES_TEACHER_ROLE_ID =
			(
				from ApplicationRole ur in context.ApplicationRoles
				where
					ur.Application.Code == CES_APP_CODE &&
					ur.Role.Code == TEACHER_ROLE_CODE
				select
					ur.Role.IDRole
			).First();

			CES_COORDONATOR_ROLE_ID =
			(
				from ApplicationRole ur in context.ApplicationRoles
				where
					ur.Application.Code == CES_APP_CODE &&
					ur.Role.Code == COORDONATOR_ROLE_CODE
				select
					ur.Role.IDRole
			).First();

			context.Dispose();
		}

		/// <summary>
		///     This function makes sure that the passed csadmin context object is initialised.
		/// </summary>
		/// <param name="context">The csadmin context object that needs to be verified</param>
		public static void Initialise(ref CSAdminContext context)
		{
			if (context == null)
			{
				context = new CSAdminContext();
			}
		}


		// mildly stolen from:
		// https://stackoverflow.com/questions/3769457/how-can-i-remove-accents-on-a-string#answer-3769995

		private static IEnumerable<char> RemoveDiacriticsEnum(string src, Func<char, char> customFolding)
		{
			foreach (char c in src.Normalize(NormalizationForm.FormD))
				switch (CharUnicodeInfo.GetUnicodeCategory(c))
				{
					case UnicodeCategory.NonSpacingMark:
					case UnicodeCategory.SpacingCombiningMark:
					case UnicodeCategory.EnclosingMark:
						//do nothing
						break;
					default:
						yield return customFolding(c);
						break;
				}
		}

		private static IEnumerable<char> RemoveDiacriticsEnum(string src)
		{
			return RemoveDiacritics(src, c => c);
		}

		private static string RemoveDiacritics(string src, Func<char, char> customFolding)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in RemoveDiacriticsEnum(src, customFolding))
				sb.Append(c);
			return sb.ToString();
		}

		public static string NormaliseName(string src)
		{
			return Regex.Replace(RemoveDiacritics(src, c => c), "([^A-Za-z]+)", "");
		}
	}
}