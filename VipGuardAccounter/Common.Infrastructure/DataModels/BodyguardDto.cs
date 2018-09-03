namespace Common.Infrastructure.DataModels
{
    public class BodyguardDto
    {
        public bool IsMale { get; set; }
        public uint ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string FullName { get; set; }
        public uint Rate { get; set; }
        public string Phone { get; set; }

        public BodyguardDto(uint id, string firstName, string surName, string lastName, uint rate, string phone, bool isMale = true)
        {
            ID = id;
            FirstName = firstName;
            SurName = surName;
            LastName = lastName;
            FullName = $"{LastName} {FirstName} {SurName}";
            Rate = rate;
            Phone = phone;
            IsMale = isMale;
        }
    }
}
