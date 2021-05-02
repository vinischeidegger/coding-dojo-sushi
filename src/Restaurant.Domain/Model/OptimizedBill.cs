using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Model
{
    public class OptimizedBill
    {
        public IDictionary<string, PersonalPrice> PersonalPrice { get; set; }
    }
}
