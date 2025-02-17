using Terraria.ModLoader;
using Terraria;
using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace PortableBuffStation.Configs
{
    public class PortableBuffStationConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Enable Portable Buff Stations")]
        [DefaultValue(true)]
        public bool EnablePortableStations;

        [Label("Enable Banners In Inventory")]
        [DefaultValue(true)]
        public bool EnableBanners;

        [Label("Enable Garden Gnome Effect In Inventory")]
        [DefaultValue(true)]
        public bool EnableGardenGnome;

        [Label("Enable Honey Bucket In Inventory")]
        [DefaultValue(true)]
        public bool EnableHoney;

        // 你还可以继续加更多开关
        // ...
    }
}
