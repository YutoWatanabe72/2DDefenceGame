using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject Endpoint;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        Endpoint = GameObject.Find("EndPoint");
    }

    void Update()
    {
        Vector2 dir = Endpoint.transform.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if(Vector2.Distance(transform.position, Endpoint.transform.position) <= 0.5f)
        {
            EndPath();
        }

        enemy.speed = enemy.startSpeed;
    }

    /// <summary>
    /// 終了地点に到達したら敵の消滅、プレイヤーのライフ減少
    /// </summary>
    void EndPath()
    {
        if (PlayerStetas.Lives > 0)
        {
            PlayerStetas.Lives--;
        }
        WaveSpawner.EnemyAlive--;
        Destroy(gameObject);
    }
}
