using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CreepSpawnController : EventHandler
{
    [SerializeField] private GameObject[] SpawnPoints;

    [SerializeField] private LevelDescriptor CurrentLevel;
    
    [SerializeField] private HpBar[] HpBarPool;
    
    // Start is called before the first frame update
    void Start()
    {
        EventController.Instance.RegisterListener(Visit);
    }

    public override void Handle(INextLevelEvent nextLevelEvent)
    {
        CallInNextWave(nextLevelEvent.Descriptor);
    }

    private void CallInNextWave(LevelDescriptor nextWave)
    {
        CurrentLevel = nextWave;
        StartCoroutine(SpawnRoutine(CurrentLevel));
    }

    private IEnumerator SpawnRoutine(LevelDescriptor levelToSpawn)
    {
        yield return null;

        var SpawnedCounter = 0;
        
        while (SpawnedCounter < levelToSpawn.CreepsToSpawn)
        {
            var RandomizedPos = new Vector3(Random.Range(1,5f),0,Random.Range(1,5f));
            var SpawnPointPos = GetSpawnPointPos();
            
            var Spawned = Instantiate(levelToSpawn.CreepPrefab, SpawnPointPos + RandomizedPos, Quaternion.identity);
            Spawned.name = Spawned.name.Replace("(Clone)", "");
            
            //Set pathfinding things
            Spawned.GetComponent<NavMeshAgent>().SetDestination(new Vector3(0,1,45));
            var VacantHpBar = HpBarPool.FirstOrDefault(x => !x.Active);
            if(VacantHpBar != null) Spawned.GetComponent<CreepBehaviour>().SetHealthhBar(VacantHpBar);

            SpawnedCounter++;
            
            yield return new WaitForSeconds(levelToSpawn.TimeBetweenSpawns);
        }
    }

    private Vector3 GetSpawnPointPos()
    {
        if (SpawnPoints.Length == 0)
        {
            throw new Exception("No Spawnpoints set on CreepSpawnController!");
        }

        return SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position;
    }
}
