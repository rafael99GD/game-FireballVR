using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas, OptionsCanvas;
    [SerializeField] private Slider AmbienceSlider, MusicSlider, FXSlider;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private AudioClip startGameFanfare;
    [SerializeField] private float musicFadeOutTime = 1.5f;

    void Start()
    {
        MostrarPanelMenuPrincipal();

        if (SoundManager.Instance != null)
        {
            AmbienceSlider.value = PlayerPrefs.GetFloat("AmbienceVolume", 1f);  
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);       
            FXSlider.value = PlayerPrefs.GetFloat("FXVolume", 1f);              
        }

        OnMusicVolumeChanged();  
        OnAmbienceVolumeChanged(); 
    }

    public void MostrarPanelMenuPrincipal()
    {
        MainCanvas.SetActive(true);
        OptionsCanvas.SetActive(false);
    }

    public void MostrarPanelAjustes()
    {
        MainCanvas.SetActive(false);
        OptionsCanvas.SetActive(true);
    }

    public void Salir()
    {
        if (sceneController != null)
        {
            sceneController.QuitApplication();
        }
        else
        {
            Application.Quit();
        }
    }

    public void IniciarMainGame()
    {
        StartCoroutine(IniciarJuegoCoroutine());
    }
    private IEnumerator IniciarJuegoCoroutine()
    {
        if (SoundManager.Instance != null)
        {
            // Fade out música
            yield return SoundManager.Instance.FadeOutMusic(musicFadeOutTime);

            // Reproducir fanfarre
            if (startGameFanfare != null)
            {
                yield return SoundManager.Instance.PlayFanfareAndWait(startGameFanfare);
            }
        }

        // Cargar escena
        if (sceneController != null)
        {
            sceneController.LoadTutorialScene();
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void OnAmbienceVolumeChanged()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetAmbienceVolume(AmbienceSlider.value);
            PlayerPrefs.SetFloat("AmbienceVolume", AmbienceSlider.value);
        }
    }

    public void OnMusicVolumeChanged()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusicVolume(MusicSlider.value);
            PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        }
    }

    public void OnFXVolumeChanged()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetFxVolume(FXSlider.value);
            PlayerPrefs.SetFloat("FXVolume", FXSlider.value);
        }
    }
}