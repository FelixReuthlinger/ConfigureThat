using JetBrains.Annotations;

namespace ConfigureThat.Model;

public class ItemDropModel : Configurable<ItemDrop>
{
    [UsedImplicitly] public bool AutoPickup;
    [UsedImplicitly] public bool Teleportable;
    [UsedImplicitly] public float Weight;
    [UsedImplicitly] public int Value;
    [UsedImplicitly] public int MaxStackSize;
    [UsedImplicitly] public string ItemType;

    public static ItemDropModel From(ItemDrop original)
    {
        return new ItemDropModel
        {
            ItemType = original.m_itemData.m_shared.m_itemType.ToString(),
            AutoPickup = original.m_autoPickup,
            Teleportable = original.m_itemData.m_shared.m_teleportable,
            Weight = original.m_itemData.m_shared.m_weight,
            Value = original.m_itemData.m_shared.m_value,
            MaxStackSize = original.m_itemData.m_shared.m_maxStackSize
        };
    }

    public override void Configure(ItemDrop original)
    {
        original.m_autoPickup = AutoPickup;
        original.m_itemData.m_shared.m_teleportable = Teleportable;
        original.m_itemData.m_shared.m_weight = Weight;
        original.m_itemData.m_shared.m_value = Value;
        original.m_itemData.m_shared.m_maxStackSize = MaxStackSize;
    }
}