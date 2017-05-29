using Autofac;
using EFT.Infrastructure.Settings;
using System;
using System.Linq;
using System.Reflection;


namespace EFT.Infrastructure.Module
{
    public class SettingsModule : Autofac.Module
    {
        private readonly string _configurationFilePath;
        private readonly string _sectionNameSuffix;
        public SettingsModule(string configurationFilePath, string sectionNameSuffix = "Settings")
        {
            _configurationFilePath = AppDomain.CurrentDomain.BaseDirectory  + configurationFilePath;
            _sectionNameSuffix = sectionNameSuffix;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new Settings.SettingsReader(_configurationFilePath, _sectionNameSuffix))
                .As<ISettingsReader>()
                .SingleInstance();
            var settings = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.EndsWith(_sectionNameSuffix, StringComparison.InvariantCulture))
                .ToList();

            settings.ForEach(type =>
            {
                builder.Register(C => C.Resolve<ISettingsReader>().LoadSection(type))
                .As(type)
                .SingleInstance();
            });
        }

    }
}
