using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using PortableBuffStation.Configs;

namespace PortableBuffStation.Systems
{
    public class ApplyBuffGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => false; // 对同type的物品共用逻辑

        /// <summary>
        /// 判断是否是随身增益站物品、旗帜、侏儒、蜂蜜等
        /// </summary>
        public static bool IsItemAvailable(Item item)
        {
            var config = ModContent.GetInstance<PortableBuffStationConfig>();
            if (item == null || item.IsAir) return false;

            // 1. 随身增益站（环境BuffTile）
            if (config.EnablePortableStations)
            {
                var tileBuffs = BuffTileLookups.GetTileBuffsForItem(item);
                if (tileBuffs.Count > 0)
                    return true;
            }

            // 2. 旗帜
            if (config.EnableBanners)
            {
                var npcTypes = BuffTileLookups.GetBannerNPCs(item);
                if (npcTypes.Length > 0)
                    return true;
            }

            // 3. 花园侏儒
            if (config.EnableGardenGnome && item.type == ItemID.GardenGnome)
            {
                return true;
            }

            // 4. 蜂蜜桶
            if (config.EnableHoney && item.type == ItemID.HoneyBucket)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 返回此物品能给玩家提供的BuffID列表（只针对环境Buff）
        /// 若是旗帜或侏儒或蜂蜜，就特殊处理
        /// </summary>
        public static List<int> GetItemBuffType(Item item)
        {
            var result = new List<int>();

            // 先看它是不是环境BuffTile
            var tileBuffs = BuffTileLookups.GetTileBuffsForItem(item);
            if (tileBuffs.Count > 0)
                result.AddRange(tileBuffs);

            // 花园侏儒(原版没有专门BuffID, 但它提供幸运加成)
            // 你可以用自定义BuffID, 或只在 Player层做 SceneMetrics.GnomeCount++ 
            // 这里先假设我们要给个“GnomeLuckBuff”= BuffID.Gnome
            if (item.type == ItemID.GardenGnome)
            {
                // 原版内部并没有 BuffID.GnomeLuck, 这里只是演示
                result.Add(BuffID.Lucky); // 假设Lucky
            }

            // 蜂蜜桶: 给 BuffID.Honey
            if (item.type == ItemID.HoneyBucket)
            {
                result.Add(BuffID.Honey);
            }

            return result;
        }

        /// <summary>
        /// 返回此物品针对哪些NPC有“旗帜增益”
        /// </summary>
        public static int[] GetBannerNPCs(Item item)
        {
            return BuffTileLookups.GetBannerNPCs(item);
        }

        // -------------以下是示例：给Tooltip加点说明-------------
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // 若不是增益站物品，就不加
            if (!IsItemAvailable(item))
                return;

            var tileBuffs = BuffTileLookups.GetTileBuffsForItem(item);
            var bannerNPCs = BuffTileLookups.GetBannerNPCs(item);
            bool isGnome = (item.type == ItemID.GardenGnome);
            bool isHoney = (item.type == ItemID.HoneyBucket);

            if (tileBuffs.Count > 0)
            {
                // 例如 “Provides Campfire Buff In Inventory”
                string buffNames = "";
                foreach (var b in tileBuffs)
                {
                    buffNames += Lang.GetBuffName(b) + ", ";
                }
                buffNames = buffNames.TrimEnd(',', ' ');
                tooltips.Add(new TooltipLine(Mod, "PortableBuffStation", $"[In Inventory] Provides: {buffNames}")
                {
                    OverrideColor = Color.SkyBlue
                });
            }

            if (bannerNPCs.Length > 0)
            {
                tooltips.Add(new TooltipLine(Mod, "PortableBanner", "[In Inventory] Provides Banner Buff")
                {
                    OverrideColor = Color.Yellow
                });
            }

            if (isGnome)
            {
                tooltips.Add(new TooltipLine(Mod, "PortableGnome", "[In Inventory] Provides Gnome Luck (example)")
                {
                    OverrideColor = Color.Green
                });
            }

            if (isHoney)
            {
                tooltips.Add(new TooltipLine(Mod, "PortableHoney", "[In Inventory] Provides Honey Buff")
                {
                    OverrideColor = Color.Orange
                });
            }
        }
    }
}
