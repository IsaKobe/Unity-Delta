using Player;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Item/Health")]
public class HealthItem : ShopItemWithData<int>
{
    public override void ModPlayer(Ship ship)
    {
        ship.maxHealth += data[currentLevel];
    }
}
