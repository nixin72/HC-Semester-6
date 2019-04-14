using System.Collections.Generic;
using CSAdmin2.Clara;
using CSAdmin2.Model;

namespace CSAdmin2.Logic
{
	/// <summary>
	///     The logic class used to manage programs
	/// </summary>
	public class ProgramManager
	{
		/// <summary>
		///     Finds programs that are taught in the college.
		/// </summary>
		/// <param name="context">The context to use for the query if the underlying query already has one open.</param>
		/// <returns>The list of programs taught in the college.</returns>
		public static IEnumerable<Programme> SelectCegepPrograms(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.Database.SqlQuery<Programme>(
				@"Select
					distinct(p.numero)
					g.titre,
					g.IDProgramme
				from
					ClaraGrilles.Grille g
					left join ClaraProgrammes.Programme p
						on p.IDProgramme = g.IDProgramme
				where
					p.IDTypeSanction < 11
				order
					by p.Numero END");
		}
	}
}