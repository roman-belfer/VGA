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
        private bool _isMale;
        private uint _id;
        private uint _index;
        private uint _rate;
        private string _name;
        private string _phone;
        private string _category;

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
        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                if (_isMale != value)
                {
                    _isMale = value;
                    OnPropertyChanged(nameof(IsMale));
                }
            }
        }
        public uint ID
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
        public uint Index
        {
            get { return _index; }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged(nameof(Index));
                }
            }
        }
        public uint Rate
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
        public string Category
        {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        static Random rand;
        static uint index;
        public static ItemViewModel ConvertFromDto(BodyguardDto dto)
        {
            var model = new ItemViewModel();

            if (dto != null)
            {
                model.Index = index++;
                model.IsReadOnly = true;
                model.ID = dto.ID;
                model.Phone = dto.Phone;
                model.IsMale = dto.IsMale;
                model.Name = string.Join(" ", dto.FirstName, dto.SurName, dto.LastName);

                switch(rand.Next(2))
                {
                    case 0:
                        model.Category = "A";
                        break;
                    case 1:
                        model.Category = "B";
                        break;
                    case 2:
                        model.Category = "C";
                        break;
                }
            }

            return model;
        }

        public static IEnumerable<ItemViewModel> ConvertFromDto(IEnumerable<BodyguardDto> dtos)
        {
            var result = new List<ItemViewModel>();

            if (dtos != null && dtos.Any())
            {
                rand = new Random();
                index = 0;
                foreach (var d in dtos.ToList())
                {
                    result.Add(ConvertFromDto(d));
                    index++;
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
