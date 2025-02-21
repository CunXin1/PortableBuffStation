using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace PortableBuffStation.Systems
{
    /// <summary>
    /// 维护物品与Buff/NPC之间的映射关系，适用于药水、旗帜、增益站等效果。
    /// Vanilla（原版）：指游戏原始内置内容。
    /// </summary>
    public static class BuffLookups
    {
        // 存储原版增益站物品与对应Buff的映射关系
        public static Dictionary<int, int> VanillaBuffStations = new Dictionary<int, int>()
        {
            // 1. 普通营火映射到 Campfire Buff
            { ItemID.Campfire, BuffID.Campfire },

            // 2. 心形灯笼、星瓶、向日葵各自映射到对应的Buff
            { ItemID.HeartLantern, BuffID.HeartLamp },
            { ItemID.StarinaBottle, BuffID.StarInBottle },
            { ItemID.Sunflower, BuffID.Sunflower },

            // 3. 花园侏儒：原版无对应Buff，使用 -100 表示特殊处理（例如幸运加成）
            { ItemID.GardenGnome, -100 },

            // 4. 三种蜡烛映射
            { ItemID.WaterCandle, BuffID.WaterCandle },
            { ItemID.PeaceCandle, BuffID.PeaceCandle },
            { ItemID.ShadowCandle, BuffID.ShadowCandle },

            // 5. 巴斯特雕像映射
            { ItemID.CatBast, BuffID.CatBast },

            // 6. 其他增益站物品映射（女巫桌、弹药箱、磨刀架、水晶球、蛋糕）
            { ItemID.BewitchingTable, BuffID.Bewitched },
            { ItemID.AmmoBox, BuffID.AmmoBox },
            { ItemID.SharpeningStation, BuffID.Sharpened },
            { ItemID.CrystalBall, BuffID.Clairvoyance },
            { ItemID.SliceOfCake, BuffID.SugarRush },

            // 7. 蜂蜜桶映射（允许背包中也享受蜂蜜Buff）
            { ItemID.HoneyBucket, BuffID.Honey }
        };

        // 用于存储其他Mod添加的增益站物品映射
        public static Dictionary<int, int> ModdedStations = new Dictionary<int, int>();

        /// <summary>
        /// 添加灾厄模组（CalamityMod）中的 buff station 映射。
        /// 注意：以下内部名称（internal names）仅为示例，请根据灾厄模组实际内部名称进行调整。
        /// </summary>
        public static void AddCalamityBuffStations(Mod calamity)
        {
            // Chaos Candle
            ModdedStations.Add(calamity.Find<ModItem>("ChaosCandle").Type, calamity.Find<ModBuff>("ChaosCandleBuff").Type);
            // Corruption Effigy
            ModdedStations.Add(calamity.Find<ModItem>("CorruptionEffigy").Type, calamity.Find<ModBuff>("CorruptionEffigyBuff").Type);
            // Crimson Effigy
            ModdedStations.Add(calamity.Find<ModItem>("CrimsonEffigy").Type, calamity.Find<ModBuff>("CrimsonEffigyBuff").Type);
            // Effigy of Decay
            ModdedStations.Add(calamity.Find<ModItem>("EffigyOfDecay").Type, calamity.Find<ModBuff>("EffigyOfDecayBuff").Type);
            // Weightless Candle 
            ModdedStations.Add(calamity.Find<ModItem>("WeightlessCandle").Type, calamity.Find<ModBuff>("CirrusBlueCandleBuff").Type);
            // Resilient Candle
            ModdedStations.Add(calamity.Find<ModItem>("ResilientCandle").Type, calamity.Find<ModBuff>("CirrusPurpleCandleBuff").Type);
            // Spiteful Candle
            ModdedStations.Add(calamity.Find<ModItem>("SpitefulCandle").Type, calamity.Find<ModBuff>("CirrusYellowCandleBuff").Type);
            // Tranquility Candle
            ModdedStations.Add(calamity.Find<ModItem>("TranquilityCandle").Type, calamity.Find<ModBuff>("TranquilityCandleBuff").Type);
            // Vigorous Candle
            ModdedStations.Add(calamity.Find<ModItem>("VigorousCandle").Type, calamity.Find<ModBuff>("CirrusPinkCandleBuff").Type);

        }


        /// <summary>
        /// 根据物品类型返回对应的Buff编号。
        /// 若物品不属于任何增益站，则返回 -1。
        /// </summary>
        public static int GetStationBuff(int itemType)
        {
            // 优先检查原版物品映射
            if (VanillaBuffStations.TryGetValue(itemType, out var buff))
                return buff;
            // 再检查其他Mod添加的物品映射
            if (ModdedStations.TryGetValue(itemType, out var modBuff))
                return modBuff;
            // 找不到则返回 -1
            return -1;
        }
    }

    /// <summary>
    /// 通过将 buffNoTimeDisplay 设置为 true 来隐藏Buff倒计时显示，
    /// 使得玩家界面（User Interface）上不会显示增益剩余时间。
    /// </summary>
    public class HideBuff : ModSystem
    {
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            // 对各个增益Buff进行设置，隐藏倒计时数字
            Main.buffNoTimeDisplay[BuffID.Campfire] = true;      // 营火（Campfire）
            Main.buffNoTimeDisplay[BuffID.HeartLamp] = true;       // 心形灯笼（Heart Lantern）
            Main.buffNoTimeDisplay[BuffID.Sunflower] = true;       // 向日葵（Sunflower）
            Main.buffNoTimeDisplay[BuffID.StarInBottle] = true;    // 星瓶（Star in Bottle）
            Main.buffNoTimeDisplay[BuffID.WaterCandle] = true;     // 水蜡烛（Water Candle）
            Main.buffNoTimeDisplay[BuffID.PeaceCandle] = true;       // 和平蜡烛（Peace Candle）
            Main.buffNoTimeDisplay[BuffID.ShadowCandle] = true;      // 暗影蜡烛（Shadow Candle）
            Main.buffNoTimeDisplay[BuffID.CatBast] = true;           // 巴斯特雕像（Cat Bast）
            Main.buffNoTimeDisplay[BuffID.Bewitched] = true;         // 女巫桌（Bewitching Table）
            Main.buffNoTimeDisplay[BuffID.AmmoBox] = true;           // 弹药箱（Ammo Box）
            Main.buffNoTimeDisplay[BuffID.Sharpened] = true;         // 磨刀架（Sharpening Station）
            Main.buffNoTimeDisplay[BuffID.Clairvoyance] = true;      // 水晶球（Crystal Ball）
            Main.buffNoTimeDisplay[BuffID.SugarRush] = true;         // 蛋糕（Slice of Cake）
            Main.buffNoTimeDisplay[BuffID.Honey] = true;             // 蜂蜜（Honey）

            
        }
    }
}
