using RRModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RRWebUI.Models
{
    /// <summary>
    /// Restaurant View Model. This contains the neccessary information I want presented to the
    /// end user, or some info that is vital to data processing (i.e the id)
    /// </summary>
    public class RestaurantVM
    {
        public RestaurantVM(Restaurant restaurant)
        {
            Id = restaurant.Id;
            Name = restaurant.Name;
            City = restaurant.City;
            State = restaurant.State;
        }
        public RestaurantVM()
        {

        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [DisplayName("State or Province")]
        [Required]
        public string State { get; set; }
    }
}
