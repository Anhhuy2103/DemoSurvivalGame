using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    public int minDame = 30 , maxDame = 100;  
    public float explosionRadius;
    public float explosionForce; 
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            BlowObject_MultiDameged();
            CreatePlasmaExplosionImpactEffect(objectWeHit);          
            SoundManager.Instance.PlayExplosionBullet();
            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
          
            CreatePlasmaExplosionImpactEffect(objectWeHit);            
            SoundManager.Instance.PlayExplosionBullet();
            Destroy(gameObject);
        }
        if (objectWeHit.gameObject.CompareTag("Ground"))
        {

            CreatePlasmaExplosionImpactEffect(objectWeHit);
            SoundManager.Instance.PlayExplosionBullet();
            Destroy(gameObject);
        }

        //Hit enemy => Zombie in this case; => bleeding blood

        //if (objectWeHit.gameObject.CompareTag("Zombie"))
        //{
        //    if (objectWeHit.gameObject.GetComponent<Enemy>().isDead == false)
        //    {
        //        BlowObject_MultiDameged();
        //    }
        //    CreatePlasmaExplosionImpactEffect(objectWeHit);       
        //    SoundManager.Instance.PlayExplosionBullet();
        //    Destroy(gameObject);
        //}

        // Hit enemy => Rock in this case;
        if (objectWeHit.gameObject.CompareTag("Rock"))
        {
            if (objectWeHit.gameObject.GetComponentInParent<Enemy>().isDead == false)
            {
                objectWeHit.gameObject.GetComponentInParent<Enemy>().takedameForEnemy(minDame, maxDame);
                BlowObject_MultiDameged();
            }
            CreatePlasmaExplosionImpactEffect(objectWeHit);          
            SoundManager.Instance.PlayExplosionBullet();
            Destroy(gameObject);
        }

        // Hit enemy => Creep in this case;
        if (objectWeHit.gameObject.CompareTag("Creep"))
        {
            if (objectWeHit.gameObject.GetComponentInParent<EnemyCreep>().isDead == false)
            {
                objectWeHit.gameObject.GetComponentInParent<EnemyCreep>().takedameForEnemy(minDame, maxDame);
                BlowObject_MultiDameged();
            }
            CreatePlasmaExplosionImpactEffect(objectWeHit);
            SoundManager.Instance.PlayExplosionBullet();
            Destroy(gameObject);
        }
    }

    //--explosion bullet --
    private void BlowObject_MultiDameged()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        for(int i =0; i< affectedObjects.Length; i++)
        {          
            AddForceToObject(affectedObjects[i]);
           
        }

    }

    private void AddForceToObject(Collider affectedObjects)
    {
        Rigidbody rigidbody = affectedObjects.attachedRigidbody;
        if (rigidbody)
        {
            rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1,
                ForceMode.Impulse);
        }
    }

    
    //--------------------------------------------------------------------------------------++
    //-- Bullet Effect --
    void CreatePlasmaExplosionImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.Grenade_PlasmaExplosion_bulletImpactEffectPrefab,
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
