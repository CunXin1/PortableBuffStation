using Terraria;
using Terraria.ModLoader;
// 关键的命名空间:
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using Terraria.UI.Chat;
using System;
using Terraria.ID;

// 下面这些是关键：
using Mono.Cecil.Cil;              // 提供 ILContext、ILCursor 等               // 提供 ILHook
//using MonoMod.Cil.Extensions;   
using Terraria.GameContent.UI.Chat;

namespace DisableBuffTimeDisplay
{
    public class DisableBuffTimeMod : Mod
    {
        public override void Load()
        {
            // 这里 Hook IL
            IL_Main.DrawBuffIcon += RemoveBuffTimeIL;
        }

        public override void Unload()
        {
            // 卸载hook
            IL_Main.DrawBuffIcon -= RemoveBuffTimeIL;
        }

        private void RemoveBuffTimeIL(ILContext il)
        {
            var c = new ILCursor(il);

            // 以下是示例，你要自己对照 tModLoader 的 IL 找到真正绘制时间文本的地方
            // 并 Remove/跳过

            while (c.TryGotoNext(MoveType.After,
                i => i.MatchCall(typeof(ChatManager), nameof(ChatManager.DrawColorCodedString))))
            {
                // 回退一条指令
                c.Index--;
                // 移除 call ChatManager.DrawColorCodedString
                c.Remove();

                // 同时把它之前压入栈的参数也移除
                // (根据实际函数签名可能是9个参数,这里做示例)
                for (int n = 0; n < 9; n++)
                {
                    c.Index--;
                    c.Remove();
                }
            }
        }
    }
}
