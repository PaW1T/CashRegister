using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CashRegister
{
    class Program
    {
        static void Main(string[] args)
        {
            initData();
            run();

        }

        private static void run()
        {
            Console.WriteLine("Welcome to cashier system");
            Console.WriteLine("");
            Console.WriteLine("There are two users in this application:");
            Console.WriteLine("Administrator - login = admin, password = admin;");
            Console.WriteLine("Client - login = client, password = client.");
            Console.WriteLine("");

            Console.WriteLine("Please enter your credentials to log in");

            User user = null;
            while (true)
            {
                Console.WriteLine("Username");
                String username = Console.ReadLine();
                Console.WriteLine("Password");
                String password = Console.ReadLine();
           
                user = getUserForGivenCredentials(username, password);
                if (user == null)
                {
                    Console.WriteLine("User does not exists");
                }
                else
                {
                    //user is exist
                    break;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Welcome, " + user.username);

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Choose one of the following possibilites:");
                user.showPossibilites();
                Console.WriteLine("");
                try
                {
                    int action = Int32.Parse(Console.ReadLine());
                    user.makeAction(action);
                }
                catch (NotFiniteNumberException n)
                {
                    Console.WriteLine("Please enter a valid number");
                }
                catch (FormatException f)
                {
                    Console.WriteLine("Please enter a valid number");

                }

                while (true)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Press enter to choose another possibilites or Q for exit");
                    ConsoleKeyInfo input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else if (input.Key == ConsoleKey.Q)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        private static User getUserForGivenCredentials(String username, String password)
        {

            //since there is no database we check it. With the base it would be different
            if (username.Equals("admin") && password.Equals("admin"))
            {
                return new Administrator(username, password);
            }

            if (username.Equals("client") && password.Equals("client"))
            {
                return new Client(username, password);
            }
            return null;
        }

        private static void initData()
        {
            File.Create(Util.BILLS_FILE_NAME + ".json").Dispose();
            File.Create(Util.ARTICLES_FILE_NAME + ".json").Dispose();
            File.Create(Util.USER_FILE_NAME + ".json").Dispose();

            initArticles();
            initUsers();
        }

        private static void initArticles()
        {

            ArticlePerKg Tangerines = new ArticlePerKg(1, "Tangerines", 30, 80, 3, 1);
            ArticlePerKg Honey = new ArticlePerKg(2, "Honey", 100, 220, 2.5, 1 );
            ArticlePerKg Cables = new ArticlePerKg(3, "Cables", 10, 85, 10, 1);

            List<Article> articles = new List<Article>();
            
            articles.Add(Tangerines);
            articles.Add(Honey);
            articles.Add(Cables);
            Util.saveArticles(articles);
        }

        private static void initUsers()
        {
            Administrator administrator = new Administrator("admin", "admin");
            Client client = new Client("client", "client");

            List<User> users = new List<User>();
            users.Add(administrator);
            users.Add(client);
            Util.saveUsers(users);
        }

    }
}
