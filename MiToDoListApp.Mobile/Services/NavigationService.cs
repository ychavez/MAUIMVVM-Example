using MiToDoListApp.Applicationx.Services.Abstractions;


namespace MiToDoListApp.Mobile.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation _navigation;

        public NavigationService()
        {

            _navigation = Application.Current.Windows[0].Page.Navigation;
        }

        public async Task NavigateToAsync(Type pageType)
        {
            if (typeof(Page).IsAssignableFrom(pageType))
            {
                var page = Activator.CreateInstance(pageType) as Page;
                await _navigation.PushAsync(page);
            }
        }
        public async Task GoBackAsync()
        {
            await _navigation.PopAsync();
        }
    }
}
