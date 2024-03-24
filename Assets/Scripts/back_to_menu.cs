using UnityEngine;
using UnityEngine.SceneManagement;

public class back_to_menu : MonoBehaviour
{
    public void back_to_main_menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
