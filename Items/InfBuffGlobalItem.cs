using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using PortableBuffStation.Configs;
using PortableBuffStation.Systems;

namespace PortableBuffStation
{
    /// <summary>
    /// 物品层面的逻辑：判断物品是否为增益站（Buff Station），并设置工具提示（Tooltip）。
    /// </summary>
    public class InfBuffGlobalItem : GlobalItem
    {
        // 是否为每个物品实体创建独立实例
        // InstancePerEntity（Instance Per Entity）：决定是否为每个物品实例单独存储状态，此处设为 false 表示共享同一个实例
        public override bool InstancePerEntity => false;

        /// <summary>
        /// 返回该物品可能提供的所有Buff列表。
        /// Buff：状态增益（Status Buff），如药水增益或增益站增益。
        /// 也支持特殊物品，例如侏儒（Gnome）效果。
        /// </summary>
        public static List<int> GetBuffTypes(Item item)
        {
            // 创建一个列表存储所有可能的Buff类型（buff type：增益编号）
            var list = new List<int>();

            // 获取当前Mod的配置实例（Configuration：配置项）
            var config = ModContent.GetInstance<Configs.MyModConfig>();

            // 判断是否启用便携式增益站功能
            if (config.EnablePortableStations)
            {
                // 根据物品类型查找对应的Buff编号
                int stationBuff = BuffLookups.GetStationBuff(item.type);
                // 若找到有效的Buff（-1表示无效）
                if (stationBuff != -1)
                    list.Add(stationBuff);
                // 注意：对于侏儒物品，返回值可能为 -100，此标记需要特殊处理（例如在玩家属性中增加幸运值）
            }

            // 返回该物品可能提供的所有Buff类型
            return list;
        }
    }
}
