using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressAnyKeyToAdvance : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private bool loading = false;
    private Animator fadePanel;

    private void Awake() {
        fadePanel = GameObject.FindGameObjectWithTag("Fade Panel").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!loading && Input.anyKeyDown) {
            StartCoroutine(AsyncLoad());
        }
    }

    private IEnumerator AsyncLoad() {
        loading = true;
        fadePanel.SetTrigger("Fade");

        while (!fadePanel.GetCurrentAnimatorStateInfo(0).IsName("PanelIdleDark")) {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("AndrewScene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
