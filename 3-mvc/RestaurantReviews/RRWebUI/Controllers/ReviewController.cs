using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRBL;
using RRModels;
using RRWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRWebUI.Controllers
{
    public class ReviewController : Controller
    {
        private IRestaurantBL _restaurantBL;
        private IReviewBL _reviewBL;

        public ReviewController(IRestaurantBL restaurantBL, IReviewBL reviewBL)
        {
            _restaurantBL = restaurantBL;
            _reviewBL = reviewBL;
        }
        // GET: ReviewController
        public ActionResult Index(int id)
        {
            //Viewbag and ViewData 
            // Viewbag, dynamically infers a type....
            // Viewdata stores everything as an object
            // These two sharwe the same memory, they're both dictionary types that store things
            // in a key value manner
            // These two last 1 req/res lc
            ViewBag.Restaurant = _restaurantBL.GetRestaurantById(id);
            Tuple<List<Review>, int> result = _reviewBL.GetReviews(_restaurantBL.GetRestaurantById(id));
            if (result.Item1.Count > 0)
            {
                ViewData.Add("OverallRating", result.Item2);
            }
            else
            {
                ViewData.Add("OverallRating", "No reviews yet");
            }
            return View(result.Item1.Select(review => new ReviewVM(review)).ToList());
        }

        // GET: ReviewController/Create
        public ActionResult Create(int id)
        {
            return View(new ReviewVM(id));
        }

        // POST: ReviewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewVM review)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _reviewBL.AddReview(_restaurantBL.GetRestaurantById(review.RestauranId), new Review { Rating = review.Rating, Description = review.Description });
                    return RedirectToAction(nameof(Index), new { id = review.RestauranId });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

      
    }
}
