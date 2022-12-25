using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using AirBnb_Part_2.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using System.Reflection.PortableExecutable;
using Microsoft.TeamFoundation.Build.WebApi;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;


/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    //--------------------------------------------------------------------------------------------------
    //                                 **********LOGIN*************
    //--------------------------------------------------------------------------------------------------

    //---------------------------------------------------------------------------------
    // Create the login 
    //---------------------------------------------------------------------------------

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    //         **********CONNECTION AND SQL COMMAND ( FEET TO ALL, NOT NEED TO CHANGE  )*************
    //--------------------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }
    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }






    //--------------------------------------------------------------------------------------------------
    //                                 **********FLATS*************
    //--------------------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------------------
    // # GET ALL FLATS                               
    //--------------------------------------------------------------------------------------------------
    public List<Flat> getFlatsFromDB()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Flat> list_flat = new List<Flat>();
        cmd = CreateCommandWithStoredProcedureGetAllFlats("spGetAllFlats", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                Flat f = new Flat();
                f.FlatId = Convert.ToInt32(dataReader["FlatId"]);
                f.City = dataReader["city"].ToString();
                f.Address = dataReader["address"].ToString();
                f.Price = Convert.ToDouble(dataReader["price"]);
                f.NumOfRooms = Convert.ToInt32(dataReader["rooms"]);
                list_flat.Add(f);
            }



        }
        catch (Exception ex) { throw (ex); }
        finally { con.Close(); }
        return list_flat;
    }

    // Create the SqlCommand using a stored procedure to Get All Flats
    private SqlCommand CreateCommandWithStoredProcedureGetAllFlats(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure




        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // # INSERT FLAT                               
    //--------------------------------------------------------------------------------------------------

    // This method inserts a Flat to the Flats table 
    public int InsertFlatToDB(Flat flat)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureInsertFlat("spInsertFlat", con, flat);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    // Create the SqlCommand using a stored procedure to Update Flat
    private SqlCommand CreateCommandWithStoredProcedureInsertFlat(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure


        cmd.Parameters.AddWithValue("@city", flat.City);

        cmd.Parameters.AddWithValue("@address", flat.Address);

        cmd.Parameters.AddWithValue("@price", flat.Price);

        cmd.Parameters.AddWithValue("@rooms", flat.NumOfRooms);




        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // # UPDATE FLAT                               
    //--------------------------------------------------------------------------------------------------

    // This method update a Flat to the Flats table 
    public int UpdateFlatToDB(Flat flat)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureUpdateFlat("spUpdateFlat", con, flat);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    // Create the SqlCommand using a stored procedure to Update Flat
    private SqlCommand CreateCommandWithStoredProcedureUpdateFlat(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@FlatId", flat.FlatId);

        cmd.Parameters.AddWithValue("@city", flat.City);

        cmd.Parameters.AddWithValue("@address", flat.Address);

        cmd.Parameters.AddWithValue("@price", flat.Price);

        cmd.Parameters.AddWithValue("@rooms", flat.NumOfRooms);




        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // # DELETE FLAT                               
    //--------------------------------------------------------------------------------------------------
    public int DeleteFlatFromDB(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureDeleteFlat("spDeleteFlat", con, id);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    // Create the SqlCommand using a stored procedure
    private SqlCommand CreateCommandWithStoredProcedureDeleteFlat(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }





    //--------------------------------------------------------------------------------------------------
    //                                 **********VACATIONS*************
    //--------------------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------------------
    // # INSERT VACATION                               
    //--------------------------------------------------------------------------------------------------

    // This method inserts a vacation to the vacations table 
    public int InsertVacationToDB(Vacation vacation)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureInsertVacation("spInsertVacation", con, vacation);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    // Create the SqlCommand using a stored procedure to Update Vacation
    private SqlCommand CreateCommandWithStoredProcedureInsertVacation(String spName, SqlConnection con, Vacation vacation)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure


        cmd.Parameters.AddWithValue("@EndDate", vacation.EndDate);

        cmd.Parameters.AddWithValue("@StartDate", vacation.StartDate);

        cmd.Parameters.AddWithValue("@email", vacation.email);

        cmd.Parameters.AddWithValue("@FlatId", vacation.FlatId);




        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // # UPDATE VACATION                               
    //--------------------------------------------------------------------------------------------------
    public int UpdateVacationToDB(Vacation vacation)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureUpdateVacation("spUpdateVacation", con, vacation);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    // Create the SqlCommand using a stored procedure to UPDATE VACATION  
    private SqlCommand CreateCommandWithStoredProcedureUpdateVacation(String spName, SqlConnection con, Vacation vacation)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@id", vacation.id);

        cmd.Parameters.AddWithValue("@EndDate", vacation.EndDate);

        cmd.Parameters.AddWithValue("@StartDate", vacation.StartDate);

        cmd.Parameters.AddWithValue("@email", vacation.email);

        cmd.Parameters.AddWithValue("@FlatId", vacation.FlatId);




        return cmd;
    }
    //--------------------------------------------------------------------------------------------------
    // # GET ALL VACATIONS                              
    //--------------------------------------------------------------------------------------------------
    public List<Vacation> getVacationFromDB()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Vacation> list_vacations = new List<Vacation>();
        cmd = CreateCommandWithStoredProcedureGetAllvacations("spGetAllVacations", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                Vacation v = new Vacation();
                v.id = Convert.ToInt32(dataReader["id"]);
                v.email = Convert.ToString(dataReader["email"]);
                v.FlatId = Convert.ToInt32(dataReader["FlatId"]);
                v.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
                v.EndDate = Convert.ToDateTime(dataReader["EndDate"]);
                list_vacations.Add(v);
            }



        }
        catch (Exception ex) { throw (ex); }
        finally { con.Close(); }
        return list_vacations;
    }


    // Create the SqlCommand using a stored procedure to GET ALL VACATIONS   

    private SqlCommand CreateCommandWithStoredProcedureGetAllvacations(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure




        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // # DELETE VACATION                               
    //--------------------------------------------------------------------------------------------------

    public int DeleteVacationFromDB(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureDeleteVacation("spDeleteVacation", con, id);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    private SqlCommand CreateCommandWithStoredProcedureDeleteVacation(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }





    //--------------------------------------------------------------------------------------------------
    //                                 **********USERS*************
    //--------------------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------------------
    // # GET ALL USERS                               
    //--------------------------------------------------------------------------------------------------


    public List<UserProfile> getUserFromDB()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<UserProfile> list_users = new List<UserProfile>();
        cmd = CreateCommandWithStoredProcedureGetAllUsers("spGetAllUsers", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                UserProfile u = new UserProfile();
                u.UserId = Convert.ToInt32(dataReader["UserId"]);
                u.UserPassword = Convert.ToString(dataReader["UserPassword"]);
                u.familyName = Convert.ToString(dataReader["familyName"]);
                u.firstName = Convert.ToString(dataReader["firstName"]);
                u.email = Convert.ToString(dataReader["email"]);
                list_users.Add(u);
            }



        }
        catch (Exception ex) { throw (ex); }
        finally { con.Close(); }
        return list_users;
    }


    // Create the SqlCommand using a stored procedure to GET ALL USERS   

    private SqlCommand CreateCommandWithStoredProcedureGetAllUsers(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // # INSERT USER TO DB                              
    //--------------------------------------------------------------------------------------------------

    public int InsertUserToDB(UserProfile profile)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureInsertUser("spInsertUser", con, profile);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    // Create the SqlCommand using a stored procedure to Insert User Profile
    private SqlCommand CreateCommandWithStoredProcedureInsertUser(String spName, SqlConnection con, UserProfile profile)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure


        cmd.Parameters.AddWithValue("@firstName", profile.firstName);

        cmd.Parameters.AddWithValue("@familyName", profile.familyName);

        cmd.Parameters.AddWithValue("@email", profile.email);

        cmd.Parameters.AddWithValue("@UserPassword", profile.UserPassword);




        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // # Update USER IN DB                              
    //--------------------------------------------------------------------------------------------------
    public int UpdateUserToDB(UserProfile profile)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureUpdateUser("spUpdateUser", con, profile);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    // Create the SqlCommand using a stored procedure to UPDATE VACATION  
    private SqlCommand CreateCommandWithStoredProcedureUpdateUser(String spName, SqlConnection con, UserProfile profile)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@Userid", profile.UserId);

        cmd.Parameters.AddWithValue("@firstName", profile.firstName);

        cmd.Parameters.AddWithValue("@familyName", profile.familyName);

        cmd.Parameters.AddWithValue("@email", profile.email);

        cmd.Parameters.AddWithValue("@UserPassword", profile.UserPassword);




        return cmd;
    }
    //--------------------------------------------------------------------------------------------------
    // # DELETE USER From DB                              
    //--------------------------------------------------------------------------------------------------



    public int DeleteUserProfile(string email)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

        cmd = CreateCommandWithStoredProcedureDeleteUser("spDeleteUser", con, email);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    private SqlCommand CreateCommandWithStoredProcedureDeleteUser(String spName, SqlConnection con, string email)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@email", email);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // # GET ACCESS USER From DB                              
    //--------------------------------------------------------------------------------------------------

    public UserProfile GetAccessFromDB(string email, string password)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string
        UserProfile tempUser = new UserProfile();
        cmd = CreateCommandWithStoredProcedureGetAccess("spGetUserAccess", con, email, password);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {

                tempUser.UserId = Convert.ToInt32(dataReader["UserId"]);
                tempUser.UserPassword = Convert.ToString(dataReader["UserPassword"]);
                tempUser.familyName = Convert.ToString(dataReader["familyName"]);
                tempUser.firstName = Convert.ToString(dataReader["firstName"]);
                tempUser.email = Convert.ToString(dataReader["email"]);

            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        return tempUser;
    }
    


    private SqlCommand CreateCommandWithStoredProcedureGetAccess(String spName, SqlConnection con, string email, string password)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password", password);

        return cmd;
    }



}