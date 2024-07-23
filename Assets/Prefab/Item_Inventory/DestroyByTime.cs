using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] private float tickTime = 60f; // 3
    [SerializeField] private float timeDelay; // 0




    private void delay()
    {
        this.tickTime -= Time.deltaTime;
        GlobalReferences.Instance.tickTime = this.tickTime;
        if (this.tickTime < this.timeDelay)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            delay();
        }
    }
}
