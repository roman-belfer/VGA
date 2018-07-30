using Common.Infrastructure.Interfaces.Views;
using Prism.Events;

namespace Common.Infrastructure.Events
{
    public class NavigationEvents
    {
        public class NavigateViewEvent : PubSubEvent<IView> { }
    }
}
