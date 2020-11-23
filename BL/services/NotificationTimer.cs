using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BL.services
{
<<<<<<< HEAD
    public class NotificationTimer
    {
=======
    public class NnotificationTimer
    {//totake:את כל המחלקה 
     //פונקציות היוצרות טיימר שבכל דקה נתונה מבצע בדיקה
     //ומפעילות שליחת התראה לתורים שאמורים לקבלה בזמן זה
>>>>>>> 9b54f49236dcce9ef556932bae3cbb80dda7e0a8
        public static void StartTimer()
        {

            // Create a timer with a two second interval-ask if interval is appropriate.
            //Interval property is in milliseconds.
            // 1 sec = 1000 milliseconds
            Timer timer = new Timer(60000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private async static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var allTurns = DAL.TurnDal.GetAllCustomersInTurn();
            foreach (var item in allTurns)
            {
<<<<<<< HEAD
                //check if there is correct token and if the time to alert is now)
                if (item.customer.firebaseToken.Length != 1 && item.estimatedHour.AddMinutes(-item.preAlert) == DateTime.Now)
                    //מפעיל את שליחת ההתרעה
                    await NotificationService.SendNotification("שים לב התראה  OnLine", "התור שלך הוא בעוד" + item.preAlert + "דקות", item.customer.firebaseToken);

            }


=======
                //check if there is correct token an23d if the time to alert is now)
                if (item.customer.firebaseToken.Length != 1 && item.estimatedHour.AddMinutes(-item.preAlert) == DateTime.Now)
                    await NotificationService.SendNotification("", "", item.customer.firebaseToken);
            }
>>>>>>> 9b54f49236dcce9ef556932bae3cbb80dda7e0a8
        }
    }
}
