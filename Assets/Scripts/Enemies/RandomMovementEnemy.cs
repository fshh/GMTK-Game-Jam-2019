using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementEnemy : Enemy
{
    /// <summary>
    /// This is the distance from the center where the direction is purely random. 
    /// The Enemy "tries" to get to this distance.
    /// </summary>
    [SerializeField] private float distanceFromCenterTrend = 1;

    /// <summary>
    /// The amount of time this enemy spends moving before changing directions.
    /// </summary>
    [SerializeField] private float timeSpentUnchanged = 2F;

    private float timeMoving = 0f;
    private Vector2 currentDir = new Vector2();

    protected override Vector3 GetDirection()
    {
        if (timeMoving <= 0)
        {
            float xDir = Random.Range(-1f, 1f);
            float yDir = Random.Range(-1f, 1f);
            float distToCenter = Vector2.Distance(transform.position, Vector2.zero);
            currentDir = (-transform.position.normalized + new Vector3(xDir, yDir) * (distanceFromCenterTrend / distToCenter)).normalized;
            timeMoving = timeSpentUnchanged;
            return currentDir;
        }
        timeMoving -= Time.fixedDeltaTime;
        return currentDir;
    }

    protected override void Attack()
    {
        
    }
}
