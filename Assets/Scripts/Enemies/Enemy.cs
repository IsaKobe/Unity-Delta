using Assets.Scripts;
using Assets.Scripts.Enemies;
using System;
using UnityEngine;

public class Enemy : DamagableObject
{
    [SerializeField] float armor;
    public override void TakeDamage(float damage)
    {
        float dam = damage - armor;
        base.TakeDamage(dam);
    }

    protected override void OnDeath()
    {
        Destroy(gameObject);
    }

    /*    [SerializeField] float health = 30;
        public float Health { get => health; set => health = value; }
    *//*
        public void OnDeath()
        {
            Destroy(gameObject);
        }*/

}
