using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pssst.Client.Interface;
using Windows.Storage;

namespace pssst.Client.BusinessLogic
{
    public sealed class PssstSettingsService : IPssstSettingsService
    {
        private readonly ApplicationDataContainer settings;
        private readonly IUserRepository userRepository;

        private const string ServerUriSettingKeyName = "ServerUriSetting";
        private const string SelectedUserSettingKeyName = "SelectedUserSetting";

        private static readonly string ServerUriSettingDefault = "http://localhost";
        private static readonly string SelectedUserSettingDefault = string.Empty;

        public PssstSettingsService(
            IUserRepository userRepository)
        {
            this.settings = ApplicationData.Current.RoamingSettings;
            this.userRepository = userRepository;
        }

        private bool AddOrUpdateValue(string key, object value)
        {
            bool valueChanged = false;

            if (this.settings.Values.ContainsKey(key))
            {
                if (this.settings.Values[key] != value)
                {
                    this.settings.Values[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                this.settings.Values.Add(key, value);
                valueChanged = true;
            }

            return valueChanged;
        }

        private T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            if (this.settings.Values.ContainsKey(key))
            {
                value = (T)this.settings.Values[key];
            }
            else
            {
                value = defaultValue;
            }

            return value;
        }

        public Uri ServerUriSetting
        {
            get
            {
                return new Uri(this.GetValueOrDefault<string>(PssstSettingsService.ServerUriSettingKeyName, PssstSettingsService.ServerUriSettingDefault));
            }
            set
            {
                AddOrUpdateValue(PssstSettingsService.ServerUriSettingKeyName, value.ToString());
            }
        }

        public string SelectedUser
        {
            get
            {
                return this.GetValueOrDefault<string>(PssstSettingsService.SelectedUserSettingKeyName, PssstSettingsService.SelectedUserSettingDefault);
            }
            set
            {
                AddOrUpdateValue(PssstSettingsService.SelectedUserSettingKeyName, value);
            }
        }
    }
}
