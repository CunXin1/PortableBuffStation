using Terraria;
using Terraria.ModLoader;
// 关键的命名空间:
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;

namespace DisableBuffTimeDisplay
{
    public class DisableBuffTimeMod : Mod
    {
        public override void Load()
        {
           
        }

		public override void Unload()
		{
			// 这个方法在 Mod 卸载时运行（防止数据残留）
			Logger.Info("Portable Buff Station Mod Unloaded!");
		}
	}
}
