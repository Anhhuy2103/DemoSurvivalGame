
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMap : MonoBehaviour
{
    public Transform player;
    


    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y =this.gameObject.transform.position.y;
        this.gameObject.transform.position = newPos;

       
    }
}
