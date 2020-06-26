using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //ラウンド毎の敵の数
    public static int EnemyAlive = 0;

    public Wave[] waves;

    public float timeBetweenWaves = 5f;
    private float countdown = 8f;
    private int waveIndex;

    [HideInInspector]
    public bool waveStart;

    public Transform spawnPoint;
    public Text timeText;
    public GameManager gameManager;
    public GameObject roundDisp;

    void Start()
    {
        waveIndex = 0;
        waveStart = false;
    }

    void Update()
    {
        if (!waveStart)
            return;

        if (EnemyAlive > 0)
            return;

        if (waveIndex == waves.Length)
        {
            gameManager.GameClear();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            WaveStart();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        timeText.text = countdown.ToString();
    }

    /// <summary>
    /// ウェーブの開始
    /// </summary>
    void WaveStart()
    {
        PlayerStetas.Rounds++;
        roundDisp.SetActive(true);

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.counts.Length; i++)
        {
            EnemyAlive += wave.counts[i];
        }

        for(int i = 0; i < wave.enemies.Length; i++)
        {
            StartCoroutine(SpawnWave(wave, i));
        }
        waveIndex++;
    }

    /// <summary>
    /// ウェーブ処理１
    /// ウェーブ中に出現する敵をほぼ同時に出現
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnWave(Wave wave, int n)
    {
        for (int i = 0; i < wave.counts[n]; i++)
        {
            StartCoroutine(SpawnEnemy(wave,n));
            yield return new WaitForSeconds(1f / 0.5f);
        }
    }

    /// <summary>
    /// ウェーブ処理２
    /// 敵オブジェクト生成
    /// </summary>
    /// <param name="enemy"></param>
    IEnumerator SpawnEnemy(Wave wave, int n)
    {
            Instantiate(wave.enemies[n], new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z - n), spawnPoint.rotation);
            yield return new WaitForSeconds(1f / wave.rate[n]);
    }
}
