using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GravityGun : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerController2 playerController;
    public UIController uiController;

    [Header("Shooting")]
    public float timeBetweenShots;
    private float currentTimeBetweenShots;
    private float lastShotTime;

    [Header("Energy")]
    public Image energyBar;
    public bool isEnergyRefilling;
    public static float maxEnergy = 200;
    public float maxEnergyDisplay;
    public static float currentEnergy;
    public float currentEnergyDisplay;
    public static float energyFillRate;
    public float energyFillRateDisplay;

    [Header("Raycast")]
    public GameObject gunEnd;

    public float distance;
    public GameObject lastHit;

    public RaycastHit hitinfo;
    public Vector3 collision = Vector3.zero;

    [Header("Gravity")]
    public int changeGravityCost;
    public float earthGravity = -9.8f;
    public float invertedGravity;
    public bool isInverted = false;

    ConstantForce cf;

    [Header("Pick up")]
    public GameObject heldObject;
    public Transform holdParent;
    public int pickUpCost;
    public float moveForce = 250;
    public bool isHeld = false;

    [Header("Yeet object")]
    public float yeetForce;


    void Start()
    {
        currentTimeBetweenShots = timeBetweenShots;
        currentEnergy = maxEnergy;

        cf = gameObject.GetComponent<ConstantForce>();
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
        energyBar = GameObject.Find("EnergyFill").GetComponent<Image>();

        invertedGravity = earthGravity * (-2);
    }

    private void SetStatics()
    {
        //temporary function
        maxEnergy = maxEnergyDisplay;
        energyFillRate = energyFillRateDisplay;
    }

    void Update()
    {
        SetStatics();
        CastRay();

        if (heldObject != null) MoveObject();
        currentEnergyDisplay = currentEnergy;
    }

    public void Shoot(string type)
    {
        if (Time.time > lastShotTime + currentTimeBetweenShots)
        {
            lastShotTime = Time.time;

            switch (type)
            {
                case "change":
                    ChangeGravity();
                    break;

                case "pick":
                    PickUpOrDrop();
                    break;

                case "yeet":
                    if (heldObject != null) YeetObject();
                    break;
            }

            if (!isEnergyRefilling) StartCoroutine(EnergyRefill());
        }

        UIController.UpdateUI();
    }

    public bool UseEnergy(int amount)
    {
        UIController.UpdateUI();

        if (currentEnergy - amount >= 0)
        {
            Debug.Log("Energy used");
            currentEnergy -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough energy");
            return false;
        }
    }

    void CastRay()
    {
        //Debug.DrawRay(gunEnd.transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.red);
        //if (Physics.Raycast(gunEnd.transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, distance))
        if (Physics.Raycast(playerController.cameraTransform.position, playerController.cameraTransform.forward, out hitinfo, distance))
        {
            collision = hitinfo.point;
            lastHit = hitinfo.transform.gameObject;
            cf = lastHit.transform.gameObject.GetComponent<ConstantForce>();

            if (hitinfo.transform.gameObject.GetComponent<Rigidbody>() != null)
            {
                invertedGravity = earthGravity * (-2) * hitinfo.transform.gameObject.GetComponent<Rigidbody>().mass;
            }
        }
    }

    public void ChangeGravity()
    {
        //invertedGravity = earthGravity * (-2) * ;

        if (!UseEnergy(changeGravityCost)) return;

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

    public IEnumerator EnergyRefill()
    {
        for (int i = 0; i < 100; i++)
        {
            if(currentEnergy != maxEnergy)
            {
                isEnergyRefilling = true;
                currentEnergy += (maxEnergy / 100);
                UIController.UpdateUI();
                yield return new WaitForSeconds(energyFillRate / 100);
            }
            else
            {
                UIController.UpdateUI();
                isEnergyRefilling = false;
                StopCoroutine(EnergyRefill());
            }
        }
    }

    public void PickUpOrDrop()
    {
        //if(!isHeld && heldObject != null)
        if(!isHeld)
        {
            PickUpObject();
            isHeld = true;
        }
        else
        {
            DropObject();
            isHeld = false;
        }
    }

    public void PickUpObject()
    {
        if (!UseEnergy(pickUpCost)) return;
        if (lastHit.GetComponent<Rigidbody>() == null) return;

        Rigidbody objectRB = lastHit.GetComponent<Rigidbody>();
        objectRB.useGravity = false;
        objectRB.freezeRotation = true;
        objectRB.drag = 10;
        //objectRB.transform.parent = holdParent;
        heldObject = lastHit;
    }

    public void MoveObject()
    {
        if(Vector3.Distance(heldObject.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    public void DropObject()
    {
        if (heldObject.GetComponent<Rigidbody>() == null) return;

        Rigidbody objectRB = heldObject.GetComponent<Rigidbody>();
        objectRB.useGravity = true;
        objectRB.freezeRotation = false;
        objectRB.drag = 1;

        //objectRB.transform.parent = null;
        heldObject = null;
    }

    public void YeetObject()
    {
        Debug.Log("YEET");
        if (heldObject.GetComponent<Rigidbody>() == null) return;

        Rigidbody objectRB = heldObject.GetComponent<Rigidbody>();
        objectRB.useGravity = true;
        objectRB.freezeRotation = false;
        objectRB.drag = 1;

        isHeld = false;
        heldObject = null;

        objectRB.AddForce(transform.forward.normalized * yeetForce);
        //objectRB.AddExplosionForce(yeetForce, transform.position, 10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}