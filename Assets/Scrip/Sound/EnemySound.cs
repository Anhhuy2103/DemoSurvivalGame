using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private SphereCollider playColider;
    [SerializeField] private AudioSource EnemySource;
    [SerializeField] private AudioClip SpikemWalk_clip;
   
    [SerializeField] private float tickTime;
   
    
   
    private void OnTriggerEnter(Collider other)
    {
     

        if (other.CompareTag("Player"))
        {
            
            EnemySource.PlayOneShot(SpikemWalk_clip);
                        
        }
    }

    private void OnTriggerStay(Collider other)
    {
        tickTime -= Time.deltaTime;
        if (tickTime < 0 )
        {
            EnemySource.PlayOneShot(SpikemWalk_clip);
            tickTime = 5.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemySource.Stop();            
            
        }
    }
}
