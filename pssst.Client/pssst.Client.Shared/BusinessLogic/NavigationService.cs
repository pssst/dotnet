using System;
using System.Collections.Generic;
using System.Text;
using pssst.Client.Interface;

namespace pssst.Client.BusinessLogic
{
    public class NavigationService : INavigationService
    {
        private readonly Microsoft.Practices.Prism.Mvvm.Interfaces.INavigationService navigationService;

        public NavigationService(Microsoft.Practices.Prism.Mvvm.Interfaces.INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public bool Navigate(Experiences experience, object param)
        {
            return this.navigationService.Navigate(experience.ToString(), param);
        }
    }
}
