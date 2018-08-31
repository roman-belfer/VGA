using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Infrastructure.DataModels
{
    [Table("Bodyguards")]
    public class BodyguardDto
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string FullName { get; set; }
        public int Rate { get; set; }
        public string Phone { get; set; }

        public BodyguardDto() { }
        public BodyguardDto(int id, string firstName, string surName, string lastName, int rate, string phone)
        {
            ID = id;
            FirstName = firstName;
            SurName = surName;
            LastName = lastName;
            FullName = $"{LastName} {FirstName} {SurName}";
            Rate = rate;
            Phone = phone;
        }
    }
}
