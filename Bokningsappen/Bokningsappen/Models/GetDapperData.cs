using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bokningsappen.Models
{
    public class GetDapperData
    {
        public static void GetWhiteBoards()
        {
            string connString = "Server=tcp:mydberik.database.windows.net,1433;Initial Catalog=ErikDatabaseTwo;Persist Security Info=False;User ID=erikadmin;Password=Pumpa123;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var sql = $"SELECT * FROM Rooms ORDER BY Whiteboard desc";
            var roomsWhiteboard = new List<Room>();
            {
                using(var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    roomsWhiteboard = connection.Query<Room>(sql).ToList();
                    connection.Close();
                }
                Console.WriteLine("Rum med whiteboard längst upp!");
                foreach(var room in roomsWhiteboard)
                {
                    Console.WriteLine($"Rumnamn: {room.Name} Whiteboard tillgänglighet: {room.Whiteboard}");
                }
            }
        }
        public static void GetSeats()
        {
            string connString = "Server=tcp:mydberik.database.windows.net,1433;Initial Catalog=ErikDatabaseTwo;Persist Security Info=False;User ID=erikadmin;Password=Pumpa123;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var sql = $"SELECT * FROM Rooms ORDER BY Seats desc";
            var roomSeats = new List<Room>();
            {
                using(var connection =new SqlConnection(connString))
                {
                    connection.Open();
                    roomSeats = connection.Query<Room>(sql).ToList();
                    connection.Close();
                }
                Console.WriteLine("Antal platser per konferensrum i storleksordning!");
                foreach(var room in roomSeats)
                {
                    Console.WriteLine($"Rumnamn: {room.Name} Antal platser: {room.Seats}");
                }
            }
        }
    }
}
