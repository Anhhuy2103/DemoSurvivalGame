using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesHealthBar : MonoBehaviour
{
    [Header("Infomation")]
    private Slider healthSlider;
    private int currentHealth;
    private int MaxHealth;

    [Header("Global")]
    public GameObject GlobalState;


    
    private void Start()
    {
        healthSlider = GetComponent<Slider>();
    }
    private void Update()
    {
        inIt();
    }
    private void inIt()
    {
        GameObject seletedTree = InteractionManager.Instance.HoveredSeletedTree;
        if(seletedTree != null)
        {
            currentHealth = GlobalState.GetComponent<GlobalReferences>().TreeCurrentHP;
            MaxHealth = GlobalState.GetComponent<GlobalReferences>().TreeMaxHP;
        }
        // ---  stone --- 

        GameObject selectedStone = InteractionManager.Instance.HoveredSeletedStone;
        if(selectedStone != null)
        {
            currentHealth = GlobalState.GetComponent<GlobalReferences>().StoneHP;
            MaxHealth = GlobalState.GetComponent<GlobalReferences>().StoneMaxHP;
        }
     
        healthSlider.maxValue = MaxHealth;
        healthSlider.value = currentHealth;
    }
}
