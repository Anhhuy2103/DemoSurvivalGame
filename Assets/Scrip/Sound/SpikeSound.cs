using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSound : FatherSound
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemySource.PlayOneShot(SpikemWalk_clip);
            isTriggerPlayer = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        tickTime -= Time.deltaTime;
        if (tickTime < 0)
        {
            EnemySource.PlayOneShot(SpikemWalk_clip);
            tickTime = 50f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemySource.Stop();
            isTriggerPlayer = false;
        }
    }
}
