using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepBehaviour : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NavigationAgent;
    [SerializeField] private int StartingHealth = 3;
    
    private int CurrentHealth;

    private HpBar SelfHpBar;

    private bool Killed = false;

    private float NormalSpeed;
    private int SlowCount = 0;
    
    private void Start()
    {
        CurrentHealth = StartingHealth;
        NormalSpeed = NavigationAgent.speed;
    }

    private void Slow(float slowIntensity)
    {//Sticks with the stronger slow
        if (NavigationAgent.speed > NormalSpeed * slowIntensity)
            NavigationAgent.speed = NormalSpeed * slowIntensity;
        SlowCount++;
    }

    private void NormalizeSpeed()
    {
        SlowCount--;
        if(SlowCount == 0) NavigationAgent.speed = NormalSpeed;
    }

    private void Damage(int damageQuantity)
    {
        CurrentHealth -= damageQuantity;
        if (CurrentHealth <= 0 && !Killed)
        {
            Killed = true; //Needs this flag since it's not destroy immediate and destroy immediate only works on editor
            Destroy(gameObject);
            EventController.Instance.Raise(new CreepKillEvent(true));
        }
        else if (CurrentHealth < StartingHealth && SelfHpBar != null && !SelfHpBar.gameObject.activeSelf)
        {
            SelfHpBar.gameObject.SetActive(true);
            SelfHpBar.UpdateValue(CurrentHealth);
        }
        else if (SelfHpBar != null)
        {
            SelfHpBar.UpdateValue(CurrentHealth);
        }
    }

    public void SetHealthhBar(HpBar hpBar)
    {
        SelfHpBar = hpBar;
        SelfHpBar.Active = true;
        SelfHpBar.FullValue = StartingHealth;
    }

    private void OnDestroy()
    {
        if (SelfHpBar != null)
        {
            SelfHpBar.Active = false;
            SelfHpBar.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (SelfHpBar != null && SelfHpBar.gameObject.activeSelf)
        {
            SelfHpBar.transform.position = transform.position + new Vector3(0,0,1);
        }
    }
}
