using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorShiftText : MonoBehaviour
{
    [SerializeField] private List<Color> colors;
    [SerializeField] private float shiftTime;

    private Text text;
    private int currentColorIndex = 0;
    private int nextColorIndex = 1;
    private float timeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.color = colors[currentColorIndex];
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= shiftTime) {
            timeElapsed = 0f;
            currentColorIndex = nextColorIndex;
            nextColorIndex = nextColorIndex + 1 >= colors.Count ? 0 : nextColorIndex + 1;
        }

        text.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], timeElapsed / shiftTime);
    }
}
