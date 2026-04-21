using Assets.Scripts;
using Assets.Scripts.Enemies;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    //[SerializeField] float armor;
    public event Action<Enemy, bool> OnDeath;

    public float Health { get; set; }
    [SerializeField] float score;
    public float Score { get => score; set => score = value; }

    void IDamagable.OnDeath()
    {
        OnDeath?.Invoke(this, true);
        Destroy(gameObject);
    }

    protected void DestroySelf() 
    {
        OnDeath?.Invoke(this, false);
        Destroy(gameObject);
    }
}
