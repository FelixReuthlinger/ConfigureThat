using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ConfigureThat.Model;
using Jotunn;
using Jotunn.Managers;

namespace ConfigureThat.Data;

public static class Registry
{
    private const string Plants = "Plants";
    private const string ItemDrops = "ItemDrops";

    private static ConfigObject<PlantModel, Plant> PlantsConfig;
    private static ConfigObject<ItemDropModel, ItemDrop> ItemsConfig;

    public static void Initialize()
    {
        Logger.LogInfo($"initializing registry");
        PlantsConfig = new ConfigObject<PlantModel, Plant>(Plants);
        ItemsConfig = new ConfigObject<ItemDropModel, ItemDrop>(ItemDrops);

        PlantsConfig.ConfigureAll();
        ItemsConfig.ConfigureAll();
    }

    public static void WriteDefaultFiles()
    {
        Dictionary<string, PlantModel> plantsFromGame = PrefabManager.Cache.GetPrefabs(typeof(Plant))
            .GroupBy(kv => CleanGameObjectName(kv.Key))
            .ToDictionary(
                group => group.Key,
                group => PlantModel.From((Plant)group.FirstOrDefault().Value)
            );
        ConfigFileAccess.WriteFile(
            fileNameAndPath: ConfigFileAccess.GetFileNameAndPath(Plants, isDefaultFile: true),
            fileContent: ConfigFileAccess.Serialize(plantsFromGame)
        );

        Dictionary<string, Dictionary<string, ItemDropModel>> itemsFromGame = PrefabManager.Cache.GetPrefabs(typeof(ItemDrop))
            .GroupBy(kv => CleanGameObjectName(kv.Key))
            .ToDictionary(
                group => group.Key,
                group => ItemDropModel.From((ItemDrop)group.FirstOrDefault().Value)
            ).GroupBy(kv => kv.Value.ItemType)
            .ToDictionary(
                group => group.Key,
                group => group.OrderBy(kv => kv.Key)
                    .ToDictionary(kv => kv.Key, kv => kv.Value)
            );
        ConfigFileAccess.WriteFile(
            fileNameAndPath: ConfigFileAccess.GetFileNameAndPath(ItemDrops, isDefaultFile: true),
            fileContent: ConfigFileAccess.Serialize(itemsFromGame)
        );
    }

    private static string CleanGameObjectName(string dirtyObject)
    {
        return Regex.Replace(dirtyObject, @"\(\d\)", "").Trim();
    }
}