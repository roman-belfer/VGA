using Common.Infrastructure.DataModels;
using Prism.Events;

namespace Common.Infrastructure.Events
{
    public class DataEvents
    {
        public class DetailEvent : PubSubEvent<EditModel> { }
        public class EditEvent : PubSubEvent<EditModel> { }
    }
}
