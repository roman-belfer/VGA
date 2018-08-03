namespace Common.Infrastructure.Interfaces
{
    public interface INavigator
    {
        void Launch();
        void Index();
        void Filter();
        void Detail();
        void Edit();
        void StartMenu();

        void Back();

        object GetMenuView();
        bool IsFirstViewActive();
    }
}
