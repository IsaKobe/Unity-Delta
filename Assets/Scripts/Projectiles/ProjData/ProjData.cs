using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Projectiles.Controllers.Data
{
    /// <summary>
    /// Data class for reusing projectiles.
    /// Each type of projectile has it's own type that it uses when initializing itself.
    /// </summary>
    [CreateAssetMenu(fileName = "projData", menuName = "Projectiles/Base", order = 0)]
    public class ProjData : ScriptableObject
    {
        [Header("Visual")]
        public Sprite sprite;
        public Color color;

        public Vector2 size = new (0.15f, 0.15f);
        public Vector2 colliderSize = new(1,2);


        [Header("Combat")]
        public float damage = 10;
        public float speed = 0.275f;
        public bool isPlayerProj = false;
    }
}