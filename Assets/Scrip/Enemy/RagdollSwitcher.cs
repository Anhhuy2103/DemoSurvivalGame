using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSwitcher : MonoBehaviour
{
    public Rigidbody[] rigids;

    [ContextMenu("Retrieve Rigidbodies")]
    private void RetrieveBodies()
    {
        rigids = GetComponentsInChildren<Rigidbody>();
    }
}
