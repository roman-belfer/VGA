using System;

namespace Common.Infrastructure.DataModels
{
    public class OrderDto
    {
        public uint ID { get; set; }
        public uint BodyguardID { get; set; }
        public double TotalPrice { get; set; }
        public double Prepayment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public OrderDto(uint id, uint bodyguardId, double totalPrice, double prepayment, DateTime startDate, DateTime endDate)
        {
            ID = id;
            BodyguardID = bodyguardId;
            TotalPrice = totalPrice;
            Prepayment = prepayment;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
