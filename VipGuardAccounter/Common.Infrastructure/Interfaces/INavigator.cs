﻿using Common.Infrastructure.Interfaces.Views;

namespace Common.Infrastructure.Interfaces
{
    public interface INavigator
    {
        void Launch();
        void Index();
        void Orders();
        void Filter();
        void Detail();
        void Edit();
        void StartMenu();

        void Back();

        object GetMenuView();
        bool IsFirstViewActive();
        bool IsSearchAvailable();
        bool IsOrdersActive();
        bool IsEmployeesActive();
    }
}
