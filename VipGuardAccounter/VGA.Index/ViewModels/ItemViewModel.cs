using Common.Infrastructure.DataModels;
using Common.MVVM;
using System.Collections.Generic;
using System.Linq;

namespace VGA.Index.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private bool _isReadOnly;
        private uint _id;
        private uint _rate;
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

        public static ItemViewModel ConvertFromDto(BodyguardDto dto)
        {
            var model = new ItemViewModel();

            if (dto != null)
            {
                model.IsReadOnly = true;
                model.ID = dto.ID;
                model.Rate = dto.Rate;
                model.Phone = dto.Phone;
                model.Name = string.Join(" ", dto.FirstName, dto.SurName, dto.LastName);
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
    }
}
