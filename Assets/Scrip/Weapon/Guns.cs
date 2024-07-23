using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Guns : MonoBehaviour
{  


    // 
    internal Animator animator;
   
    // Outline Weapon
    public bool isActiveWeapon;
    public int minDame, maxDame;
    

    [Header("SHOOTING")]
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;
    public bool isADS;

    [Header("BURST")]
    public int bulletsPerBurst = 3;
    public int BurstBulletLeft;

    [Header("SPREAD")]
    //spread
    public float spreadIntensity;
    public float hipSpreadIntensity; // More than ADS please;
    public float adsSpreadIntensity;

    [Header("LOADING")]
    // Loading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;


    //bullet
    [Header("Weapon")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;

    // Spawn Position Weapon
    [Header("Spawn Position Gun")]
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

  
    


    public enum WeaponModel
    {
        Sci_max,
        Akai_max
    }
   
    public WeaponModel thisWeaponModel;
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        BurstBulletLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
        

        bulletsLeft = magazineSize;
        spreadIntensity = hipSpreadIntensity;
    }

    private void Update()
    {
        // CheckAnimatorWhenPlayerDead();
        if (isActiveWeapon
            && GlobalReferences.Instance.isActiveWeaponbro == true
            && InventorySystem.Instance.isInventoryOpen == false 
            && IngameMenuManager.Instance.isActiveMenuPanel == false && PlayerStatusManager.Instance.isDead ==false
            && !CraftingManager.Instance.IsCraftOpen && !ShopManager.Instance.isShopOpen
            && !QuestManager.Instance.isQuestMenuOpen)
        {
            // Layer weapon khong bi che
            foreach(Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("WeaponRender");
            }

            if (Input.GetMouseButtonDown(1))
            {
               EnterADS();
            }

            if (Input.GetMouseButtonUp(1))
            {
                ExitADS();
            }

           

            if (bulletsLeft <= 0 && isShooting)
            {
                this.currentShootingMode = ShootingMode.Single;
                SoundManager.Instance.PlaySciBlueSound_Empty();
            }
            if (bulletsLeft > 0 && isShooting)
            {
                this.currentShootingMode = ShootingMode.Auto;
            }

            if (currentShootingMode == ShootingMode.Auto)
            {
                // Holding to shoot
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single ||
                currentShootingMode == ShootingMode.Burst)
            {
                // clicking to shoot

                isShooting = Input.GetKeyDown(KeyCode.Mouse0);

            }
            // ----------- look: CheckAmmoLeftFor => use to check ammo from inventory to reload ----- 
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false && WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > 0) 
            {
                Reload();
                
            }
            //--------//
            if (!isReloading && readyToShoot && isShooting && bulletsLeft > 0)
            {
                BurstBulletLeft = bulletsPerBurst;
                FireWeapon();
            }

            //-----
           
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }

    private void EnterADS()
    {
        animator.SetTrigger("enterADS");
        isADS = true;
       // HUBManager.Instance.middleDot.SetActive(false);
        spreadIntensity = adsSpreadIntensity;
    }
    private void ExitADS()
    {
        animator.SetTrigger("exitADS");
        isADS = false;
       // HUBManager.Instance.middleDot.SetActive(true);
        spreadIntensity = hipSpreadIntensity;
    }

    private void FireWeapon()
    {
        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();


        if (isADS)
        {
            animator.SetTrigger("SHOOTADS");
        }
        else
        {
            animator.SetTrigger("SHOOT");
        }
       
       

        //  SoundManager.Instance.PlaySciBlueSound_Shooting();

        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;
        Vector3 shootingDirection = CalcuteDirectionAndSpread().normalized;

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        
        // Phan Chia Chung Loai Dame cua Bullet tuy theo loai sung.
        if(thisWeaponModel == WeaponModel.Sci_max) 
        {
           SciRiffle_Bullet sciRiffle = bullet.GetComponent<SciRiffle_Bullet>();
            sciRiffle.minDame = minDame;
            sciRiffle.maxDame = maxDame;
        }

        if(thisWeaponModel == WeaponModel.Akai_max)
        {
            GrenadeBullet grenadeBullet = bullet.GetComponent<GrenadeBullet>();
            grenadeBullet.minDame = minDame;
            grenadeBullet.maxDame = maxDame;
        }

        

        // Pointing  the bullet to face  the shooting direction
        bullet.transform.forward = shootingDirection;
        
        // Shoot Bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity,ForceMode.Impulse);

        // Destroy Bullet
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        // Check if done of shooting
        if(allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Burst Mode
        if( currentShootingMode == ShootingMode.Burst && BurstBulletLeft > 1)
        {
            BurstBulletLeft--;
            Invoke("FireWeapon", shootingDelay);
        }

      
    }


    private void Reload()
    {
        animator.SetTrigger("RELOAD");
        SoundManager.Instance.PlaySciBlueSound_Reloading();
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        if (WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > magazineSize)
        {
            bulletsLeft = magazineSize;
            WeaponManager.Instance.DecreaseTotalAmmo(bulletsLeft, thisWeaponModel);
        }
        else
        {
            bulletsLeft = WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel);
            WeaponManager.Instance.DecreaseTotalAmmo(bulletsLeft, thisWeaponModel);
        }
       
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalcuteDirectionAndSpread()
    {

        // Shooting  from the middle scene  to check where we are poiting
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            // Hitting Something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting at the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(0,y,z); 

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }


    // ------------- destroy when spawn to much -----------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Gasbage"))
        {
            Destroy(this.gameObject);
        }
    }
   

    //private void CheckAnimatorWhenPlayerDead()
    //{
    //    if (PlayerStatus.Instance.isDead)
    //    {
    //        animator.enabled = false;
    //    }
    //    else
    //    {
    //        animator.enabled = true;
    //    }
    //}
}
