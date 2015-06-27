using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace pssst.Client.Interface
{
    public interface INewMessagePageViewModel
    {
        string Receiver { get; set; }
        string Message { get; set; }
        ICommand SendMessageCommand { get; }
    }
}
