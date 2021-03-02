using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoinSpentEvent : IEvent
{
    int QuantitySpent { get; }
}
