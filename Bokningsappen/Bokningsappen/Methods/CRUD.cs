using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokningsappen.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Bokningsappen.Methods
{
    internal class CRUD
    {
        public static void InsertWeekday(string name) // Tar emot inmatning ifrån skapa veckodag metoden
        {
            using (var db = new MyDbContext())
            {
                var newWeekday = new Weekday
                {
                    Day = name,
                    //RoomId = roomId
                };
                var WeekdayList = db.Weekdays;
                WeekdayList.Add(newWeekday);
                db.SaveChanges(); // sparar i databasen
                Console.WriteLine("Veckodag " + name + " har lagts till i databasen!");
            }
        }
        public static void InsertPerson(string name) // Tar emot inmatning ifrån skapa person metoden
        {
            using (var db = new MyDbContext())
            {
                var newPerson = new Person
                {
                    Name = name
                };
                var PersonList = db.Persons;
                PersonList.Add(newPerson);
                db.SaveChanges(); // sparar i databasen
                Console.WriteLine("Personen " + name + " har lagts till systemet!");
            }
        }
        public static void InsertRoom(string name, int seats, bool whiteboard, bool booked, int? personid, int dayid) // Tar emot inmatning ifrån skapa rum metoden
        {

            using (var db = new MyDbContext())
            {
                var newRoom = new Room
                {
                    Name = name,
                    Seats = seats,
                    Whiteboard = whiteboard,
                    Booked = booked,
                    PersonId = personid,
                    WeekdayId = dayid
                };
                var RoomList = db.Rooms;
                RoomList.Add(newRoom);
                db.SaveChanges(); // sparar i databasen
                Console.WriteLine("Rum " + name + " har lagts till i databasen!");
            }
        }
        public static void CreatePerson() // Skapar person
        {
            Console.WriteLine("Ange namn: ");
            var name = Console.ReadLine();

            InsertPerson(name);
        }
        public static void CreateWeekday() // Skapar dag
        {
            Console.WriteLine("Ange dag: ");
            var name = Console.ReadLine();
            InsertWeekday(name);
            //Console.WriteLine("Ange rum ID som ska vara tillgängliga denna dag: ");
            //int roomId;
            //if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out roomId))
            //{
            //    InsertWeekday(name, roomId); // Skickar in dagarna i databasen
            //}
            //else
            //{
            //    Console.Clear();
            //    Console.WriteLine("Fel inmatning");
            //}


        }
        public static void CreateRoom() // Skapar rum
        {
            Console.WriteLine("Namn på rum: ");
            var name = Console.ReadLine();
            Console.WriteLine("Platser: ");
            var seats = int.Parse(Console.ReadLine());
            Console.WriteLine("DagId? (1-5)");
            int dayid;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out dayid))
            {
                Console.WriteLine("Whiteboard: Ja/Nej?");
                var whiteboard = Console.ReadLine();
                bool hasWhiteboard = false;
                bool isBooked = false;
                int? personid = null;
                if (whiteboard == "Ja")
                {
                    hasWhiteboard = true;
                    InsertRoom(name, seats, hasWhiteboard, isBooked, personid, dayid); // Skickar in rummen i databasen
                }
                else if (whiteboard == "Nej")
                {
                    hasWhiteboard = false;
                    InsertRoom(name, seats, hasWhiteboard, isBooked, personid, dayid); // Skickar in rummen i databasen
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Fel inmatning!");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Fel inmatning");
            }
            
                       
        }
        public static void ShowSchedule()
        {
            using (var myDb = new MyDbContext())
            {
                foreach (var weekday in myDb.Weekdays)
                {
                    Console.WriteLine(weekday.Day + "\n");
                    Console.WriteLine("Rum: " + "\t" + "\t" + "RumId:" + "\t" + "\t" + "Antal platers: " + "\t" + "\t" + "Bokad:" + "\t" + "\t" + "Whiteboard: " + "\t" + "\t" + "PersonId:");
                    
                    foreach (var room in myDb.Rooms)
                    {
                        if (room.WeekdayId == weekday.Id)
                            Console.WriteLine(room.Name + "\t" + room.Id + "\t" + "\t" + room.Seats + "\t" + "\t" + "\t" + room.Booked + "\t" + "\t" + room.Whiteboard + "\t" + "\t" + "\t" + room.PersonId);
                        //foreach (var person in myDb.Persons)
                        //{
                        //    if (room.PersonId == person.Id)
                        //    {
                        //        Console.WriteLine("\t" + "\t" + "\t" + person.Name);
                        //    }
                        //}
                    }
                    Console.WriteLine("");
                }
            }
        }
        public static void ShowPerson()
        {
            using (var myDb = new MyDbContext())
            {
                foreach(var person in myDb.Persons)
                {
                    Console.WriteLine("PersonId: " + person.Id + "\t" + "Namn: " + person.Name);
                }
            }
        }
        public static void UpdateRoom()
        {
            Console.WriteLine("Vill du boka eller avboka konferensrum?");
            Console.WriteLine("[B] - Boka");
            Console.WriteLine("[A] - Avboka");
            var key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case 'b':
                    Console.Clear();
                    ShowSchedule();
                    Console.WriteLine("Vilket rumId skulle du vilja boka?");
                    string roomId = Console.ReadLine();
                    int room;
                    if (int.TryParse(roomId, out room))
                    {                        
                        using (var db = new MyDbContext())
                        {
                            var updateRoom = (from r in db.Rooms
                                              where r.Id == room
                                              select r).SingleOrDefault();
                            if(updateRoom == null)
                            {
                                Console.WriteLine("Detta rum existerar ej!");
                            }
                            else if (updateRoom != null && updateRoom.Booked == false)
                            {
                                ShowPerson();
                                Console.WriteLine("Ange personid för bokning: ");
                                int personId = int.Parse(Console.ReadLine());
                                updateRoom.Booked = true;
                                updateRoom.PersonId = personId;
                                db.SaveChanges();
                                Console.WriteLine("Bokning lyckad!");
                            }
                            else if (updateRoom != null && updateRoom.Booked == true)
                            {
                                Console.WriteLine("Rummet är tyvärr redan bokat! Försök igen med ett annat rum!");
                            }

                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Fel inmatning");
                    }                   
                    break;
                case 'a':
                    Console.Clear();
                    ShowSchedule();
                    Console.WriteLine("Vilket rum skulle du vilka avboka?");
                    string roomId2 = Console.ReadLine();
                    int room2;
                    if (int.TryParse(roomId2, out room2))
                    {
                        using (var db = new MyDbContext())
                        {
                            var updateRoom = (from r in db.Rooms
                                              where r.Id == room2
                                              select r).SingleOrDefault();
                            if (updateRoom != null && updateRoom.Booked == true)
                            {
                                updateRoom.Booked = false;
                                updateRoom.PersonId = null;
                                db.SaveChanges();
                                Console.WriteLine("Avbokning lyckad!");
                            }
                            else
                            {
                                Console.WriteLine("Inget att avboka!");
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Fel inmatning");
                    }                   
                    break;
            }           
        }
        public static void WelcomeIntro()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hej och välkommen till bokning av konferensrum!");
                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("[A] - Adminsida");
                Console.WriteLine("[L] - Lägg till person i system");
                Console.WriteLine("[S] - Se personer i systemet");
                Console.WriteLine("[B] - Bokning eller avbokning av konferensrum");
                Console.WriteLine("[V] - Visa bokningsschema");

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                        Console.Clear();
                        Console.WriteLine("Välkommen Admin!");
                        Console.WriteLine("Vad vill du göra?");
                        Console.WriteLine("[L] - Lägga till objekt");
                        Console.WriteLine("[D] - Lägg till veckodag");
                        Console.WriteLine("[Q] - Se Queries");
                        Console.WriteLine("[S] - Gå till start");
                        var key2 = Console.ReadKey(true);
                        switch (key2.KeyChar)
                        {
                            case 'l':
                                Console.Clear();
                                CRUD.CreateRoom();
                                Console.ReadLine();
                                break;
                            case 'd':
                                Console.Clear();
                                CRUD.CreateWeekday();
                                Console.ReadLine();
                                break;
                            case 'q':
                                Console.Clear();

                                Console.ReadLine();
                                break;
                            case 's':
                                Console.Clear();
                                break;
                        }
                        break;
                    case 'l':
                        Console.Clear();
                        CreatePerson();
                        Console.ReadLine();
                        break;
                    case 's':
                        Console.Clear();
                        ShowPerson();
                        Console.ReadLine();
                        break;
                    case 'b':
                        Console.Clear();
                        UpdateRoom();
                        Console.ReadLine();
                        break;
                    case 'v':
                        Console.Clear();
                        ShowSchedule();
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
