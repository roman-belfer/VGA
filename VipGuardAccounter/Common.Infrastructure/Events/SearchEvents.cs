using Common.Infrastructure.DataModels;
using Prism.Events;

namespace Common.Infrastructure.Events
{
    public class SearchEvents
    {
        public class SearchParameterEvent : PubSubEvent<SearchParameters> { }
    }
}
