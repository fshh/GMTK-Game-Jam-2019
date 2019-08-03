using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateArenaBoundary : MonoBehaviour
{
    [SerializeField] private int numberOfEdges;
    [SerializeField] private float radius;

    // Use this for initialization
    void Start() {
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[numberOfEdges + 1];

        for (int i = 0; i < numberOfEdges; i++) {
            float angle = 2 * Mathf.PI * i / numberOfEdges;
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            points[i] = new Vector2(x, y);
        }
        points[numberOfEdges] = points[0];
         
        edgeCollider.points = points;
     }
 }