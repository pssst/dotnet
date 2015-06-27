using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using pssst.Client.Interface;

namespace pssst.Client.ViewModels.DesignTime
{
    public sealed class NewMessagePageViewModel : INewMessagePageViewModel
    {
        public string Receiver { get; set; }

        public string Message { get; set; }

        public ICommand SendMessageCommand { get { return null; } }
    }
}
