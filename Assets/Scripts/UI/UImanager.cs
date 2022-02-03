using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameController gameController;
    public GameOver gameOver;
    public Transform AttemtsContainer;
    public GameObject AttempDotPrefab;
    public TutorialHand tutorialHand;
    public Transform SkullPlate;
    public GameObject SkullPrefab;
    public SkullRepaint skullRepaint;
    public ParticleSystem ConfettiWin;

    void OnEnable()
    {
        tutorialHand.gameObject.SetActive(true);
        ConfettiWin.gameObject.SetActive(false);
        for(byte i = 0; i < gameController.enemyController.Length; i++)
        {
            Instantiate(SkullPrefab, SkullPlate);
        }

        gameOver.gameObject.SetActive(false);
        AttemtsContainer.gameObject.SetActive(true);
        for(byte i = 0; i < gameController.MaxSpawnMacheteNumber; i++)
        {
            Instantiate(AttempDotPrefab, AttemtsContainer);
        }

    }

    void Update()
    {
        if(gameController.playerController.WeaponController.TapNumber == 1)
        {
            tutorialHand.gameObject.SetActive(false);
        }

        if(gameController.LevelNumber == 1 && !Physics2D.autoSimulation && !tutorialHand.gameObject.activeSelf)
        {
            tutorialHand.gameObject.SetActive(true);
        }
        else if(gameController.playerController.WeaponController.TapNumber > 1)
        {
            tutorialHand.gameObject.SetActive(false);
        }
        
        for(byte i = 0; i < gameController.enemyController.Length; i++)
        {
            if(gameController.enemyController[i].Health <= 0)
            {
                SkullPlate.GetChild(i).GetComponent<Image>().sprite = skullRepaint.WhiteSkull;
            }
        }

        if(gameController.playerController.Health <= 0 
        || (gameController.SpawnMacheteNumber >= gameController.MaxSpawnMacheteNumber && gameController.playerController.WeaponController.isMacheteThrown && gameController.AliveEnemies != 0))
        {
            gameOver.gameObject.SetActive(true);
            gameOver.VictoryScreen.SetActive(false);
            gameOver.DefeatScreen.SetActive(true);
        }
        else if(gameController.playerController.Health > 0 && gameController.AliveEnemies == 0)
        {
            if(!ConfettiWin.gameObject.activeSelf)
            {
                ConfettiWin.gameObject.SetActive(true);
                ConfettiWin.Play();
            }
            gameOver.gameObject.SetActive(true);
            gameOver.VictoryScreen.SetActive(true);
            gameOver.DefeatScreen.SetActive(false);
        }

        //attemps
        int attemptsLeft = 0;
        attemptsLeft = gameController.MaxSpawnMacheteNumber - gameController.SpawnMacheteNumber;
        for(byte i = 0; i < gameController.SpawnMacheteNumber-1; i++)
        {
            //if(i < attemptsLeft)
            {
                AttemtsContainer.GetChild(i).gameObject.SetActive(false);
            }
            /*else
            {
                AttemtsContainer.GetChild(i).gameObject.SetActive(false);
            }*/
        }
        if(attemptsLeft == 0 && gameOver.gameObject.activeSelf)
        {
            for(byte i = 0; i < AttemtsContainer.childCount; i++)
            {
                AttemtsContainer.GetChild(i).gameObject.SetActive(false);
            }
        }
        
    }
}
