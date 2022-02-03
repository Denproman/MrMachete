using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{    
    public MacheteControllerPlayer WeaponController;
    private Collider2D PlayerCol;
    public Vector2 StartHandPos;
    [SerializeField] private Animation animPlayer;
    private void OnEnable() 
    {
        PlayerCol = GetComponent<Collider2D>();
        StartHandPos = Hand.localPosition;
        
        //start idle anim
    }

    public void BecomeTouchable(bool state)
    {
        PlayerCol.isTrigger = state;
    }

    public override void ThrowMachete()
    {      
        //weapon throw
        //WeaponController.Machete.transform.position += Vector3.Lerp(WeaponController.Machete.transform.localPosition.normalized, WeaponController.Machete.transform.localPosition.normalized + new Vector3(1, 1, 0)*1f, 1f);
        WeaponController.rb.freezeRotation = true;
        WeaponController.isMacheteReleazed = true;
        WeaponController.SwordTrailStart(true);
        //WeaponController.ParticlesEmitting(true);
        //Vector3 StartPosY = WeaponController.Machete.transform.position;
        //float dist = (StartPosY - WeaponController.Machete.transform.position).magnitude;
        StartCoroutine(ThrowUpdate(/*dist,*/ 2.2f));
        //WeaponController.Machete.transform.Translate(Vector3.down * 1.5f);
        /*if(WeaponController.Machete.transform.position.y >= StartPosY * 3)
        WeaponController.rb.AddRelativeForce(Vector2.up*WeaponController.ThrowForce);*/
    }

    IEnumerator ThrowUpdate(/*float dist,*/ float Speed)
    {
        Vector3 StartPosY = WeaponController.Machete.transform.position;
        float dist = (StartPosY - WeaponController.Machete.transform.position).magnitude;
        while(dist < /*StartPosY **/ 0.4f)
        {
            dist = (StartPosY - WeaponController.Machete.transform.position).magnitude;
            WeaponController.Machete.transform.Translate(Vector3.down * /*StartPosY **/ 0.4f * Time.deltaTime*Speed);
            if(dist >= /*StartPosY **/ 0.4f)
            {
                WeaponController.rb.AddRelativeForce(Vector2.up*WeaponController.ThrowForce, ForceMode2D.Impulse);
            }
            yield return null;
        }
    }

    public void ThrowUpMachete()
    {
        //DOTween anim of hand
        anim.SetTrigger("StartThrow");
        //anim.runtimeAnimatorController.animationClips.
    }

    public override void Death()
    {
        //anim.Play("anim");
        //show screen
        base.Death();

    }

    public override void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.layer == 9)
        {
            MacheteControl machete = collision.gameObject.GetComponent<MacheteControl>();
            /*if(WeaponController.Rotations >= 0//>= WeaponController.MaxRotationNumber 
            || WeaponController.isMacheteReleazed)*/
            if(WeaponController.TapNumber > 0)
            {
               
                Health -= machete.DamageValue;
                 Death();
            }
        }
        
    }
    
    public void BecomeIntouchable(bool trigger)
    {
        this.GetComponent<Collider2D>().isTrigger = trigger;
    }
    
}
