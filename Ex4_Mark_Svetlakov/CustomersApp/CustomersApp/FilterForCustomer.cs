using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
    class FilterForCustomer
    {
        public char FromLetter { get; private set; }
        public char ToLetter { get; private set; }

        public FilterForCustomer(char fromLetter, char toLetter)
        {
            this.FromLetter = char.ToUpper(fromLetter);
            this.ToLetter = char.ToUpper(toLetter);
        }


        public bool NameFirstLettersFromTo(Customer customerToCheck)
        {
            if (char.ToUpper(customerToCheck.Name[0]) >= this.FromLetter && char.ToUpper(customerToCheck.Name[0]) <= this.ToLetter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
