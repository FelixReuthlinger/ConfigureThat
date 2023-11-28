using JetBrains.Annotations;

namespace ConfigureThat.Model;

public class PlantModel : Configurable<Plant>
{
    [UsedImplicitly] public float GrowTime;
    [UsedImplicitly] public float GrowRadius;
    [UsedImplicitly] public float MinScale;
    [UsedImplicitly] public float MaxScale;
    [UsedImplicitly] public bool NeedCultivatedGround;
    [UsedImplicitly] public bool DestroyIfCantGrow;

    public static PlantModel From(Plant original)
    {
        return new PlantModel
        {
            GrowTime = original.m_growTime,
            GrowRadius = original.m_growRadius,
            MinScale = original.m_minScale,
            MaxScale = original.m_maxScale,
            NeedCultivatedGround = original.m_needCultivatedGround,
            DestroyIfCantGrow = original.m_destroyIfCantGrow
        };
    }

    public override void Configure(Plant original)
    {
        if (GrowRadius != 0) original.m_growRadius = GrowRadius;
        if (GrowTime != 0) original.m_growTime = GrowTime;
        if (MinScale != 0) original.m_minScale = MinScale;
        if (MaxScale != 0) original.m_maxScale = MaxScale;
        original.m_needCultivatedGround = NeedCultivatedGround;
        original.m_destroyIfCantGrow = DestroyIfCantGrow;
    }
}