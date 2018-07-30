using Common.Infrastructure.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace VGA.Index.Models
{
    public class IndexModel
    {
        public uint ID { get; set; }
        public uint Rate { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public static IndexModel ConvertFromDto(BodyguardDto dto)
        {
            var model = new IndexModel();

            if (dto != null)
            {
                model.ID = dto.ID;
                model.Rate = dto.Rate;
                model.Phone = dto.Phone;
                model.Name = string.Join(" ", dto.FirstName, dto.SurName, dto.LastName);
            }

            return model;
        }

        public static IEnumerable<IndexModel> ConvertFromDto(IEnumerable<BodyguardDto> dtos)
        {
            var result = new List<IndexModel>();

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
