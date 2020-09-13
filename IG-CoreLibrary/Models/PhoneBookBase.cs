using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG_CoreLibrary.Models
{
    public class PhoneBookBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<PhoneBookDetails> L_PhoneBook { get; set; }

        public PhoneBookBase() 
        {
            L_PhoneBook = new List<PhoneBookDetails>();
        }
     
    }
}
