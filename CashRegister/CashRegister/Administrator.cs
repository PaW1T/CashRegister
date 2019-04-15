using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    class Administrator : User
    {

        public Administrator(String username, String password)
        {
            this.username = username;
            this.password = password;
            this.userRole = UserPermission.role.ADMIN;
        }

        public void defineNewArticle()
        {
            Console.WriteLine("Enter article name");
            string name = Console.ReadLine();
            if (name == null || name.Length == 0)
            {
                Console.WriteLine("Article name is required");

            }

            Console.WriteLine("Enter article retail price");
            string retailprice = Console.ReadLine();
            if (retailprice == null || retailprice.Length == 0)
            {
                Console.WriteLine("Article retail price is required");
            }

            Console.WriteLine("Enter article promoution price");
            string promprice = Console.ReadLine();
            if (promprice == null || promprice.Length == 0)
            {
                Console.WriteLine("Article promoution price is required");
            }

            Console.WriteLine("Enter article promoution quantity");
            string promquantity = Console.ReadLine();
            if (promquantity == null || promquantity.Length == 0)
            {
                Console.WriteLine("Article promoution quantity is required");
            }
            ArticlePerKg articlePerKg = new ArticlePerKg(Util.getGlobalIdCounter(), name, double.Parse(retailprice), double.Parse(promprice), double.Parse(promquantity), 1);
            articlePerKg.save();
            Console.WriteLine("Article created:");
            articlePerKg.print();
            

           
        }

        public override void printExtraPossibilites()
        {
            Console.WriteLine("Admin possibilities:");
            Console.WriteLine((int)UserPossibilities.DEFINE_NEW_ARTICLE + " - define new article");
            Console.WriteLine((int)UserPossibilities.REDEFINE_BILL + " - redefine bill");
            Console.WriteLine((int)UserPossibilities.PRINT_USERS + " - show all users");

        }



        public void redefineBill()
        {
            Console.WriteLine("Enter id of bill to redefine");
            int id = Int32.Parse(Console.ReadLine());

            Bill bill = Util.getBillById(id);
            if (bill == null)
            {
                Console.WriteLine("Bill with id " + id + "does not exists");
            }
            else
            {
                bill.printBill();
                Console.WriteLine("Enter id of article to be removed or Q to cancel");
                string input = Console.ReadLine();

                if (input == "q")
                {
                    return;
                }
                else
                {
                    int articleId = -1;
                    try
                    {
                        articleId = Int32.Parse(input);
                    }
                    catch (FormatException f) { }
                    bill.removeArticle(articleId);
                    List<Bill> bills = Util.getBills();
                    List<Bill> redefinedList = new List<Bill>();

                    foreach (Bill billR in bills)
                    {
                        if (billR.id != id)
                        {

                            redefinedList.Add(billR);
                        }
                    }

                    redefinedList.Add(bill);
                    Util.saveBills(redefinedList);
                    Console.WriteLine("Bill with id " + id + " is redefined");
                }
            }

        }

        public override void makeAction(int action)
        {
            switch (action)
            {
                case (int)UserPossibilities.REDEFINE_BILL:
                    Console.WriteLine("Redefine bill choosen");
                    redefineBill();
                    return;
                case (int)UserPossibilities.DEFINE_NEW_ARTICLE:
                    Console.WriteLine("Define new article choosen");
                    defineNewArticle();
                    return;
                case (int)UserPossibilities.PRINT_USERS:
                    Console.WriteLine("Print users choosen");
                    Util.printUsers();
                    return;
            }
            base.makeAction(action);
        }

    }
}
