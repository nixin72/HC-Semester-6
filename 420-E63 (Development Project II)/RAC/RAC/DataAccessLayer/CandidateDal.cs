using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RAC.Models;
using System.Data.Entity;
using System.Web.DynamicData;
using RAC.BusinessLogic;

namespace RAC.DataAccessLayer
{
	public class CandidateDal
	{
		#region Create
			public int CreateCandidate(Candidate newCandidate)
			{
				var db = new RACModelContainer();
				try
				{ 
					Candidate candidate = (Candidate) db.User.Add(newCandidate);
					Notification n = new Notification();
					n.sendEmail(candidate.Email, "dumaresq.philip@cegep-heritage.qc.ca", (int) Messages.NEW_CANDIDATE_ACCOUNT);
				}
				catch
				{
					return -1;
				}
			
				return 0;
			}
		#endregion

		#region Retrieve
			public List<Candidate> RetrieveCandidates()
			{
				var db = new RACModelContainer();
				return db.User.Select(user => user).Select(user =>
					new Candidate()
					{
						UserId = user.UserId,
						FirstName = user.FirstName,
						LastName = user.LastName,
						Email = user.Email,
						HomePhone = user.HomePhone,
						WorkPhone = user.WorkPhone,
						UserType = user.UserType
					}
				).ToList();
			}

			public Candidate RetrieveCanadidate(int id)
			{
				var db = new RACModelContainer();
				return RetrieveCanadidate(db.User.Where(u => u.UserId == id).Select(u => u));
			}
		
			public Candidate RetrieveCanadidate(string email)
			{
				var db = new RACModelContainer();
				return RetrieveCanadidate(db.User.Where(u => u.Email.Equals(email)).Select(u => u));
			}

			private Candidate RetrieveCanadidate(IQueryable<User> user)
			{
				return user.Select(u => new Candidate()
				{
					UserId = u.UserId,
					FirstName = u.FirstName,
					LastName = u.LastName,
					Email = u.Email,
					HomePhone = u.HomePhone,
					WorkPhone = u.WorkPhone,
					UserType = u.UserType
				}).FirstOrDefault();
			}
		#endregion

		#region Update
			
		#endregion

		#region Delete
			public Candidate DeleteCanadidate(int id)
			{
				var db = new RACModelContainer();
				var user = RetrieveCanadidate(db.User.Where(u => u.UserId == id).Select(u => u));

				return (Candidate) db.User.Remove(user);
			}

			public Candidate DeleteCanadidate(string email)
			{
				var db = new RACModelContainer();
				var user = RetrieveCanadidate(db.User.Where(u => u.Email.Equals(email)).Select(u => u));
			
				return (Candidate) db.User.Remove(user);
			
			}
		#endregion

	}
}