﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public int EnemyCount => enemies.Count;

    private List<Enemy> enemies = new List<Enemy>();
    private Dictionary<EnemyType, Enemy> enemyPrefabs;
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
            spotlight.target = closestEnemy?.transform;
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

    public void SpawnEnemy(EnemyType name, int spawnIndex)
    {
        Enemy e = GameObject.Instantiate(enemyPrefabs[name], spawns[spawnIndex].transform.position, Quaternion.identity);
        e.Target = player;
        enemies.Add(e);
    }

    public void SpawnEnemy(EnemyType name) {
        int spawnIndex = Random.Range(0, spawns.Length);
        SpawnEnemy(name, spawnIndex);
    }

    public void RemoveEnemy(Enemy enemy) {
        int points = (int)enemy.type;
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
        if (!PlayerController.IsDead())
        {
            GameObject.FindGameObjectWithTag("Score Board").GetComponent<ScoreBoard>().Score += points;
        }
    }
}
