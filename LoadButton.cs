using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    [SerializeField] private string SceneToload;
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(LoadScene);
    }
    public void LoadScene()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        FindObjectOfType<SceneLoader>().LoadScene(SceneToload);
    }
}
