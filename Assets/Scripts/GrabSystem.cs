using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSystem : MonoBehaviour
{

    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform item_slot;

    [SerializeField]
    private Transform coin_inventory;

    [SerializeField]
    private Transform item_container;

    [SerializeField]
    private Player_Manager player;

    [SerializeField]
    AudioSource coins;


    // Reference to the picked item.
    private PickableItem pickedItem;

    private bool isPicking = true;

    void OnCollisionEnter(Collision collision) 
    {
        if (isPicking)
        {
            var pickable = collision.gameObject.GetComponent<PickableItem>();

            if (pickable) 
            {
                isPicking = false;

                if (pickable.transform.tag == "Coin")
                {
                    PickItem(pickable, coin_inventory);
                    player.Heal(1);
                    coins.Play();
                }

                else 
                {
                    // Empty the slot.
                    DropItem(pickedItem);

                    // Assign reference
                    pickedItem = pickable;
                    switch(pickedItem.tag)
                    {
                        case "Bow":
                            player.GetComponent<Shoot>().Switch_weapon(Weapon_type.Bow);
                            break;
                        case "Sword":
                            player.GetComponent<Shoot>().Switch_weapon(Weapon_type.Big_Ass_Club_bzw_Gigaschwert);
                            break;
                        case "Sling":
                            player.GetComponent<Shoot>().Switch_weapon(Weapon_type.Sling_Shot);
                            break;
                        case "Knife":
                            player.GetComponent<Shoot>().Switch_weapon(Weapon_type.Throwing_Knive);
                            break;
                    }

                    PickItem(pickedItem, item_slot);
                }

                // Debounce Collisions
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



    /// <summary>
    /// Method for picking up item.
    /// </summary>
    private void PickItem(PickableItem item, Transform storage)
    {

        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.transform.gameObject.SetActive(false);
        // item.Rb.velocity = Vector3.zero;
        // item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        item.transform.SetParent(storage);
        // Reset position and rotation
        // item.transform.localPosition = Vector3.zero;
        // item.transform.localEulerAngles = Vector3.zero;
    }

    /// <summary>
    /// Method for dropping item.
    /// </summary>
    private void DropItem(PickableItem item)
    {
        // Nothing to drop
        if (item == null) return;

        item.transform.SetParent(item_container);
        Vector3 PlayerPosition = transform.position;
        item.transform.gameObject.transform.position = PlayerPosition + new Vector3(0, 2, 0);
        item.Rb.isKinematic = false;
        item.transform.gameObject.SetActive(true);
    }

}
