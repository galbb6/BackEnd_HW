namespace AirBnb_Part_2.Models
{
    public class Vication
    {

       
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FlatId { get; set; }
        public DateTime StartDate { get ; set; }
        public DateTime EndDate { get; set; }

        private static List<Vication> OrderesList = new List<Vication>();

  
        public  bool Insert( )
        {
          try
            {
            foreach (Vication item in OrderesList)
            {
                if (item.Id == this.Id )
                {
                    return false;

                }
                else if (this.CheckVicationDates())
                {
                        return false;
                }               
            }
                OrderesList.Add(this);
                InsertVacationToDB(this);

            }
            catch (Exception exp)
            {
                // write to error log file
                throw new Exception(" didn't succeed in inserting Flat " + exp.Message);
            }

            return true;

        }

        public static List<Vication> getByDatesOrders(DateTime startDate, DateTime endDate)
        {
            List<Vication> tempList = new List<Vication>();
            foreach (Vication item in OrderesList)
            {
                if (item.StartDate>= startDate && item.EndDate<=endDate)
                {
                    tempList.Add(item);
                }
            }

            return tempList ;
        }

        public static  List<Vication> Read()
        {
            return OrderesList;

        }

        public bool CheckVicationDates()
        {
            // הפונקציה מקבלת חופשה מסויימת ובודקת האם אפשר לסגור אותה מבחינת תאריכים
            // הפונקציה תחזיר אמת במידה והדירה כבר קיימת בתאריכים אלו
            foreach (Vication item in OrderesList)
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

        public int InsertVacationToDB(Vication v)
        {
            DBservices dbs = new DBservices();

            return dbs.InsertVacation(v);
        }

    }
}
