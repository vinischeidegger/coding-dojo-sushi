using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Model
{
    public class OptimizedBill
    {
        public IList<PersonalPrice> PersonalPrice { get; set; }
    }
}
