using Common.Infrastructure.DataModels;
using Common.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VGA.Index.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private bool _isReadOnly;
        private int _id;
        private int _rate;
        private string _name;
        private string _phone;

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }
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
        public int Rate
        {
            get { return _rate; }
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    OnPropertyChanged(nameof(Rate));
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }

        public static ItemViewModel ConvertFromDto(BodyguardDto dto)
        {
            var model = new ItemViewModel();

            if (dto != null)
            {
                model.IsReadOnly = true;
                model.ID = dto.ID;
                model.Rate = dto.Rate;
                model.Phone = dto.Phone;
                model.Name = dto.FullName;
            }

            return model;
        }

        public static IEnumerable<ItemViewModel> ConvertFromDto(IEnumerable<BodyguardDto> dtos)
        {
            var result = new List<ItemViewModel>();

            if (dtos != null && dtos.Any())
            {
                foreach (var d in dtos)
                {
                    result.Add(ConvertFromDto(d));
                }
            }

            return result;
        }

        public static IEnumerable<BodyguardDto> ConvertToDto(IEnumerable<ItemViewModel> collection)
        {
            var result = new List<BodyguardDto>();

            if (collection != null && collection.Any())
            {
                foreach (var d in collection)
                {
                    result.Add(ConvertToDto(d));
                }
            }

            return result;
        }

        public static BodyguardDto ConvertToDto(ItemViewModel item)
        {
            var model = new BodyguardDto();

            if (item != null)
            {
                model.ID = item.ID;
                model.Rate = item.Rate;
                model.Phone = item.Phone;
                model.FullName = item.Name;
            }

            return model;
        }
    }
}
