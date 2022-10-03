using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facer : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Quaternion originalRotation;

    private bool isShaking = false;

    private float shakeStrength = 1f;
    private float shakeTimer = 0f;
    void Start()
    {
        originalRotation = gameObject.transform.rotation;
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).direction * 10, Color.yellow);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).direction, out hit))
        {
            //Debug.Log("HITEROO");

            player.transform.LookAt(new Vector3(hit.point.x, player.position.y, hit.point.z));
        }

        //1 second timer
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            isShaking = true;
        }
        else
        {
            shakeTimer = 0;
            isShaking = false;
            gameObject.transform.rotation = originalRotation;
            //Debug.Log("Shake time is 0");
        }
        //screen shake
        if (isShaking)
        {
            gameObject.transform.rotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(Random.Range(-shakeStrength, shakeStrength), Random.Range(-shakeStrength, shakeStrength), 0f));

        }
    }

    public void shakeScreen(float shakeTime, float shakeStrength)
    {
        this.shakeStrength = shakeStrength;
        shakeTimer = shakeTime;
    }
}
