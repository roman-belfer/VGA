namespace Common.Infrastructure.Interfaces.Views
{
    public interface IView
    {
        IView Previous { get; set; }
        IView Next { get; set; }
    }
}
