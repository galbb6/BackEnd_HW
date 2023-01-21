

using System;
using System.Data;

namespace AirBnb_Part_2.Models
{
    public class UserProfile
    {

    
        public int UserId { get; set; }
        public string firstName { get; set; }
        public string familyName { get; set; }
        public string email { get; set; }
        public string UserPassword { get; set; }
        public bool isActive { get; set; }
        public bool isAdmin { get ; set ; }
        

        private static List<UserProfile> UsersList = new List<UserProfile>();

        //--------------------------------------------------------------------------------------------------
        // # GET ALL USERS                           
        //--------------------------------------------------------------------------------------------------
        public static List<UserProfile> Read()
        {
            DBservices dbs = new DBservices();
            UsersList = dbs.getUserFromDB();
            return UsersList;
        }

        //--------------------------------------------------------------------------------------------------
        // # INSERT NEW USER                            
        //--------------------------------------------------------------------------------------------------
        public static int Insert(UserProfile profile)
        {

            DBservices dbs = new DBservices();
            return dbs.InsertUserToDB(profile);
        }

        //--------------------------------------------------------------------------------------------------
        // # UPDATE USER PROFILE                          
        //--------------------------------------------------------------------------------------------------

        public static int UpdateUserProfile(UserProfile profile)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateUserToDB(profile);

        }

        //--------------------------------------------------------------------------------------------------
        // # UPDATE USER ACTIVITY                     
        //--------------------------------------------------------------------------------------------------

        public static int setActive(string email, bool isActive )
        {
            DBservices dbs = new DBservices();
            return dbs.setActiveDB(email,isActive);

        }

        //--------------------------------------------------------------------------------------------------
        // # DELETE USER PROFILE                             
        //--------------------------------------------------------------------------------------------------
        public static int DeleteUserProfile(string email)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteUserProfile(email);
        }
        //--------------------------------------------------------------------------------------------------
        // # FIND USER PROFILE                             
        //--------------------------------------------------------------------------------------------------
        public UserProfile GetAccess(string email)
        {
            DBservices dbs = new DBservices();

            return dbs.GetAccessFromDB(email);
        }


        public List<Object> GetAvgOfCities(int month)
        {
            DBservices dbs = new DBservices();

            return dbs.GetAvgOfCitiesFromDB(month);


        }

    }
}
