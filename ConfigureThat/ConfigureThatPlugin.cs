using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using ConfigureThat.Data;
using ConfigureThat.Model;
using HarmonyLib;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using ServerSync;
using Paths = BepInEx.Paths;

namespace ConfigureThat
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ConfigureThatPlugin : BaseUnityPlugin
    {
        internal const string PluginAuthor = "FixItFelix";
        internal const string PluginGuid = PluginAuthor + "." + PluginName;
        internal const string PluginName = "ConfigureThat";
        internal const string PluginVersion = "1.0.1";

        private static ConfigureThatPlugin _instance = null!;

        private const string ConfigFileName = PluginGuid + ".cfg";

        internal static readonly ConfigSync ConfigSync = new(PluginGuid)
        {
            DisplayName = PluginName,
            CurrentVersion = PluginVersion
        };

        private static ConfigEntry<bool> _configLocked = null!;

        public void Awake()
        {
            _instance = this;

            _configLocked = CreateConfig("1 - General", "Lock Configuration", true,
                "If 'true' and playing on a server, config can only be changed on server-side configuration, " +
                "clients cannot override");
            ConfigSync.AddLockingConfigEntry(_configLocked);

            PrefabManager.OnPrefabsRegistered += Registry.Initialize;
            CommandManager.Instance.AddConsoleCommand(new PlantsPrintController());
            
            SetupFileWatcher(ConfigFileName);
        }

        private void SetupFileWatcher(string fileName)
        {
            FileSystemWatcher watcher = new(Paths.ConfigPath, fileName);
            watcher.Changed += ReloadConfig;
            watcher.Created += ReloadConfig;
            watcher.Renamed += ReloadConfig;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }

        private void ReloadConfig(object _, FileSystemEventArgs __)
        {
            Config.Reload();
        }

        private static ConfigEntry<T> CreateConfig<T>(string group, string parameterName, T value,
            ConfigDescription description,
            bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = _instance.Config.Bind(group, parameterName, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = ConfigSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        private static ConfigEntry<T> CreateConfig<T>(string group, string parameterName, T value, string description,
            bool synchronizedSetting = true) => CreateConfig(group, parameterName, value,
            new ConfigDescription(description), synchronizedSetting);
    }

    public class PlantsPrintController : ConsoleCommand
    {
        public override void Run(string[] args)
        {
            Registry.WriteDefaultFiles();
        }

        public override string Name => "configure_that_write_defaults";

        public override string Help =>
            "Write all prefabs loaded in-game into a YAML defaults config file inside the BepInEx config folder. " +
            "Can be used to create your own configs without creating everything by hand. " +
            "You will just need to rename the file and remove the '.Default'";
    }
}