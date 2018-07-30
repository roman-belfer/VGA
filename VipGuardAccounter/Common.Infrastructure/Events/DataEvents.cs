using Prism.Events;

namespace Common.Infrastructure.Events
{
    public class DataEvents
    {
        public class DetailEvent : PubSubEvent<uint> { }
        public class EditEvent : PubSubEvent<uint?> { }
    }
}
