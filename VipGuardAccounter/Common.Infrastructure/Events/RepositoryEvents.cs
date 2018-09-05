using Prism.Events;

namespace Common.Infrastructure.Events
{
    public class RepositoryEvents
    {
        public class BodyguardRepositoryChanged : PubSubEvent { }
        public class OrderRepositoryChanged : PubSubEvent { }
    }
}
