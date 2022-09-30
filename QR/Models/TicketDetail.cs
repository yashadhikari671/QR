using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QR.Models
{
    public class TicketDetail
    {
        public int Id { get; set; } = 2;
        public string Name { get; set; } = "Yash adhikari";
        public string Event { get; set; } = "FootBall Match";
        public string Venue { get; set; } = "FootBall Ground";
        public DateTime EventDate { get; set; } 
        public int price { get; set; } = 500;

    }
}
