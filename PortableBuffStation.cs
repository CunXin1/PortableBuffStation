using PortableBuffStation.Systems;
using Terraria.ID;
using Terraria.ModLoader;

namespace DisableBuffTimeDisplay
{
    public class DisableBuffTimeMod : Mod
    {
        public override void Load()
        {
           // 检查灾厄模组（CalamityMod）是否加载
            Mod calamity = ModLoader.GetMod("CalamityMod");
            if (calamity != null)
            {
                // 添加灾厄 buff station 的映射
                BuffLookups.AddCalamityBuffStations(calamity);
            }
        }

        public override void Unload()
        {
          // 建议在卸载时清空 ModdedStations，以防止静态变量残留
            BuffLookups.ModdedStations.Clear();
         
        }
        
    }
}
