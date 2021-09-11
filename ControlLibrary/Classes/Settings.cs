using System;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace ControlLibrary.Classes
{
    public class Settings
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}//Settings.json";
        private ObservableCollection<Data.Data> settings;

        private ObservableCollection<Data.Data> DefaultData()
        {
            settings = new ObservableCollection<Data.Data>();
            settings.Add(new Data.Data(true, 0, false));
            return settings;
        }

        public void SetDefaultData()
        {
            SaveSettings(DefaultData());
        }

        public void SaveSettings(object settings)
        {
            using (StreamWriter writer = new StreamWriter(PATH))
            {
                object outputSettings = JsonConvert.SerializeObject(settings);
                writer.Write(outputSettings);
            }
        }

        public ObservableCollection<Data.Data> LoadSettings()
        {
            if (!File.Exists(PATH))
            {
                SetDefaultData();
                var defualtSettings = LoadSettings();
                return defualtSettings;
            }
            else
            {
                using (var reader = File.OpenText(PATH))
                {
                    try
                    {
                        var settings = reader.ReadToEnd();
                        var inputSettings = JsonConvert.DeserializeObject<ObservableCollection<Data.Data>>(settings);
                        return inputSettings;
                    }
                    catch (Exception)
                    {
                        return DefaultData();
                    }
                }
            }
        }
    }
}
