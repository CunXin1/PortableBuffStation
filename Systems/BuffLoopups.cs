using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace MyMod.Systems
{
    /// <summary>
    /// 维护各种物品→Buff/NPC映射，用于药水、旗帜、增益站等
    /// 也留出ModdedXxx的字典以兼容其他Mod物品
    /// </summary>
    public static class BuffLookups
    {
        // 原版药水 (简化示例)
        public static Dictionary<int, int> VanillaPotions = new Dictionary<int, int>()
        {
            // 例如: IronskinPotion -> BuffID.Ironskin
            { ItemID.IronskinPotion, BuffID.Ironskin },
            { ItemID.SwiftnessPotion, BuffID.Swiftness },
            { ItemID.LuckPotion, BuffID.Lucky },
            // ... 这里需要自己补充
        };

        // ModdedPotions: 给外部或你自己加 Mod物品 => BuffID
        public static Dictionary<int, int> ModdedPotions = new Dictionary<int, int>();

        // 原版旗帜
        public static Dictionary<int, int[]> VanillaBanners = new Dictionary<int, int[]>()
        {
            // 例如: ItemID.ZombieBanner -> new[]{ NPCID.Zombie, NPCID.ZombieEskimo, ... }
            // 这里要自己补全
            
            // ...
        };

        // ModdedBanners: 给Mod物品 => npcType数组
        public static Dictionary<int, int[]> ModdedBanners = new Dictionary<int, int[]>();

        // 原版增益站(放置型)
        // 对于营火/心灯/星瓶/向日葵/侏儒/蜡烛 之类, 我们把 "物品ID -> buffID" 或 "物品ID -> specialFlag" 简化处理
        // 也可使用 SceneMetrics 方式, 这里仅做一个记录
        public static Dictionary<int, int> VanillaBuffStations = new Dictionary<int, int>()
        {
            // 例如: 全部营火 => BuffID.Campfire
            { ItemID.Campfire, BuffID.Campfire },
            { ItemID.BoneCampfire, BuffID.Campfire },
            { ItemID.CursedCampfire, BuffID.Campfire },
            // ...
            { ItemID.HeartLantern, BuffID.HeartLamp },
            { ItemID.StarinaBottle, BuffID.StarInBottle },
            { ItemID.Sunflower, BuffID.Sunflower },
            { ItemID.GardenGnome, -100 }, // 用 -100 表示侏儒, 需要特殊处理
            { ItemID.WaterCandle, BuffID.WaterCandle },
            { ItemID.PeaceCandle, BuffID.PeaceCandle },
            // ...
        };

        // ModdedStations
        public static Dictionary<int, int> ModdedStations = new Dictionary<int, int>();

        /// <summary>
        /// 返回 item 是否为原版或modded药水，以及对应的Buff
        /// </summary>
        public static int GetPotionBuff(int itemType)
        {
            if (VanillaPotions.TryGetValue(itemType, out var buff))
                return buff;
            if (ModdedPotions.TryGetValue(itemType, out var modBuff))
                return modBuff;
            return -1;
        }

        /// <summary>
        /// 返回 item 是否是旗帜，以及对应的NPCType[]
        /// </summary>
        public static int[] GetBannerNPCs(int itemType)
        {
            if (VanillaBanners.TryGetValue(itemType, out var npcs))
                return npcs;
            if (ModdedBanners.TryGetValue(itemType, out var modNPCs))
                return modNPCs;
            return new int[0];
        }

        /// <summary>
        /// 返回 item 是否是增益站物品(营火/心灯/侏儒/蜡烛等)
        /// 若是侏儒, 可能返回 -100 这样的特殊标记
        /// </summary>
        public static int GetStationBuff(int itemType)
        {
            if (VanillaBuffStations.TryGetValue(itemType, out var buff))
                return buff;
            if (ModdedStations.TryGetValue(itemType, out var modBuff))
                return modBuff;
            return -1;
        }
    }
}
