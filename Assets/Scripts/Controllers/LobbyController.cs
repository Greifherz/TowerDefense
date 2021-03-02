using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{

    [SerializeField] private GameObject[] DisappearWhenLoading;
    [SerializeField] private Image BackgroundToFade;
    public void GoToGameplay()
    {
        for(int i = 0 ; i < DisappearWhenLoading.Length ; i++) DisappearWhenLoading[i].SetActive(false);
        
        var AsyncLoad = SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
        AsyncLoad.completed += (op) =>
        {
            BackgroundToFade.DOFade(0, 2.5f).OnComplete(() =>
            {
                BackgroundToFade.DOKill();
                SceneManager.UnloadSceneAsync(0);
            });
        };
    }
}
