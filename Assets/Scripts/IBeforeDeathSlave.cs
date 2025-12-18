using System;
using UnityEngine;

public interface IBeforeDeathSlave<T>
{
    Action<T> onDeath { get; set; }
}
