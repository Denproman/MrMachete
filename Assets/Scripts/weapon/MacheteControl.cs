using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class MacheteControl : MonoBehaviour
{
    public GameObject Machete;
    //[HideInInspector] 
    public Rigidbody2D rb;
    public Vector3 InstancePosition;
    public Vector3 InstanceRotation;
    
    public float ThrowForce;


    public bool isMacheteThrown = false;
    public bool isMacheteReleazed = false;
    //public bool isMacheteFalling = false;

    public ParticleSystem MacheteParticles;
    public bool StartEmitting = false;

    public TrailRenderer SwordTrail;

    public byte DamageValue;

    public virtual void OnEnable()
    {
        //SetPositionOnInstanceMachete();
        if(rb == null)
        {
            rb = Machete.GetComponent<Rigidbody2D>();
        }

        isMacheteThrown = false;
        isMacheteReleazed = false;
        //ParticlesEmitting(false);
        SwordTrailStart(false);
        
    }

    private void Update() {
        
    }


    public virtual void SetInstancePosition()
    {
        Machete.transform.localPosition = InstancePosition;
        Machete.transform.eulerAngles = InstanceRotation;
    }

    public virtual void OnBecameInvisible()
    {
        isMacheteThrown = true;
        //ParticlesEmitting(false);
        SwordTrailStart(false);
        Destroy(Machete);
        
    }

    public virtual void ThrowMachete(float Force)
    {

        isMacheteReleazed = true;
        rb.AddRelativeForce(Vector2.left*Force, ForceMode2D.Impulse);
        //ParticlesEmitting(true);

    }
    
    public void DestroyMachete()
    {
        if(isMacheteReleazed)  //isMacheteThrown
        {
            if(Machete != null)
            {
                Destroy(Machete, 3);
            }
        }
    }
    
    public void ParticlesEmitting(bool enableEmitting)
    {
        MacheteParticles.gameObject.SetActive(enableEmitting);
    }

    public void SwordTrailStart(bool trailStart)
    {
        SwordTrail.gameObject.SetActive(trailStart);
    }

    /*public void IsMacheteFalling()
    {
        lastMachetePos = curMachetePos
        float pos2;
        
        pos2 = pos1;
        pos1 = 
    }*/
}
