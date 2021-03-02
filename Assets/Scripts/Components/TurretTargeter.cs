using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class TurretTargeter
{
    public TurretTargeter(Transform turret,GameObject bulletPrefab,MonoBehaviour coroutineRunner,int damageQuantity)
    {
        TurretToTurn = turret;
        BulletPrefab = bulletPrefab;
        CoroutineRunner = coroutineRunner;
        DamageQuantity = damageQuantity;
    }

    private Transform TurretToTurn;
    private GameObject BulletPrefab;
    private MonoBehaviour CoroutineRunner;
    private bool TargeterActive = false;
    private int DamageQuantity;

    private List<Transform> Targets = new List<Transform>();

    private Transform _activeTarget;
    private Transform ActiveTarget
    {
        get
        {
            return _activeTarget;
        }
        set
        {
            _activeTarget = value;
            if (_activeTarget != null)
            {
                RotateToTarget();
                ShootAtTarget();
            }
        }
    }

    public void ActivateTargeter()
    {
        TargeterActive = true;
        ActiveTarget = _activeTarget; //To boot up
    }
    
    // The priority is the closest to base. Since they're going up (+z) to the base, the uppermost is the closest to the base.
    // Not a solution I like, but it's cheap and works. The right solution would be to actually check the distance and order by it.
    public void InRange(Transform targetCandidate)
    {
        if (Targets.Count > 0)
        {
            var Added = false;
            for (var i = 0; i < Targets.Count; i++)
            {
                if (Targets[i] == null)
                {
                    Targets.RemoveAt(i);
                    i--;
                    continue;
                }
                
                if (targetCandidate.position.z > Targets[i].position.z)
                {
                    Targets.Insert(i,targetCandidate);
                    Added = true;
                    break;
                }
            }
            if(!Added) Targets.Add(targetCandidate);
        }
        else
        {
            Targets.Add(targetCandidate);
        }
        GetTarget();
    }

    public void OutOfRange(Transform targetCandidate)
    {
        if (targetCandidate == null || ActiveTarget == null) return;
        if (targetCandidate.GetInstanceID() == ActiveTarget.GetInstanceID())//If active target, null it and get another one
        {
            ActiveTarget = null;
            GetTarget();
        }
        else //else dequeue if in queue
        {
            Targets.Remove(targetCandidate);
        }
    }

    private void GetTarget()
    {
        while (ActiveTarget == null && Targets.Count > 0)
        {
            ActiveTarget = Targets[0];
            Targets.RemoveAt(0);
        }
    }

    private void RotateToTarget()
    {
        if(ActiveTarget == null || !TargeterActive) return;
        
        TurretToTurn.DOLookAt(ActiveTarget.position + (ActiveTarget.forward * 1.5f),80, AxisConstraint.Y).SetSpeedBased().OnUpdate(() => //Need to look a little bit ahead for the looking part to make sense
        {
            if (ActiveTarget == null)
            {
                TurretToTurn.DOKill(false);
                GetTarget();
            }
        }).OnComplete(RotateToTarget);
    }

    private void ShootAtTarget()
    {
        if (!TargeterActive) return;
        
        CoroutineRunner.StopAllCoroutines(); //Ensure just one coroutine is running at given time per CoroutineRunner
        CoroutineRunner.StartCoroutine(ShootRoutine(ActiveTarget));
    }

    private IEnumerator ShootRoutine(Transform shootingTarget)
    {
        yield return null;
        while (shootingTarget != null)
        {
            var DistanceToTarget = Vector3.Distance(shootingTarget.position, TurretToTurn.position);
            var PointingDistance = Vector3.Distance(shootingTarget.position, TurretToTurn.position + TurretToTurn.forward) + 0.8f; 
            //Added 0.8f to make the shootable cone more focused, also not making turrets able to shoot targets too close to them
            
            if (PointingDistance < DistanceToTarget )
            {
                var SpawnedBullet = GameObject.Instantiate(BulletPrefab,
                    TurretToTurn.position + TurretToTurn.forward * 0.1f, Quaternion.identity);
                SpawnedBullet.transform.DOMove(shootingTarget.position + (shootingTarget.forward * 1.5f), 35).SetSpeedBased().OnComplete(() =>
                {
                    GameObject.Destroy(SpawnedBullet);
                    if(shootingTarget != null) DamageEnemy(shootingTarget);
                    else GetTarget();
                }); // Need to shoot a little bit ahead so it makes sense
                yield return new WaitForSeconds(0.9f);
            }

            yield return null;
        }
    }

    private void DamageEnemy(Transform targetHit)
    {
        targetHit.SendMessage("Damage",1);
    }
}
