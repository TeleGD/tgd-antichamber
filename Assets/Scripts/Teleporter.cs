using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 offset;
    public float angle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = RotatePointAroundPivot(other.transform.position, transform.position, Vector3.down * angle);
            other.transform.position += offset;
            other.GetComponent<PlayerController>().RotatePlayer(angle);
        }
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    private void OnDrawGizmos()
    {
        Collider trigger = GetComponent<Collider>();
        Gizmos.color = new Color(0, 0.5f, 1, 0.2f);
        Gizmos.DrawCube(trigger.bounds.center, trigger.bounds.size);
        Gizmos.color = new Color(1, 0.8f, 0, 0.2f);
        Gizmos.DrawCube(trigger.bounds.center + offset, trigger.bounds.size);
    }
}
