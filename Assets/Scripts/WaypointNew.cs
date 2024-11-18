using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNew : MonoBehaviour
{
    public Vector3[] localWaypoints;
    Vector3[] globalWaypoints;

    [SerializeField]
    float obstacleSpeed;

    [SerializeField]
    bool cyclic;

    [SerializeField]
    float waitTime;

    [SerializeField]
    [Range(0, 2)]
    float easeAmount;

    int fromWaypointIndex;
    float percentageBetweenWaypoints;
    float nextMoveTime;

    public void Start()
    {
        globalWaypoints = new Vector3[localWaypoints.Length];

        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
    }

    public void Update()
    {
        Vector3 velocity = CalculatePlatformMovement();
        transform.Translate(velocity);
    }

    float Ease(float x)
    {
        float a = easeAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    Vector3 CalculatePlatformMovement()
    {
        if (Time.time < nextMoveTime)
        {
            return Vector3.zero;
        }

        fromWaypointIndex %= globalWaypoints.Length;
        int toWayPointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;

        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWayPointIndex]);

        percentageBetweenWaypoints += Time.deltaTime * obstacleSpeed / distanceBetweenWaypoints;

        percentageBetweenWaypoints = Mathf.Clamp01(percentageBetweenWaypoints);

        float easedPercentWaypoints = Ease(percentageBetweenWaypoints);

        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWayPointIndex], easedPercentWaypoints);

        if (percentageBetweenWaypoints >= 1)
        {
            percentageBetweenWaypoints = 0;
            fromWaypointIndex++;

            if (!cyclic)
            {
                if (fromWaypointIndex >= globalWaypoints.Length - 1)
                {
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }

            }

            nextMoveTime = Time.time + waitTime;
        }

        return newPos - transform.position;
    }

    private void OnDrawGizmos()
    {
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = 0.3f;

            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }
}
