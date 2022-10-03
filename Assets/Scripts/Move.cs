using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Animator anim;
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



    }

    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {
        Vector3 move = new Vector3();
        //Debug.Log(rb.velocity.y);
        //rb.velocity = new Vector3(0, rb.velocity.y, 0);
        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }
        move = move.normalized * speed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
        //change to walk animation
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            if (Vector3.Angle(transform.forward, rb.velocity) > 90)
            {
                anim.SetBool("Backwards", true);
                anim.SetBool("Forward", false);
            }
            else
            {
                anim.SetBool("Backwards", false);
                anim.SetBool("Forward", true);
            }
        }
        else
        {
            anim.SetBool("Backwards", false);
            anim.SetBool("Forward", false);
        }
    }
}
