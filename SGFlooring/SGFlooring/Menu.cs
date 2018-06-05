using SGFlooring.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring
{
    public class Menu
    {
        public static void SplashScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("***************************************");
            Console.WriteLine(@"                 _______     ______   ");
            Console.WriteLine(@"          ______/*******\___/******\  ");
            Console.WriteLine(@"         /**************************| ");
            Console.WriteLine(@"        /***____********____********| ");
            Console.WriteLine(@"       /***/    | [**] |    \______/  ");
            Console.WriteLine(@"      /***/     | [**] |              ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                | [**] |              ");
            Console.WriteLine(@"                \______/         N.A.B");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   Software Guild Flooring Company     ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("***************************************");
            Console.ReadKey();
        }
        public static void Start()
        {
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("SGFlooring Order Menu");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("*****************************");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. Display Order Information");
                Console.WriteLine("2. Add A New Order");
                Console.WriteLine("3. Edit An Existing Order");
                Console.WriteLine("4. Remove An Existing Order");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nQ to quit");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter Selection:");
                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "1":
                        DisplayOrderWorkflow displayOrder = new DisplayOrderWorkflow();
                        displayOrder.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow addNewOrder = new AddOrderWorkflow();
                        addNewOrder.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow editOrder = new EditOrderWorkflow();
                        editOrder.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkflow removeOrder = new RemoveOrderWorkflow();
                        removeOrder.Execute();
                        break;
                    case "Q":
                        return;
                }
            }
        }
    }
}

