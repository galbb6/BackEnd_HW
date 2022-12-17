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
                f.Id = Convert.ToInt32(dataReader["id"]);
                f.City = dataReader["city"].ToString();
                f.Address = dataReader["address"].ToString();
                f.Price = Convert.ToDouble(dataReader["price"]);
                f.NumOfRooms = Convert.ToInt32(dataReader["rooms"]);
                list_flat.Add(f);
            }


              
        }       
        catch(Exception ex) { throw (ex); }
        finally { con.Close(); }
        return list_flat;
    }

    //----------------------------------------------------------------
    // Create the SqlCommand using a stored procedure to Get All Flats
    //----------------------------------------------------------------
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

    //--------------------------------------------------
    // This method inserts a Flat to the Flats table 
    //--------------------------------------------------
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








    //----------------------------------------------------------------
    // Create the SqlCommand using a stored procedure to Update Flat
    //----------------------------------------------------------------
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

    //----------------------------------------
    // Build the Insert Flat command String
    //----------------------------------------
    private String BuildInsertCommand(Flat flat)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', {2}, {3})", flat.City, flat.Address, flat.Price, flat.NumOfRooms);
        String prefix = "INSERT INTO Flats_2022 " + "([city],[address],[price],[rooms]) ";
        command = prefix + sb.ToString();

        return command;
    }
    

    //--------------------------------------------------------------------------------------------------
    // # UPDATE FLAT                               
    //--------------------------------------------------------------------------------------------------

    //--------------------------------------------------
    // This method update a Flat to the Flats table 
    //--------------------------------------------------
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
    //----------------------------------------------------------------
    // Create the SqlCommand using a stored procedure to Update Flat
    //----------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureUpdateFlat(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@id", flat.Id);

        cmd.Parameters.AddWithValue("@city", flat.City);

        cmd.Parameters.AddWithValue("@address", flat.Address);

        cmd.Parameters.AddWithValue("@price", flat.Price);

        cmd.Parameters.AddWithValue("@rooms", flat.NumOfRooms);




        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // # GET FLAT                               
    //--------------------------------------------------------------------------------------------------




    //--------------------------------------------------------------------------------------------------
    // # DELETE FLAT                               
    //--------------------------------------------------------------------------------------------------

    //---------------------------------------
    // TODO Build the Flat Delete  method
    // DeleteFlight(int id)
    //---------------------------------------
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

    //-------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //-------------------------------------------------
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

    //---------------------------------------------------------
    // This method inserts a Vacation to the Vacations table 
    //---------------------------------------------------------
    public int InsertVacation(Vication vacation)
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

        String cStr = BuildInsertCommand(vacation);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

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

    //----------------------------------------
    // Build the Insert Vacation command String
    //----------------------------------------
    private String BuildInsertCommand(Vication vacation)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0}, {1}, '{2}', '{3}')", vacation.UserId, vacation.FlatId, vacation.StartDate, vacation.EndDate);
        String prefix = "INSERT INTO Vacations_2022 " + "([UserId],[FlatId],[StartDate],[EndDate]) ";
        command = prefix + sb.ToString();

        return command;
    }


    //--------------------------------------------------------------------------------------------------
    // # UPDATE VACATION                               
    //--------------------------------------------------------------------------------------------------


    //--------------------------------------------------------------------------------------------------
    // # GET VACATION                               
    //--------------------------------------------------------------------------------------------------


    //--------------------------------------------------------------------------------------------------
    // # DELETE VACATION                               
    //--------------------------------------------------------------------------------------------------



















}
