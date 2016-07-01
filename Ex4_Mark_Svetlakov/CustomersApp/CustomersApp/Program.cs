using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
    class Program
    {

        public delegate bool CustomerFilter(Customer customer);

        static void Main(string[] args)
        {
            List<Customer> CustomersList = new List<Customer>();
            AnotherCustomerComparer customerComparer = new AnotherCustomerComparer();

            Customer c1 = new Customer("Alon", 100, "Tel-aviv");
            Customer c2 = new Customer(null, -11, null);
            Customer c3 = null;
            Customer c4 = new Customer("alon", 100, "Tel-aviv");

            CustomersList.Add(new Customer("Moshe", 40, "Bat-yam"));
            CustomersList.Add(new Customer("Ziv", 301, "Rishon le zion"));
            CustomersList.Add(new Customer("Barak", 30, "Tel-aviv"));
            CustomersList.Add(new Customer("Rikki", 250, "Haifa"));
            CustomersList.Add(c1);
            CustomersList.Add(c2);
            CustomersList.Add(c3);
            CustomersList.Add(c4);
            CustomersList.Add(new Customer("Elena", 65, "Tel-aviv"));
            CustomersList.Add(new Customer("Porat", 136, "Tel-aviv"));

            CustomersList.Sort();


            //Print before IComparer
            foreach (Customer element in CustomersList)
            {
                Console.WriteLine(element);
            }

            CustomersList.Sort(customerComparer);


            Console.WriteLine("----------After IComparer----------");

            //Print after IComparer
            foreach (Customer element in CustomersList)
            {
                Console.WriteLine(element);
            }




            //Exercise 4
            Console.WriteLine("\nExercise 4:\n");

            FilterForCustomer nameFilter = new FilterForCustomer('a', 'K');

            //Delegate of type CustomerFilter
            CustomerFilter filter = nameFilter.NameFirstLettersFromTo;

            Console.WriteLine($"Customers with name that starts with latters A-K:");

            foreach (Customer customer in GetCustomers(CustomersList, filter))
            {
                Console.WriteLine(customer.Name);
            }


            //Anonymous delegate
            Console.WriteLine($"Customers with name that starts with latters L-Z:");

            foreach (Customer customer in GetCustomers(CustomersList, delegate (Customer customer) { return new FilterForCustomer('L', 'Z').NameFirstLettersFromTo(customer); }))
            {
                Console.WriteLine(customer.Name);
            }


            //Lambda expression
            Console.WriteLine($"Customers whose ID is less than 100:");

            foreach (Customer customer in GetCustomers(CustomersList, customer => customer.ID < 100))
            {
                Console.WriteLine(customer.Name);
            }

        }


        public static ICollection<Customer> GetCustomers(ICollection<Customer> CustomerCollection, CustomerFilter FilterFunction)
        {
            List<Customer> NewCustomerList = new List<Customer>();
            foreach (Customer customer in CustomerCollection)
            {
                if (customer != null)
                {
                    if (customer.ID > 0 || !string.IsNullOrEmpty(customer.Name) || !string.IsNullOrEmpty(customer.Address))
                    {
                        if (FilterFunction.Invoke(customer))
                        {
                            NewCustomerList.Add(customer);
                        }
                    }
                }
            }
            return NewCustomerList;
        }
    }
}