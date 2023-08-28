using UnityEngine;

public class Mage : MonoBehaviour
{
    private Transform target;
    private bool isCasting;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 20f;

    [Header("Use Spells (default)")]

    public GameObject spellPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Beam")]

    public int damageOverTime = 10;
    public float slowPercent = .5f;
    public bool useBeam = false;
    public LineRenderer lineRenderer;

    [Header("Setup Fields")]

    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;
    private AudioSource spellSound;

    public Transform firePoint;
    

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        spellSound = GetComponent<AudioSource>();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        } else
        {
            target = null;
        }
    }

    
    void Update() 
    {
        fireCountdown -= Time.deltaTime;

        if (target == null)
        {
            if (useBeam)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useBeam)
        {
            Beam();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Beam()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPercent);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position + new Vector3(0,2,0));

        Vector3 dir = firePoint.position - target.position;
    }

    void Shoot()
    { 
        GameObject spellGO = (GameObject)Instantiate(spellPrefab, firePoint.position, firePoint.rotation);
        Spell spell = spellGO.GetComponent<Spell>();
        spellSound.Play();

        if (spell != null )
        {
            spell.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public bool IsCasting()
    {
        return isCasting;
    }
}
