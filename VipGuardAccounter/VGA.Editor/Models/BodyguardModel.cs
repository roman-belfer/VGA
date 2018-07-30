using Common.Infrastructure.DataModels;
using System;
using System.Windows.Media.Imaging;

namespace VGA.Editor.Models
{
    public class BodyguardModel : ICloneable
    {
        public int Height { get; set; }
        public int Weight { get; set; }
        public short Age { get; set; }
        public short Expirience { get; set; }
        public BitmapSource Photo { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        internal static BodyguardModel ConvertToModel(BodyguardDetailDto dto)
        {
            BodyguardModel result = null;

            if (dto != null)
            {
                result = new BodyguardModel()
                {
                    FirstName = dto.BaseInfo.FirstName,
                    SurName = dto.BaseInfo.SurName,
                    LastName = dto.BaseInfo.LastName,
                };

                result.FullName = $"{result.LastName} {result.FirstName} {result.SurName}";
            }

            return result;
        }

        public object Clone()
        {
            return new BodyguardModel()
            {
                FirstName = FirstName,
                SurName = SurName,
                LastName = LastName,
                Age = Age,
                Expirience = Expirience,
                FullName = FullName,
                Height = Height,
                Photo = Photo,
                Weight = Weight
            };
        }
    }
}
