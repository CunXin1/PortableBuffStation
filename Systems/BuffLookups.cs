using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace PortableBuffStation.Systems
{
    /// <summary>
    /// 维护各种物品→Buff/NPC映射，用于药水、旗帜、增益站等
    /// 也留出ModdedXxx的字典以兼容其他Mod物品
    /// </summary>
    public static class BuffLookups
    {
        // 原版增益站(放置型)
        // 对于营火/心灯/星瓶/向日葵/侏儒/蜡烛 之类, 我们把 "物品ID -> buffID" 或 "物品ID -> specialFlag" 简化处理
        // 也可使用 SceneMetrics 方式, 这里仅做一个记录
        public static Dictionary<int, int> VanillaBuffStations = new Dictionary<int, int>()
        {
        // 1. 只保留一个普通营火 => BuffID.Campfire
        { ItemID.Campfire, BuffID.Campfire },

        // 2. 心形灯笼、星瓶、向日葵
        { ItemID.HeartLantern, BuffID.HeartLamp },
        { ItemID.StarinaBottle, BuffID.StarInBottle },
        { ItemID.Sunflower, BuffID.Sunflower },

        // 3. 花园侏儒（原版没有对应Buff，用 -100 代表“侏儒Luck”，需特殊逻辑处理）
        { ItemID.GardenGnome, -100 },

        // 4. 三种蜡烛：水蜡烛、和平蜡烛、暗影蜡烛
        { ItemID.WaterCandle, BuffID.WaterCandle },
        { ItemID.PeaceCandle, BuffID.PeaceCandle },
        { ItemID.ShadowCandle, BuffID.ShadowCandle },

        // 5. 巴斯特雕像（BuffID.Bast）
        { ItemID.CatBast, BuffID.CatBast },

        // 6. 女巫桌、弹药箱、磨刀架、水晶球、蛋糕
        { ItemID.BewitchingTable, BuffID.Bewitched },    // +1仆从上限
        { ItemID.AmmoBox, BuffID.AmmoBox },              // 20%几率不消耗弹药
        { ItemID.SharpeningStation, BuffID.Sharpened },  // 近战武器穿刺
        { ItemID.CrystalBall, BuffID.Clairvoyance },     // +2魔力上限等
        { ItemID.SliceOfCake, BuffID.SugarRush },        // 移速与攻击速度提升

        // 7. 蜂蜜桶（如果你想在背包里也能享受蜂蜜Buff）
        { ItemID.HoneyBucket, BuffID.Honey }
        };

        // ModdedStations
        public static Dictionary<int, int> ModdedStations = new Dictionary<int, int>();



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



    /// <summary>
    /// 这里将其 buffNoTimeDisplay 设置为 true，隐藏倒计时。
    /// </summary>
    public class HideBuff : ModSystem
    {
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {

            Main.buffNoTimeDisplay[BuffID.Campfire] = true; // 营火
            Main.buffNoTimeDisplay[BuffID.HeartLamp] = true; // 心形灯笼
            Main.buffNoTimeDisplay[BuffID.Sunflower] = true; // 向日葵
            Main.buffNoTimeDisplay[BuffID.StarInBottle] = true; // 星瓶
            Main.buffNoTimeDisplay[BuffID.WaterCandle] = true; // 水蜡烛
            Main.buffNoTimeDisplay[BuffID.PeaceCandle] = true; // 和平蜡烛
            Main.buffNoTimeDisplay[BuffID.ShadowCandle] = true; // 暗影蜡烛
            Main.buffNoTimeDisplay[BuffID.CatBast] = true; // 巴斯特雕像
            Main.buffNoTimeDisplay[BuffID.Bewitched] = true; // 女巫桌
            Main.buffNoTimeDisplay[BuffID.AmmoBox] = true; // 弹药箱
            Main.buffNoTimeDisplay[BuffID.Sharpened] = true; // 磨刀架
            Main.buffNoTimeDisplay[BuffID.Clairvoyance] = true; // 水晶球
            Main.buffNoTimeDisplay[BuffID.SugarRush] = true; // 蛋糕
            Main.buffNoTimeDisplay[BuffID.Honey] = true; // 蜂蜜

        }
    }


}