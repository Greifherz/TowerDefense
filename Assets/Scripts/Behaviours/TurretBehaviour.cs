using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField] private ColliderTriggerDetector Detector;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private int Damage = 1;
 
    private TurretTargeter Targetter;

    private void Start()
    {
        Targetter = new TurretTargeter(transform.GetChild(1),BulletPrefab,this,Damage);
        Detector.SetEnterCallback(ObjectEnteredRange);
        Detector.SetExitCallback(ObjectExitedRange);
    }

    public void Place()
    {
        Targetter.ActivateTargeter();
    }

    private void ObjectEnteredRange(GameObject entree)
    {
        if(!entree.tag.Equals("Enemy")) return;
        Targetter.InRange(entree.transform);
    }

    private void ObjectExitedRange(GameObject exitee)
    {
        if(!exitee.tag.Equals("Enemy")) return;
        Targetter.OutOfRange(exitee.transform);
    }
    
}
