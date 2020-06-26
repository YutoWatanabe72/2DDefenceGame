using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("EnemyStatus")]
    public float startSpeed = 10f;
    public float speed;
    public float startHealth = 100f;
    private float health;
    public int worth = 10;
    public int classChangecost = 1;


    public GameObject hitEffect;

    private bool isDead = false;

    public Image image;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="amount">ディフェンダーの攻撃値</param>
    public void TakeDamage(int amount)
    {
        health -= amount;
        image.fillAmount = health / startHealth;
        //Debug.Log(image.fillAmount);
        GameObject effect = Instantiate(hitEffect, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z - 2f), transform.rotation);
        Destroy(effect, 5f);

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡時
    /// </summary>
    void Die()
    {
        isDead = true;

        PlayerStetas.Money += worth;
        PlayerStetas.ClassChangeFragment += classChangecost;

        WaveSpawner.EnemyAlive--;
        Destroy(gameObject);
    }
}
