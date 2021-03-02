using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : EventHandler
{
   [SerializeField] private int BaseHealth;
   
   [SerializeField] private List<LevelDescriptor> NextLevels;

   private int CreepCount = 0;

   private void Start()
   {
      EventController.Instance.RegisterListener(Visit);
   }

   public override void Handle(IBaseDamageEvent baseDamageEvent)
   {
      BaseHealth--;
      EventController.Instance.Raise(new UpdateUIEvvent(UIUpdateType.BaseHealth,BaseHealth));
      if (BaseHealth <= 0)
      {
         EventController.Instance.Raise(new EndGameEvent(false));
         StartCoroutine(BackToLobby());
      }
   }
   
   //Need to listen to the start spawn event, with level descriptor, keep tabs on how many creeps were spawned
   public override void Handle(INextLevelEvent nextLevelEvent)
   {
      CreepCount = nextLevelEvent.Descriptor.CreepsToSpawn;
      EventController.Instance.Raise(new UpdateUIEvvent(UIUpdateType.EnemiesLeft,CreepCount));
      
      for (int i = 0; i < NextLevels.Count; i++)
      {
         if (NextLevels[i].name.Equals(nextLevelEvent.Descriptor.name))
         {
            NextLevels.RemoveAt(i);
            break;
         }   
      }
   }

   //Need to listen to the creep kill, count the kills
   public override void Handle(ICreepKillEvent creepKillEvent)
   {
      CreepCount--;
      EventController.Instance.Raise(new UpdateUIEvvent(UIUpdateType.EnemiesLeft,CreepCount));
      
      if (CreepCount == 0)
      {
         CheckGameWin();
      }
   }
   
   public override void Handle(IPlayerInputEvent playerInputEvent)
   {
      if (CreepCount == 0 && playerInputEvent.InputType == PlayerInputType.CallNextWave && NextLevels.Count > 0)
      {
         var nextLevel = NextLevels[0];
         EventController.Instance.Raise(new NextLevelEvent(nextLevel)); 
         NextLevels.RemoveAt(0);
      }
   }

   //In case the creep kill count == level descriptor's spawned quantity, you win
   private void CheckGameWin()
   {
      if (NextLevels.Count == 0)
      {
         EventController.Instance.Raise(new EndGameEvent(true));
         StartCoroutine(BackToLobby());
      }
   }

   private IEnumerator BackToLobby()
   {
      yield return new WaitForSeconds(7.5f);
      SceneManager.LoadSceneAsync(0);
   }
}
