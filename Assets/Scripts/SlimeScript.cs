using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public Transform[] waypoints; // Punkty, do kt�rych przeciwnik b�dzie si� porusza�
    public float movementSpeed = 3f; // Szybko�� poruszania si� przeciwnika

    private int currentWaypointIndex = 0; // Indeks bie��cego punktu docelowego

    void Update()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("Brak punkt�w docelowych. Dodaj punkty do tablicy waypoints.");
            return;
        }

        // Sprawd� czy przeciwnik osi�gn�� bie��cy punkt docelowy
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Przejd� do nast�pnego punktu docelowego
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // Poruszaj si� w kierunku bie��cego punktu docelowego
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // Oblicz kierunek do bie��cego punktu docelowego
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Przesu� przeciwnika w tym kierunku z zadan� szybko�ci�
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}
