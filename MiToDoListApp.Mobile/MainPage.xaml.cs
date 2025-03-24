using MiToDoListApp.Applicationx.Services.Abstractions;

namespace MiToDoListApp.Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly INavigationService navigationService;
        int count = 0;

        public MainPage(INavigationService navigationService)
        {
            InitializeComponent();
            this.navigationService = navigationService;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            navigationService.NavigateToAsync(typeof(AddTodoPage));
        }
    }

}
