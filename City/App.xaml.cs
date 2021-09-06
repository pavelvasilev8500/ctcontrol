using System.Windows;
using City.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using ModuleA;
using Prism.Regions;
using City.Core.Regions;
using System.Windows.Controls;

namespace City
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleAModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(Grid),
                Container.Resolve<GridRegionAdapter>());
        }
    }
}
