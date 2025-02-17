using Terraria.ModLoader;
using Terraria;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MyMod.Configs
{
    public class MyModConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Enable Infinite Potions")]
        [DefaultValue(true)]
        public bool EnableInfinitePotions;

        [Label("Enable Portable Buff Stations")]
        [DefaultValue(true)]
        public bool EnablePortableStations;

        [Label("Enable Banners In Inventory")]
        [DefaultValue(true)]
        public bool EnableBanners;

        [Label("Hide Short Buff Icons (0s)")]
        [DefaultValue(true)]
        public bool HideShortBuffIcons;

        [Label("Potion Requirement For Infinite")]
        [DefaultValue(30)]
        [Range(1, 999)]
        public int InfinitePotionRequirement;

        // 你可以再加更多开关，比如:
        // [Label("Enable Gnome Luck In Inventory")]
        // [DefaultValue(true)]
        // public bool EnableGnomeLuck;

        // ...
    }
}
