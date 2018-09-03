using Common.Infrastructure.DataModels;
using Common.Infrastructure.Enums;
using Common.Infrastructure.Events;
using Common.MVVM;
using Prism.Events;

namespace VGA.Editor.ViewModels
{
    public class EditViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public EditViewModel()
        {
            _eventAggregator = EventContainer.EventInstance.EventAggregator;

            _eventAggregator.GetEvent<DataEvents.EditEvent>().Subscribe(OnEdit, ThreadOption.UIThread);

            OrderEditViewModel = new OrderEditViewModel();
            EmployeeEditViewModel = new EmployeeEditViewModel();
        }

        public BaseEditViewModel EmployeeEditViewModel { get; set; }
        public BaseEditViewModel OrderEditViewModel { get; set; }

        private void OnEdit(EditModel editModel)
        {
            switch (editModel.EditMode)
            {
                case EditModeEnum.EditEmployee:
                    EmployeeEditViewModel.Edit(editModel.ID);
                    break;
                case EditModeEnum.EditOrder:
                    OrderEditViewModel.Edit(editModel.ID);
                    break;
                case EditModeEnum.NewEmployee:
                    EmployeeEditViewModel.Create();
                    break;
                case EditModeEnum.NewOrder:
                    OrderEditViewModel.Create();
                    break;
            }
        }
    }
}
