using Microsoft.Maui.Controls;
using MiToDoListApp.Mobile.ViewModels;

namespace MiToDoListApp.Mobile.Behaviors
{
    public class ValidationErrorBehavior : Behavior<Entry>
    {
        private BaseViewModel? _viewModel;
        private string? _propertyName;

        protected override void OnAttachedTo(Entry entry)
        {
            entry.BindingContextChanged += OnBindingContextChanged;
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.BindingContextChanged -= OnBindingContextChanged;
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            if (sender is Entry entry)
            {
                _viewModel = entry.BindingContext as BaseViewModel;

                // Verificamos si el Entry tiene una binding a la propiedad Text
                var binding = entry.BindingContext as Binding;
                _propertyName = binding?.Path;
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry && _viewModel != null && !string.IsNullOrEmpty(_propertyName))
            {
                if (_viewModel.Errors.ContainsKey(_propertyName))
                {
                    entry.TextColor = Colors.Red;
                    entry.BackgroundColor = Color.FromArgb("#FFF0F0");
                }
                else
                {
                    entry.TextColor = Colors.Black;
                    entry.BackgroundColor = Colors.Transparent;
                }
            }
        }
    }
}
