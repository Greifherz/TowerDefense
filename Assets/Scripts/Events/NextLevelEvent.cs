using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelEvent : INextLevelEvent
{
    public NextLevelEvent(LevelDescriptor descriptor)
    {
        Descriptor = descriptor;
    }
    
    public void Visit(IEventHandler handler)
    {
        handler.Handle(this);
    }

    public LevelDescriptor Descriptor { get; set; }
}
