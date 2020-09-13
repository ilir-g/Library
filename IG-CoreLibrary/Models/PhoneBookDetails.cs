using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG_CoreLibrary.Models
{
    public class PhoneBookDetails 
    {
        public Type PhoneType { get; set; }
        public string Number { get; set; }
    }

    public enum Type
    {
        Work, Cellphone, Home
    }
}
