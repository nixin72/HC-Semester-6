using System.DirectoryServices.AccountManagement;

namespace CSAdmin2.Logic
{
	public class UserPrincipalExSearchFilter : AdvancedFilters
	{
		public UserPrincipalExSearchFilter(Principal p) : base(p)
		{
		}

		public void LogonCount(int value, MatchType mt)
		{
			AdvancedFilterSet("LogonCount", value, typeof(int), mt);
		}
	}
}