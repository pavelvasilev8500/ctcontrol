using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleAModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["ContentRegion"];

            var languageView = containerProvider.Resolve<LanguageView>();
            var backgroundView = containerProvider.Resolve<BackgroundView>();
            var commonView = containerProvider.Resolve<CommonView>();
            var settingsView = containerProvider.Resolve<SettingsView>();
            var mainView = containerProvider.Resolve<MainView>();
            region.Add(mainView);
            region.Add(settingsView);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();
            containerRegistry.RegisterForNavigation<SettingsView>();
            containerRegistry.RegisterForNavigation<LanguageView>();
            containerRegistry.RegisterForNavigation<BackgroundView>();
            containerRegistry.RegisterForNavigation<CommonView>();
        }
    }
}
