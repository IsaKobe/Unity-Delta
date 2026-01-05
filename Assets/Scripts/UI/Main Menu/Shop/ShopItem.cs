using Player;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [HideInInspector] protected int currentLevel = 0;
    public Texture2D image;
    public string Name;
    [TextArea] public string paragraph;

    [SerializeField] protected int maxLevel = 0;
    [SerializeField] List<int> costs = new();

    public int index;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public int MaxLevel => maxLevel;

    protected virtual void OnValidate()
    {
        costs.EnforceListLength(maxLevel);
    }

    public abstract void ModPlayer(Ship ship);


    public abstract string Original();
    public abstract string NewValue();

    public int GetCost()
        => costs[currentLevel];

    public void IncreaseLevel()
    {
        if(currentLevel < maxLevel)
        {
            SaveController.BuyItem(GetCost(), index);
            currentLevel++;
        }
    }
}