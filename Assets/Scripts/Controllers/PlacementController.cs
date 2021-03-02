using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementController : EventHandler
{
    [SerializeField] private GameObject TowerPrefab;
    [SerializeField] private GameObject SlowTowerPrefab;
    [SerializeField] private GameObject PlacingTower;
    [SerializeField] private bool Placing = false;
    [SerializeField] private CurrencyController CurrencyController;

    // Start is called before the first frame update
    void Start()
    {
        EventController.Instance.RegisterListener(Visit);
    }

    public override void Handle(IPlayerInputEvent playerInputEvent)
    { //As there are more types of tower, It's best to have somewhere where I relate this tower type with tower cost. Since It's only two, I'll flag as refactor and leave it at that.
        if(playerInputEvent.InputType == PlayerInputType.PlaceTower && CurrencyController.AllowSpendingOf(5)) PlaceNewTower(TowerPrefab,5);
        if(playerInputEvent.InputType == PlayerInputType.PlaceSlowTurret && CurrencyController.AllowSpendingOf(15)) PlaceNewTower(SlowTowerPrefab,15);
    }

    public void PlaceNewTower(GameObject placementPrefab,int cost)
    {
        PlacingTower = Instantiate(placementPrefab, GetMouseWorldPosition(), Quaternion.identity);
        Placing = true;
        StartCoroutine(PlaceTowerRoutine(cost));
    }

    private IEnumerator PlaceTowerRoutine(int cost)
    {
        yield return null;
        while (Placing)
        {
            PlacingTower.transform.position = GetMouseWorldPosition();
            yield return null;
            if (Input.GetMouseButtonUp(0) && PlacingTower.transform.position.y < 1000)
            {
                PlacingTower.SendMessage("Place");
                Placing = false;
                EventController.Instance.Raise(new CoinSpentEvent(cost));
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                Destroy(PlacingTower);
                Placing = false;
            }
        }
    }

    private Vector3 GetMouseWorldPosition() //I can improve later both raycast and this LinQ, they aren't exactly efficient but are semantic
    {
        Vector3 ReturnPos = new Vector3();
        
        //Needs to raycast, sadly
        RaycastHit Hit;
        //The following if means, if it hits the terrain and only the terrain. Needs two raycasts for it or a RaycastAll, commented below is the RaycastAll solution
        //that might be useful if I want to tell the player what's invalidating his placement, but it's less efficient since it needs to iterate through the various hits.
        if (!EventSystem.current.IsPointerOverGameObject() &&
            !Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out Hit, 500, ~(1 << 8 | 1 << 10)) 
            && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 500, (1 << 8)))
        {
            ReturnPos = Hit.point;
        }
        else
        {
            ReturnPos = new Vector3(0,1000,0);
        }
        
        // var Hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 500);
        //
        // //Check if any Rock was hit along the way, all terrain details are flagged as static so I'll just use that as a compare instead of making two raycasts
        // var HitRock = Hits.Any(x => x.transform.gameObject.isStatic);
        //
        // //If it didn't Ok to place and to teleport there
        // if (!HitRock && !EventSystem.current.IsPointerOverGameObject()) ReturnPos = Hits.FirstOrDefault(x => !x.transform.gameObject.isStatic).point;
        // //If it did, just send it outside camera - Since it's always the same spot when it's invalid, we can check that to say if it's a invalid placement
        // else ReturnPos = new Vector3(0,1000,0);
        
        return ReturnPos;

    }
}
