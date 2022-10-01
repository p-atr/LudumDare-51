using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    [SerializeField]
    private double thinktime = 1;

    private double timer;

    private ArrayList points = new ArrayList();
    private Transform player;

    private int animStage = 0;
    private bool isInAnim = false;
    private Vector3 step1, goal;
    private Vector3 dir1, dir2;
    // Start is called before the first frame update
    void Start()
    {
        timer = thinktime;
        player = GameObject.Find("Player").transform;
        points.AddRange(GameObject.FindGameObjectsWithTag("Point"));
        foreach (GameObject item in points)
        {
            //Debug.Log(item.transform.position);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if(timer > 0 && !isInAnim)
        {
            timer -= Time.deltaTime;
        }
        else if(!isInAnim)
        {
            goal = CalculateMove();
            if(goal != transform.position)
            {
                dir2 = (goal - transform.parent.position).normalized;
                dir1 = (dir2 + (Vector3.up * 5)).normalized;
                step1 = goal + (Vector3.up * 5);
                isInAnim = true;
            }
            timer = thinktime;

        }
        if(isInAnim)
        {           
            switch(animStage)
            {
                case 0:
                    transform.parent.position += (dir1 * 0.2f);
                    if(transform.parent.position.y >= 5)
                    {
                        transform.parent.position = new Vector3(transform.parent.position.x, 5, transform.parent.position.z);
                        animStage++;
                    }
                    break;
                case 1:
                    transform.parent.position += (dir2 * 0.2f);
                    //Debug.Log(Vector3.Distance(transform.parent.position, step1) + "  " + transform.parent.position + "  " + step1);
                    if (Vector3.Distance(transform.parent.position, step1)<0.09)
                    {
                        transform.parent.position = step1;
                        animStage++;
                    }
                    break;
                case 2:
                    transform.parent.position += Vector3.down * 0.4f;
                    if (Vector3.Distance(transform.parent.position, goal) < 0.41)
                    {
                        transform.parent.position = goal;
                        animStage = 0;
                        isInAnim = false;
                    }
                    break;
            }
        }
    }

    private Vector3 CalculateMove()
    {
        double min = double.MaxValue;
        GameObject minPoint = null;
        foreach (GameObject g in points)
        {
            double temp = Mathf.Abs((player.position - g.transform.position).magnitude);
            if (temp < min)
            {
                minPoint = g;
                min = temp;
            }
        }
        return minPoint.transform.position;
    }
}
