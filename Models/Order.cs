using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public string UserNumber { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [BindNever]
        public bool Shipped {get; set;}

        [Required(ErrorMessage = "Please enter your credit card number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please enter the expiration month")]
        public string EMonth { get; set; }

        [Required(ErrorMessage = "Please enter the expiration year")]
        public string EYear { get; set; }

        [Required(ErrorMessage = "Please enter the CVV")]
        public int CVV { get; set; }
        
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter in your primary address in the first line")]
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter in your city name")]
            public string City { get; set; }

        [Required(ErrorMessage = "Please enter in your state name")]
            public string State { get; set; }
            public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter in your country name")]
            public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
