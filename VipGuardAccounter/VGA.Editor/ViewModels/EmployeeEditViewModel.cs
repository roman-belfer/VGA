using Common.Infrastructure;
using Common.Infrastructure.DataModels;
using Common.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VGA.Editor.ViewModels
{
    public class EmployeeEditViewModel : BaseEditViewModel
    {
        private readonly IBodyguardRepository _repository;
        private readonly IDictionaryRepository _dictionaryRepository;

        private uint _id;
        private string _firstName;
        private string _surName;
        private string _lastName;
        private DateTime _birthDate = DateTime.Now;
        private string _phone;
        private bool _isMale = true;
        private string _city;
        private string _address;
        private uint _categoryId = 0;
        private bool _isEnglish;
        private bool _isShortGun;
        private bool _isRiffle;
        private string _personalWeapon;
        private string _driverLicense;
        private int _weight;
        private int _height;

        public EmployeeEditViewModel()
        {
            _repository = Container.Resolve<IBodyguardRepository>();
            _dictionaryRepository = Container.Resolve<IDictionaryRepository>();

            CategoryCollection = _dictionaryRepository.GetCategoryCollection().ToList();
        }

        public List<ValueModel> CategoryCollection { get; set; }

        #region Properties
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string SurName
        {
            get { return _surName; }
            set
            {
                if (_surName != value)
                {
                    _surName = value;
                    OnPropertyChanged(nameof(SurName));
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnPropertyChanged(nameof(BirthDate));
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

        public int Weight
        {
            get { return _weight; }
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged(nameof(Weight));
                }
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged(nameof(Height));
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

        public bool IsShortGun
        {
            get { return _isShortGun; }
            set
            {
                if (_isShortGun != value)
                {
                    _isShortGun = value;
                    OnPropertyChanged(nameof(IsShortGun));
                }
            }
        }

        public bool IsRiffle
        {
            get { return _isRiffle; }
            set
            {
                if (_isRiffle != value)
                {
                    _isRiffle = value;
                    OnPropertyChanged(nameof(IsRiffle));
                }
            }
        }

        public bool IsEnglish
        {
            get { return _isEnglish; }
            set
            {
                if (_isEnglish != value)
                {
                    _isEnglish = value;
                    OnPropertyChanged(nameof(IsEnglish));
                }
            }
        }

        public string DriverLicense
        {
            get { return _driverLicense; }
            set
            {
                if (_driverLicense != value)
                {
                    _driverLicense = value;
                    OnPropertyChanged(nameof(DriverLicense));
                }
            }
        }

        public string PersonalWeapon
        {
            get { return _personalWeapon; }
            set
            {
                if (_personalWeapon != value)
                {
                    _personalWeapon = value;
                    OnPropertyChanged(nameof(PersonalWeapon));
                }
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public uint CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
                    OnPropertyChanged(nameof(CategoryId));
                }
            }
        }
        #endregion

        public override void Create()
        {
            base.Create();
        }

        public override void Edit(uint id)
        {
            base.Edit(id);

            Task.Run(async () =>
            {
                var dto = await _repository.GetDetail(id);

                ConvertFromDto(dto);
            });
        }

        private BodyguardDto ConvertToDto()
        {
            var dto = new BodyguardDto()
            {
                Address = Address,
                BirthDate = BirthDate,
                CategoryId = CategoryId,
                City = City,
                DriverLicense = DriverLicense,
                FirstName = FirstName,
                Height = Height,
                IsEnglish = IsEnglish,
                ID = _id,
                IsMale = IsMale,
                IsRiffle = IsRiffle,
                IsShortGun = IsShortGun,
                LastName = LastName,
                PersonalWeapon = PersonalWeapon,
                Phone = Phone,
                SurName = SurName,
                Weight = Weight
            };

            return dto;
        }

        private void ConvertFromDto(BodyguardDto dto)
        {
            Address = dto.Address;
            BirthDate = dto.BirthDate;
            CategoryId = dto.CategoryId;
            City = dto.City;
            DriverLicense = dto.DriverLicense;
            FirstName = dto.FirstName;
            Height = dto.Height;
            IsEnglish = dto.IsEnglish;
            _id = dto.ID;
            IsMale = dto.IsMale;
            IsRiffle = dto.IsRiffle;
            IsShortGun = dto.IsShortGun;
            LastName = dto.LastName;
            PersonalWeapon = dto.PersonalWeapon;
            Phone = dto.Phone;
            SurName = dto.SurName;
            Weight = dto.Weight;
        }

        protected override void OnSave()
        {
            var dto = ConvertToDto();
            _repository.Save(dto);

            base.OnSave();
        }
    }
}
