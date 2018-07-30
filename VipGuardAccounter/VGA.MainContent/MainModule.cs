using Common.Infrastructure.Interfaces;
using VGA.MainContent.Views;

namespace VGA.MainContent
{
    public class MainModule : IMainModule
    {
        public void Launch()
        {
            new MainContentView().Show();
        }
    }
}
