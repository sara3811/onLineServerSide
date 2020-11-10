using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BL.services
{
    public class NnotificationTimer
    {
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
          //  var allTurns = DAL.TurnDal.GetAllCustomersInTurn();
          //  foreach (var item in allTurns)
           // {
                //check if there is correct token and if the time to alert is now)
           //    if (item.customer.firebaseToken.Length!=1&& item.estimatedHour.AddMinutes(-item.preAlert) == DateTime.Now)
                    //מפעיל את שליחת ההתרעה
           //         await NotificationService.SendNotification("שים לב התראה  OnLine", "התור שלך הוא בעוד"+item.preAlert+"דקות",item.customer.firebaseToken); 

          //  }
               //    await NotificationService.SendNotification("שים לב התראה  OnLine", "התור שלך הוא בעודדקות", "fPVOygOBeEq2ACHlpHIXga:APA91bGZ2Z4dkMHEGsdtw9-4hyINJqhY1pCW4vFhOEWN6kG5kjf3qGgBdjNACLXkXMVazNdd-DXScvmIiULHEDBVxH8ITNXP72hecOEeLBiqoKnKdINShRz2dBP7fyPBGKvooBXtff8I"); 

        }
    }
}
