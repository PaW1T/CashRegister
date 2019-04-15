using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CashRegister
{
    public abstract class Article
    {
        public int id { get; set; }
        public abstract int type { get; }
        public String name { get; set; }
        public double retailprice { get; set; }
        public double promprice { get; set; }
        public double promquantity { get; set; }
        public double ammount { get; set; }
        public abstract float getValue();
        public abstract int getTypeId();

        

        public abstract void save();
        public abstract void print();
        public abstract string getFileName(int id);

    }
}
