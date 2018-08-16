using Common.MVVM;

namespace VGA.Orders.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private uint _id;

        public uint ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }
    }
}
