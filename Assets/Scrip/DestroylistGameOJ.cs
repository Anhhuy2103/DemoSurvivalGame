using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroylistGameOJ : MonoBehaviour
{
    [SerializeField] private GameObject mainGameOJ;
    [SerializeField] private List<GameObject> listGamesob;

    
    private void Update()
    {                    
        if (mainGameOJ.IsDestroyed())
        {
            foreach(GameObject gameObject in listGamesob)
            {
                mainGameOJ = null;
                Destroy(gameObject,1f);
                
            }
        }
    }
}
