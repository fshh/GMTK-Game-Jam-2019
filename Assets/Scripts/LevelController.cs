using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [System.Serializable]
    private class Level {
        [SerializeField] public List<EnemySpawnParams> spawnParams;
        [SerializeField] public float spawnTime;
        [SerializeField] public float timeLimit;
        /// <summary>
        /// The number of spawns in this level. Clamped from sum of minSpawns to sum of maxSpawns.
        /// Make -1 for a random number between the min and max.
        /// </summary>
        [SerializeField] public int totalNumberToSpawn;
    }
    [System.Serializable]
    private class EnemySpawnParams
    {
        [SerializeField] public EnemyType type;
        [SerializeField] public int minSpawn;
        [SerializeField] public int maxSpawn;
    }
    [SerializeField] private List<Level> levels;

    private EnemyController enemyController;
    private float levelTimeElapsed = 0f;
    private float spawnTimeElapsed = 0f;
    private int currentLevel = 0;
    private List<EnemyType> levelSpawns = new List<EnemyType>();
    private int currentLevelSpawnNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
        CreateLevelSpawns();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.IsDead()) {
            levelTimeElapsed += Time.deltaTime;
            spawnTimeElapsed += Time.deltaTime;

            if (currentLevel >= levels.Count)
            {
                // TODO: endgame behavior
                return;
            }

            if (spawnTimeElapsed >= levels[currentLevel].spawnTime / currentLevelSpawnNum 
                    && levelTimeElapsed < levels[currentLevel].spawnTime + 1)
            {
                SpawnRandomFromCurrentLevel();
                spawnTimeElapsed = 0f;
            }

            if (levelTimeElapsed >= levels[currentLevel].timeLimit 
                    || (enemyController.EnemyCount < 1 && levelTimeElapsed > levels[currentLevel].spawnTime))
            {
                // TEMP: Repeat last level
                if (currentLevel < levels.Count - 1)
                {
                    currentLevel++;
                }
                levelTimeElapsed = 0f;
                spawnTimeElapsed = 0f;
                CreateLevelSpawns();
            }
        }
    }

    private void CreateLevelSpawns()
    {
        levelSpawns.Clear();
        int sumMinSpawn = 0;
        int sumMaxSpawn = 0;
        // Populate minimum spawns
        foreach (EnemySpawnParams enemy in levels[currentLevel].spawnParams)
        {
            for (int i = 0; i < enemy.minSpawn; i++)
            {
                levelSpawns.Add(enemy.type);
            }
            sumMinSpawn += enemy.minSpawn;
            sumMaxSpawn += enemy.maxSpawn;
        }
        if (levels[currentLevel].totalNumberToSpawn == -1)
        {
            currentLevelSpawnNum = Random.Range(sumMinSpawn, sumMaxSpawn);
        }
        currentLevelSpawnNum = Mathf.Clamp(levels[currentLevel].totalNumberToSpawn, sumMinSpawn, sumMaxSpawn);
        // Added extra spawns
        for (int i = 0; i < currentLevelSpawnNum - sumMinSpawn; i++)
        {
            levelSpawns.Add(levels[currentLevel].spawnParams[Random.Range(0, levels[currentLevel].spawnParams.Count)].type);
        }
    }

    private void SpawnRandomFromCurrentLevel() {
        EnemyType enemyType = levelSpawns[Random.Range(0, levelSpawns.Count)];
        enemyController.SpawnEnemy(enemyType);
    }
}
