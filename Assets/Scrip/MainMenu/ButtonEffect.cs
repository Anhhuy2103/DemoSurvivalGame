using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMainMenu : MonoBehaviour
{  
    public void Play_PointerSFX()
    {
       SoundMainMenu.Instance.Play_PointerSFX();
    }
    public void Play_PressSFX()
    {
        SoundMainMenu.Instance.Play_PressSFX();
    }
}
