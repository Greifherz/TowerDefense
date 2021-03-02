using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SlowTurretBehaviour : MonoBehaviour
{
    [SerializeField] private ColliderTriggerDetector Detector;
    [SerializeField] private float SlowIntensity = 0.5f;
    
    private List<GameObject> CreepsInRange = new List<GameObject>();
    private bool Placed = false;
    
    void Start()
    {
        Detector.SetEnterCallback(ObjectEnteredRange);
        Detector.SetExitCallback(ObjectExitedRange);

        StartCoroutine(CleanCacheRoutine());
    }

    private void Place()
    {
        Placed = true;
        for(int i = 0 ; i < CreepsInRange.Count ; i++)
            CreepsInRange[i].SendMessage("Slow",SlowIntensity);
    }

    private void ObjectEnteredRange(GameObject entree)
    {
        if(!entree.tag.Equals("Enemy")) return;
        CreepsInRange.Add(entree);

        if (Placed) entree.SendMessage("Slow",SlowIntensity);
    }

    private void ObjectExitedRange(GameObject exitee)
    {
        if(!exitee.tag.Equals("Enemy")) return;
        CreepsInRange.Remove(exitee);
        if(Placed) exitee.SendMessage("NormalizeSpeed");
    }

    private IEnumerator CleanCacheRoutine()
    {
        while (Placed)
        {
            yield return new WaitForSeconds(5); //Cleans the cache every 5 seconds so it won't use up too much memory
            CleanCache();
        }
    }

    private void CleanCache()
    {//Love LinQ, but regular loop is more performatic given there aren't more than 10k elements
        
    }
}
