using Common.Infrastructure.DataModels;
using System.Collections.Generic;

namespace Common.Infrastructure.Interfaces.Repositories
{
    public interface IDictionaryRepository
    {
        IEnumerable<ValueModel> GetHeightCollection();
        IEnumerable<ValueModel> GetWeightCollection();
        IEnumerable<ValueModel> GetExpirienceCollection();
        IEnumerable<ValueModel> GetMilitaryCollection();
        IEnumerable<ValueModel> GetHandgunCollection();
        IEnumerable<ValueModel> GetHandgunBulletCollection();
        IEnumerable<ValueModel> GetRifleCollection();
        IEnumerable<ValueModel> GetRifleBulletCollection();
        IEnumerable<ValueModel> GetCategoryCollection();

    }
}
