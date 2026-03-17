using Assets.Scripts;
using Assets.Scripts.Enemies;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    //[SerializeField] float armor;

    public float Health { get; set; }
    public float Score { get; set; }

    void IDamagable.OnDeath()
    {
        ScoreManager.AddScore(Score);
        Destroy(gameObject);
    }

}
