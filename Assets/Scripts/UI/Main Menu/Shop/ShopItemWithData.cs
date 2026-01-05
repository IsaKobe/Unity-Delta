using Player;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class ShopItemWithData<T> : ShopItem
{

    [SerializeField] protected List<T> data;

    protected override void OnValidate()
    {
        base.OnValidate();
        data.EnforceListLength(maxLevel);
    }

    public override string Original()
    {
        if (currentLevel == 0)
            return "None";
        return data[currentLevel - 1].ToString();
    }

    public override string NewValue()
    {
        return data[currentLevel].ToString();
    }
}
