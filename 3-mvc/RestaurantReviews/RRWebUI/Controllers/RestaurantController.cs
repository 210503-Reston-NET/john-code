using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RRBL;
using RRWebUI.Models;
using RRModels;

namespace RRWebUI.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantBL _restaurantBL;
        public RestaurantController(IRestaurantBL restaurantBL)
        {
            _restaurantBL = restaurantBL;
        }
        // GET: RestaurantController
        // Actions are public methods in controllers that respond to client requests
        // You can have specific actions respond to specific requests,
        // you can also have actions, that respond to multiple kinds of requests
        // You just have to map the request type to the action properly
        public ActionResult Index()
        {
            // You have different kinds of Views:
            // Strongly typed views - tied to a model, you declare the model at the top of the page with 
            //                          @model DataType
            // Weakly typed views - not tied to a model. You can still pass data to it via, viewbag
            //                      viewdata, tempdata etc
            // Dynamic views - pass a model, let view figure it out. @model dynamic
            // This is an example of a strongly typed view
            return View(_restaurantBL
                .GetAllRestaurants()
                .Select(restaurant => new RestaurantVM(restaurant))
                .ToList()
                );
        }

        // GET: RestaurantController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RestaurantController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RestaurantVM restaurantVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _restaurantBL.AddRestaurant(new Restaurant
                    {
                        Name = restaurantVM.Name,
                        City = restaurantVM.City,
                        State = restaurantVM.State
                    }
                    );
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
                
            catch
            {
                return View();
            }
        }

        // GET: RestaurantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RestaurantController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new RestaurantVM(_restaurantBL.GetRestaurantById(id)));
        }

        // POST: RestaurantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _restaurantBL.DeleteRestaurant(_restaurantBL.GetRestaurantById(id));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
