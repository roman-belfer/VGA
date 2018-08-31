using System;
using System.Collections.Generic;
using System.Linq;
using Common.Infrastructure.DataModels;
using Common.MVVM;

namespace VGA.Orders.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private int _id;
        private double _totalPrice;
        private double _prepayment;
        private string _employee;
        private DateTime _startDate;
        private DateTime _endDate;

        public int ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public double Prepayment
        {
            get { return _prepayment; }
            set
            {
                if (_prepayment != value)
                {
                    _prepayment = value;
                    OnPropertyChanged(nameof(Prepayment));
                }
            }
        }

        public string Employee
        {
            get { return _employee; }
            set
            {
                if (_employee != value)
                {
                    _employee = value;
                    OnPropertyChanged(nameof(Employee));
                }
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public static ItemViewModel ConvertFromDto(OrderDto dto, Dictionary<int, string> employees)
        {
            var model = new ItemViewModel();

            if (dto != null)
            {
                model.ID = dto.ID;
                model.TotalPrice = dto.TotalPrice;
                model.Prepayment = dto.Prepayment;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
                model.Employee = employees != null && employees.ContainsKey(dto.BodyguardID) 
                    ? employees[dto.BodyguardID] : string.Empty;
            }

            return model;
        }

        public static IEnumerable<ItemViewModel> ConvertFromDto(IEnumerable<OrderDto> dtos, Dictionary<int, string> employees)
        {
            var result = new List<ItemViewModel>();

            if (dtos != null && dtos.Any())
            {
                foreach (var d in dtos)
                {
                    result.Add(ConvertFromDto(d, employees));
                }
            }

            return result;
        }
    }
}
