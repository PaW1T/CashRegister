using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CashRegister
{
    class User : UserInterface
    {
        public int id = Util.getGlobalIdCounter();
        public String password { get; set; }
        public String username { get; set; }

        public UserPermission.role userRole { get; set; }
        public Bill createBill()
        {
            Bill bill = new Bill();
            bill.save();
            Console.WriteLine("Bill with id: " + bill.id + " was created");
            return bill;
        }

        public int getId()
        {
            return id;
        }

        public void printBill()
        {
            Console.WriteLine("Please enter bill id");
            int id = Int32.Parse(Console.ReadLine());
            List<Bill> bills = null;
            using (StreamReader reader = File.OpenText(Util.BILLS_FILE_NAME + ".json"))
            {
                string json = reader.ReadToEnd();
                bills = JsonConvert.DeserializeObject<List<Bill>>(json, new ArticleCreationConverter());
            }
            if (bills == null)
            {
                bills = new List<Bill>();
            }

            foreach (Bill b in bills)
            {
                if (b.id == id) {
                    Console.WriteLine("Bill:" + b.id + " " + b.getDate() + " " + b.getArticlesCount() + " " + b.getSumPrice() + "$");
                }
            }

            //using (StreamReader file = File.OpenText(Util.BILLS_FILE_NAME + ".json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    Bill bill = (Bill)serializer.Deserialize(file, typeof(Bill));
            //    if (bill != null)
            //    {
            //        string json = JsonConvert.SerializeObject(bill);
            //        Console.WriteLine(json);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Bill does not exists");
            //    }
            //}

        }

        public void addNewArticleToBill()
        {

            Console.WriteLine("Enter bill id");
            int billId = Int32.Parse(Console.ReadLine());
            List<Bill> bills = Util.getBills();
            Bill bill = null;
            foreach (Bill b in bills)
            {
                if (b.id == billId)
                {
                    bill = b;
                    break;
                }
            }
            if (bill == null)
            {
                Console.WriteLine("Bill with given id does not exists");
                return;
            }


            Console.WriteLine("Enter article id");
            Console.WriteLine("1 - Tangerines");
            Console.WriteLine("2 - Honey");
            Console.WriteLine("3 - Cables");

            int articleId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter amount of this article");
            double ammountt = double.Parse(Console.ReadLine());

            List<Article> articlesSaved = Util.getArticles();
            foreach (Article article in articlesSaved)
            {
                if (article.id == articleId)
                {
                    article.ammount = ammountt;
                    bill.addArticle(article);
                }
            }
            Console.WriteLine("Article with id: " + articleId + " is added to bill with id: " + billId);
            Util.saveBills(bills);
        }

        public void removeArticleFromBill()
        {

           
            Console.WriteLine("Enter bill id");
            int billId = Int32.Parse(Console.ReadLine());
            List<Bill> bills = Util.getBills();
            Bill bill = null;
            foreach (Bill b in bills)
            {
                if (b.id == billId)
                {
                    bill = b;
                    break;
                }
            }
            if (bill == null)
            {
                Console.WriteLine("Bill with given id does not exists");
                return;
            }


            Console.WriteLine("Enter article id");
            int articleId = Int32.Parse(Console.ReadLine());

            bill.removeArticle(id);

            Console.WriteLine("Article with id: " + articleId + " is removed to bill with id: " + billId);
            Util.saveBills(bills);
        }

        public void showPossibilites()
        {
            Console.WriteLine((int)UserPossibilities.CREATE_NEW_BILL + " - create new bill");
            Console.WriteLine((int)UserPossibilities.PRINT_BILL_WITH_PRICE + " - print bill with price");
            Console.WriteLine((int)UserPossibilities.ADD_NEW_ARTICLE_TO_BILL + " - add new article to bill");
            Console.WriteLine((int)UserPossibilities.PRINT_DAILY_BILL_REPORT + " - print daily bill report");
            Console.WriteLine((int)UserPossibilities.PRINT_DAILY_ARTICLES_REPORT + " - print daily articles report");

            printExtraPossibilites();
        }

        public virtual void printExtraPossibilites() { }
        public virtual void printUser()
        {
            string json = JsonConvert.SerializeObject(this);
            Console.WriteLine(json);
        }

        public virtual void makeAction(int action)
        {

            switch (action)
            {
                case (int)UserPossibilities.CREATE_NEW_BILL:
                    Console.WriteLine("Create new bill choosen");
                    createBill();
                    break;
                case (int)UserPossibilities.PRINT_BILL_WITH_PRICE:
                    Console.WriteLine("Print bill with price choosen");
                    printBill();
                    break;
                case (int)UserPossibilities.ADD_NEW_ARTICLE_TO_BILL:
                    Console.WriteLine("Add new article to bill choosen");
                    addNewArticleToBill();
                    break;
                case (int)UserPossibilities.PRINT_DAILY_BILL_REPORT:
                    Console.WriteLine("Print daily bill report choosen");
                    Util.printDailyBillReport();
                    break;
                case (int)UserPossibilities.PRINT_DAILY_ARTICLES_REPORT:
                    Console.WriteLine("Print daily articles report choosen");
                    Util.printDailyArticlesReport();
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
        }
    }
}
