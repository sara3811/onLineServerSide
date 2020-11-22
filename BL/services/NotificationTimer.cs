using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BL.services
{
    public class NnotificationTimer
    {//totake:את כל המחלקה 
     //פונקציות היוצרות טיימר שבכל דקה נתונה מבצע בדיקה
     //ומפעילות שליחת התראה לתורים שאמורים לקבלה בזמן זה
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
                //check if there is correct token an23d if the time to alert is now)
                if (item.customer.firebaseToken.Length != 1 && item.estimatedHour.AddMinutes(-item.preAlert) == DateTime.Now)
                    await NotificationService.SendNotification("", "", item.customer.firebaseToken);
            }
        }
    }
}
