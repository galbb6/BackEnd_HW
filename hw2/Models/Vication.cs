namespace AirBnb_Part_2.Models
{
    public class Vacation
    {

       
        public int id { get; set; }
        public int UserId { get; set; }
        public int FlatId { get; set; }
        public DateTime StartDate { get ; set; }
        public DateTime EndDate { get; set; }

        private static List<Vacation> OrderesList = new List<Vacation>();

        //--------------------------------------------------------------------------------------------------
        // # INSERT VACATION                               
        //--------------------------------------------------------------------------------------------------
        public int Insert(Vacation v)
        {
          try
            {
            foreach (Vacation item in OrderesList)
            {
                if (item.id == this.id )
                {
                    return 0;

                }
                else if (this.CheckVicationDates())
                {
                        return 0;
                }               
            }

                DBservices dbs = new DBservices();

                return dbs.InsertVacationToDB(v);
               

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting Flat " + exp.Message);
            }

         

        }
        //--------------------------------------------------------------------------------------------------
        // # GET VACATION  BY DATES                            
        //--------------------------------------------------------------------------------------------------
        public static List<Vacation> getByDatesOrders(DateTime startDate, DateTime endDate)
        {
            List<Vacation> tempList = new List<Vacation>();

            DBservices dbs = new DBservices();
            OrderesList = dbs.getVacationFromDB();

            foreach (Vacation item in OrderesList)
            {
                if (item.StartDate>= startDate && item.EndDate<=endDate)
                {
                    tempList.Add(item);
                }
            }

            return tempList ;
        }
        //--------------------------------------------------------------------------------------------------
        // # GET ALL VACATIONS                               
        //--------------------------------------------------------------------------------------------------
        public static  List<Vacation> Read()
        {
            DBservices dbs = new DBservices();
            OrderesList = dbs.getVacationFromDB();
            return OrderesList;


        }
        //--------------------------------------------------------------------------------------------------
        // # UPDATE VACATION                             
        //--------------------------------------------------------------------------------------------------
        public int UpdateVacation(Vacation vacation)
        {

            DBservices dbs = new DBservices();
            return dbs.UpdateVacationToDB(vacation);
        }
        //--------------------------------------------------------------------------------------------------
        // # DELETE VACATION                             
        //--------------------------------------------------------------------------------------------------
        public int DeleteVacation(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteVacationFromDB(id);


        }
        //--------------------------------------------------------------------------------------------------
        // # CHECK VACATION VALIDATION BY DATES <HELPER>                              
        //--------------------------------------------------------------------------------------------------
        public bool CheckVicationDates()
        {
            // הפונקציה מקבלת חופשה מסויימת ובודקת האם אפשר לסגור אותה מבחינת תאריכים
            // הפונקציה תחזיר אמת במידה והדירה כבר קיימת בתאריכים אלו
            foreach (Vacation item in OrderesList)
            {
                if (item.FlatId == this.FlatId)
                {

               
                 if (this.StartDate >= item.StartDate && this.StartDate <= item.EndDate)
                 {

                    return true;
                 }
                 if (this.EndDate >= item.StartDate && this.StartDate <= item.EndDate)
                 {
                    return true;
                 }

                 
                }
            }
                  return false;
        }

      
    }
}
