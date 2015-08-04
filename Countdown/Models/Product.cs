using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown.Models
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Barcode { get; set; }
        public virtual string Name { get; set; }
        public virtual float Price { get; set; }

        public string Print()
        {
            return String.Format("{0} ${1:n2}{2}", Name, Price, Environment.NewLine);
        }
    }
}