using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace VGA.Editor.ViewModels
{
    public class EmployeeEditViewModel : BaseEditViewModel
    {
        private readonly IBodyguardRepository _repository;

        private string _firstName;
        private string _surName;
        private string _lastName;
        private DateTime _birthDate = DateTime.Now;
        private string _phone;
        private bool _isMale = true;
        private string _city;
        private string _address;

        public EmployeeEditViewModel()
        {
            _repository = Container.Resolve<IBodyguardRepository>();
        }

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

        public override void Create()
        {
            base.Create();
        }

        public override void Edit(uint id)
        {
            base.Edit(id);

            Task.Run(async() =>
            {
                var detailDto = await _repository.GetBodyguardDetails(id);

               // Bodyguard = BodyguardModel.ConvertToModel(detailDto);
            });
        }

        protected override void OnSave()
        {
            //TO DO: Save logic
            base.OnSave();
        }
    }
}
