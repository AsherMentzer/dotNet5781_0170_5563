using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// class with only properties
    /// for defenition of user
    /// </summary>
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
}
