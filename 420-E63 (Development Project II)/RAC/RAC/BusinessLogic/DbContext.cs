using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAC.RACModels;

namespace RAC.BusinessLogic {
	public class DbContext
	{
		private static RacModelContainer dbcx = null;

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