using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventHandler : MonoBehaviour,IEventHandler 
{
    public virtual void Visit(IEvent gameEvent)
    {
        gameEvent.Visit(this);
    }
    
    public virtual void Handle(ICreepKillEvent creepKillEvent)
    {
        //Do nothing as abstract
    }

    public virtual void Handle(IBaseDamageEvent baseDamageEvent)
    {
        //Do nothing as abstract
    }

    public virtual void Handle(ICoinSpentEvent coinSpentEvent)
    {
        //Do nothing as abstract
    }

    public virtual void Handle(INextLevelEvent nextLevelEvent)
    {
        //Do nothing as abstract
    }

    public virtual void Handle(IUpdateUIEvent updateUiEvent)
    {
        //Do nothing as abstract
    }

    public virtual void Handle(IPlayerInputEvent playerInputEvent)
    {
        //Do nothing as abstract
    }

    public virtual void Handle(IEndGameEvent endGameEvent)
    {
        //Do nothing as abstract
    }
}
