using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSystem : MonoBehaviour
{

    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform slot;

    [SerializeField]
    private Transform item;


    // Reference to the picked item.
    private PickableItem pickedItem;

    private bool isPicking = true;

    void OnCollisionEnter(Collision collision) {
        if (isPicking){
            var pickable = collision.gameObject.GetComponent<PickableItem>();

            
            if (pickable) {
                // Debug.Log("pickable!!!!");
                if(pickedItem != null) {
                    pickedItem.transform.SetParent(item);
                    Vector3 PlayerPosition = transform.position;
                    pickedItem.transform.gameObject.transform.position = PlayerPosition + new Vector3(0, 10, 0);
                    pickedItem.Rb.isKinematic = false;
                    pickedItem.transform.gameObject.SetActive(true);
                    // pickedItem = null;
                }
                // get parent of picked item


                // pickedItem = null;
                PickItem(pickable);
                StartCoroutine(waiter());
            }
        }

    }

    private IEnumerator waiter(float time=0.5f) {
        // Debug.Log("wait start");
        isPicking = false;
        yield return new WaitForSecondsRealtime(time);
        isPicking = true;
        // Debug.Log("wait end");
    }


    // void OnCollisionExit(Collision collision) {
    //     isPicking = false;
    // }



    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(PickableItem item)
    {
        // Assign reference
        pickedItem = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.transform.gameObject.SetActive(false);
        // item.Rb.velocity = Vector3.zero;
        // item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        item.transform.SetParent(slot);
        // Reset position and rotation
        // item.transform.localPosition = Vector3.zero;
        // item.transform.localEulerAngles = Vector3.zero;
    }

    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item)
    {
        // Remove reference
        
        // Remove parent
        // item.transform.SetParent(null);
        // // Enable rigidbody
        // item.Rb.isKinematic = false;
        // // Add force to throw item a little bit
        // item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }

}
