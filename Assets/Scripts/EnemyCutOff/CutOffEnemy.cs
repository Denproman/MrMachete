using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOffEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.layer == 9)
        {
            MacheteControllerPlayer playerMachete = collider.gameObject.GetComponent<MacheteControllerPlayer>();
            playerMachete.EnemyBodyParts.Add(this.GetComponent<Collider2D>());
        }
    }
}
