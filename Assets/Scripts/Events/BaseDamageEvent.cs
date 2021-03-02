using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamageEvent : IBaseDamageEvent
{
    public void Visit(IEventHandler handler)
    {
        handler.Handle(this);
    }
}
