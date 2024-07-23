using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Guns;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }

    public GameObject dropPoint;


    [Header("--AMMO--")]
    public int totalRifleAmmo = 0;
    public int totalGrenadeAmmo = 0;

    [Header("--Throwables General--")]
    public float throwForce = 20f;
    public float DropupForce = 20f;
    public float DropDownForce = 20f;
    public GameObject throwableSpawn;
    public float forceMultiplier = 0;
    public float forceMultiplierLimit = 2f;

    [Header("--Lethals--")]
    public int lethalsCount = 0;
    public int maxLethals = 10;
    public ThrowAble.ThrowableType equippedLethalType;
    public GameObject grenadePrefab_1;


    [Header("--Taticals--")]
    public int tacticalsCount = 0;
    public int maxTacticals = 10;
    public ThrowAble.ThrowableType equippedTaticalsType;
    public GameObject smokeboomGrenade;



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


    private void Start()
    {
        equippedLethalType = ThrowAble.ThrowableType.None;
        equippedTaticalsType = ThrowAble.ThrowableType.None;
    }

    private void Update()
    {

        // --Throwable--

        if (Input.GetKey(KeyCode.G) || Input.GetKey(KeyCode.T))
        {
            forceMultiplier += Time.deltaTime;

            if (forceMultiplier > forceMultiplierLimit)
            {
                forceMultiplier = forceMultiplierLimit;
            }
        }

        if (Input.GetKeyUp(KeyCode.G) && InventorySystem.Instance.isInventoryOpen == false 
            && IngameMenuManager.Instance.isActiveMenuPanel == false 
            && PlayerStatusManager.Instance.isDead == false && 
            !CraftingManager.Instance.IsCraftOpen
            && !ShopManager.Instance.isShopOpen)
        {
            if (lethalsCount > 0)
            {
                ThrowLethal();
            }

            forceMultiplier = 0;
        }

        if (Input.GetKeyUp(KeyCode.T) && InventorySystem.Instance.isInventoryOpen == false 
            && IngameMenuManager.Instance.isActiveMenuPanel == false 
            && PlayerStatusManager.Instance.isDead == false
            && !CraftingManager.Instance.IsCraftOpen
            && !ShopManager.Instance.isShopOpen)
        {
            if (tacticalsCount > 0)
            {
                ThrowTacticals();
            }

            forceMultiplier = 0;
        }
    }
    // --------------- AMMO CASE += PICKUP --------------------
    internal void PickUpAmmoBox(AmmoBox ammo)
    {
        switch (ammo.ammoType)
        {
            case AmmoBox.AmmoType.RiffleAmmo:
                totalRifleAmmo += ammo.ammoAmount;
                break;

            case AmmoBox.AmmoType.GrenadeAmmo:
                totalGrenadeAmmo += ammo.ammoAmount;
                break;
        }
    }

    internal void DecreaseTotalAmmo(int bulletToDecrease, Guns.WeaponModel thisWeaponModel)
    {
        switch (thisWeaponModel)
        {
            case Guns.WeaponModel.Akai_max:
                totalGrenadeAmmo -= bulletToDecrease;
                break;

            case Guns.WeaponModel.Sci_max:
                totalRifleAmmo -= bulletToDecrease;
                break;
        }
    }

    // ----------- look: CheckAmmoLeftFor => use to check ammo from inventory to reload ----- 
    public int CheckAmmoLeftFor(Guns.WeaponModel thisWeaponModel)
    {
        switch (thisWeaponModel)
        {
            case Guns.WeaponModel.Akai_max:
                return totalGrenadeAmmo;


            case Guns.WeaponModel.Sci_max:
                return totalRifleAmmo;

            default:
                return 0;


        }
    }


    // --------------------------------------THROWABLE----------------------------------------

    #region || ---- Throwable --- ||
    public void PickUpThrowable(ThrowAble throwable)
    {
        switch (throwable.throwabletype)
        {
            case ThrowAble.ThrowableType.Grenade:

                PickupThrowableAsLethal(ThrowAble.ThrowableType.Grenade);
                break;

            case ThrowAble.ThrowableType.Smoke:

                PickupThrowableAsTatical(ThrowAble.ThrowableType.Smoke);
                break;
        }
    }

    private void PickupThrowableAsTatical(ThrowAble.ThrowableType tactical)
    {
        if (equippedTaticalsType == tactical || equippedTaticalsType == ThrowAble.ThrowableType.None)
        {
            equippedTaticalsType = tactical;

            if (tacticalsCount < maxTacticals)
            {
                tacticalsCount += 1;
                Destroy(InteractionManager.Instance.hoveredThrowable.gameObject);
                HUBManager.Instance.UpdateThrowables();

            }
            else
            {
                print("Tacticals Limit Reach");
            }
        }

        else
        {
            //cannotpickup diffirent tacticals
            // Option to Swap tacticals
        }
    }

    private void PickupThrowableAsLethal(ThrowAble.ThrowableType lethal)
    {
        if (equippedLethalType == lethal || equippedLethalType == ThrowAble.ThrowableType.None)
        {
            equippedLethalType = lethal;

            if (lethalsCount < maxLethals)
            {
                lethalsCount += 1;
                Destroy(InteractionManager.Instance.hoveredThrowable.gameObject);
                HUBManager.Instance.UpdateThrowables();

            }
            else
            {
                print("Lethal Limit Reach");
            }
        }

        else
        {
            //cannotpickup
        }
    }


    private void ThrowLethal()
    {
        GameObject lethalPrefab = GetThrowablePrefab(equippedLethalType);

        GameObject throwable = Instantiate(lethalPrefab, throwableSpawn.transform.position, Camera.main.transform.rotation);
        Rigidbody rb = throwable.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * (throwForce * forceMultiplier), ForceMode.Impulse);

        throwable.GetComponent<ThrowAble>().hasbeenThrown = true;

        lethalsCount -= 1;

        if (lethalsCount <= 0)
        {
            equippedLethalType = ThrowAble.ThrowableType.None;
        }



        HUBManager.Instance.UpdateThrowables();
    }

    private void ThrowTacticals()
    {

        GameObject tacticalsPrefab = GetThrowablePrefab(equippedTaticalsType);

        GameObject throwable = Instantiate(tacticalsPrefab, throwableSpawn.transform.position, Camera.main.transform.rotation);
        Rigidbody rb = throwable.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * (throwForce * forceMultiplier), ForceMode.Impulse);

        throwable.GetComponent<ThrowAble>().hasbeenThrown = true;

        tacticalsCount -= 1;

        if (tacticalsCount <= 0)
        {
            equippedTaticalsType = ThrowAble.ThrowableType.None;
        }



        HUBManager.Instance.UpdateThrowables();

    }

    private GameObject GetThrowablePrefab(ThrowAble.ThrowableType throwableType)
    {
        switch (throwableType)
        {
            case ThrowAble.ThrowableType.Grenade:
                return grenadePrefab_1;
            case ThrowAble.ThrowableType.Smoke:
                return smokeboomGrenade;
        }
        return new();
    }
    #endregion
}
