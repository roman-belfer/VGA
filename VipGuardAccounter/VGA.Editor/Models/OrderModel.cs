using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.DataModels;

namespace VGA.Editor.Models
{
    public class OrderModel
    {
        internal static OrderModel ConvertToModel(OrderDto detailDto)
        {
            return new OrderModel();
        }
    }
}
