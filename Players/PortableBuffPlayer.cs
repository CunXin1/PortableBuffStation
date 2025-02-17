using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

public class PortableBuffPlayer : ModPlayer
{
    public bool HasCampfire;
    public bool HasHeartLantern;
    public bool HasSunflower;
    public bool HasStarInBottle;

    public override void ResetEffects()
    {
        HasCampfire = false;
        HasHeartLantern = false;
        HasSunflower = false;
        HasStarInBottle = false;
    }

    public override void PostUpdateBuffs()
    {
        foreach (Item item in Player.inventory)
        {
            if (item.IsAir) continue;

            switch (item.type)
            {
                case ItemID.Campfire:
                    HasCampfire = true;
                    break;
                case ItemID.HeartLantern:
                    HasHeartLantern = true;
                    break;
                case ItemID.Sunflower:
                    HasSunflower = true;
                    break;
                case ItemID.StarinaBottle:
                    HasStarInBottle = true;
                    break;
            }
        }

        if (HasCampfire)
            Player.AddBuff(BuffID.Campfire, 30);
        if (HasHeartLantern)
            Player.AddBuff(BuffID.HeartLamp, 30);
        if (HasSunflower)
            Player.AddBuff(BuffID.Sunflower, 30);
        if (HasStarInBottle)
            Player.AddBuff(BuffID.StarInBottle, 30);
    }

    public override void PreUpdateBuffs()
{
    if (HasCampfire)
        Main.SceneMetrics.HasCampfire = true;
    if (HasHeartLantern)
        Main.SceneMetrics.HasHeartLantern = true;
    if (HasSunflower)
        Main.SceneMetrics.HasSunflower = true;
    if (HasStarInBottle)
        Main.SceneMetrics.HasStarInBottle = true;
}

}
