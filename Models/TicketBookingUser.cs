using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_IRCTC_Revervation.Models
{
    public class TicketBookingUser
    {
        public int ticketId { get; set; }
        public string trainName { get; set; }
        public string trainNumber { get; set; }
        public string emailId { get; set; }
        public DateTime departureDate { get; set; }
        public int ticketFare { get; set; }

    }

    public class TrainMaster
    {
        public int Id { get; set; }       
        public string trainName { get; set; }
        public int trainNumber { get; set; }
        public string fromPlace { get; set; }
        public string toDestination { get; set; }
        public Boolean seatAvailability { get; set; }
    }
    public class TrainList
    {
        public int Id { get; set; }
        public string trainName { get; set; }
        
    }
    public class TrainNumber
    {
        public int Id { get; set; }
        public int trainNumber { get; set; }
    }
}
