using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public Transform[] waypoints; // Punkty, do których przeciwnik bêdzie siê porusza³
    public float movementSpeed = 3f; // Szybkoœæ poruszania siê przeciwnika

    private int currentWaypointIndex = 0; // Indeks bie¿¹cego punktu docelowego

    void Update()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("Brak punktów docelowych. Dodaj punkty do tablicy waypoints.");
            return;
        }

        // SprawdŸ czy przeciwnik osi¹gn¹³ bie¿¹cy punkt docelowy
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // PrzejdŸ do nastêpnego punktu docelowego
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // Poruszaj siê w kierunku bie¿¹cego punktu docelowego
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // Oblicz kierunek do bie¿¹cego punktu docelowego
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Przesuñ przeciwnika w tym kierunku z zadan¹ szybkoœci¹
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}
