using Common.Infrastructure;
using Common.Infrastructure.Interfaces.Repositories;
using System.Threading.Tasks;
using VGA.Editor.Models;

namespace VGA.Editor.ViewModels
{
    public class OrderEditViewModel : BaseEditViewModel
    {
        private readonly IOrderRepository _repository;
        private OrderModel _order;

        public OrderEditViewModel()
        {
            _repository = Container.Resolve<IOrderRepository>();
        }

        public OrderModel Order
        {
            get { return _order; }
            set
            {
                if (_order != value)
                {
                    _order = value;
                    OnPropertyChanged(nameof(Order));
                }
            }
        }

        public override void Create()
        {
            base.Create();

            Order = new OrderModel();
        }

        public override void Edit(uint id)
        {
            base.Edit(id);

            Task.Run(async () =>
            {
                var detailDto = await _repository.GetDetail(id);

                Order = OrderModel.ConvertToModel(detailDto);
            });
        }

        protected override void OnSave()
        {
            //TO DO: Save logic
            base.OnSave();
        }
    }
}
