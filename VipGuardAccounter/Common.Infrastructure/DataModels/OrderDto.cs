using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Infrastructure.DataModels
{
    [Table("Orders")]
    public class OrderDto
    {
        [Key]
        public int ID { get; set; }
        public int BodyguardID { get; set; }
        public double TotalPrice { get; set; }
        public double Prepayment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public OrderDto(int id, int bodyguardId, double totalPrice, double prepayment, DateTime startDate, DateTime endDate)
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
