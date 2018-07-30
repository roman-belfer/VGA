using Common.Infrastructure.DataModels;
using Common.Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;

namespace Common.Repository
{
    public class DictionaryRepository : IDictionaryRepository
    {
        public IEnumerable<ValueModel> GetExpirienceCollection()
        {
            return new List<ValueModel>()
            {
                new ValueModel(0, "1 год"),
                new ValueModel(1, "3 года"),
                new ValueModel(2, "5 лет"),
                new ValueModel(3, "10 лет"),
                new ValueModel(4, "15 лет"),
                new ValueModel(5, "20 лет")
            };
        }

        public IEnumerable<ValueModel> GetHandgunBulletCollection()
        {
            return new List<ValueModel>()
            {
                new ValueModel(0, "7.62 мм"),
                new ValueModel(1, "9 мм"),
                new ValueModel(2, "11.43 мм")
            };
        }

        public IEnumerable<ValueModel> GetHandgunCollection()
        {
            return new List<ValueModel>()
            {
                new ValueModel(0, "ПМ"),
                new ValueModel(1, "ТТ"),
                new ValueModel(2, "Colt"),
                new ValueModel(3, "Glock"),
                new ValueModel(4, "Beretta"),
                new ValueModel(5, "ФОРТ")
            };
        }

        public IEnumerable<ValueModel> GetHeightCollection()
        {
            return new List<ValueModel>()
            {
                new ValueModel(0, "-"),
                new ValueModel(1, "160"),
                new ValueModel(2, "170"),
                new ValueModel(3, "180"),
                new ValueModel(4, "190"),
                new ValueModel(5, "200")
            };
        }

        public IEnumerable<ValueModel> GetMilitaryCollection()
        {
            return new List<ValueModel>()
            {
                new ValueModel(0, "-"),
                new ValueModel(1, "сержант"),
                new ValueModel(2, "лейтенант"),
                new ValueModel(3, "капитан"),
                new ValueModel(4, "майор"),
                new ValueModel(5, "подполковник"),
                new ValueModel(6, "полковник")
            };
        }

        public IEnumerable<ValueModel> GetRifleBulletCollection()
        {
            return new List<ValueModel>()
            {
                new ValueModel(0, "5.45 мм"),
                new ValueModel(1, "7.62 мм")
            };
        }

        public IEnumerable<ValueModel> GetRifleCollection()
        {
            return new List<ValueModel>()
            {
                new ValueModel(0, "Сайга"),
                new ValueModel(1, "AK"),
                new ValueModel(2, "M14")
            };
        }

        public IEnumerable<ValueModel> GetWeightCollection()
        {
            return new List<ValueModel>()
            {
                new ValueModel(0, "-"),
                new ValueModel(1, "60"),
                new ValueModel(2, "70"),
                new ValueModel(3, "80"),
                new ValueModel(4, "90"),
                new ValueModel(5, "100"),
                new ValueModel(6, "110"),
                new ValueModel(7, "120")
            };
        }
    }
}
