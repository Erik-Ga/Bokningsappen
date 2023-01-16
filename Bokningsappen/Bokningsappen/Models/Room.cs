using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    internal class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public bool Whiteboard { get; set; }
        public bool Booked { get; set; }
        public int? PersonId { get; set; }
        public int WeekdayId { get; set; }
    }
}
