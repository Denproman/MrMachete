using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterController : MonoBehaviour
{
    public GameObject MainBody;
    public GameObject weaponPrefab;
    public Transform Hand;
    public int Health;
    public Vector3 MacheteInstancePosition;
    public Vector3 MacheteInstanceRotation;
    public ParticleSystem BloodVFX;

    public Animator anim;
    public SkeletonAnimation SpineAnim;
    public SkeletonAnimation SpineAnimDeath;
    
    private void OnEnable() 
    {
        BloodVFX.gameObject.SetActive(false);
        //start idle anim
    }

    public virtual void Update() 
    {
       // Death();
    }

    public virtual void ThrowMachete()
    {
        //DOTween anim of hand up
        
    }

    public virtual void Death()
    {
        if(Health <= 0)
        {
            MainBody.SetActive(false);
            SpineAnimDeath.gameObject.SetActive(true);
            SpineAnimDeath.loop = false;
            SpineAnimDeath.AnimationName = "death";

            //anim.Play("anim");
            //show screen
            //Destroy(this.gameObject);
        }

    }

    public virtual void OnTriggerEnter2D(Collider2D collision) 
    {
        Debug.Log("collide");
        if(collision.gameObject.layer == 9)
        {
            MacheteControl machete = collision.gameObject.GetComponent<MacheteControl>();
            //if(machete.isMacheteReleazed)
            {
                Health -= machete.DamageValue;
            }
            Vector2 CutPoint = new Vector2();
            for(byte i = 0; i < collision.GetComponent<Collision2D>().contacts.Length; i++)
            {
                //if(collision.)
                CutPoint = collision.GetComponent<Collision2D>().contacts[i].point;
            }
            BloodVFX.transform.position = CutPoint;
            BloodVFX.gameObject.SetActive(true);
            //machete.transform.position = new Vector2(machete.transform.position.x, machete.transform.position.y);
        }
        
    }

    /*private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 9)
        {

        }
        //Debug.Log(collision.contacts.Length);
    }
    
    private void OnCollisionExit2D(Collision2D collision) {
        //if(collision.gameObject.layer == 9)
        //Debug.Log(collision.contacts.Length);
    }*/
    
}
