using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp 
{
    class AnotherCustomerComparer : IComparer<Customer>
    {
        public int Compare(Customer firstCustomer, Customer secondCustomer)
        {

            if (firstCustomer == null)
            {
                if (secondCustomer == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (secondCustomer == null)
                {
                    return 1;
                }
                else
                {
                    int retval = firstCustomer.ID.CompareTo(secondCustomer.ID);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {
                        return firstCustomer.CompareTo(secondCustomer);
                    }
                }
            }
        }
    }

}
