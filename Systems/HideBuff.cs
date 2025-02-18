using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;


namespace PortableBuffStation.Systems
{
    /// <summary>
    /// 这里将其 buffNoTimeDisplay 设置为 true，隐藏倒计时。
    /// </summary>
    public class HideBuff : ModSystem
    {
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {

            Main.buffNoTimeDisplay[BuffID.Campfire] = true; // 营火
            Main.buffNoTimeDisplay[BuffID.HeartLamp] = true; // 心形灯笼
            Main.buffNoTimeDisplay[BuffID.Sunflower] = true; // 向日葵
            Main.buffNoTimeDisplay[BuffID.StarInBottle] = true; // 星瓶
            Main.buffNoTimeDisplay[BuffID.WaterCandle] = true; // 水蜡烛
            Main.buffNoTimeDisplay[BuffID.PeaceCandle] = true; // 和平蜡烛
            Main.buffNoTimeDisplay[BuffID.ShadowCandle] = true; // 暗影蜡烛
            Main.buffNoTimeDisplay[BuffID.CatBast] = true; // 巴斯特雕像
            Main.buffNoTimeDisplay[BuffID.Bewitched] = true; // 女巫桌
            Main.buffNoTimeDisplay[BuffID.AmmoBox] = true; // 弹药箱
            Main.buffNoTimeDisplay[BuffID.Sharpened] = true; // 磨刀架
            Main.buffNoTimeDisplay[BuffID.Clairvoyance] = true; // 水晶球
            Main.buffNoTimeDisplay[BuffID.SugarRush] = true; // 蛋糕
            Main.buffNoTimeDisplay[BuffID.Honey] = true; // 蜂蜜

        }
    }
}
