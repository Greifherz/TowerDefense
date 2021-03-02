using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepKillEvent : ICreepKillEvent
{
    
    public CreepKillEvent(bool rewardsCoin)
    {
        RewardsCoin = rewardsCoin;
    }

    public void Visit(IEventHandler handler)
    {
        handler.Handle(this);
    }

    public bool RewardsCoin { get; }
}
