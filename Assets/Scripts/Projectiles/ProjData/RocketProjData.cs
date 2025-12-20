using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Projectiles.Controllers.Data
{
    [CreateAssetMenu(fileName = "projData", menuName = "Projectiles/Rocket", order = 3)]
    public class RocketProjData : ProjData
    {
        public float timeToLive = 5;
        public float rotSpeed = 5;
    }
}
