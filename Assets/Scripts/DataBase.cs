using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    // Statyczne pole przechowuj�ce instancj� klasy
    private static DataBase instance;
    private float volume = 1.0f;
    private float time = 0.0f;

    // Metoda dost�pu do instancji klasy
    public static DataBase Instance
    {
        get
        {
            // Je�li instancja nie istnieje, utw�rz j�
            if (instance == null)
            {
                // Sprawd�, czy w scenie istnieje ju� obiekt z t� klas�
                instance = FindObjectOfType<DataBase>();

                // Je�li nie istnieje, utw�rz nowy obiekt z t� klas�
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(DataBase).Name);
                    instance = singletonObject.AddComponent<DataBase>();
                }
            }

            // Zwr�� instancj� klasy
            return instance;
        }
    }

    // Metoda inicjalizuj�ca instancj� klasy
    private void Awake()
    {
        // Sprawd�, czy istnieje ju� instancja klasy w obiekcie
        if (instance == null)
        {
            // Je�li nie, ustaw bie��cy obiekt jako instancj� klasy
            instance = this;
            // Upewnij si�, �e obiekt nie zostanie zniszczony przy zmianie sceny
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Je�li istnieje ju� instancja klasy, usu� bie��cy obiekt, aby zapobiec duplikacji
            Destroy(gameObject);
        }
    }

    // Tutaj mo�esz dodawa� metody i pola, kt�re dotycz� bazy danych

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
