using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityGun : MonoBehaviour
{
    public PlayerInput playerInput;

    [Header("Raycast")]
    public GameObject gunEnd;

    public float distance;
    public GameObject lastHit;

    public RaycastHit hitinfo;
    public Vector3 collision = Vector3.zero;

    [Header("Gravity")]
    public float earthGravity = -9.8f;
    public float invertedGravity;
    public bool isInverted = false;

    ConstantForce cf;

    void Start()
    {
        cf = gameObject.GetComponent<ConstantForce>();

        Physics.gravity = new Vector3(0, earthGravity, 0);
        invertedGravity = earthGravity * (-2);
    }
    void Update()
    {
        CastRay();
        //GetPointerObjectForce();
    }

    void CastRay()
    {
        Debug.DrawRay(gunEnd.transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.red);
        if (Physics.Raycast(gunEnd.transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, distance))
        {
            collision = hitinfo.point;
            lastHit = hitinfo.transform.gameObject;
            cf = lastHit.transform.gameObject.GetComponent<ConstantForce>();

            if(hitinfo.transform.gameObject.GetComponent<Rigidbody>() != null)
            {
                invertedGravity = earthGravity * (-2) * hitinfo.transform.gameObject.GetComponent<Rigidbody>().mass;
            }
        }
    }

    void GetPointerObjectForce()
    {
        cf = lastHit.transform.gameObject.GetComponent<ConstantForce>();
    }

    public void ChangeGravity()
    {
        //invertedGravity = earthGravity * (-2) * ;

        if (cf != null)
        {
            if (!isInverted)
            {
                cf.force = new Vector3(0, invertedGravity, 0);
            }
            else
            {
                cf.force = new Vector3(0, 0, 0);
            }
            isInverted = !isInverted;
        }
        cf = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}