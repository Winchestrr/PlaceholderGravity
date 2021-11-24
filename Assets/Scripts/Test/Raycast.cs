using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public GameObject lastHit;
    public float distance;

    public RaycastHit hitinfo;
    public Vector3 collision = Vector3.zero;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, distance))
        {
            
            collision = hitinfo.point;
            lastHit = hitinfo.transform.gameObject;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(collision, 0.2f);
    //}
}
