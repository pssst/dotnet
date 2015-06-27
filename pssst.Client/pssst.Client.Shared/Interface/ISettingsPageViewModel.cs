using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using pssst.Client.Model;

namespace pssst.Client.Interface
{
    /// <summary>
    /// Represents the interface of the settings page ViewModel.
    /// </summary>
    public interface ISettingsPageViewModel
    {
        /// <summary>
        /// Gets or sets the pssst server uri.
        /// </summary>
        /// <value>
        /// The server pssst server uri.
        /// </value>
        string ServerUri { get; set; }

        /// <summary>
        /// Gets the available users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        ObservableCollection<string> Users { get; }

        /// <summary>
        /// Gets or sets the selected user.
        /// </summary>
        /// <value>
        /// The selected user.
        /// </value>
        string SelectedUser { get; set; }

        /// <summary>
        /// Gets or sets the name of the user that should be created.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        ICommand CreateUserCommand { get; }

        /// <summary>
        /// Sets the current user.
        /// </summary>
        ICommand SetUserCommand { get; }
    }
}
