using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Projectiles.Controllers.Data
{
    [CreateAssetMenu(fileName = "projData", menuName = "Projectiles/Vector", order = 2)]
    public class VecProjData : ProjData
    {
        public Vector2 initialVector = Vector2.up;
    }
}
