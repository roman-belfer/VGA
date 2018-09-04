using System;

namespace Common.Infrastructure.DataModels
{
    [Serializable]
    public class BodyguardDto
    {
        #region Properties
        public uint ID { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public bool IsMale { get; set; }

        public bool IsShortGun { get; set; }

        public bool IsRiffle { get; set; }

        public bool IsEnglish { get; set; }

        public string DriverLicense { get; set; }

        public string PersonalWeapon { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public uint CategoryId { get; set; }
        #endregion

        public BodyguardDto() { }
        public BodyguardDto(uint id, string firstName, string surName, string lastName, string phone, bool isMale = true)
        {
            ID = id;
            FirstName = firstName;
            SurName = surName;
            LastName = lastName;
            Phone = phone;
            IsMale = isMale;
        }
    }
}
