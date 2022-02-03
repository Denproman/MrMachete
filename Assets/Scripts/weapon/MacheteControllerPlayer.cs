using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class MacheteControllerPlayer : MacheteControl
{
    [Header("Positions")]
    public Vector3 HigherPosition;
    public Quaternion HigherRotation;
    public float StartAngle;

    public Vector2 StartCutPoint;
    public Vector2 EndCutPoint;
    [Header("Forces")]
    public float FallForce;
    
    [Header("Speeds")]
    public float ThrowingUpSpeed;
    public float RotationSpeed;
    [Header("Rotation")]

    public float MaxRotationNumber;
    public float Rotations;
    
    public byte TapNumber = 0;

    public List<Collider2D> EnemyBodyParts;
    public GameObject Dots;

    public bool isEnemyKilled;
    
    public override void OnEnable()
    {
        base.OnEnable();

        Rotations = -1;

        TapNumber = 0;
    }

    public override void OnBecameInvisible() {
        isMacheteThrown = true;
        SwordTrailStart(false);
        if(isEnemyKilled)
        {

        }
        Destroy(Machete);
    }


    void Update()
    {    
        SwitchDots();    
        /*if(Input.GetMouseButtonDown(0))
        {
            TapNumber++;
            
            //if (Machete.transform.position.y == HigherPosition.y)
            if(TapNumber == 1)
            {
                ThrowUpMachete();
                MacheteRotation();
            }
            if(Rotations < MaxRotationNumber*/ 
            //&& TapNumber > 1/*Rotations >= 0*/)
            /*{
                ThrowMachete(ThrowForce);
            }
        }

        if(Rotations == MaxRotationNumber)
        {
            FallDownMachete(FallForce);
        }*/
        

        /*if(Input.touchCount > 0)
        {
            
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                TapNumber++;
                ThrowUpMachete();
                //if (Machete.transform.position.y == HigherPosition.y)
                if(TapNumber == 1)
                {
                    MacheteRotation();
                }
                if(Rotations < MaxRotationNumber && TapNumber == 2)
                {
                    ThrowMachete(ThrowForce);
                }
            }
        }*/

    }

    private void OnTriggerEnter2D(Collider2D collide) 
    {
        if(collide.gameObject.layer == 8)
        {
            //if(collide.GetComponent<EnemyController>().Health <= 0)
            {
                isEnemyKilled = true;
            }
            //StartCutPoint = transform.position;
        }
    }
    public override void SetInstancePosition()
    {
        Machete.transform.position = InstancePosition;
        Machete.transform.eulerAngles = InstanceRotation;
    }

    public void ThrowUpMachete()
    {
        Machete.transform.parent = null;
        //DOTween anim
        //Machete.GetComponent<Collider2D>().isTrigger = false;
        rb.gravityScale = 1;
        rb.AddForce(Vector2.up * ThrowingUpSpeed, ForceMode2D.Impulse);
        rb.AddTorque(RotationSpeed, ForceMode2D.Impulse);
        //StartCoroutine(ThrowUpUpdate());
        //if(Machete.transform.eulerAngles.z < 330)
        {
            /*var Seq = DOTween.Sequence();
            Machete.transform.DOMove(HigherPosition + new Vector3(0, 0.01f, 0), ThrowingUpSpeed, false);
            Seq.Join(Machete.transform.DORotate(new Vector3(0,0,90), ThrowingUpSpeed, RotateMode.Fast));*/
            
        }
    }
    
    private IEnumerator ThrowUpUpdate()
    {
        //while(Rotations < MaxRotationNumber/*Machete.transform.position.y < HigherPosition.y*/)
        {
            MacheteRotation();
            while(Machete.transform.position.y >= HigherPosition.y || Rotations >= MaxRotationNumber)
            //while(Rotations >= MaxRotationNumber)
            {
                rb.freezeRotation = true;
                rb.AddForce(Vector2.down, ForceMode2D.Force);

                yield return null;
            }
            //Machete.transform.position = new Vector3(Machete.transform.position.x, Machete.transform.position.y + ThrowingUpSpeed * Time.deltaTime, Machete.transform.position.z);
            //if(Machete.transform.eulerAngles.z != 170)
            {
              //  Machete.transform.rotation = Quaternion.Lerp(Machete.transform.rotation, HigherRotation, Time.deltaTime * (2f));
            }
            //rb.AddForce(Vector2.up * 1f);
            //Machete.transform.rotation *= Quaternion.AngleAxis(RotationSpeed, Vector3.forward);
            //MacheteRotation();

            
        }
    }

    public void MacheteRotation()
    {
        //StartAngle = Machete.transform.eulerAngles.z;
        //Debug.Log("StartAngle " + StartAngle);
        StartCoroutine(RotateMachete());
    }

    public IEnumerator RotateMachete()
    {
        //if(Machete.transform.localPosition == HigherPosition)
        while(Rotations < MaxRotationNumber && TapNumber == 1)
        {
            //Debug.Log(Math.Round(Machete.transform.position.y, 1) + " " + HigherPosition.y);
            //if (Machete.transform.position.y >= HigherPosition.y)
            {
                //Debug.Log(Machete.transform.eulerAngles.z);
                float lessTargetAngle = Machete.transform.eulerAngles.z;
                 
                //Machete.transform.rotation *= Quaternion.AngleAxis(RotationSpeed, Vector3.forward);
                //Machete.transform.DORotate(Vector3.forward, 1);
                //Machete.transform.eulerAngles += Vector3.forward;
                //Debug.Log("CurrentAngle" + Machete.transform.eulerAngles.z);
                float moreTargetAngle = Machete.transform.eulerAngles.z + 10;

                if(lessTargetAngle <= StartAngle && moreTargetAngle >= StartAngle /*&& Machete.transform.position.y >= HigherPosition.y*/)
                {
                    Rotations += 1;
                }
            }
            yield return null;
        }
    }


    public void FallDownMachete(float Force)
    {
        isMacheteThrown = true;
        rb.gravityScale = 1;
        //rb.AddForce(new Vector2(0, -1)*Force);
    }
    
    /*private void OnCollisionEnter2D(Collision2D collide) {
        if(collide.gameObject.layer == 11)
        {
            Machete.GetComponent<Collider2D>().isTrigger = true;
        }
    }*/

    public void SwitchDots()
    {
        if(TapNumber == 0 || TapNumber == 2)
        {
            Dots.SetActive(false);
        }
        else if(TapNumber == 1)
        {
            Dots.SetActive(true);
        }

    }
}
