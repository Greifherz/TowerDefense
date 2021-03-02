using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INextLevelEvent : IEvent
{
    LevelDescriptor Descriptor { get; }
}
