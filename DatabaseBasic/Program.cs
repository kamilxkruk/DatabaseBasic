using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseBasic.DataFramework;
using DatabaseBasic.DataFramework.Model;

namespace DatabaseBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            var contactsDAL = new ContactDAL();

            Console.WriteLine("Podaj mi imię kontaktów które chcesz pobrać: ");

            var contactName = Console.ReadLine();

            var contacts = contactsDAL.GetContactsByName(contactName);

            foreach (var contact in contacts)
            {
                Console.WriteLine(contact);

            }

            Console.ReadLine();
        }
    }
}
