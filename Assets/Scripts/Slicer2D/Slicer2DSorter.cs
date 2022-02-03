using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer2DSorter : MonoBehaviour
{
    public List<GameObject> GroupParent;
    public GameObject parentObj;
    public GameController gameController;
    public Transform FirstSliceGroup;
    public Transform SecondSliceGroup;
    /*private void OnTriggerEnter2D(Collider2D collision) 
    {
        for(int i = 0; i < parentObj.transform.childCount; i++)
        {
			if(collision != parentObj.transform.GetChild(i).GetComponent<Collider2D>())
            {
			Destroy(parentObj.transform.GetChild(i).gameObject);
            }
		}
    }*/
    
    private void OnEnable() {
        gameController = GetComponentInParent<GameController>();
    }

    void Update()
    {
        for(int i = 0; i < parentObj.transform.childCount; i++)
        {
            Vector2 colCenter = parentObj.transform.GetChild(i).GetComponent<Collider2D>().bounds.center;
            if(/*(parentObj.transform.GetChild(i).position.x > gameController.StartWeaponThrowPoint.x
            || parentObj.transform.GetChild(i).position.y > gameController.StartWeaponThrowPoint.y)
            &&*/ //(parentObj.transform.GetChild(i).position.x < gameController.FinishWeaponThrowPoint.x
            //|| 
            colCenter.y < gameController.FinishWeaponThrowPoint.y)
            {
                //parentObj.transform.GetChild(i).SetParent(FirstSliceGroup);
            }
            else if(/*(parentObj.transform.GetChild(i).position.x < gameController.StartWeaponThrowPoint.x
                || parentObj.transform.GetChild(i).position.y < gameController.StartWeaponThrowPoint.y)
                &&*/ //(parentObj.transform.GetChild(i).position.x > gameController.FinishWeaponThrowPoint.x
                //|| 
                colCenter.y > gameController.FinishWeaponThrowPoint.y)
            {
                //parentObj.transform.GetChild(i).SetParent(SecondSliceGroup);
            }
            
        }
        if(FirstSliceGroup.childCount > 0 && SecondSliceGroup.childCount > 0)
            {
                //FirstSliceGroup.GetComponent<Rigidbody2D>().gravityScale = 1;
                //SecondSliceGroup.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        
    }

    
}
