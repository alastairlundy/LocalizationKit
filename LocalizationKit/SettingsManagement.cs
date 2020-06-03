using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AluminiumTech.LocalizationKit{
    /// <summary>
    /// 
    /// </summary>
    public class SettingsManagement : ManagementBase {
        
        private bool collect_error_information_with_rollbar;
        
        /// <summary>
        /// 
        /// </summary>
        public SettingsManagement(){
            GetSettingsInfo();
            if (_settings.GetValue("CollectErrorInfoAndCrashStatistics").Equals("false")){
                collect_error_information_with_rollbar = false;
            }
        }
               
        /// <summary>
        /// 
        /// </summary>
        public void GetSettingsInfo() {
            using (StreamReader file = File.OpenText(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Settings.json"))
            using (JsonTextReader reader = new JsonTextReader(file)) {
                JObject json = (JObject)JToken.ReadFrom(reader);

                _settings.PutIfAbsent("Locale", (string)json.GetValue("Locale"));
                _settings.PutIfAbsent("CheckForUpdatesOnStartup", (string)json.GetValue("CheckForUpdatesOnStartup"));
                _settings.PutIfAbsent("ShowAvailableUpdates", (string)json.GetValue("ShowAvailableUpdates"));
                _settings.PutIfAbsent("AutomaticallyDownloadAvailableUpdates", (string)json.GetValue("AutomaticallyDownloadAvailableUpdates"));
                _settings.PutIfAbsent("AutomaticallyInstallUpdates", (string)json.GetValue("AutomaticallyInstallUpdates"));
                _settings.PutIfAbsent("CollectErrorInfoAndCrashStatistics", (string)json.GetValue("CollectErrorInfoAndCrashStatistics"));
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetRollbarApikey(){
            string postServerItemAccessToken = File.ReadAllText(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Rollbar.txt");

            if (collect_error_information_with_rollbar.Equals(false)){
                //If it is false then make sure rollbar isn't initialized.
                //And make sure to clear the access token.
                postServerItemAccessToken = "";
            }

            return postServerItemAccessToken;
        }
    }
}