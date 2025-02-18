using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using PortableBuffStation.Configs;
using PortableBuffStation.Systems;


namespace PortableBuffStation
{
    /// <summary>
    /// 物品层面的逻辑：判断是不是增益站
    /// 并给出 Tooltip
    /// </summary>
    public class InfBuffGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => false;

        /// <summary>
        /// 返回此物品可能提供的Buff列表(药水Buff 或 增益站Buff)
        /// 也可包含侏儒/蜂蜜等特殊判断
        /// </summary>
        
        public static List<int> GetBuffTypes(Item item)
        {
            var list = new List<int>();
            var config = ModContent.GetInstance<Configs.MyModConfig>();


            // 2) 如果是增益站
            if (config.EnablePortableStations)
            {
                int stationBuff = BuffLookups.GetStationBuff(item.type);
                if (stationBuff != -1)
                    list.Add(stationBuff);
                // 侏儒 -> stationBuff = -100, 代表要特殊处理
                // 也可以在InfBuffPlayer里处理
            }

           
            return list;
        }
        
        
    }
}