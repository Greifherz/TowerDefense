using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventHandler 
{
    void Handle(ICreepKillEvent creepKillEvent);
    void Handle(IBaseDamageEvent baseDamageEvent);
    void Handle(ICoinSpentEvent coinSpentEvent);
    void Handle(INextLevelEvent nextLevelEvent);
    void Handle(IUpdateUIEvent updateUiEvent);
    void Handle(IPlayerInputEvent playerInputEvent);
    void Handle(IEndGameEvent endGameEvent);
}
