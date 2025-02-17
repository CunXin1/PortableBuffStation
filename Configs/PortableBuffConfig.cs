using Terraria.ModLoader.Config;
using System.ComponentModel;

public class PortableBuffConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(true)]
    public bool EnableCampfireBuff { get; set; }

    [DefaultValue(true)]
    public bool EnableHeartLanternBuff { get; set; }

    [DefaultValue(true)]
    public bool EnableSunflowerBuff { get; set; }

    [DefaultValue(true)]
    public bool EnableStarInBottleBuff { get; set; }
}
 
 