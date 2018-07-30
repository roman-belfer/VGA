namespace Common.Infrastructure.Interfaces
{
    public interface INavigator
    {
        void Launch();
        void Index();
        void Filter();
        void Detail();

        void Back();

        object GetMenuView();
        bool IsFirstViewActive();
    }
}
