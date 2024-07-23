using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameByTime : MonoBehaviour
{
    public float tickTime;
    public int SmokeMinDame = 10;
    public int SmokeMaxDame = 50;
    

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            tickTime -= Time.deltaTime;

            if (tickTime < 0 && GetComponent<Enemy>().isDead == false)
            {
                tickTime = 2.0f;
                GetComponent<Enemy>().takedameForEnemy(SmokeMinDame,SmokeMaxDame);
            }
        }
        if (collision.gameObject.tag == "Creep")
        {
            tickTime -= Time.deltaTime;

            if (tickTime < 0 && GetComponent<Enemy>().isDead == false)
            {
                tickTime = 2.0f;
                GetComponent<Enemy>().takedameForEnemy(SmokeMinDame, SmokeMaxDame);
            }
        }
    }
}
