using Player;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rocket", menuName = "Item/Rocket")]
public class RocketItem : ShopItemWithData<float>
{

    public override void ModPlayer(Ship ship)
    {
        if (currentLevel > 0)
            ship.Input.EnableRockets(data[currentLevel]);
    }
}
