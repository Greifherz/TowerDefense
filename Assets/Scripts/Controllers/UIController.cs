using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIController : EventHandler
{
    [SerializeField] private Text CurrencyValue;

    [SerializeField] private Text BaseHealthValue;

    [SerializeField] private Text EnemiesToNextWaveValue;

    [SerializeField] private GameObject WonPanel;
    [SerializeField] private GameObject LostPanel;
    [SerializeField] private Image EndGamePanelBackground;
    
    // Start is called before the first frame update
    void Start()
    {
        EventController.Instance.RegisterListener(Visit); //Has to be the last to register. Made sure of that on execution order. Don't like it but it's better than coupling
    }

    public override void Handle(IUpdateUIEvent updateUiEvent)
    {
        switch (updateUiEvent.UpdateType)
        {
            case UIUpdateType.Currency:
                CurrencyValue.text = updateUiEvent.UpdateValue.ToString();
                break;
            case UIUpdateType.BaseHealth:
                BaseHealthValue.text = updateUiEvent.UpdateValue + "/10";
                break;
            case UIUpdateType.EnemiesLeft:
                EnemiesToNextWaveValue.text = updateUiEvent.UpdateValue.ToString();
                break;
            default:
                //Do nothing
                break;
        }
    }

    public override void Handle(IEndGameEvent endGameEvent)
    {
        var PanelToTurnOn = endGameEvent.GameWon ? WonPanel : LostPanel;
        
        EndGamePanelBackground.DOFade(1, 5f).OnComplete(() => PanelToTurnOn.SetActive(true));
    }

    public void PlaceNewTowerClicked()
    {
        EventController.Instance.Raise(new PlayerInputEvent(PlayerInputType.PlaceTower));
    }
    
    public void PlaceSlowTowerClicked()
    {
        EventController.Instance.Raise(new PlayerInputEvent(PlayerInputType.PlaceSlowTurret));
    }

    public void NextWaveClicked()
    {
        EventController.Instance.Raise(new PlayerInputEvent(PlayerInputType.CallNextWave));
    }
}
