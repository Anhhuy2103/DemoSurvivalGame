using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUser : MonoBehaviour
{
    public static WeaponUser Instance { get; set; }
  
    [SerializeField] private Transform gunSpawnPoint;
   
    [Header("Gun_Prefab")]
    [SerializeField] private GameObject RiffleGun_Prefab_1; 
    [SerializeField] private GameObject GrenadeGun_Prefab_1; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // drop the gun
    public void InstantiateGun_1()
    {
        GameObject Gun = Instantiate(RiffleGun_Prefab_1, gunSpawnPoint.position, Quaternion.identity);


        Gun.GetComponent<Guns>().isActiveWeapon = false;
        Gun.GetComponent<Guns>().animator.enabled = false;

        Gun.transform.SetParent(null);
        Rigidbody rb = Gun.GetComponent<Rigidbody>();

        rb.AddForce(Camera.main.transform.forward * 15, ForceMode.Impulse);
        rb.AddForce(Camera.main.transform.up * 7, ForceMode.Impulse);
        rb.isKinematic = false;
    }

    public void InstantiateGun_2()
    {
        GameObject G_Gun = Instantiate(GrenadeGun_Prefab_1, gunSpawnPoint.position, Quaternion.identity);


        G_Gun.GetComponent<Guns>().isActiveWeapon = false;
        G_Gun.GetComponent<Guns>().animator.enabled = false;

        G_Gun.transform.SetParent(null);
        Rigidbody rb = G_Gun.GetComponent<Rigidbody>();

        rb.AddForce(Camera.main.transform.forward * 15, ForceMode.Impulse);
        rb.AddForce(Camera.main.transform.up * 7, ForceMode.Impulse);
        rb.isKinematic = false;
    }

}
