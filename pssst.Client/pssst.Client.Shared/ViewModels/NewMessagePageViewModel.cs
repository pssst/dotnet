using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using pssst.Client.Interface;

namespace pssst.Client.ViewModels
{
    public sealed class NewMessagePageViewModel : ViewModel, INewMessagePageViewModel
    {
        private IPssstClientService pssstService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewMessagePageViewModel"/> class.
        /// </summary>
        /// <param name="pssstService">The PSSST service.</param>
        public NewMessagePageViewModel(IPssstClientService pssstService)
        {
            this.pssstService = pssstService;
        }

        private string receiver;

        public string Receiver
        {
            get
            {
                return this.receiver;
            }
            set
            {
                SetProperty(ref this.receiver, value);
                ((DelegateCommand)this.sendMessageCommand).RaiseCanExecuteChanged();
            }
        }

        private string message;

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                SetProperty(ref this.message, value);
                ((DelegateCommand)this.sendMessageCommand).RaiseCanExecuteChanged();
            }
        }

        private ICommand sendMessageCommand;
        public ICommand SendMessageCommand
        {
            get
            {
                if (this.sendMessageCommand == null)
                {
                    this.sendMessageCommand = DelegateCommand.FromAsyncHandler(
                        this.ExecuteSendMessageCommand,
                        this.CanExecuteSendMessageCommand);
                }

                return this.sendMessageCommand;
            }
        }

        private async Task ExecuteSendMessageCommand()
        {
            this.pssstService.SendMessage(this.Receiver, this.Message);
        }

        private bool CanExecuteSendMessageCommand()
        {
            return this.pssstService != null 
                && !string.IsNullOrWhiteSpace(this.Receiver) 
                && !string.IsNullOrWhiteSpace(this.Message);
        }
    }
}
