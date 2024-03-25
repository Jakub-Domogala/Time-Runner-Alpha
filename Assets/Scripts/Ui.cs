using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Slider volumeSlider; // Slider do ustawiania g�o�no�ci

    void Start()
    {
        // Ustawienie pocz�tkowych warto�ci slider�w
        volumeSlider.value = DataBase.Instance.Volume;
        DataBase.Instance.Time = 0;
        Debug.Log(DataBase.Instance.Time);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    // Metoda wywo�ywana po zmianie warto�ci na sliderze g�o�no�ci
    public void OnVolumeChanged()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
        DataBase.Instance.Volume = volume;
    }

    public void QuitGame()
    {
        Debug.Log("Wyj�cie z gry");
        Application.Quit(); // Zamkni�cie aplikacji (dzia�a tylko w buildzie)
    }
    // Metoda wywo�ywana po naci�ni�ciu przycisku zmiany sceny
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Za�aduj now� scen� na podstawie nazwy
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void GoToSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
