using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TurnDal
    {
        //todo: to take only isActive
        public static List<customersInLine> GetAllCustomersInTurn()
        {
            try
            {
                using (onLineEntities1 entities = new onLineEntities1())
                {

                    return entities.customersInLines.Where(t=>t.preAlert>0).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<customersInLine> GetLinePerActivityTime(int activityTimeId)
        {
            try
            {
                using (onLineEntities1 entities = new onLineEntities1())
                {
                    var q = entities.customersInLines.Include("activityTime").ToList();
                    return q.Where(a => a.activityTimeId == activityTimeId && (a.ActualHour == new TimeSpan() || a.ActualHour == null)).OrderBy(l => l.estimatedHour).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<customersInLine> GetLineByCustomer(int custId)
        {
            try
            {
                using (onLineEntities1 entities = new onLineEntities1())
                {
                    return entities.customersInLines.Where(a => a.custId == custId && a.ActualHour == new TimeSpan()).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static customersInLine GetTurnByTurnId(int turnId)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    return entities1.customersInLines.First(t => t.TurnId == turnId);
                }
            }
            catch
            {
                throw;
            }
        }

        public static int AddAppointment(customersInLine turn)
        {
            try
            {
                using (onLineEntities1 entities = new onLineEntities1())
                {
                    entities.customersInLines.Add(turn);
                    entities.SaveChanges();
                    return turn.TurnId;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateTimeForTurns(List<customersInLine> line)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    for (int i = 0; i < line.Count(); i++)
                    {
                        customersInLine turn = entities1.customersInLines.FirstOrDefault(l => l.TurnId == line[i].TurnId);
                        turn.estimatedHour = line[i].estimatedHour;
                    }
                    entities1.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdateTurn(customersInLine turnToUpdate)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {

                    customersInLine turn = entities1.customersInLines.FirstOrDefault(l => l.TurnId == turnToUpdate.TurnId);
                    turn.preAlert = turnToUpdate.preAlert;
                    turn.statusTurn = turnToUpdate.statusTurn;
                    turn.verificationCode = turnToUpdate.verificationCode;
                    turn.ActualHour = turnToUpdate.ActualHour;

                    // entities1.Entry(turnToUpdate).State = EntityState.Modified;
                    entities1.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteTurn(int turnId)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    entities1.customersInLines.Remove(entities1.customersInLines.FirstOrDefault(t => t.TurnId == turnId));
                    entities1.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
