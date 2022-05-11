using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Classes
{
   public class Goods
    {//поля
        string name;
        string mname;
        double price;
        int count;

        //свойства
        public string Name
        { 
            get { return name; }
            set { name = value; } 
        }
        public string Mname
        {
            get { return mname; }
            set { mname = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        //конструкторы
        public Goods()
        {
            name = "";
            mname = "";
            price = 0;
            count = 0;
        }
        public Goods(string name, string mname, double price, int count)
        {
            this.name = name;
            this.mname = mname;
            this.price = price;
            this.count = count;
        }
    }
}
