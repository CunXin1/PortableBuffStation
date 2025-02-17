using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using MyMod.Configs;

namespace MyMod.Systems
{
    public class HideBuffSystem : ModSystem
    {
        // 标记本帧要隐藏的 buffType
        internal static bool[] BuffTypesShouldHide = new bool[0];

        public override void Load()
        {
            Array.Resize(ref BuffTypesShouldHide, BuffLoader.BuffCount);
        }

        public override void PostSetupContent()
        {
            // 如果mod中新增了buff, BuffLoader.BuffCount会变动
            Array.Resize(ref BuffTypesShouldHide, BuffLoader.BuffCount);
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
           
            // 每帧先清空
            Array.Clear(BuffTypesShouldHide, 0, BuffTypesShouldHide.Length);

            // 收集要隐藏的Buff
            CollectBuffsToHide(Main.LocalPlayer);

            // 如果有“共享队友”逻辑, 也可以对队友执行 CollectBuffsToHide
        }

        private static void CollectBuffsToHide(Player player)
        {
            // 你的 InfBuffPlayer 里，肯定有 “可用物品列表”
            var infBuffPlayer = player.GetModPlayer<InfBuffPlayer>();
            if (infBuffPlayer == null) return;

            foreach (Item item in infBuffPlayer.AvailableItems)
            {
                // 可能是药水Buff or 站Buff
                // 只要判断：这个物品会让 player.AddBuff(xxx, 30) 出现 0秒
                // 就把 xxx 标记为隐藏
                var buffTypes = InfBuffGlobalItem.GetBuffTypes(item);
                foreach (int buffType in buffTypes)
                {
                    if (buffType >= 0 && buffType < BuffTypesShouldHide.Length)
                    {
                        BuffTypesShouldHide[buffType] = true;
                    }
                }
            }
        }
    }
}
