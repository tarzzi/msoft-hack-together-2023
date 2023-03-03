using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSiteTools
{
    public sealed record CountChangedMessage(string Text);
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string message = "Click me";

        private int count;

        [RelayCommand]
        private void IncrementCounter()
        {
            count++;
            if (count == 1)
                message = $"Clicked {count} time";
            else
                message = $"Clicked {count} times";

            WeakReferenceMessenger.Default.Send(new CountChangedMessage(message));
        }
    }
}
