using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
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
