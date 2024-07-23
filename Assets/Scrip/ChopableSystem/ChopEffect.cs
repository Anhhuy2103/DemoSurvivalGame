using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ChopEffect : MonoBehaviour
{

    private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Resources"))
        {

            Instantiate(GlobalReferences.Instance.Chop_WoodStoneEffect_Prefab, gameObject.transform.position, Quaternion.identity);
           
        }
        if (objectWeHit.gameObject.CompareTag("Creep"))
        {
            if (objectWeHit.gameObject.GetComponentInParent<EnemyCreep>().isDead == false)
            {
                objectWeHit.gameObject.GetComponentInParent<EnemyCreep>().takedameForEnemy(20,50);
            }
            ChopEffect_Resources(objectWeHit);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    //------- Creat Effect --------

    void ChopEffect_Resources(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.Chop_WoodStoneEffect_Prefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
