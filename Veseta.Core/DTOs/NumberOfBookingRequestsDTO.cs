using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veseta.CoreCore.DTOs
{
    public class NumberOfBookingRequestsDTO
    {
        public int TotalNumberOfRequests { get; set; }
        public int NumberOfPending { get; set; }
        public int NumberOfCompleted { get; set; }
        public int NumberOfCancelled { get; set; }
    }
}
