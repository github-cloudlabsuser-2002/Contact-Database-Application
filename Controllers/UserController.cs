using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index()
        {
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

      // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Generate a new ID for the user
                user.Id = userlist.Any() ? userlist.Max(u => u.Id) + 1 : 1;
                userlist.Add(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                // Update other properties as needed

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                userlist.Remove(user);
                return RedirectToAction("Index");
            }

            return HttpNotFound();
        }

        public ActionResult Search(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View(userlist);
            }

            var results = userlist.Where(u => u.Name.Contains(name));
            return View(results);
        }

    }
}
