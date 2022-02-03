using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialHand : MonoBehaviour
{
    [Header("Hand Params")]
    public RectTransform Hand;
    public Quaternion StartAngle;
    public Quaternion FinishAngle;
    [Header("Circle Params")]
    public RectTransform Circle;
    public Vector2 MinScale;
    public Vector2 MaxScale;

    private Quaternion TargetRot;

    private void OnEnable() 
    {
       
       //Circle.localScale = MinScale;
       //CircleScaleChange();
      // RotateHand();
    }
    
    /*private void Update() {
        RotateHand();
    }*/

    private void CircleScaleChange()
    {
        StartCoroutine(CircleScale());
    }

    private IEnumerator CircleScale()
    {
        
        while(Circle.localScale.y < MaxScale.y && Circle.localScale.y > 0)
        {
            Circle.localScale += new Vector3(1, 1) * Time.deltaTime;
            /*if(Circle.localScale.y >= MaxScale.y)
            {
                Circle.localScale = Vector2.zero;
            }*/
            if(Hand.eulerAngles.z >= 0)
            {
                Circle.localScale = MinScale;
            }
            
            yield return null;
        }
    }

    private void RotateHand()
    {
        if(Hand.rotation.eulerAngles.z >= FinishAngle.z)
            {
                TargetRot = StartAngle;
            }
            else if(Hand.rotation.eulerAngles.z <= StartAngle.z)
            {
                TargetRot = FinishAngle;
            }
        StartCoroutine(HandRotate());
    }

    private IEnumerator HandRotate()
    {
        while(Hand.eulerAngles.z < FinishAngle.eulerAngles.z)
        {
            
            Hand.eulerAngles += new Vector3(0, 0, Hand.eulerAngles.z+1) * Time.deltaTime * 5; //= Quaternion.Euler(0, 0, Hand.rotation.z + 2*Time.deltaTime);
            /*if(Hand.rotation.eulerAngles.z >= FinishAngle.z)
            {
                Hand.rotation = StartAngle;
            }*/
            
            yield return null;
        }
    }


}
