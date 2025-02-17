using Terraria;
using Terraria.ModLoader;
using System;
using MyMod.Configs;
using Terraria.ID;

namespace MyMod
{
    /// <summary>
    /// 类似“ApplyBuffStation”文件，用来设置SceneMetrics
    /// </summary>
    public class PortableBuffStationSystem : ModSystem
    {
        public static bool HasCampfire { get; set; }
        public static bool HasHeartLantern { get; set; }
        public static bool HasSunflower { get; set; }
        public static bool HasGardenGnome { get; set; }
        public static bool HasStarInBottle { get; set; }
        public static bool HasWaterCandle { get; set; }
        public static bool HasPeaceCandle { get; set; }
        public static bool HasShadowCandle { get; set; }

        public static void ResetFlags()
        {
            HasCampfire = false;
            HasHeartLantern = false;
            HasSunflower = false;
            HasGardenGnome = false;
            HasStarInBottle = false;
            HasWaterCandle = false;
            HasPeaceCandle = false;
            HasShadowCandle = false;
        }

        // 每帧在 TileCountsAvailable 里处理蜡烛计数
        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            if (HasWaterCandle)
                Main.SceneMetrics.WaterCandleCount++;
            if (HasPeaceCandle)
                Main.SceneMetrics.PeaceCandleCount++;
            if (HasShadowCandle)
                Main.SceneMetrics.ShadowCandleCount++;
        }

        public override void ResetNearbyTileEffects()
        {
            // 只在客户端执行
            if (Main.netMode == NetmodeID.Server)
                return;

            if (HasCampfire)
                Main.SceneMetrics.HasCampfire = true;
            if (HasHeartLantern)
                Main.SceneMetrics.HasHeartLantern = true;
            if (HasSunflower)
                Main.SceneMetrics.HasSunflower = true;
            if (HasGardenGnome)
                Main.SceneMetrics.HasGardenGnome = true;
            if (HasStarInBottle)
                Main.SceneMetrics.HasStarInBottle = true;
        }
    }
}
