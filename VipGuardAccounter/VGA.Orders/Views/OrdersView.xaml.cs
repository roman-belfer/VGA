using Common.Infrastructure.Interfaces.Views;
using Common.UI;
using VGA.Orders.ViewModels;

namespace VGA.Orders.Views
{
    public partial class OrdersView : BaseView<OrdersViewModel>, IOrdersView
    {
        public OrdersView()
        {
            InitializeComponent();            
        }
    }
}
