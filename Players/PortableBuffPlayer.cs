using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

public class PortableBuffPlayer : ModPlayer
{
    // 1. 定义一个静态字典，物品ID → 给玩家添加Buff或设置环境效果的逻辑
    public static Dictionary<int, Action<Player>> BuffStationActions = new Dictionary<int, Action<Player>>
    {
        // 篝火
        { ItemID.Campfire, (player) =>
            {
                // 让游戏认为有篝火环境
                Main.SceneMetrics.HasCampfire = true;
                // 也可以手动加Buff
                player.AddBuff(BuffID.Campfire, 30);
            }
        },
        // 心形灯笼
        { ItemID.HeartLantern, (player) =>
            {
                Main.SceneMetrics.HasHeartLantern = true;
                player.AddBuff(BuffID.HeartLamp, 30);
            }
        },
        // 向日葵
        { ItemID.Sunflower, (player) =>
            {
                Main.SceneMetrics.HasSunflower = true;
                player.AddBuff(BuffID.Sunflower, 30);
            }
        },
        // 星瓶
        { ItemID.StarinaBottle, (player) =>
            {
                Main.SceneMetrics.HasStarInBottle = true;
                player.AddBuff(BuffID.StarInBottle, 30);
            }
        },
        // ———————— 5. Bast Statue ————————
        { ItemID.CatBast, (player) =>
        {
            // Bast是 +5 防御
            player.AddBuff(BuffID.CatBast, 30);
            
        }
        },
        
    };

    public override void PostUpdateBuffs()
    {
        // 先检查玩家背包
        CheckBuffStations(Player.inventory);

        // 检查 猪猪存钱罐 / 保险箱 / 拍卖箱 / 虚空仓库 (旧版 TML)
        if (Player.bank != null && Player.bank.item != null)
            CheckBuffStations(Player.bank.item);
        if (Player.bank2 != null && Player.bank2.item != null)
            CheckBuffStations(Player.bank2.item);
        if (Player.bank3 != null && Player.bank3.item != null)
            CheckBuffStations(Player.bank3.item);
        if (Player.bank4 != null && Player.bank4.item != null)
            CheckBuffStations(Player.bank4.item);
    }

    // 2. 封装一个方法，用来遍历物品数组，并根据字典执行对应逻辑
    private void CheckBuffStations(Item[] items)
    {
        foreach (var item in items)
        {
            if (item.IsAir)
                continue;

            if (BuffStationActions.TryGetValue(item.type, out var action))
            {
                // 如果字典里有这个物品的映射，就执行对应的逻辑
                action.Invoke(Player);
            }
        }
    }
}
