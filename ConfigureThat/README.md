# Configure That

A small mod to configure some small parts of the game where I haven't found a satisfying other small mod to configure
some things that I always wanted to configure. There is a huge variety of mods out there that do many things and also
maybe configure some of those parts, but if you look for something lightweight that helps to just change these values,
you might have a hard time to find something similar. I was using other huge mods just because they also enabled me to
change those values, but I wanted to get rid of conflicts and huge mods to just change these tiny things.

## Features

This mod aims to configure some simple and very basic values of the vanilla components of objects:

1. ItemDrop
2. Plant

The mod uses ServerSync and YamlDotNet.

### Configured Values

It does not configure everything for those, since there is a huge variety of things to configure:

1. ItemDrop
    * AutoPickup (true/false)
    * Teleportable (true/false)
    * Weight
    * Value (to be able to sell it to the trader)
    * MaxStackSize (up to how big a stack can be)
2. Plant
    * GrowTime
    * GrowRadius
    * MinScale
    * MaxScale
    * NeedCultivatedGround (true/false)
    * DestroyIfCantGrow (true/false)

### Reading configured values from YAML file

The mod comes with 2 prepared YAML files for purely vanilla items and plants. But you can generate the same files for
also your loaded mods, see next section.

Note: in both cases the header element is always the prefab name of the object.

#### Example content for a configured item

```yaml
Acorn:
  AutoPickup: true
  Teleportable: true
  Weight: 0.1
  Value: 0
  MaxStackSize: 100
  ItemType: Material
```

Note: the ItemType can be ignored, the mod will not change the item type, even if added to the config, this is used
purely to group information.

#### Example content for a configured plant

```yaml
Oak_Sapling:
  GrowTime: 6000
  GrowRadius: 3
  MinScale: 0.7
  MaxScale: 0.9
  NeedCultivatedGround: false
  DestroyIfCantGrow: true
```

### Output a prepared file from your game and mods

You can use the console command `configure_that_write_defaults` to let the mod create 1 prepared file for each of the
configs. The file created has a slightly different name than the file that is read into the mod to change the configured
values. You will have to rename the file from e.g. "FixItFelix.ConfigureThat.Default.ItemDrops.yaml" to "
FixItFelix.ConfigureThat.ItemDrops.yaml" to enable the configured values.

NOTE: for using the output of the ItemDrops file, you will need to change the structure a bit, since the prepared output
is grouped by item type (to make is easier to find something maybe).

# Miscellaneous

<details>
  <summary>License, credits, attributions</summary>

* icon [config by Flatart](https://thenounproject.com/browse/icons/term/config/) (CC BY 3.0)

</details>

<details>
  <summary>Contact</summary>

* https://github.com/FelixReuthlinger/ConfigureThat
* Discord: `fluuxxx` (you can find me around some of the Valheim modding discords, too)

</details>
