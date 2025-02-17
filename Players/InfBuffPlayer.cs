using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using MyMod.Configs;
using MyMod.Systems;
using Microsoft.Xna.Framework;

namespace MyMod
{
    /// <summary>
    /// 核心：收集可用物品 + 每帧或每隔X帧给玩家应用Buff
    /// </summary>
    public class InfBuffPlayer : ModPlayer
    {
        // 记录本玩家当前可用的物品
        public List<Item> AvailableItems = new();
        public HashSet<Item> AvailableItemsHash = new();

        private int _scanCooldown = 0;
        private const int ScanInterval = 120; // 每120帧更新一次

        public override void PostUpdateBuffs()
        {
            // 每帧先让 "PortableBuffStationSystem" 重置 bool
            PortableBuffStationSystem.ResetFlags();

            // 然后把可用物品的Buff应用给玩家
            ApplyAvailableBuffs();

            // 每隔2秒刷新物品列表
            _scanCooldown++;
            if (_scanCooldown >= ScanInterval)
            {
                _scanCooldown = 0;
                SetupItemsList();
            }
        }

        private void ApplyAvailableBuffs()
        {
            var config = ModContent.GetInstance<MyModConfig>();
            foreach (var item in AvailableItems)
            {
                var buffTypes = InfBuffGlobalItem.GetBuffTypes(item);
                foreach (int buffType in buffTypes)
                {
                    if (buffType >= 0)
                    {
                        // 如果是普通BuffID，给个AddBuff(30帧)
                        // 但我们会隐藏它的图标
                        Player.AddBuff(buffType, 30);

                        // 若是营火/心灯/星瓶/侏儒等, 还要让 SceneMetrics 生效
                        if (buffType == BuffID.Campfire)
                            PortableBuffStationSystem.HasCampfire = true;
                        else if (buffType == BuffID.HeartLamp)
                            PortableBuffStationSystem.HasHeartLantern = true;
                        else if (buffType == BuffID.StarInBottle)
                            PortableBuffStationSystem.HasStarInBottle = true;
                        else if (buffType == BuffID.Sunflower)
                            PortableBuffStationSystem.HasSunflower = true;
                        else if (buffType == BuffID.WaterCandle)
                            PortableBuffStationSystem.HasWaterCandle = true;
                        else if (buffType == BuffID.PeaceCandle)
                            PortableBuffStationSystem.HasPeaceCandle = true;
                        else if (buffType == BuffID.ShadowCandle)
                            PortableBuffStationSystem.HasShadowCandle = true;
                        else if (buffType == -100)
                            PortableBuffStationSystem.HasGardenGnome = true;
                    }
                }
            }
        }

        // 每隔一段时间更新可用物品列表
        private void SetupItemsList()
        {
            var oldList = new List<Item>(AvailableItems);
            AvailableItems.Clear();
            AvailableItemsHash.Clear();

            // 遍历玩家背包+银行
            CheckItems(Player.inventory);
            CheckItems(Player.bank?.item);
            CheckItems(Player.bank2?.item);
            CheckItems(Player.bank3?.item);
            CheckItems(Player.bank4?.item);

            AvailableItemsHash = AvailableItems.ToHashSet();

        }

        private void CheckItems(Item[] container)
        {
            if (container == null) return;
            var config = ModContent.GetInstance<MyModConfig>();
            foreach (var item in container)
            {
                if (item is null || item.IsAir) continue;
                // 如果 GetBuffTypes 不为空，就视为可用
                var buffTypes = InfBuffGlobalItem.GetBuffTypes(item);
                if (buffTypes.Count > 0)
                {
                    AvailableItems.Add(item);
                }
            }
        }
    }
}
