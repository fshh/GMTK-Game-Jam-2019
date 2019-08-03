using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private List<Enemy> enemies = new List<Enemy>();
    private Dictionary<string, Enemy> enemyPrefabs;
    private GameObject[] spawns;
    private float timeFlashed = 0f;
    private SmoothFollowObject spotlight;
    
    void Start()
    {
        enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemies").ToDictionary(x => x.type);
        spawns = GameObject.FindGameObjectsWithTag("Enemy Spawn");
        spotlight = GameObject.FindGameObjectWithTag("Enemy Spotlight").GetComponent<SmoothFollowObject>();
    }
    
    void Update()
    {
        if (!player) {
            GameObject.FindGameObjectWithTag("Spotlight Mask").GetComponent<Image>().enabled = false;
            return;
        }

        if (timeFlashed <= 0f)
        {
            Enemy closestEnemy = null;
            float smallestDist = Mathf.Infinity;
            foreach (Enemy enemy in enemies)
            {
                float dist = Vector3.Distance(enemy.transform.position, player.transform.position);
                if (dist < smallestDist)
                {
                    smallestDist = dist;
                    closestEnemy = enemy;
                }
                if (enemy.Visible)
                {
                    //enemy.SetVisibility(false);
                }
            }
            //closestEnemy?.SetVisibility(true);
            //spotlight.target = closestEnemy?.transform;
        }
        else
        {
            timeFlashed -= Time.deltaTime;
        }
    }

    public void FlashAllEnemies(float seconds)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.SetVisibility(true);
        }
        timeFlashed = seconds;
    }

    public void SpawnEnemy(string name, int spawnIndex)
    {
        Enemy e = GameObject.Instantiate(enemyPrefabs[name], spawns[spawnIndex].transform.position, Quaternion.identity);
        e.Target = player;
        enemies.Add(e);
    }

    public void SpawnEnemy(string name) {
        int spawnIndex = Random.Range(0, spawns.Length);
        SpawnEnemy(name, spawnIndex);
    }

    public void RemoveEnemy(Enemy enemy) {
        enemies.Remove(enemy);
        GameObject.FindGameObjectWithTag("Score Board").GetComponent<ScoreBoard>().Score += 1;
        Destroy(enemy.gameObject);
    }
}
