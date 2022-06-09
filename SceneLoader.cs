using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] TMP_Text progressText;

    [SerializeField] Slider slider;

    private AsyncOperation operation;
    [SerializeField] GameObject Holder;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string sceneName)
    {
        UpdateProgressUI(0);
        Holder.gameObject.SetActive(true);

        StartCoroutine(BeginLoad(sceneName));
    }
    private IEnumerator BeginLoad(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            UpdateProgressUI(operation.progress);
            yield return null;
        }
        UpdateProgressUI(1);
        yield return new WaitForSeconds(0.3f);
        operation.allowSceneActivation = true;
        operation = null;

        Holder.gameObject.SetActive(false);
    }
    private void UpdateProgressUI(float progress)
    {
        slider.value = progress;
        progressText.text = Mathf.Round((progress * 100f)) + "%";
    }
}
