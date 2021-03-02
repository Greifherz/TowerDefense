using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameEvent : IEndGameEvent
{
    public EndGameEvent(bool gameWon)
    {
        GameWon = gameWon;
    }
    
    public void Visit(IEventHandler handler)
    {
        handler.Handle(this);
    }

    public bool GameWon { get; }
}
