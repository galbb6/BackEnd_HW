using System.Net;

namespace AirBnb_Part_2.Models
{
    public class Flat
    {

        private double LIB_TO_DOLLAR = 3.55;
        public int FlatId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public int NumOfRooms { get; set; }

        private static List<Flat> FlatList = new List<Flat>();


        //--------------------------------------------------------------------------------------------------
        // # CHECK IF THERE ALREADY THIS FLAT  - mabye we dont need it                              
        //--------------------------------------------------------------------------------------------------
        public bool CheckFlatId()
        {
            foreach (Flat item in FlatList)
            {
                if (item.FlatId == FlatId)
                {

                    return false;
                   
                }
            }
            return true;

        }

        //--------------------------------------------------------------------------------------------------
        // # RETURN FLATS WHERE CITY == CITY AND PRICE <= PRICE                               
        //--------------------------------------------------------------------------------------------------

        public static List<Flat> getCityPrice(string city,double price)
        {
            List<Flat> tempList = new List<Flat>();
            price = price / 3.55;

            DBservices dbs = new DBservices();
            FlatList =  dbs.getFlatsFromDB();

            foreach (Flat item in FlatList)
            {
                if (item.City==city&&item.Price<=price)
                {
                    tempList.Add(item);
                }

            }

            return tempList;
        }


        //--------------------------------------------------------------------------------------------------
        // # RETURN FLATS LIST                                
        //--------------------------------------------------------------------------------------------------

        public static List<Flat> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.getFlatsFromDB();

        }

        //--------------------------------------------------------------------------------------------------
        // # DISCOUNT FLAT                               
        //--------------------------------------------------------------------------------------------------

        public double Discount(double Price , int NumOfRooms)
        {
            if (NumOfRooms > 1 && Price >= 100)
            {
                double AfterPrice = Price - Price * 0.1;
                return AfterPrice;
            }
            return Price;

        }


        //--------------------------------------------------------------------------------------------------
        // # INSERT FLAT                               
        //--------------------------------------------------------------------------------------------------
        public int InsertFlat(Flat flat)
        {
            this.Price = this.Price / LIB_TO_DOLLAR;
            this.Price = Discount(this.Price, this.NumOfRooms);

            this.Price = Math.Round(this.Price);
            FlatList.Add(this);
            DBservices dbs = new DBservices();

            return dbs.InsertFlatToDB(flat);
        }
        //--------------------------------------------------------------------------------------------------
        // # UPDATE FLAT                               
        //--------------------------------------------------------------------------------------------------

        public int UpdateFlat(Flat flat)
        {
           

            DBservices dbs = new DBservices();
            return dbs.UpdateFlatToDB(flat);
        }
        //--------------------------------------------------------------------------------------------------
        // # DELETE FLAT                               
        //--------------------------------------------------------------------------------------------------
        public int DeleteFlat(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteFlatFromDB(id);
        }


        //--------------------------------------------------------------------------------------------------
        // # INSERT FLAT TO LIST                              
        //--------------------------------------------------------------------------------------------------

        //public bool Insert()
        //{
            
        //    try
        //    {
        //        if (FlatList != null)
        //        {

               
        //          foreach (Flat item in FlatList)
        //          {
        //            if (this.Id == item.Id)
        //            {
        //                return false;
                       
        //            }
        //          }
        //           this.Price = this.Price / LIB_TO_DOLLAR;
        //           this.Price =Discount(this.Price, this.NumOfRooms);
                
        //        FlatList.Add(this);

        //        return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        // write to error log file
        //        throw new Exception(" Didn't succeed in inserting Flat " + exp.Message);
                
               
        //    }
        //}


    }
}
