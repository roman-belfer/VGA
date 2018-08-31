using Prism.Events;

namespace Common.Infrastructure.Events
{
    public class DataEvents
    {
        public class DetailEvent : PubSubEvent<int> { }
        public class EditEvent : PubSubEvent<int?> { }
    }
}
