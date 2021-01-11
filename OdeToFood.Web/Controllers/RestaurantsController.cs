using OdeToFood.Data.Models;
using OdeToFood.Data.Services;
using System.Web.Mvc;

namespace OdeToFood.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantData db;

        public RestaurantsController(IRestaurantData db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = db.Get(id);
            if(model == null)
            {
                return View ("NotFound");
            }
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new Restaurant();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create(Restaurant restaurant)
        {            
            if (ModelState.IsValid)
            {
                db.Add(restaurant);
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = db.Get(id);
            if(model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Update(restaurant);
                return RedirectToAction("Details", new { id = restaurant.Id });
            }            
            return View();            
        }

        public ActionResult Delete(int id)
        {
            var model = db.Get(id);
            if(model == null)
            {
                return RedirectToAction("NotFound");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}