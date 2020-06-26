using UnityEngine;

public class Defender : MonoBehaviour
{
    private Transform target;

    [Header("AttackInfomation")]
    public float rangeX = 10f;
    public float rangeY = 10f;
    public float hitRate = 1f;
    private float hitCountdown = 0f;

    public string enemyTag = "Enemy";

    public Transform hitPoint;

    public int damage = 50;
    public int upgradeDamage = 30;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    /// <summary>
    /// ターゲットの更新
    /// </summary>
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearrestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = transform.position.x - enemy.transform.position.x;
            if (distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearrestEnemy = enemy;
            }
        }

        if (nearrestEnemy != null && Mathf.Abs(shortestDistance) <= rangeX)
        {
            target = nearrestEnemy.transform;

        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
            return;

        if (hitCountdown <= 0f)
        {
            AttackAnim();
            hitCountdown = 1f / hitRate;
        }

        hitCountdown -= Time.deltaTime;
    }

    /// <summary>
    /// アニメーションの再生
    /// </summary>
    void AttackAnim()
    {
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// アニメーションイベントから実行
    /// </summary>
    public void Attack()
    {
        Enemy e = target.GetComponent<Enemy>();
        if (e != null)
        { 
            e.TakeDamage(damage);
        }
    }

    /// <summary>
    /// ダメージ量の変更
    /// </summary>
    public void SetDamage()
    {
        damage += (upgradeDamage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, -0.5f), new Vector2(rangeX, rangeY));
    }
}
