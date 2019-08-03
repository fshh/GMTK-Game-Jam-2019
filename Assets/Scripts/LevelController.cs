using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [System.Serializable]
    private class Level {
        [SerializeField] public List<string> enemyTypes;
        [SerializeField] public float duration;
        [SerializeField] public int totalNumberToSpawn;
    }
    [SerializeField] private List<Level> levels;

    private EnemyController enemyController;
    private float levelTimeElapsed = 0f;
    private float spawnTimeElapsed = 0f;
    private int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        levelTimeElapsed += Time.deltaTime;
        spawnTimeElapsed += Time.deltaTime;

        if (currentLevel >= levels.Count) {
            // TODO: endgame behavior
            return;
        }

        if (spawnTimeElapsed >= levels[currentLevel].duration / levels[currentLevel].totalNumberToSpawn) {
            SpawnRandomFromCurrentLevel();
            spawnTimeElapsed = 0f;
        }

        if (levelTimeElapsed >= levels[currentLevel].duration) {
            currentLevel++;
            levelTimeElapsed = 0f;
            spawnTimeElapsed = 0f;
        }
    }

    private void SpawnRandomFromCurrentLevel() {
        string enemyType = levels[currentLevel].enemyTypes[Random.Range(0, levels[currentLevel].enemyTypes.Count)];
        enemyController.SpawnEnemy(enemyType);
    }
}
