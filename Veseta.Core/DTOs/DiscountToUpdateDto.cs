using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veseta.Core.entites.Enum;

namespace Veseta.CoreCore.DTOs
{
    public class DiscountToUpdateDto
    {
        public int Id { get; set; }
        public string DiscoundCode { get; set; }
        public int NumberOfRequests { get; set; }
        public decimal Value { get; set; }
        public DiscountType DiscountType { get; set; }
        public bool IsActive { get; set; }
    }
}
