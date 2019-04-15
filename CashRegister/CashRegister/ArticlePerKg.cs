using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CashRegister
{
    class ArticlePerKg : Article
    {

        public override int type {
            get {
                return 1;
            }
        }
        public ArticlePerKg() { }


        public ArticlePerKg(int id, String name, double retailprice, double promprice, double promquantity, double ammount)
        {
            this.id = id;
            this.name = name;
            this.retailprice = retailprice;
            this.promprice = promprice;
            this.promquantity = promquantity;
            this.ammount = ammount;
        }

        public override float getValue()
        {
            return (float)((int)(ammount/promquantity)* promprice + (ammount%promquantity) * retailprice);
        }

        public override void save()
        {
            List<Article> articles = Util.getArticles();
            if (articles == null)
            {
                articles = new List<Article>();
            }
            articles.Add(this);
            Util.saveArticles(articles);
        }

        public override void print()
        {
            String json = JsonConvert.SerializeObject(this);
            Console.WriteLine(json);
        }

        public override int getTypeId()
        {
            return 1;
        }

        public override string getFileName(int id)
        {
            return "articlePerKg" + id + ".json";
        }
    }
}
