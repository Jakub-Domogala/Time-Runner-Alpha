using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    // Statyczne pole przechowuj¹ce instancjê klasy
    private static DataBase instance;
    private float volume = 1.0f;
    private float time = 0.0f;

    // Metoda dostêpu do instancji klasy
    public static DataBase Instance
    {
        get
        {
            // Jeœli instancja nie istnieje, utwórz j¹
            if (instance == null)
            {
                // SprawdŸ, czy w scenie istnieje ju¿ obiekt z t¹ klas¹
                instance = FindObjectOfType<DataBase>();

                // Jeœli nie istnieje, utwórz nowy obiekt z t¹ klas¹
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(DataBase).Name);
                    instance = singletonObject.AddComponent<DataBase>();
                }
            }

            // Zwróæ instancjê klasy
            return instance;
        }
    }

    // Metoda inicjalizuj¹ca instancjê klasy
    private void Awake()
    {
        // SprawdŸ, czy istnieje ju¿ instancja klasy w obiekcie
        if (instance == null)
        {
            // Jeœli nie, ustaw bie¿¹cy obiekt jako instancjê klasy
            instance = this;
            // Upewnij siê, ¿e obiekt nie zostanie zniszczony przy zmianie sceny
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Jeœli istnieje ju¿ instancja klasy, usuñ bie¿¹cy obiekt, aby zapobiec duplikacji
            Destroy(gameObject);
        }
    }

    // Tutaj mo¿esz dodawaæ metody i pola, które dotycz¹ bazy danych

    public float Volume
    {
        get { return volume; }
        set { volume = value; }
    }
    public float Time
    {
        get { return time; }
        set { time = time + value; }
    }
}
