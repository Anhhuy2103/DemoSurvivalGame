using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUBManager : MonoBehaviour
{
    public static HUBManager Instance { get; set; }

    [Header("Ammmo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountAUI;

    public Sprite emtySlot;
    public Sprite greySlot;
    public GameObject middleDot;

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

    private void Update()
    {
        Guns activeWeapon = EquipManager.Instance.toolHolder.GetComponentInChildren<Guns>();


        if (activeWeapon)
        {
            if (activeWeapon.thisWeaponModel == Guns.WeaponModel.Sci_max)
            {
                 TempBulletsLerftManager.Instance.SCI_bullets_afterDestroy = activeWeapon.bulletsLeft ;
            }
            if (activeWeapon.thisWeaponModel == Guns.WeaponModel.Akai_max)
            {
                TempBulletsLerftManager.Instance.Grenade_bullets_afterDestroy = activeWeapon.bulletsLeft;
            }
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeftFor(activeWeapon.thisWeaponModel)}";

            Guns.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(model);



        }

        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";

            ammoTypeUI.sprite = emtySlot;

        }


        if (WeaponManager.Instance.lethalsCount <= 0)
        {
            lethalUI.sprite = greySlot;
        }

        if (WeaponManager.Instance.tacticalsCount <= 0)
        {
            tacticalUI.sprite = greySlot;
        }
    }

    private Sprite GetAmmoSprite(Guns.WeaponModel model)
    {
        switch (model)
        {
            case Guns.WeaponModel.Sci_max:
                return Resources.Load<GameObject>("Riffle_Ammo").GetComponent<SpriteRenderer>().sprite;

            case Guns.WeaponModel.Akai_max:
                return Resources.Load<GameObject>("Grenade_Ammo").GetComponent<SpriteRenderer>().sprite;

            default:
                return null;
        }
    }


    internal void UpdateThrowables()
    {
        lethalAmountUI.text = $"{WeaponManager.Instance.lethalsCount}";
        tacticalAmountAUI.text = $"{WeaponManager.Instance.tacticalsCount}";

        //---- Lethals ----
        switch (WeaponManager.Instance.equippedLethalType)
        {
            case ThrowAble.ThrowableType.Grenade:
                lethalUI.sprite = Resources.Load<GameObject>("MotoCocktail").GetComponent<SpriteRenderer>().sprite;
                break;

        }
        //---- Tacticals ----
        switch (WeaponManager.Instance.equippedTaticalsType)
        {
            case ThrowAble.ThrowableType.Smoke:
                tacticalUI.sprite = Resources.Load<GameObject>("Smoke_Boom").GetComponent<SpriteRenderer>().sprite;
                break;
        }
    }
}
