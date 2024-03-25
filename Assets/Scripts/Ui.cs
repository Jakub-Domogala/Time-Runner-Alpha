using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Slider volumeSlider; // Slider do ustawiania g³oœnoœci

    void Start()
    {
        // Ustawienie pocz¹tkowych wartoœci sliderów
        volumeSlider.value = DataBase.Instance.Volume;
        DataBase.Instance.Time = 0;
        Debug.Log(DataBase.Instance.Time);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    // Metoda wywo³ywana po zmianie wartoœci na sliderze g³oœnoœci
    public void OnVolumeChanged()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
        DataBase.Instance.Volume = volume;
    }

    public void QuitGame()
    {
        Debug.Log("Wyjœcie z gry");
        Application.Quit(); // Zamkniêcie aplikacji (dzia³a tylko w buildzie)
    }
    // Metoda wywo³ywana po naciœniêciu przycisku zmiany sceny
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Za³aduj now¹ scenê na podstawie nazwy
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
