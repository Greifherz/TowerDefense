using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretPlacementHelper : MonoBehaviour
{
    [SerializeField] private Collider[] TurretColliders;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var TurretCollider in TurretColliders)
        {
            TurretCollider.enabled = false;
        }
    }

    public void Place()
    {
        foreach (var TurretCollider in TurretColliders)
        {
            TurretCollider.enabled = true;
        }
        
        GameObject.Find("Terrain").GetComponent<NavMeshSurface>().BuildNavMesh();
        
        Destroy(this);
    }
}
