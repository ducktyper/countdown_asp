using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public float Amount { get; set; }

        public string Print()
        {
            return String.Format("{0} -${1:n2}{2}", Product.Name, Amount, Environment.NewLine);
        }
    }
}