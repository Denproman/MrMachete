using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject PlayerMachetePrefab;
    public GameObject EnemyMachetePrefab;
    public PlayerController playerController;
    public EnemyController[] enemyController;
    //public TutorialHand tutorialHand;
    public byte deadEnemies;
    public byte SpawnMacheteNumber = 0;
    public byte MaxSpawnMacheteNumber = 3;
    public TurnOf TurnToThrow;

    public byte AliveEnemies;
    
    public Vector2 StartWeaponThrowPoint;
    public Vector2 FinishWeaponThrowPoint;

    public float LevelNumber;
    public byte isGameFinished;
    
    public bool isMacheteUp;

    public bool checkGameOver;
    public bool isResumePhysics;
    
    private void OnEnable()
    {
        GetSceneNumber();
        if(LevelNumber == 3 && isGameFinished == 0)
        {
            isGameFinished = 1;
            PlayerPrefs.SetInt("isRandom", isGameFinished);
        }
        
        isGameFinished = (byte)PlayerPrefs.GetInt("isRandom");
        checkGameOver = true;
        if(isGameFinished == 0)
        {
            isResumePhysics = false;
        }

        TurnToThrow = TurnOf.player;
        InstantiateMachete();
    }

    void Update()
    {
        
        //InstantiateMachete();
        //ChangeTurn();
        if(playerController.WeaponController == null 
        && playerController.WeaponController.Rotations < playerController.WeaponController.MaxRotationNumber)
        {
            //EnemyThrowMachete();
        }
        //EnemyThrowMachete();

        if(playerController.Health <= 0)
        {
            GameOver();
        }
        //if(playerController.WeaponController == null)
        {
            ChangeTurn();
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            if(TurnToThrow == TurnOf.player)
            {
                if(playerController.WeaponController != null && playerController.WeaponController.TapNumber == 0)
                {                    
                    //if(playerController.WeaponController.transform.position.y == playerController.MacheteInstancePosition.y
                    //|| playerController.WeaponController.transform.position.y == playerController.WeaponController.HigherPosition.y)
                    {
                        playerController.WeaponController.TapNumber++;
                        /*if(playerController.WeaponController.TapNumber > 2)
                        {
                            playerController.WeaponController.TapNumber = 0;
                        }*/
                    }
                }
                else if(playerController.WeaponController.TapNumber == 1 && isMacheteUp && LevelNumber > 1 )
                {
                    playerController.WeaponController.TapNumber++;
                }
                else if(Physics2D.autoSimulation == false && LevelNumber == 1 && isGameFinished == 0)
                {
                    playerController.WeaponController.TapNumber++;
                }
                else if(LevelNumber == 1 && isGameFinished == 1)
                {
                    playerController.WeaponController.TapNumber++;
                }
            
                //if (Machete.transform.position.y == HigherPosition.y)
                if(playerController.WeaponController.TapNumber == 1)
                {
                    if(playerController.WeaponController.transform.parent != null)
                    {
                        playerController.ThrowUpMachete();
                    }
                    //if(playerController.anim.)
                    //Invoke("ThrowUpSword", 1);
                    if(playerController.Hand.position.y == 2.42f) //2.4254
                    {
                        playerController.WeaponController.ThrowUpMachete();
                    }

                    if(LevelNumber == 1 /*&& isGameFinished == 0*/ && Physics2D.autoSimulation == false)
                    {
                        playerController.WeaponController.TapNumber++;
                    }
                    //playerController.BecomeTouchable(false);
                    //playerController.WeaponController.MacheteRotation();

                    
                }
                else if(//playerController.WeaponController.Rotations < playerController.WeaponController.MaxRotationNumber 
                    //&& 
                    playerController.WeaponController.TapNumber > 1/*Rotations >= 0*/)
                {
                    //StartWeaponThrowPoint = playerController.WeaponController.transform.position;
                    
                    
                    {
                        Physics2D.autoSimulation = true;
                        playerController.WeaponController.rb.gravityScale = 0;
                        playerController.ThrowMachete();
                    }
                    
                    
                    TurnToThrow = TurnOf.enemy;
                    ChangeTurn();
                }
            }
        }
    if(LevelNumber == 1)
    {
        if(isGameFinished == 0)
        {
            if(playerController.WeaponController != null && playerController.WeaponController.TapNumber == 1)
            {
                if(playerController.WeaponController.transform.position.y > 2f 
                && playerController.WeaponController.transform.eulerAngles.z <= 205f 
                && playerController.WeaponController.transform.eulerAngles.z >= 195f)
                {
                    Physics2D.autoSimulation = false; 
                    //tutorialHand.gameObject.SetActive(true);
                }
            }
        }
        /*else if(isGameFinished == 1 && playerController.WeaponController.TapNumber == 1)
        {
            playerController.WeaponController.TapNumber++;
        }*/
        else if(playerController.WeaponController.TapNumber > 1 && Physics2D.autoSimulation == false)
        {
            Physics2D.autoSimulation = true;
            playerController.WeaponController.rb.gravityScale = 0;
            playerController.ThrowMachete();
        }
    }

        if(playerController.Hand.position.y >= -4.2f && !isMacheteUp)
        {
            playerController.WeaponController.ThrowUpMachete();
            isMacheteUp = true;
        }

        /*if(playerController.WeaponController.Rotations == playerController.WeaponController.MaxRotationNumber)
        {
            playerController.WeaponController.FallDownMachete(playerController.WeaponController.FallForce);
        }*/

        /*if(playerController.WeaponController.isMacheteReleazed)
        {
            FinishWeaponThrowPoint = playerController.WeaponController.transform.position;
        }*/
    }

    public float RandomLevel()
    {
        float CurrentLevelNumber = LevelNumber;
        float NextLevelNumber = Random.Range(1, 4);
        if(CurrentLevelNumber == NextLevelNumber)
        {
            return RandomLevel();
        }
        else if(NextLevelNumber == 4)
        {
            NextLevelNumber = 3;
            return NextLevelNumber;
        }
        else
        {
            return NextLevelNumber;        
        }
    }

    private void LoadLvl()
    {
        
        SceneManager.LoadScene("Level-" + LevelNumber);
    }

    private void GetSceneNumber()
    {
        string[] LvlName = SceneManager.GetActiveScene().name.Split(char.Parse("-"));
        float.TryParse(LvlName[1], out LevelNumber);
    }


    //Machete Part
    private void InstantiateMachete()
    {
        //Instance for player
        if(TurnToThrow == TurnOf.player)
        {
            
            if(playerController.WeaponController == null && playerController.Health > 0)
            {
                playerController.BecomeTouchable(true);
                //if(SpawnMacheteNumber < MaxSpawnMacheteNumber)
                //if(AliveEnemies > 0)
                {
                    isMacheteUp = false;
                    playerController.Hand.localPosition = playerController.StartHandPos;
                    GameObject GO = Instantiate(playerController.weaponPrefab, playerController.Hand);
                    playerController.WeaponController  = GO.GetComponent<MacheteControllerPlayer>();
                    playerController.WeaponController.InstancePosition = playerController.MacheteInstancePosition;
                    playerController.WeaponController.InstanceRotation = playerController.MacheteInstanceRotation;
                    playerController.WeaponController.SetInstancePosition();
                    //playerController.WeaponController.transform.eulerAngles = new Vector3(0, 0, 285);
                    //if(playerController.WeaponController.isMacheteReleazed)
                    {
                        SpawnMacheteNumber++;
                    }
                }
            }
        

            //Instance for enemy
            /*foreach(EnemyController enemy in enemyController)
            {
                if(enemy != null && enemy.WeaponController == null)
                {
                    GameObject GO = Instantiate(enemy.weaponPrefab, enemy.Hand);
                    enemy.WeaponController = GO.GetComponent<MacheteControllerEnemy>();
                    enemy.WeaponController.InstancePosition = enemy.MacheteInstancePosition;
                    //SpawnMacheteNumber++;
                }
            }*/
        }
        
    }
        


    //just test
    private void GameOver()
    {
        if(checkGameOver)
        {
        
            if(playerController.Health <= 0)
            {
                //GetSceneNumber();
                Invoke("LoadLvl", 4);
            }
            //else if(enemyController.Length == 0)
            else if(AliveEnemies == 0)
            {
                //win
                if(isGameFinished == 0)
                {
                    LevelNumber++;
                }
                else
                {
                    LevelNumber = RandomLevel();
                }
                
                //Invoke("LoadLvl", 2);
            }
        }

        checkGameOver = false;
    }

    //whose turn to throw
    /*private void ChangeTurn()
    {
        if(TurnToThrow == TurnOf.player)
        {
            if(playerController.WeaponController == null)
            {
                TurnToThrow = TurnOf.enemy;
                playerController.WeaponController.isMacheteThrown = false;
            }
        }
        
        EnemyThrowMachete();
        //else
        {
            foreach(EnemyController enemy in enemyController)
            {
                if(enemy.WeaponController == null)
                {
                    TurnToThrow = TurnOf.player;
                    //enemy.WeaponController.isMacheteThrown = false;
                    InstantiateMachete();
                    break;
                }
            }
        }
        
    }*/

    public void ThrowUpSword()
    {
        playerController.WeaponController.ThrowUpMachete();
    }

    void EnemyThrowMachete()
    {
        if(TurnToThrow == TurnOf.enemy && playerController != null)
        {
            if(playerController.Health > 0)
            {
                List<EnemyController> enemies = RecalculateEnemies();
                int Number = Random.Range(0, enemies.Count);
                
                enemies[Number].ThrowMachete();
                //TurnToThrow = TurnOf.player;
                //if(enemyController[Number].WeaponController == null)
                {    
                // InstantiateMachete();
                }
            }  
        }
    }
    
    List<EnemyController> RecalculateEnemies()
    {
        List<EnemyController> enemies = new List<EnemyController>();

        foreach(EnemyController enemy in enemyController)
        {
            //if(enemy.WeaponController != null)
            {
                if(enemy.Health > 0)
                {
                    enemies.Add(enemy);
                }
            }
        }
        if(enemies.Count <= 0)
        {
            playerController.MainBody.SetActive(false);
            playerController.SpineAnim.gameObject.SetActive(true);
            playerController.SpineAnim.loop = true;
            playerController.SpineAnim.AnimationName = "win";
            
            //checkGameOver = true;
            GameOver();
            Invoke("LoadLvl", 2);
        }

        return enemies;
    }
    public void ChangeTurn()
    {
        if(TurnToThrow == TurnOf.player)
        {
            byte aliveEnemies = 0;
            foreach(EnemyController enemy in enemyController)
            {
                
                if(enemy.Health > 0)
                {
                    aliveEnemies++;
                }
                /*else
                {
                    if(AliveEnemies > 0)
                    {
                        AliveEnemies--;
                    }
                }*/
            }
            AliveEnemies = aliveEnemies;
            
            if(playerController.WeaponController.isMacheteThrown)
            {
                TurnToThrow = TurnOf.enemy;
                if(SpawnMacheteNumber < MaxSpawnMacheteNumber)
                {
                    //EnemyThrowMachete();
                }
            }
        }
        else if(TurnToThrow == TurnOf.enemy)
        {
            byte aliveEnemies = 0;
            foreach(EnemyController enemy in enemyController)
            {
                
                if(enemy.Health > 0)
                {
                    aliveEnemies++;
                }
                /*else
                {
                    if(AliveEnemies > 0)
                    {
                        AliveEnemies--;
                    }
                }*/
            }
            AliveEnemies = aliveEnemies;
            //TurnToThrow = TurnOf.player;
            //foreach(EnemyController enemy in enemyController)
            {
                //if(enemy.WeaponController.isMacheteThrown)
                {
                    List<EnemyController> enemies = RecalculateEnemies();
                    
                    if(enemies.Count > 0 && playerController.Health > 0 && AliveEnemies > 0)
                    {    
                        
                        if(SpawnMacheteNumber < MaxSpawnMacheteNumber && playerController.WeaponController.isMacheteThrown)
                        {
                            TurnToThrow = TurnOf.player;
                            InstantiateMachete();
                        }
                        else if(SpawnMacheteNumber >= MaxSpawnMacheteNumber && playerController.WeaponController.isMacheteThrown)
                        {
                            playerController.MainBody.SetActive(false);
                            playerController.SpineAnim.gameObject.SetActive(true);
                            playerController.SpineAnim.loop = true;
                            playerController.SpineAnim.AnimationName = "lose";

                            Invoke("LoadLvl", 2);
                        }
                    }
                }
            }
        }
        
    }

}



public enum TurnOf
{
    player,
    enemy
}
