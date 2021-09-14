using ControlLibrary.Classes;

namespace ModuleA.Models
{
    public class SettingsModel
    {

        Autorun autorun = new Autorun(); 

        public void ResetSettings()
        {
            Properties.Settings.Default.Swither = false;
            autorun.SetAutorunValue(Properties.Settings.Default.Swither);
            Properties.Settings.Default.Save();
        }
    }
}
