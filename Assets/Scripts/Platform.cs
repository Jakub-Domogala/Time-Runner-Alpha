using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public Transform[] waypoints;
    public Transform plaformTransform;
    // Prêdkoœæ poruszania siê platformy
    public float speed = 2f;

    // Numer aktualnego punktu docelowego, do którego porusza siê platforma
    private int currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Ustawienie platformy na pierwszym punkcie docelowym
        transform.position = waypoints[currentWaypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        // Sprawdzenie czy platforma osi¹gnê³a aktualny punkt docelowy
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Przejœcie do nastêpnego punktu docelowego
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // Poruszanie platformy w kierunku aktualnego punktu docelowego z zadan¹ prêdkoœci¹
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime * GameMaster.Instance.timeMultiplayer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("kolizja");
        // SprawdŸ, czy obiekt wejœcia jest graczem
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("kolizja z Graczem");
            // Ustaw obiekt gracza jako dziecko platformy
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Jeœli gracz opuœci platformê, to przestaje siê poruszaæ razem z ni¹
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    // Rysowanie linii ³¹cz¹cych punkty docelowe platformy w trybie edycji
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2)
            return;

        // Rysowanie linii ³¹cz¹cych punkty docelowe platformy
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }

        // Rysowanie linii ³¹cz¹cej ostatni punkt z pierwszym, tworz¹c zamkniêt¹ pêtlê
        Gizmos.color = Color.green;
        Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
    }

}
