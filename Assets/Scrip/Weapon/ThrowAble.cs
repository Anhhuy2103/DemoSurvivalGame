using UnityEngine;

public class ThrowAble : MonoBehaviour
{
    [SerializeField] private float delay = 2f;   
    [SerializeField] private float DamgedRadius = 5f;
    [SerializeField] private float explosionForce = 200f;
    [SerializeField] private int grenadeMinDame = 50;
    [SerializeField] private int grenadeMaxDame = 100;

   
    float countdown;

    bool hasExploded = false;
    public bool hasbeenThrown = false;

    public enum ThrowableType
    {
        None,
        Grenade,
        Smoke,
        knife
    }

    public ThrowableType throwabletype;

    private void Start()
    {
        countdown = delay;
        
    }

    private void Update()
    {

        if (hasbeenThrown)
        {
           
            countdown -= Time.deltaTime;
            if (countdown <= 0f && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    private void Explode()
    {
        GetThrowableEffect();
        Destroy(gameObject);
    }

    private void GetThrowableEffect()
    {
        switch (throwabletype)
        {
            case ThrowableType.Grenade:
                GrenadeEffect();
                break;

            case ThrowableType.Smoke:
                SmokeEffect();
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            SoundManager.Instance.PlayThrowSomeThing();
        }
    }
    private void GrenadeEffect()
    {
        // Visual Effect
        GameObject explosionEffect = GlobalReferences.Instance.grenadeExplosionEffect;
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // PlaySound
        SoundManager.Instance.PlayGrenadeExplosion();

        // Physical Effect
        Collider[] collidersEffect = Physics.OverlapSphere(transform.position, DamgedRadius);
        foreach (Collider objectInRange in collidersEffect)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, DamgedRadius);
            }

            // ----- EnemyBig ----
            if (objectInRange.gameObject.GetComponent<Enemy>())
            {
                if (objectInRange.gameObject.GetComponent<Enemy>().isDead == false)
                {
                    objectInRange.gameObject.GetComponent<Enemy>().takedameForEnemy(grenadeMinDame,grenadeMaxDame);
                }
            }

            //----- EnemyCrrep -----
            if (objectInRange.gameObject.GetComponent<EnemyCreep>())
            {
                if (objectInRange.gameObject.GetComponent<EnemyCreep>().isDead == false)
                {
                    objectInRange.gameObject.GetComponent<EnemyCreep>().takedameForEnemy(grenadeMinDame, grenadeMaxDame);
                }
            }
        }
    }
    private void SmokeEffect()
    {
        // Visual Effect
        GameObject explosionEffect = GlobalReferences.Instance.smokeExplosionEffect;
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // PlaySound
        SoundManager.Instance.PlaySmokeBoom();       
    }
}

