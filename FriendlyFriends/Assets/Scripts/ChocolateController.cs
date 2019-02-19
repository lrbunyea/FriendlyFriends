using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateController : MonoBehaviour
{
    #region Variables
    private bool pressedKey;
    private bool beingHeld;
    private Rigidbody rb;
    #endregion

    #region Unity API Functions
    private void Start()
    {
        pressedKey = false;
        beingHeld = false;
        rb = GetComponent<Rigidbody>();
        GameManager.Instance.DeleteObjective.AddListener(DeleteOnEvent);

        rb.AddForce(new Vector3(1, 0, 0)*1000, ForceMode.Acceleration);
    }

    private void Update()
    {
        //Check if Player is currently holding the object
        if (beingHeld)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Clearing parent");
                transform.parent = null;
                beingHeld = false;
                rb.isKinematic = false;
                rb.detectCollisions = true;
                //rb.useGravity = true;
                GameManager.Instance.SetChocolateHolding(false);
            }
        }
        //Logic for item gathering key presses
        else if (Input.GetKeyDown(KeyCode.E))
        {
            pressedKey = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            pressedKey = false;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Trigger entered!");
        if (pressedKey && other.gameObject.tag == "Player" && !beingHeld)
        {
            Debug.Log("Object picked up!");
            transform.SetParent(other.gameObject.transform);
            beingHeld = true;
            rb.isKinematic = true;
            rb.detectCollisions = false;
            //rb.useGravity = false;
            GameManager.Instance.SetChocolateHolding(true);
            pressedKey = false;
        }
    }
    #endregion

    public void DeleteOnEvent()
    {
        GameManager.Instance.SetChocolateHolding(false);
        Destroy(this.gameObject);
    }
}