using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{    
    public MacheteControllerEnemy WeaponController;
    //public Vector2 StartWeaponThrowPoint;
    //public Vector2 FinishWeaponThrowPoint;

    public Quaternion StandPosition;
    public Quaternion DeadPosition;
    private void OnEnable() 
    {
        BloodVFX.gameObject.SetActive(false);
        //start idle anim
    }
    
    

    public override void ThrowMachete()
    {
        //DOTween anim of hand up
        //weapon throw on player direction
        WeaponController.transform.rotation = Quaternion.Euler(0, 0, 340);
        WeaponController.rb.AddRelativeForce(Vector2.left*WeaponController.ThrowForce);

    }

    public override void Death()
    {
        //anim.Play("anim");
        //show screen
        //base.Death();
        if(Health <= 0)
        {
            GetComponentInParent<GameController>().deadEnemies++;
            //SpineAnim.gameObject.SetActive(false);
            SpineAnim.AnimationName = null;
            SpineAnim.gameObject.SetActive(false);
            MainBody.SetActive(true);
            if(MainBody.transform.rotation.eulerAngles.z < 90)
            {
                MainBody.transform.Rotate(Vector3.forward, -5);
            }
            //MainBody.transform.rotation = Quaternion.Lerp(StandPosition, DeadPosition, Time.deltaTime * (2f));
        }

    }

    public override void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.layer == 9)
        {
            MacheteControl machete = collision.gameObject.GetComponent<MacheteControl>();
            //if(machete.isMacheteReleazed)
            if(Health > 0)
            {
                
                Health -= machete.DamageValue;
                Death();
            }
            //machete.transform.position = new Vector2(machete.transform.position.x, machete.transform.position.y);
            Vector2 CutPoint = machete.transform.position;
            BloodVFX.transform.position = CutPoint;
            BloodVFX.gameObject.SetActive(true);
        }
        /*if(collision.gameObject.layer == 9)
        {
            //if(collision.gameObject.layer== WeaponController.gameObject)
            {
               StartWeaponThrowPoint = collision.transform.position;
            }
            Debug.Log(StartWeaponThrowPoint);
        }*/
       
    }

    /*private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.layer == 9)
        {
            //if(collision.gameObject.layer== WeaponController.gameObject)
            {
               FinishWeaponThrowPoint = collision.transform.position;
            }
            Debug.Log(FinishWeaponThrowPoint);
        }
       
    }*/
    
}
