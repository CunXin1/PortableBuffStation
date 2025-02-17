using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace PortableBuffStation
{
    public class PortableBuffStation : Mod
    {
        public override void Load()
        {
            // 这个方法在 Mod 加载时运行
            Logger.Info("Portable Buff Station Mod Loaded!");
        }

        public override void Unload()
        {
            // 这个方法在 Mod 卸载时运行（防止数据残留）
            Logger.Info("Portable Buff Station Mod Unloaded!");
        }
    }
}
