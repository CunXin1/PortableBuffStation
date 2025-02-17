using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using PortableBuffStation.Configs;

namespace PortableBuffStation.Systems
{
    public class PortableBuffStationPlayer : ModPlayer
    {
        public override void PostUpdateBuffs()
        {
            var config = ModContent.GetInstance<PortableBuffStationConfig>();
            if (!config.EnablePortableStations)
                return;

            // 先做一些计数/重置
            // SceneMetrics.HasCampfire = false; // TML 1.4.4 不一定需要手动清0
            // Player.NPCBannerBuff[npcType] = false; // ...
            // 也可以在 ResetEffects() 里重置

            // 在背包（含猪猪、保险箱等）查找物品
            CheckBuffStations(Player.inventory);
            CheckBuffStations(Player.bank?.item);
            CheckBuffStations(Player.bank2?.item);
            CheckBuffStations(Player.bank3?.item);
            CheckBuffStations(Player.bank4?.item);
        }

        private void CheckBuffStations(Item[] items)
        {
            if (items == null) return;

            // 为了避免同种物品重复叠加，你可以用HashSet<int>过滤
            HashSet<int> checkedTypes = new HashSet<int>();

            foreach (var item in items)
            {
                if (item == null || item.IsAir) continue;
                if (!ApplyBuffGlobalItem.IsItemAvailable(item))
                    continue;

                // 若你不想多次叠加同type，就：
                if (!checkedTypes.Add(item.type))
                    continue;

                // 1. 如果是环境BuffTile
                List<int> buffIDs = ApplyBuffGlobalItem.GetItemBuffType(item);
                foreach (int buffID in buffIDs)
                {
                    // 你可以直接 player.AddBuff(buffID, 60);
                    // 也可以使用 SceneMetrics 机制
                    if (buffID == BuffID.Campfire)
                    {
                        Main.SceneMetrics.HasCampfire = true;
                        // 也可以AddBuff(BuffID.Campfire, 60);
                    }
                    else if (buffID == BuffID.HeartLamp)
                    {
                        Main.SceneMetrics.HasHeartLantern = true;
                    }
                    else if (buffID == BuffID.Honey)
                    {
                        // 这就相当于在蜂蜜里
                        Player.AddBuff(BuffID.Honey, 60);
                    }
                    else if (buffID == BuffID.Lucky) // 假设侏儒Buff
                    {
                        Player.AddBuff(BuffID.Lucky, 60);
                    }
                    else
                    {
                        // 对于CrystalBall, BewitchingTable之类你也可以在这里处理
                        Player.AddBuff(buffID, 60);
                    }
                }

                // 2. 如果是旗帜
                int[] npcTypes = ApplyBuffGlobalItem.GetBannerNPCs(item);
                if (npcTypes.Length > 0 && ModContent.GetInstance<PortableBuffStationConfig>().EnableBanners)
                {
                    // 假设 npcTypes 是一个 int[] 数组
                    Main.SceneMetrics.hasBanner = true;
                    foreach (int npcType in npcTypes)
                    {
                        Main.SceneMetrics.NPCBannerBuff[npcType] = true;
                    }

                }
            }
        }
    }
}
