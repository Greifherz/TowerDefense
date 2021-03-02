using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    [SerializeField] private ColliderTriggerDetector TriggerDetector;

    private void Start()
    {
        TriggerDetector.SetEnterCallback(CreepDamage);
    }

    private void CreepDamage(GameObject damagingObject)
    {
        if (damagingObject.tag.Equals("Enemy"))
        {
            EventController.Instance.Raise(new BaseDamageEvent());
            Destroy(damagingObject);
            EventController.Instance.Raise(new CreepKillEvent(false));
        }
    }
}
