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

  
        public  int Insert(Vacation v)
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

        public static List<Vacation> getByDatesOrders(DateTime startDate, DateTime endDate)
        {
            List<Vacation> tempList = new List<Vacation>();
            foreach (Vacation item in OrderesList)
            {
                if (item.StartDate>= startDate && item.EndDate<=endDate)
                {
                    tempList.Add(item);
                }
            }

            return tempList ;
        }

        public static  List<Vacation> Read()
        {
            DBservices dbs = new DBservices();
            OrderesList = dbs.getVacationFromDB();
            return OrderesList;


        }

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

        public int InsertVacationToDB(Vacation v)
        {
            DBservices dbs = new DBservices();

            return dbs.InsertVacation(v);
        }

    }
}
