using Terraria.ModLoader;
using Terraria;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MyMod.Configs
{
    public class MyModConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Enable Portable Buff Stations")]
        [DefaultValue(true)]
        public bool EnablePortableStations;


    }
}
