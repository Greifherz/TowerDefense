using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUIEvvent : IUpdateUIEvent
{
    public UpdateUIEvvent(UIUpdateType updateType,int updateValue)
    {
        UpdateType = updateType;
        UpdateValue = updateValue;
    }
    
    public void Visit(IEventHandler handler)
    {
        handler.Handle(this);
    }

    public UIUpdateType UpdateType { get; set; }
    public int UpdateValue { get; set; }
}
