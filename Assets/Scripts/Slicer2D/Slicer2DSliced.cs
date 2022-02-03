using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer2DSliced : MonoBehaviour
{
    public List<GameObject> SlicedParts;
    public Transform AliveBody;
    public byte AliveBodyObjects;

    private void Update() {
        if(SlicedParts.Count >= 2)
        {
            AliveBodyObjects = (byte)AliveBody.childCount;
                        
            for(byte i = 0; i < AliveBodyObjects; i++)
            {
                float dist1 = Vector2.Distance(AliveBody.GetChild(i).GetComponent<Collider2D>().bounds.center, SlicedParts[0].GetComponent<Collider2D>().bounds.center);
                float dist2 = Vector2.Distance(AliveBody.GetChild(i).GetComponent<Collider2D>().bounds.center, SlicedParts[1].GetComponent<Collider2D>().bounds.center);
                
                //if(Mathf.Abs(dist1 - dist2) > 0.3f)
                if(dist1 > dist2)
                {
                    AliveBody.GetChild(i).parent = null;
                    AliveBody.GetChild(i).SetParent(SlicedParts[0].transform.parent);
                }
                else
                {
                    AliveBody.GetChild(i).parent = null;
                    AliveBody.GetChild(i).SetParent(SlicedParts[1].transform.parent);
                }
            }
            //SlicedParts[0].transform.parent.GetComponent<Rigidbody2D>().gravityScale = 1;
            //SlicedParts[1].transform.parent.GetComponent<Rigidbody2D>().gravityScale = 1;
            SlicedParts.Clear();
        }
        
        
    }

    
}
