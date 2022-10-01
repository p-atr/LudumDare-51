using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float rotateHorizontal = Input.GetAxis("Mouse X");
        //float rotateVertical = Input.GetAxis("Mouse Y");
        //transform.GetChild(0).RotateAround(this.transform.position, Vector3.up, rotateHorizontal);
        //transform.GetChild(0).RotateAround(Vector3.zero, -transform.right, rotateVertical);
        //transform.GetChild(0).eulerAngles = new Vector3(transform.GetChild(0).eulerAngles.x + rotateVertical, transform.GetChild(0).eulerAngles.y + rotateHorizontal, 0);

        rb.velocity = new Vector3(0,rb.velocity.y,0);
        //if(Input.GetKey(KeyCode.W))
        //{
        //    rb.velocity += new Vector3(transform.forward.normalized.x, 0, transform.forward.normalized.z);
        //}
        //if(Input.GetKey(KeyCode.A))
        //{
        //    rb.velocity -= new Vector3(transform.right.normalized.x, 0, transform.right.normalized.z);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    rb.velocity -= new Vector3(transform.forward.normalized.x, 0, transform.forward.normalized.z);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    rb.velocity -= new Vector3(transform.right.normalized.x, 0, transform.right.normalized.z);
        //}
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector3.right;
        }
        rb.velocity = new Vector3(rb.velocity.x * speed, rb.velocity.y, rb.velocity.z * speed);
    }
}
