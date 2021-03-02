using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEndGameEvent : IEvent
{
   bool GameWon { get; }
}
