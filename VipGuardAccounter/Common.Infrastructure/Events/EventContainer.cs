using Prism.Events;

namespace Common.Infrastructure.Events
{
    public class EventContainer
    {
        private static readonly EventContainer _eventInstance = new EventContainer();
        private IEventAggregator _eventAggregator;

        public static EventContainer EventInstance
        {
            get
            {
                return _eventInstance;
            }
        }

        public IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                {
                    _eventAggregator = new EventAggregator();
                }

                return _eventAggregator;
            }
        }

        private EventContainer()
        {

        }
    }

}
