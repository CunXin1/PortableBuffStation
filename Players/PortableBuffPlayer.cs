using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes;

namespace PortableBuffStation
{
    /// <summary>
    /// 核心：收集可用物品 + 每帧或每隔X帧给玩家应用Buff
    /// </summary>
    public class PortableBuffPlayer : ModPlayer
    {
        // 记录本玩家当前可用的物品
        public List<Item> AvailableItems = new();
        public HashSet<Item> AvailableItemsHash = new();
        private int _scanCooldown = 0;
        private const int ScanInterval = 60; // 每60帧更新一次

        public override void PostUpdateBuffs()
        {
            // 每帧先让 "PortableBuffStationSystem" 重置 bool
            //PortableBuffStationSystem.ResetFlags();

            // 然后把可用物品的Buff应用给玩家
            ApplyAvailableBuffs();

            // 每隔1秒刷新物品列表
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
                    }
                    // 如果 buffType == -100，则表示侏儒，需要特殊处理
                    else if (buffType == -100)
                    {
                        // 侏儒（幸运加成）
                        Player.luck += 0.2f;
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
