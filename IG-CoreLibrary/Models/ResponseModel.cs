using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG_CoreLibrary.Models
{
    public class ResponseModel<T> where T : class
    {
        public T item { get; set; }

        public string Messagge { get; set; }
        public bool HasError { get; set; }
    }
}
