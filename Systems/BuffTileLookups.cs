using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PortableBuffStation.Systems
{
    /// <summary>
    /// 存放原版增益站的相关数据，以及可给其他Mod物品留接口。
    /// </summary>
    public static class BuffTileLookups
    {
        /// <summary>
        /// 原版环境增益站：放置后给玩家Buff的Tile
        /// 这里的Style若不区分可以填 -1
        /// </summary>
        public struct BuffTileEntry
        {
            public int TileID;
            public int Style; // -1 表示忽略style
            public int BuffID;

            public BuffTileEntry(int tileID, int style, int buffID)
            {
                TileID = tileID;
                Style = style;
                BuffID = buffID;
            }
        }

        // 原版增益站Tile列表
        public static List<BuffTileEntry> VanillaBuffTiles = new List<BuffTileEntry>()
        {
            // 例如：心灯 (Heart Lantern)
            // Heart Lantern在游戏中TileID = TileID.Lamps, placeStyle = 1(具体要确认)
            new BuffTileEntry(TileID.HangingLanterns, 1, BuffID.HeartLamp),
            // 星瓶 (Star in a Bottle)
            new BuffTileEntry(TileID.Bottles, -1, BuffID.StarInBottle),
            // 女巫桌/弹药箱/磨刀架/水晶球 这些是右键Buff，但若你想背包就生效可写在这里
            // ...
        };

        // 营火：多个变体TileID都是 TileID.Campfire，但 style 不同
        // 如果你只想统一给 BuffID.Campfire，就写 style=-1
        public static List<int> CampfireItemIDs = new List<int>()
        {
            ItemID.Campfire, 
        };

        // 旗帜：在游戏中是 NPCBannerBuff[npcType] = true; 并 player.hasBanner = true;
        // 这里可以放一个 “物品ID -> 对应NPCType数组”的映射
        public static Dictionary<int, int[]> BannerToNPCs = new Dictionary<int, int[]>
        {
            // 例如 ItemID.ZombieBanner -> new int[]{ NPCID.Zombie, NPCID.ZombieEskimo, ... }
            // TODO: 你可以自己填充或动态生成
        };

        // 其他Mod也可以注册自己的BuffTile
        public static Dictionary<int, List<int>> ModdedItemToBuffIDs = new Dictionary<int, List<int>>();
        public static Dictionary<int, int[]> ModdedBannerToNPCs = new Dictionary<int, int[]>();

        /// <summary>
        /// 判断某物品是否是“环境增益站”型的Tile，比如心灯、星瓶等
        /// 如果是，就返回对应的BuffID列表（可能不止一个）
        /// </summary>
        public static List<int> GetTileBuffsForItem(Item item)
        {
            var buffIDs = new List<int>();

            // 营火（不区分TileID，直接给BuffID.Campfire）
            if (CampfireItemIDs.Contains(item.type))
            {
                buffIDs.Add(BuffID.Campfire);
                return buffIDs;
            }

            // 其他“放置型BuffTile”
            // 检查 item.createTile + item.placeStyle 是否在 VanillaBuffTiles
            if (item.createTile >= 0)
            {
                foreach (var entry in VanillaBuffTiles)
                {
                    if (entry.TileID == item.createTile)
                    {
                        if (entry.Style == -1 || entry.Style == item.placeStyle)
                        {
                            buffIDs.Add(entry.BuffID);
                        }
                    }
                }
            }

            // 额外检查ModdedItemToBuffIDs
            if (ModdedItemToBuffIDs.TryGetValue(item.type, out var modBuffs))
            {
                buffIDs.AddRange(modBuffs);
            }

            return buffIDs;
        }

        /// <summary>
        /// 判断是否是“旗帜”型物品（背包生效时给对应NPC的额外伤害/减伤）
        /// </summary>
        public static int[] GetBannerNPCs(Item item)
        {
            // 先查原版 BannerToNPCs
            if (BannerToNPCs.TryGetValue(item.type, out var npcTypes))
                return npcTypes;

            // 再查 ModdedBannerToNPCs
            if (ModdedBannerToNPCs.TryGetValue(item.type, out var modNPCs))
                return modNPCs;

            // 否则返回空
            return new int[0];
        }
    }
}
