using MiToDoListApp.Applicationx.Services.Abstractions;


namespace MiToDoListApp.Mobile.Services
{
    public class NavigationService : INavigationService
    {
    

        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task NavigateToAsync(Type pageType)
        {
            try
            {
                if (!typeof(Page).IsAssignableFrom(pageType))
                    throw new ArgumentException("El tipo debe ser una Page de MAUI");

                // Resuelve la Page desde el contenedor de DI
                var page = _serviceProvider.GetService(pageType) as Page;

                if (page != null)
                {
                    // Usa la navegación de Shell
                    await Shell.Current.Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
             //   Debug.WriteLine($"Error de navegación: {ex.Message}");
                throw;
            }
        }
        public async Task GoBackAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
