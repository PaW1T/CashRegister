using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    class Client : User
    {
        public Client(String username, String password)
        {
            this.username = username;
            this.password = password;
            this.userRole = UserPermission.role.CLIENT;

        }

        public override void printExtraPossibilites()
        {
            Console.WriteLine("Client possibilities");

        }

        public override void makeAction(int action)
        {

            base.makeAction(action);
        }
    }
}
