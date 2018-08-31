using Common.Infrastructure.DataModels;
using Common.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class BodyguardRepository : IBodyguardRepository
    {
        private List<BodyguardDto> _bodyguardCollection;

        public BodyguardRepository()
        {
            using (var dbContext = new ApplicationContext())
            {
                dbContext.Bodyguards.Load();
                _bodyguardCollection = dbContext.Bodyguards.Local.ToList();
            }
        }

        public Task<BodyguardDto> GetBodyguardDetails(int ID)
        {
            return Task.Run(() => _bodyguardCollection.FirstOrDefault(x => x.ID == ID));
        }

        public Task<IEnumerable<BodyguardDto>> GetCollection(SearchBodyguardsParameters searchParams)
        {
            return Task.Run(() => _bodyguardCollection.AsEnumerable());
        }

        public Task Edit(BodyguardDto item)
        {
            return Task.Run(() =>
            {
                using (var dbContext = new ApplicationContext())
                {
                   var dbItem = dbContext.Bodyguards.Find(item.ID);

                    dbItem.FirstName = item.FirstName;
                    dbItem.SurName = item.SurName;
                    dbItem.LastName = item.LastName;
                    dbItem.FullName = item.FullName;
                    dbItem.Rate = item.Rate;
                    dbItem.Phone = item.Phone;

                    dbContext.Entry(dbItem).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            });
        }
    }
}
