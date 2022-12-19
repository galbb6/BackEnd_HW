using System.Security.Claims;

namespace AirBnb_Part_2.Models
{
    public class User
    {

    
        public int UserId { get; set; }
        public string firstName { get; set; }
        public string familyName { get; set; }
        public string email { get; set; }
        public string UserPassword { get; set; }

        private static List<User> UsersList = new List<User>();


        public static List<User> Read()
        {
            DBservices dbs = new DBservices();
            UsersList = dbs.getUserFromDB();
            return UsersList;
        }








    }
}
