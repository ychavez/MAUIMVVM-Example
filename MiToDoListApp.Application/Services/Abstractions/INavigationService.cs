namespace MiToDoListApp.Applicationx.Services.Abstractions
{
    public interface INavigationService
    {

        Task NavigateToAsync(Type pageType);
        Task GoBackAsync();
    }
}
