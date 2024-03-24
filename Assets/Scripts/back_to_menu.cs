using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back_to_menu : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public void Start()
    {
        text.text = "Your total time is: " + DataBase.Instance.Time;
    }

    public void back_to_main_menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
