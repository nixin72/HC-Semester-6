using CSAdmin2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAdmin2.Logic
{
	public static class NewSemesterManager
	{
		public static int UpdateCoordinators(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			int changed = 0;
			using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
			{
				try
				{
					IEnumerable<int> inserted = context.Database.SqlQuery<int>(Properties.Resources.UpdateCoordinatorScript);
					changed = inserted.First();
					foreach (int newCoordinatorId in inserted.Skip(1))
					{
						User newCoordinator = context.Users.Find(newCoordinatorId);
						newCoordinator.Username = RoleManager.CreateUserName(newCoordinator.FirstName, newCoordinator.LastName);
					}
					context.SaveChanges();
#if DEBUG
					trans.Rollback();
#else
					trans.Commit();
#endif
				}
				catch
				{
					trans.Rollback();
				}
			}
			return changed;
		}

		public static int UpdateTeachers(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			int changed = 0;
			using (System.Data.Entity.DbContextTransaction trans = context.Database.BeginTransaction())
			{
				try
				{
					IEnumerable<int> inserted = context.Database.SqlQuery<int>(Properties.Resources.UpdateTeacherScript);
					changed = inserted.First();
					foreach (int newTeacherId in inserted.Skip(1))
					{
						User newTeacher = context.Users.Find(newTeacherId);
						newTeacher.Username = RoleManager.CreateUserName(newTeacher.FirstName, newTeacher.LastName);
					}
					context.SaveChanges();
#if DEBUG
					trans.Rollback();
#else
					trans.Commit();
#endif
				}
				catch
				{
					trans.Rollback();
				}
			}
			return changed;
		}

		public static int UpdateStudents(CSAdminContext context = null)
		{
			int changed = 0;
			Constants.Initialise(ref context);
			using (DbContextTransaction trans = context.Database.BeginTransaction())
			{
				try
				{
					changed = context.Database.ExecuteSqlCommand(Properties.Resources.UpdateStudentScript);
#if DEBUG
                    trans.Rollback();
#else
                    trans.Commit();
#endif
                }
				catch (Exception)
				{
					trans.Rollback();
				}
			}
            return changed;
        }
	}
}
