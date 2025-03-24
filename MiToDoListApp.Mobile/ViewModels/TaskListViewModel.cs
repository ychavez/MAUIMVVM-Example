
using MediatR;
using MiToDoListApp.Domain.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MiToDoListApp.Mobile.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
       private TaskItem? _selectedTask;

        public ObservableCollection<TaskItem> Tasks { get; } = new() { new TaskItem { Description = "test" } };

        public ICommand LoadTasksCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand TaskSelectedCommand { get; }
        public ICommand ToggleTaskStatusCommand { get; }

        public TaskItem? SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        public TaskListViewModel(IMediator mediator)
        {
            _mediator = mediator;

            // Inicializar comandos
            LoadTasksCommand = new Command(async () => await LoadTasks());
            AddTaskCommand = new Command(async () => await GoToAddTaskPage());
            TaskSelectedCommand = new Command(async () => await OnTaskSelected());
          //  ToggleTaskStatusCommand = new Command<TaskItem>(async (task) => await ToggleTaskStatus(task));

            // Cargar tareas al iniciar
            LoadTasksCommand.Execute(null);
        }

        private async Task LoadTasks()
        {
            try
            {
                //IsBusy = true; // Activar indicador de carga
                //var query = new GetTasksQuery { Filter = TaskFilter.All };
                //var tasks = await _mediator.Send(query);

                //Tasks.Clear();
                //foreach (var task in tasks) Tasks.Add(task);
            }
            catch (FluentValidation.ValidationException ex)
            {
                HandleValidationErrors(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ToggleTaskStatus(TaskItem task)
        {
            try
            {
                //var command = new ToggleTaskStatusCommand { TaskId = task.Id };
                //await _mediator.Send(command);

                //// Actualizar estado local
                //task.IsCompleted = !task.IsCompleted;
            }
            catch (FluentValidation.ValidationException ex)
            {
                HandleValidationErrors(ex);
            }
        }

        private async Task OnTaskSelected()
        {
            if (SelectedTask == null) return;

            await Shell.Current.GoToAsync($"TaskDetailPage?TaskId={SelectedTask.Id}");
            SelectedTask = null; // Limpiar selección
        }

        private async Task GoToAddTaskPage()
        {
            await Shell.Current.GoToAsync("AddTaskPage");
        }

        private void HandleValidationErrors(FluentValidation.ValidationException ex)
        {
            Errors.Clear();
            foreach (var error in ex.Errors)
            {
                if (!Errors.ContainsKey(error.PropertyName))
                    Errors[error.PropertyName] = new List<string>();

                Errors[error.PropertyName].Add(error.ErrorMessage);
                OnErrorsChanged(error.PropertyName);
            }
        }
    }
}
