using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Domain.Model
{
    public class OptimizedBill
    {
        public IDictionary<string, PersonalPrice> PersonalPrice { get; set; }
        public decimal OptimizedPrice { get; set; }

        public decimal Saved { 
            get
            {
                return this.PersonalPrice.Values.Sum(personalBill => personalBill.Total) - this.OptimizedPrice;
            } 
        }
    }
}
