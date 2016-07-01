using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
    class Customer : IComparable<Customer>, IEquatable<Customer>
    {
        public string Name { get; private set; }
        public int ID { get; private set; }
        public string Address { get; private set; }

        public Customer (string name, int id, string address)
        {
            this.Name = name;
            this.ID = id;
            this.Address = address;
        }

        public int CompareTo(Customer other)
        {
            if (object.ReferenceEquals(other, null) || string.IsNullOrEmpty(other.Name))
            {
                return 1;
            }
            else if (string.IsNullOrEmpty(this.Name))
            {
                return 1;
            }
            else
            {
                return Name.CompareTo(other.Name);
            }
        }


        public bool Equals(Customer other)
        {
            if (object.ReferenceEquals(other, null) || string.IsNullOrEmpty(other.Name))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(this.Name))
            {
                return false;
            }
            else if (this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase) && this.ID == other.ID)
            {
                return true;
            }
            return false;
        }


        public override bool Equals(object obj)
        {
            var item = obj as Customer;

            if (item == null)
            {
                return false;
            }

            return this.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }


        public override string ToString()
        {
            if (this.Address != null && this.Name != null)
            {
                return "ID: "+this.ID+ ", Name: " + this.Name.ToString() + ", Address: " + this.Address;
            }
            else
            {
                return "Object has empty arguments";
            }
        }
    }
}
