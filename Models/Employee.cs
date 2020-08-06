using System;
using System.Collections.Generic;

namespace CRUDApi.Models
{
    public partial class Employee
    {
        public long id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public long? mobilenumber { get; set; }
        public string roletype { get; set; }
        public string status { get; set; }
    }
}
