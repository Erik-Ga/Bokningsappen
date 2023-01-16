using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    internal class Weekday
    {
        public int Id { get; set; }
        public string? Day { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
