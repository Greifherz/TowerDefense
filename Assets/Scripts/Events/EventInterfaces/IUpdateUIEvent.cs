using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdateUIEvent : IEvent
{
    UIUpdateType UpdateType { get; }
    int UpdateValue { get; }
}

public enum UIUpdateType
{
    BaseHealth,
    Currency,
    EnemiesLeft
}
