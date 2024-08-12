using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics;
namespace Shopping_System_Project
{
    internal class Program
    {
        static public List <string> Cartitem = new List <string> (); // user shopping list
        static public Dictionary<string , double> itemPrice = new Dictionary<string, double>()
        
        {
            {"Camera" , 1200 },
            {"Laptop" , 3000 },
            {"Mobile" , 25000 }
        }; // stock
        static Stack<string> Undo = new Stack<string>();

        static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine("Welcome to the shopping system");
                Console.WriteLine("==============================");
                Console.WriteLine("1. Add item in cart");
                Console.WriteLine("2. View cart ");
                Console.WriteLine("3. Remove item");
                Console.WriteLine("4. Checkout");
                Console.WriteLine("5. Undo last action");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter Your Choise Number : ");

                string choise = Console.ReadLine();
                int intchoise = Convert.ToInt32(choise);

                switch (intchoise)
                {
                    case 1:
                        AddItem();
                        break;
                    case 2:
                        ViewCart();
                        break;
                    case 3:
                        RemoveItem();
                        break;
                    case 4:
                        Checkout();
                        break;
                    case 5:
                        UndoLastAction();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choise TA ");
                        break;
                }
            }
            
        }

        private static void AddItem()
        {
            Console.WriteLine("Item Available");
            foreach (var item in itemPrice)
            {
                Console.WriteLine($"Item : {item.Key} Price : {item.Value}");
                
            }
            Console.WriteLine("Please Enter Your Name Product");
            string ChoiseProduct = Console.ReadLine();

            if (itemPrice.ContainsKey(ChoiseProduct)) // ContainValue
            {
                Cartitem.Add(ChoiseProduct);
                Undo.Push($"Item Added{ChoiseProduct}");
                Console.WriteLine($"Item {ChoiseProduct} Added");
            }
            else
            {
                Console.WriteLine("Item Is Out Of Stock");
            }


        }
        private static void ViewCart()
        {
           
            if (Cartitem.Any()) // Any() if cart conatin anything or no
            {
                Console.WriteLine("Your Cart Item : ");
                foreach (var item in Cartitem)
                {
                    
                    Console.WriteLine($"Item : {item}");
                }
            }
            else
            {
                Console.WriteLine("Your Card Is Empty");
            }
        }
        private static void RemoveItem()
        {
            ViewCart();
            if (Cartitem.Any()) {
                Console.WriteLine("Please Select Item To Remove : ");
                string itemtoremove=Console.ReadLine();
                if (Cartitem.Contains(itemtoremove)) {
                    Cartitem.Remove(itemtoremove);
                    Undo.Push($"Item Removed {itemtoremove}");
                    Console.WriteLine($"Item {itemtoremove} Removed ");
                }
                else
                {
                    Console.WriteLine("item does't exist in UR cart");
                }
            }
        }
        private static void Checkout()
        {
            if (Cartitem.Any()) 
            {
                double totalprice = 0;

                Console.WriteLine("Ur Cart Item Are : ");

                IEnumerable<Tuple<string,double>> itemIncart = GetCartPrice();
                foreach (var item in itemIncart) { 
                    totalprice+= item.Item2;
                    Console.WriteLine(item.Item1 + " - " + item.Item2);
                }
                Console.WriteLine($"Total Price : {totalprice}");
                Console.WriteLine("Go To Payment");

                Cartitem.Clear();
            }
            else
            {
                Console.WriteLine("Ur Card Is Empty");
            }
        }
        private static IEnumerable<Tuple<string,double>> GetCartPrice()
        {
            var cartprices = new List<Tuple<string,double>>();
            foreach (var item in Cartitem) { 
                double price = 0;
                bool founditem = itemPrice.TryGetValue(item, out price);
                if (founditem) { 
                    Tuple<string,double>itemPrice = new Tuple<string, double>(item, price);
                    cartprices.Add(itemPrice);
                }
            }
            return cartprices;
        }

        private static void UndoLastAction()
        {
            if (Undo.Count > 0) { 
                string lastaction = Undo.Pop();
                Console.WriteLine($"Ur Last Action {lastaction}");
                var actionarr = lastaction.Split();
                if (lastaction.Contains("Added"))
                {
                    Cartitem.Remove(actionarr[1]);
                }else if (lastaction.Contains("Removed"))
                {
                    Cartitem.Add(actionarr[1]);
                }
                else
                {
                    Console.WriteLine("Can't UNDO");
                }
            }
        }


        
    }
}
