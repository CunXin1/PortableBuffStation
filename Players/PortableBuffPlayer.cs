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
    // 1. 检查玩家背包
    CheckBuffItems(Player.inventory);

    // 2. 检查猪猪存钱罐
    if (Player.bank != null && Player.bank.item != null)
        CheckBuffItems(Player.bank.item);

    // 3. 检查保险箱
    if (Player.bank2 != null && Player.bank2.item != null)
        CheckBuffItems(Player.bank2.item);

    // 4. 检查拍卖箱
    if (Player.bank3 != null && Player.bank3.item != null)
        CheckBuffItems(Player.bank3.item);

    // 5. 检查虚空仓库
    if (Player.bank4 != null && Player.bank4.item != null)
        CheckBuffItems(Player.bank4.item);

    // 最后，给玩家添加 Buff
    if (HasCampfire)
        Player.AddBuff(BuffID.Campfire, 30);
    if (HasHeartLantern)
        Player.AddBuff(BuffID.HeartLamp, 30);
    if (HasSunflower)
        Player.AddBuff(BuffID.Sunflower, 30);
    if (HasStarInBottle)
        Player.AddBuff(BuffID.StarInBottle, 30);
}


    // 🚀 提取方法，避免重复代码
    private void CheckBuffItems(Item[] items)
    {
        foreach (Item item in items)
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
