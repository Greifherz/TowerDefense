using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInputEvent : IEvent
{
    PlayerInputType InputType { get; }
}

public enum PlayerInputType
{
    PlaceTower,
    CallNextWave,
    PlaceSlowTurret
}
