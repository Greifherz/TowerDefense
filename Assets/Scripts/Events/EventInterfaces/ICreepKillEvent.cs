using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreepKillEvent : IEvent
{
    bool RewardsCoin { get; }
}
