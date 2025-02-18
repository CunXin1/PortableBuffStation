using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes;

namespace PortableBuffStation
{
    /// <summary>
    /// 玩家层面的核心逻辑：收集玩家拥有的可用增益物品（Available Items），并定时为玩家应用对应Buff。
    /// Player：玩家（Game Player）
    /// Buff：状态增益（Status Buff）
    /// </summary>
    public class PortableBuffPlayer : ModPlayer
    {
        // 存储玩家当前拥有的所有可用增益物品
        public List<Item> AvailableItems = new();
        // 使用HashSet用于快速查重与查询
        public HashSet<Item> AvailableItemsHash = new();

        // 计时器，用于每隔一定帧数更新物品列表
        private int _scanCooldown = 0;
        private const int ScanInterval = 60; // 每60帧（Frame）更新一次，大约1秒

        // 每帧调用，用于更新玩家的Buff状态
        public override void PostUpdateBuffs()
        {
            // 应用所有可用物品的Buff给玩家
            ApplyAvailableBuffs();

            // 每隔 ScanInterval 帧刷新一次物品列表
            _scanCooldown++;
            if (_scanCooldown >= ScanInterval)
            {
                _scanCooldown = 0;
                SetupItemsList();
            }
        }

        /// <summary>
        /// 遍历所有可用物品，并为玩家应用相应的Buff
        /// </summary>
        private void ApplyAvailableBuffs()
        {
            foreach (var item in AvailableItems)
            {
                // 获取物品可能提供的Buff类型列表
                var buffTypes = InfBuffGlobalItem.GetBuffTypes(item);
                foreach (int buffType in buffTypes)
                {
                    // 若为普通Buff（编号 ≥ 0）
                    if (buffType >= 0)
                    {
                        // 为玩家添加一个持续30帧的Buff，同时利用 HideBuffSystem 隐藏倒计时显示
                        Player.AddBuff(buffType, 30);
                    }
                    // 特殊处理：buffType为 -100 表示侏儒效果，需增加幸运值（luck）
                    else if (buffType == -100)
                    {
                        Player.luck += 0.2f;
                    }
                }
            }
        }

        /// <summary>
        /// 更新玩家可用物品列表，包括背包和各个银行（Bank）中的物品
        /// </summary>
        private void SetupItemsList()
        {
            // 记录旧的物品列表（可用于后续比较变化）
            AvailableItems.Clear();
            AvailableItemsHash.Clear();

            // 检查玩家各个物品容器
            CheckItems(Player.inventory);
            CheckItems(Player.bank?.item);
            CheckItems(Player.bank2?.item);
            CheckItems(Player.bank3?.item);
            CheckItems(Player.bank4?.item);

            // 更新HashSet，便于后续快速查找
            AvailableItemsHash = AvailableItems.ToHashSet();
        }

        /// <summary>
        /// 检查传入物品容器中的每个物品，若提供增益Buff则加入可用物品列表
        /// container：物品容器（Item Container）
        /// </summary>
        private void CheckItems(Item[] container)
        {
            if (container == null) return;
            foreach (var item in container)
            {
                // 跳过空物品或无效物品（IsAir：空物品标识）
                if (item is null || item.IsAir) continue;
                // 若该物品能提供至少一个Buff，则视为有效物品
                var buffTypes = InfBuffGlobalItem.GetBuffTypes(item);
                if (buffTypes.Count > 0)
                {
                    AvailableItems.Add(item);
                }
            }
        }
    }
}
