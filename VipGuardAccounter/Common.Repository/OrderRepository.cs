﻿using Common.Infrastructure.DataModels;
using Common.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private IEnumerable<OrderDto> _ordersCollection;

        public OrderRepository()
        {
            _ordersCollection = new List<OrderDto>()
            {
                new OrderDto(0, 1, 400, 200, new DateTime(2018, 08, 22, 10,0,0), new DateTime(2018, 08, 22, 18,0,0)),
            };
        }

        public Task<IEnumerable<OrderDto>> GetCollection(SearchOrdersParameters searchParams)
        {
            return Task.Run(() => _ordersCollection);
        }

        public Task<OrderDto> GetDetail(uint ID)
        {
            return Task.Run(() => _ordersCollection.FirstOrDefault(x => x.ID == ID));
        }
    }
}
