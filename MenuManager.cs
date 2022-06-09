using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.IO;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //[SerializeField] GameObject PauseScreen;
    [SerializeField] TMP_Text Val;

    public TMP_Dropdown dropdownQuality;
    public TMP_Dropdown dropdownRes;

    Resolution[] resolutions;

    public Slider volumeSlider;

    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject Content;

    public float AudioVolume = 1f;
    private void Start()
    {
        //volumeSlider.value = PlayerPrefs.GetFloat("volumeSlider");
        //AudioListener.volume = volumeSlider.value;

        dropdownQuality.value = QualitySettings.GetQualityLevel();
        resolutions = Screen.resolutions;

        dropdownRes.ClearOptions();

        List<string> optionsRes = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string optres = resolutions[i].width + "x" + resolutions[i].height;
            if (!optionsRes.Contains(optres))
            {
                optionsRes.Add(optres);
            }
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        dropdownRes.AddOptions(optionsRes);
        dropdownRes.value = currentResIndex;
        dropdownRes.RefreshShownValue();
    }
    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volumeSlider", volumeSlider.value);
        PlayerPrefs.Save();
    }
    private void Update()
    {
        Val.GetComponent<TMP_Text>().text = Mathf.RoundToInt(AudioListener.volume * 10).ToString();

        if (SceneManager.GetActiveScene().name != "MainMenu" || SceneManager.GetActiveScene().name != "EndScreen")
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !PauseScreen.activeSelf)
            {
                PauseScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && PauseScreen.activeSelf)
            {
                PauseScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Resume()
    {
        PauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    public void SetResolution(int value)
    {
        Resolution res = resolutions[value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void ChangeLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }
    IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        Content.SetActive(true);
    }
}
