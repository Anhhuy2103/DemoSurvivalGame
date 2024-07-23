using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainmenuAnim : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

   public void playIntroClick()
    {
        animator.SetTrigger("isPress");
    }
}
