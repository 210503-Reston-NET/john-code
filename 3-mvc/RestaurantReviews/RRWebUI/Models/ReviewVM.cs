using RRModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RRWebUI.Models
{
    public class ReviewVM
    {
        public ReviewVM()
        {

        }
        public ReviewVM(int restaurantId)
        {
            RestauranId = restaurantId;
        }
        public ReviewVM(Review review)
        {
            RestauranId = review.RestaurantId;
            Rating = review.Rating;
            Description = review.Description;
        }
        public int RestauranId { get; set; }
        [Required]
        [Range(0, 100)]
        public int Rating { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
