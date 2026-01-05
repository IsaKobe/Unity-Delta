using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace World
{
    public class PauseWaitUntil : CustomYieldInstruction
    {
        readonly float timeToWait;
        public PauseWaitUntil(float seconds)
        {
            timeToWait = TimeController.timeElapsed + seconds;
        }
        public override bool keepWaiting => TimeController.timeElapsed < timeToWait;
    }
}
