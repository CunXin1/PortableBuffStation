using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using PortableBuffStation.Configs;
using PortableBuffStation.Systems;
using Microsoft.Xna.Framework;

namespace PortableBuffStation
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
    foreach (var item in AvailableItems)
    {
        var buffTypes = InfBuffGlobalItem.GetBuffTypes(item);
        foreach (int buffType in buffTypes)
        {
            // 如果是正常BuffID（≥0）
            if (buffType >= 0)
            {
                // 给玩家短时Buff，但会用HideBuffSystem隐藏0秒图标
                Player.AddBuff(buffType, 30);

                // 同时如果是环境Buff，就设置SceneMetrics标志
                if (buffType == BuffID.Campfire)
                {
                    // 普通营火
                    PortableBuffStationSystem.HasCampfire = true;
                }
                else if (buffType == BuffID.HeartLamp)
                {
                    // 心形灯笼
                    PortableBuffStationSystem.HasHeartLantern = true;
                }
                else if (buffType == BuffID.StarInBottle)
                {
                    // 星瓶
                    PortableBuffStationSystem.HasStarInBottle = true;
                }
                else if (buffType == BuffID.Sunflower)
                {
                    // 向日葵
                    PortableBuffStationSystem.HasSunflower = true;
                }
                else if (buffType == BuffID.WaterCandle)
                {
                    // 水蜡烛
                    PortableBuffStationSystem.HasWaterCandle = true;
                }
                else if (buffType == BuffID.PeaceCandle)
                {
                    // 和平蜡烛
                    PortableBuffStationSystem.HasPeaceCandle = true;
                }
                else if (buffType == BuffID.ShadowCandle)
                {
                    // 暗影蜡烛
                    PortableBuffStationSystem.HasShadowCandle = true;
                }
                else if (buffType == BuffID.CatBast)
                {
                    // 巴斯特雕像（+5防御）
                    //PortableBuffStationSystem.HasCatBast = true;
                }
                else if (buffType == BuffID.Bewitched)
                {
                    // 女巫桌（+1仆从上限）
                }
                else if (buffType == BuffID.AmmoBox)
                {
                    // 弹药箱（20%几率不消耗弹药）
                }
                else if (buffType == BuffID.Sharpened)
                {
                    // 磨刀架（近战武器穿透）
                }
                else if (buffType == BuffID.Clairvoyance)
                {
                    // 水晶球（+2魔力上限、+5%魔伤等）
                }
                else if (buffType == BuffID.SugarRush)
                {
                    // 蛋糕（提升移速与攻击速度）
                }
                else if (buffType == BuffID.Honey)
                {
                    // 蜂蜜（加速生命回复）
                }
            }
            // 如果 buffType == -100，则表示侏儒，需要特殊处理
            else if (buffType == -100)
            {
                // 侏儒（幸运加成）
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
            var config = ModContent.GetInstance<Configs.MyModConfig>();
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
