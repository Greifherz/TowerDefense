using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : EventHandler
{
    [SerializeField] private int CurrentCoins = 10;
    // Start is called before the first frame update
    void Start()
    {
        EventController.Instance.RegisterListener(Visit);
    }

    public bool AllowSpendingOf(int quantity)
    {
        return CurrentCoins >= quantity;
    }

    public override void Handle(ICreepKillEvent creepKillEvent)
    {
        if (creepKillEvent.RewardsCoin)
        {
            CurrentCoins++;
            EventController.Instance.Raise(new UpdateUIEvvent(UIUpdateType.Currency,CurrentCoins));
        }
    }

    public override void Handle(ICoinSpentEvent coinSpentEvent)
    {
        CurrentCoins -= coinSpentEvent.QuantitySpent;
        EventController.Instance.Raise(new UpdateUIEvvent(UIUpdateType.Currency,CurrentCoins));
    }
}
