using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Guns;

public class SciRiffle_Bullet : MonoBehaviour
{
    public int minDame,maxDame;

    private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Resources"))
        {
            
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit The Wall");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("Ground"))
        {
            print("hit The Wall");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }

        //Hit enemy => Zombie in this case;
        //if (objectWeHit.gameObject.CompareTag("Zombie"))
        //{
        //    print("hit The Zombie");
        //    if (objectWeHit.gameObject.GetComponentInParent<Enemy>().isDead == false)
        //    {
        //        objectWeHit.gameObject.GetComponentInParent<Enemy>().takedameForEnemy(bulletDame);
        //    }
        //    CreateBulletBloodEffect(objectWeHit);
        //    Destroy(gameObject);
        //}

        // Hit enemy => Rock in this case;
        if (objectWeHit.gameObject.CompareTag("Rock"))
        {
            print("hit The RockGolem");
            if (objectWeHit.gameObject.GetComponentInParent<Enemy>().isDead == false)
            {
                objectWeHit.gameObject.GetComponentInParent<Enemy>().takedameForEnemy(minDame,maxDame);
            }
            CreateBulletImpactEffect(objectWeHit);
           
            Destroy(gameObject);
        }

        // Hit enemy => Creep in this case;
        if (objectWeHit.gameObject.CompareTag("Creep"))
        {
            if (objectWeHit.gameObject.GetComponentInParent<EnemyCreep>().isDead == false)
            {
                objectWeHit.gameObject.GetComponentInParent<EnemyCreep>().takedameForEnemy(minDame,maxDame);
            }
            CreateBulletImpactEffect(objectWeHit);

            Destroy(gameObject);
        }
    }
    //------- Creat Effect --------

    void CreateBulletImpactEffect(Collision objectWeHit)
    {      
        ContactPoint contact = objectWeHit.contacts[0];  
        GameObject hole = Instantiate(
            GlobalReferences.Instance.StonebulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );
       
        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }

    void CreateBulletBloodEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.BloodEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
