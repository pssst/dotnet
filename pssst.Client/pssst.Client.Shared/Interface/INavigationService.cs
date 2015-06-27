using System;
using System.Collections.Generic;
using System.Text;

namespace pssst.Client.Interface
{
    public enum Experiences { CommunicationOverview, Settings, NewMessage }

    public interface INavigationService
    {
        bool Navigate(Experiences experience, object param);
    }
}
