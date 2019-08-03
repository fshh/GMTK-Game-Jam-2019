using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private List<Enemy> enemies = new List<Enemy>();
    private Dictionary<string, Enemy> enemyPrefabs;
    private GameObject[] spawns;
    private float timeFlashed = 0f;
    
    void Start()
    {
        enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemies").ToDictionary(x => x.type);
        spawns = GameObject.FindGameObjectsWithTag("Enemy Spawn");
    }
    
    void Update()
    {
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
                    enemy.SetVisibility(false);
                }
            }
            closestEnemy?.SetVisibility(true);
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
}
