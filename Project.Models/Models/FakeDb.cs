using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuySite.Models
{
    public class FakeDb
    {
        public List<FakeUser> User { set; get; }

    }
    public class FakeUser
    {
        public int id { get;  set; }
        public string Name { get;  set; }
        public string Account { get;  set; }
        public string Password { get;  set; }
        public string[] Roles { get; set; }
        public int Salary { get; set; }
    }
}
