using RAC.RACModels;

namespace RAC.BusinessLogic {
	public class DbContext
	{
		private static RacModelContainer dbcx;

		public static RacModelContainer Context
		{
			get
			{
				if (dbcx == null)
				{
					dbcx = new RacModelContainer();
				}

				return dbcx;
			}
		}

	}
}