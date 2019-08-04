using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ScaleOverTime : MonoBehaviour
{
    [SerializeField] private float initialScale;
    [SerializeField] private List<Scale> transformations;

    private RectTransform rect;
    private float previousScale;
    private float timeElapsed = 0f;
    private int currentTransformation = 0;
    private float velocity = 0f;

    [System.Serializable]
    private class Scale { public float duration; public float targetScale; }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.localScale = GetScaleVector(initialScale);
        previousScale = initialScale;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= transformations[currentTransformation].duration) {
            timeElapsed = 0f;
            velocity = 0f;
            previousScale = transformations[currentTransformation].targetScale;
            currentTransformation++;
            if (currentTransformation >= transformations.Count) {
                Destroy(this.gameObject);
                return;
            }
        }

        rect.localScale = GetScaleVector(Mathf.SmoothStep(previousScale, transformations[currentTransformation].targetScale, timeElapsed / transformations[currentTransformation].duration));
    }

    private Vector3 GetScaleVector(float scale) {
        return new Vector3(scale, scale, 1f);
    }
}
