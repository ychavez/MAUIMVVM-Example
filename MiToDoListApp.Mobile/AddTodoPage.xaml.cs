using MiToDoListApp.Mobile.ViewModels;

namespace MiToDoListApp.Mobile;

public partial class AddTodoPage : ContentPage
{
	public AddTodoPage(TaskListViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}