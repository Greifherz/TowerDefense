using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpentEvent : ICoinSpentEvent
{
    public CoinSpentEvent(int quantitySpent)
    {
        QuantitySpent = quantitySpent;
    }
    
    public void Visit(IEventHandler handler)
    {
        handler.Handle(this);
    }

    public int QuantitySpent { get; set; }
}
