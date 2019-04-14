using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using System.Data.Entity.Infrastructure;
using CSAdmin2.Logic;
using CSAdmin2.Model;

namespace CSAdminMVC.Controllers
{
    public class UsernameController : Controller
    {
        // GET: Username
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Usernames(string sortOrder, string lastName, string firstName, string username, string currentLastFilter, string currentFirstFilter, string currentUserFilter, int? page, string searchParam)
        {
            if(searchParam == null)
                searchParam = "all";

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name" ? "Name_DESC" : "Name";
            ViewBag.UserNameSortParm = sortOrder == "Username" ? "Username_DESC" : "Username";

            ViewBag.all = false;
            ViewBag.duplicates = false;
            ViewBag.not_in_ad = false;

            


            List<User> users = new List<User>();

            if (searchParam == "all")
            {
                users = UserManager.SelectAllUsernames(sortOrder, lastName, firstName, username);
                ViewBag.all = true;
            }
            if (searchParam == "duplicates")
            {
                users = UserManager.SelectDuplicateUsernames(sortOrder, lastName, firstName, username);
                ViewBag.duplicates = true;
            }
            if (searchParam == "not_in_ad")
            {
                users = UserManager.SelectUsersNotInAD(sortOrder, lastName, firstName, username);
                ViewBag.not_in_ad = true;
            }


            if(lastName != null || firstName != null || username != null)
            {
                page = 1;
            }
            if (lastName == null)
                lastName = currentLastFilter;
            if (firstName == null)
                firstName = currentFirstFilter;
            if (username == null)
                username = currentUserFilter;


            ViewBag.currentLastFilter = lastName;
            ViewBag.currentFirstFilter = firstName;
            ViewBag.currentUserFilter = username;

            if (!String.IsNullOrEmpty(lastName))
            {
                users = users.FindAll(u => u.LastName.ToLower().Contains(lastName.ToLower()));

            }
            if (!String.IsNullOrEmpty(firstName))
            {
                users = users.FindAll(u => u.FirstName.ToLower().Contains(firstName.ToLower()));

            }
            if (!String.IsNullOrEmpty(username))
            {
                users = users.FindAll(u => u.Username.ToLower().Contains(username.ToLower()));

            }


            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.LastName).ToList();
                    break;
                case "Name":
                    users = users.OrderBy(u => u.LastName).ToList();
                    break;
                case "username_desc":
                    users = users.OrderByDescending(u => u.Username).ToList();
                    break;
                case "Username":
                    users = users.OrderBy(u => u.Username).ToList();
                    break;
               
                default:
                    users = users.OrderBy(u => u.LastName).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(users.ToPagedList(pageNumber,pageSize));
        }

        [AllowAnonymous]
        public ActionResult ManageUserRoles()
        {
            return View("ManageUserRoles");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public void EditUser(User user)
        {
            UserManager.UpdateUsername(user.IDUser, user.Username);
        }
    }
}