using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace pssst.Client.Interface
{
    public interface IPssstSettingsService
    {
        Uri ServerUriSetting { get; set; }

        string SelectedUser { get; set; }
    }
}
