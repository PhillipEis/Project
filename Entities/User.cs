using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMock.Entities
{
    public class User
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? SubID { get; set; }
        public string? SubItemID { get; set; }
        public Cantines Cantine { get; set; }


        public static User CurrentUser { get; set; }

        public User(string userId, string name, string email, string phone, string? subid, string? subitemid, Cantines cantine)
        {
            UserID = userId;
            Name = name;
            Email = email;
            Phone = phone;
            SubID = subid;
            SubItemID = subitemid;
            Cantine = cantine;
        }
    }
}
