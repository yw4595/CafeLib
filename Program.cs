/*
 * Author: Yanzhi Wang
 * Purpose: Simulate a cafe where customers can order hot drinks and waiters serve the drinks
 * Restrictions: None
 */

using System;

namespace CafeSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new customer object and take their order
            Customer customer = new Customer("Bob", "1234-5678-9012-3456");
            customer.TakeOrder();
        }
    }

    /*
     * Interface: ITakeOrder
     * Purpose: Provide a contract for classes that can take an order
     * Restrictions: None
     */
    public interface ITakeOrder
    {
        void TakeOrder();
    }

    /*
     * Class: HotDrink
     * Purpose: Represent a hot drink, such as coffee or tea, and provide methods for adding sugar and steaming
     * Restrictions: None
     */
    public class HotDrink
    {
        // Instance variables
        public bool instant;
        public bool milk;
        protected byte sugar;
        public string size;

        // Constructor
        public HotDrink(string brand)
        {
            Console.WriteLine("Preparing hot drink: " + brand);
        }

        // Method for adding sugar to the drink
        public void AddSugar(byte amount)
        {
            sugar = amount;
            Console.WriteLine("Adding {0} spoons of sugar...", amount);
        }

        // Getter and setter for the sugar variable
        public byte Sugar
        {
            get { return sugar; }
            set { sugar = value; }
        }

        // Virtual method for steaming the drink
        public virtual void Steam()
        {
            Console.WriteLine("Steaming milk...");
        }
    }

    /*
     * Class: CupOfCocoa
     * Purpose: Represent a cup of cocoa and provide methods for taking an order and steaming
     * Restrictions: None
     */
    public class CupOfCocoa : HotDrink, ITakeOrder
    {
        // Instance variables
        public int numCups;
        private bool marshmallows;
        private string source;

        // Constructor
        public CupOfCocoa(bool marshmallows) : base("Expensive Organic Brand")
        {
            numCups = 1;
            source = "farmer's market";
            this.marshmallows = marshmallows;
            Console.WriteLine("Preparing cup of cocoa...");
        }

        // Override the Steam method to steam the cocoa instead of milk
        public override void Steam()
        {
            Console.WriteLine("Steaming cocoa...");
        }

        // Implement the ITakeOrder interface's TakeOrder method to prompt the user for input
        public void TakeOrder()
        {
            Console.WriteLine("How many cups of cocoa would you like?");
            numCups = int.Parse(Console.ReadLine());

            Console.WriteLine("Would you like marshmallows? (y/n)");
            string marshmallowsInput = Console.ReadLine();
            marshmallows = marshmallowsInput.Equals("y", StringComparison.InvariantCultureIgnoreCase);

            Console.WriteLine("Where would you like the cocoa sourced from?");
            source = Console.ReadLine();
        }
    }

    /*
     * Class: CupOfCoffee
     * Purpose: Represent a cup of coffee and provide methods for taking an order and steaming
     * Restrictions: None
     */
    public class CupOfCoffee : HotDrink, ITakeOrder
    {
        // Instance variable
        public string beanType;

        // Constructor
        public CupOfCoffee(string brand) : base(brand)
        {
            Console.WriteLine("Preparing cup of coffee...");
        }

        // Override the Steam method to steam the coffee instead of milk
        public override void Steam()
        {
            Console.WriteLine("Steaming coffee...");
        }
        // Implement the ITakeOrder interface's TakeOrder method to prompt the user for input
        public void TakeOrder()
        {
            Console.WriteLine("What size coffee would you like? (small/medium/large)");
            size = Console.ReadLine();

            Console.WriteLine("What type of coffee beans would you like?");
            beanType = Console.ReadLine();

            Console.WriteLine("Would you like milk? (y/n)");
            string milkInput = Console.ReadLine();
            milk = milkInput.Equals("y", StringComparison.InvariantCultureIgnoreCase);

            Console.WriteLine("How many spoons of sugar would you like?");
            byte sugarInput = byte.Parse(Console.ReadLine());
            AddSugar(sugarInput);
        }
    }

    /*
     * Class: Customer
     * Purpose: Represent a customer and allow them to order a hot drink
     * Restrictions: None
     */
    public class Customer
    {
        // Instance variables
        public string name;
        private string creditCardNumber;

        // Constructor
        public Customer(string name, string creditCardNumber)
        {
            this.name = name;
            this.creditCardNumber = creditCardNumber;
            Console.WriteLine("Welcome to the cafe, {0}! Please place your order.", name);
        }

        // Method for taking the customer's order
        public void TakeOrder()
        {
            Console.WriteLine("What would you like to order? (coffee/cocoa)");
            string orderInput = Console.ReadLine();

            ITakeOrder drink = null;

            if (orderInput.Equals("coffee", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What brand of coffee would you like? (Starbucks/Dunkin/Peet's)");
                string brandInput = Console.ReadLine();

                drink = new CupOfCoffee(brandInput);
            }
            else if (orderInput.Equals("cocoa", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Would you like marshmallows in your cocoa? (y/n)");
                string marshmallowsInput = Console.ReadLine();
                bool marshmallows = marshmallowsInput.Equals("y", StringComparison.InvariantCultureIgnoreCase);

                drink = new CupOfCocoa(marshmallows);
            }
            else
            {
                Console.WriteLine("Sorry, we don't serve that here.");
                return;
            }

            drink.TakeOrder();

            Console.WriteLine("Thank you, {0}. Your order has been placed.", name);
        }
    }
}