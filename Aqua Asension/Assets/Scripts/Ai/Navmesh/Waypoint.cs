using System.Collections;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] protected float debugDrawRadious = 1.0f;
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadious);
    }
}
// https://youtu.be/5q4JHuJAAcQ?list=PL8lV_joQZ5sfqiNwoJcokJlcrgplW8uSs 