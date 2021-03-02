using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputEvent : IPlayerInputEvent
{
    public PlayerInputEvent(PlayerInputType inputType)
    {
        InputType = inputType;
    }
    
    public void Visit(IEventHandler handler)
    {
        handler.Handle(this);
    }

    public PlayerInputType InputType { get; }
}
