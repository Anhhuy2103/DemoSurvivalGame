using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
    public int lifeTime = 5;

    private void Start()
    {
        Destroy(gameObject, lifeTime); 
    }
}
