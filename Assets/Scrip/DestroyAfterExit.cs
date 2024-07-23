using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyAfterExit : MonoBehaviour
{
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject Coin;

    
    private void Update()
    {
        CheckIfCoinIsGone();
    }
    private void CheckIfCoinIsGone()
    {
        if (Coin.IsDestroyed())
        {
            Coin = null;          
            Destroy(chest, 5f);
        }
    }

   

}
