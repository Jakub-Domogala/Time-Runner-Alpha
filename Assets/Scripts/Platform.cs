using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public Transform[] waypoints;
    public Transform plaformTransform;
    // Pr�dko�� poruszania si� platformy
    public float speed = 2f;

    // Numer aktualnego punktu docelowego, do kt�rego porusza si� platforma
    private int currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Ustawienie platformy na pierwszym punkcie docelowym
        plaformTransform.position = waypoints[currentWaypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        // Sprawdzenie czy platforma osi�gn�a aktualny punkt docelowy
        if (Vector2.Distance(plaformTransform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Przej�cie do nast�pnego punktu docelowego
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // Poruszanie platformy w kierunku aktualnego punktu docelowego z zadan� pr�dko�ci�
        plaformTransform.position = Vector2.MoveTowards(plaformTransform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime * GameMaster.Instance.timeMultiplayer);
    }

    // Rysowanie linii ��cz�cych punkty docelowe platformy w trybie edycji
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2)
            return;

        // Rysowanie linii ��cz�cych punkty docelowe platformy
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }

        // Rysowanie linii ��cz�cej ostatni punkt z pierwszym, tworz�c zamkni�t� p�tl�
        Gizmos.color = Color.green;
        Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
    }

}
