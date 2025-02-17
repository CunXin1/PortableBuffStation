using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

public class PortableBuffSystem : ModSystem
{
    public override void PostDrawInterface(SpriteBatch spriteBatch)
    {
        if (Main.LocalPlayer.GetModPlayer<PortableBuffPlayer>().HasCampfire)
            Main.buffNoTimeDisplay[BuffID.Campfire] = true;
        if (Main.LocalPlayer.GetModPlayer<PortableBuffPlayer>().HasHeartLantern)
            Main.buffNoTimeDisplay[BuffID.HeartLamp] = true;
        if (Main.LocalPlayer.GetModPlayer<PortableBuffPlayer>().HasSunflower)
            Main.buffNoTimeDisplay[BuffID.Sunflower] = true;
        if (Main.LocalPlayer.GetModPlayer<PortableBuffPlayer>().HasStarInBottle)
            Main.buffNoTimeDisplay[BuffID.StarInBottle] = true;
    }
}
