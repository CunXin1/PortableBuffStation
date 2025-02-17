using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using MyMod.Configs;
using MyMod.Systems;

namespace MyMod
{
    /// <summary>
    /// 物品层面的逻辑：判断是不是无限药水、是不是旗帜、是不是增益站
    /// 并给出 Tooltip
    /// </summary>
    public class InfBuffGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => false;

        /// <summary>
        /// 返回此物品可能提供的Buff列表(药水Buff 或 增益站Buff)
        /// 也可包含侏儒/蜂蜜等特殊判断
        /// </summary>
        public static List<int> GetBuffTypes(Item item)
        {
            var list = new List<int>();
            var config = ModContent.GetInstance<MyModConfig>();

            // 1) 如果是药水 (检查数量是否≥config.InfinitePotionRequirement)
            if (config.EnableInfinitePotions && item.stack >= config.InfinitePotionRequirement)
            {
                int buff = BuffLookups.GetPotionBuff(item.type);
                if (buff != -1)
                    list.Add(buff);
            }

            // 2) 如果是增益站
            if (config.EnablePortableStations)
            {
                int stationBuff = BuffLookups.GetStationBuff(item.type);
                if (stationBuff != -1)
                    list.Add(stationBuff);
                // 侏儒 -> stationBuff = -100, 代表要特殊处理
                // 也可以在InfBuffPlayer里处理
            }

            // 3) 如果是旗帜
            if (config.EnableBanners)
            {
                int[] npcs = BuffLookups.GetBannerNPCs(item.type);
                // 旗帜Buff一般不是AddBuff, 需要后续SceneMetrics/NPCBannerBuff[npcType] = true
                // 这里可以返回一个特殊ID，比如 -200, or just do nothing
                if (npcs.Length > 0)
                {
                    // 这里仅返回一个标记
                    list.Add(-200); // -200 => 代表是旗帜
                }
            }

            // 你也可以处理 “HoneyBucket => BuffID.Honey” 之类

            return list;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // 如果它是可用的无限Buff物品，就加一些提示
            var buffTypes = GetBuffTypes(item);
            if (buffTypes.Count > 0)
            {
                tooltips.Add(new TooltipLine(Mod, "InfBuff", "[In Inventory] Provides infinite buff(s)."));
            }
        }
    }
}
