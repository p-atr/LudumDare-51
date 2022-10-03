using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monster_Type
{
    Default = 0,
    Zyklop = 1,
    Shooter = 2
}

public class Enemy_Behaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject shot;

    [SerializeField]
    AudioSource stomp;

    [SerializeField]
    private Transform spawnpoint;

    [SerializeField]
    private double thinktime = 1;

    [SerializeField]
    Monster_Type type;

    private double timer;

    private GameObject[][] tiles;
    public GameObject[][] Tiles
    {
        get
        {
            return tiles;
        }
        set
        {
            tiles = value;
        }
    }
    private Transform player;

    private int animStage = 2;
    private bool isInAnim = true;
    private Vector3 step1, goal;
    private Vector3 dir1, dir2;

    [SerializeField]
    private GameObject point;


    public GameObject Point
    {
        get
        {
            return point;
        }
        set
        {
            point = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (point != null)
        {
            goal = new Vector3(point.transform.position.x, 0, point.transform.position.z);
            point.GetComponent<Highlight>().HightlightTile();
        }
        else
        {
            isInAnim = false;
            animStage = 0;
        }
        //Debug.Log(tiles[0][0]);
        timer = thinktime;
        player = GameObject.Find("player1").transform;
        {
            //Debug.Log(item.transform.position);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (timer > 0 && !isInAnim)
        {
            timer -= Time.deltaTime;
        }
        else if (!isInAnim)
        {
            hasDamaged = false;
            goal = CalculateMove();
            if (goal != transform.position)
            {
                dir2 = (goal - transform.parent.position).normalized;
                dir1 = (dir2 + (Vector3.up * 5)).normalized;
                step1 = goal + (Vector3.up * 5);
                isInAnim = true;
            }
            else
            {

                switch (type)
                {
                    case Monster_Type.Default:
                        Invoke("ShootDefault", 0.2f);
                        Invoke("ShootDefault", 0.4f);
                        break;
                    case Monster_Type.Zyklop:
                        ShootZyklop();
                        break;
                    case Monster_Type.Shooter:
                        Invoke("ShootShooter", 0.1f);
                        Invoke("ShootShooter", 0.2f);
                        Invoke("ShootShooter", 0.3f);
                        break;
                }



            }
            timer = thinktime;

        }
        if (isInAnim)
        {
            switch (animStage)
            {
                case 0:
                    transform.parent.position += (dir1 * 0.2f);
                    if (transform.parent.position.y >= 5)
                    {
                        point.GetComponent<Highlight>().HightlightTile();
                        transform.parent.position = new Vector3(transform.parent.position.x, 5, transform.parent.position.z);
                        animStage++;
                    }
                    break;
                case 1:
                    transform.parent.position += (dir2 * 0.2f);
                    //Debug.Log(Vector3.Distance(transform.parent.position, step1) + "  " + transform.parent.position + "  " + step1);
                    if (Vector3.Distance(transform.parent.position, step1) < 0.2f)
                    {
                        transform.parent.position = step1;
                        animStage++;
                    }
                    break;
                case 2:
                    transform.parent.position += Vector3.down * 0.4f;
                    if (Vector3.Distance(transform.parent.position, goal) < 0.41)
                    {
                        point.GetComponent<Highlight>().ResetTile();
                        transform.parent.position = goal;
                        animStage = 0;
                        isInAnim = false;
                        stomp.Play();
                        point.GetComponentInChildren<ParticleSystem>().Play();
                    }
                    break;
            }
        }
    }

    private bool shooterMove = false;
    private Vector3 CalculateMove()
    {
        switch (type)
        {
            case Monster_Type.Default:
                if ((player.position - transform.position).magnitude > 15)
                {
                    return transform.position;
                }
                else
                {
                    double min = double.MaxValue;
                    GameObject minPoint = null;
                    float x = transform.position.x + 35.1f;
                    float y = transform.position.z - 18.6f;

                    //Debug.Log(x + "  " + y + "  " + ((int)x / 5 - 1) + "  " + ((((int)y/5) * -1) -1));
                    int i = 0;
                    int j = 0;
                    for (i = (int)x / 5 - 2; i < ((int)x / 5) + 3; i++)
                    {
                        for (j = (((int)y / 5) * -1) - 2; j < (((int)y / 5) * -1) + 3; j++)
                        {

                            //Debug.Log("i: "+ i + "  j: " + j);

                            if (i < 0 || j < 0 || i > 11 || j > 8)
                            {
                                //Debug.Log("continued");
                                continue;
                            }

                            //Debug.Log("got trough");
                            double temp = (player.position - tiles[i][j].transform.position).magnitude;
                            if (temp < min && tiles[i][j].tag == "Point")
                            {
                                minPoint = tiles[i][j];
                                min = temp;
                            }
                        }
                    }


                    //Debug.Log("FINALLYYY  i: " + i + "  j: " + j);

                    point.tag = "Point";
                    point = minPoint;
                    minPoint.tag = "Untagged";
                    return new Vector3(minPoint.transform.position.x, 0, minPoint.transform.position.z);
                }
            case Monster_Type.Zyklop:
                if ((player.position - transform.position).magnitude < 20)
                {
                    return transform.position;
                }
                else
                {
                    double min = double.MaxValue;
                    GameObject minPoint = null;
                    float x = transform.position.x + 35.1f;
                    float y = transform.position.z - 18.6f;

                    //Debug.Log(x + "  " + y + "  " + ((int)x / 5 - 1) + "  " + ((((int)y/5) * -1) -1));
                    int i = 0;
                    int j = 0;
                    for (i = (int)x / 5 - 2; i < ((int)x / 5) + 3; i++)
                    {
                        for (j = (((int)y / 5) * -1) - 2; j < (((int)y / 5) * -1) + 3; j++)
                        {

                            //Debug.Log("i: "+ i + "  j: " + j);

                            if (i < 0 || j < 0 || i > 11 || j > 8)
                            {
                                //Debug.Log("continued");
                                continue;
                            }

                            //Debug.Log("got trough");
                            double temp = Mathf.Abs((player.position - tiles[i][j].transform.position).magnitude);
                            //Debug.Log(player.position + "  " +  transform.position + "  " + temp);
                            if (temp < min && tiles[i][j].tag == "Point")
                            {
                                minPoint = tiles[i][j];
                                min = temp;
                            }
                        }
                    }


                    //Debug.Log("FINALLYYY  i: " + i + "  j: " + j);

                    point.tag = "Point";
                    point = minPoint;
                    minPoint.tag = "Untagged";
                    return new Vector3(minPoint.transform.position.x, 0, minPoint.transform.position.z);

                }
            case Monster_Type.Shooter:
                if (!shooterMove)
                {
                    shooterMove = !shooterMove;
                    return transform.position;
                }
                else
                {
                    shooterMove = !shooterMove;
                    float x = transform.position.x + 35.1f;
                    float y = transform.position.z - 18.6f;

                    int xRand = Random.Range((int)((int)x / 5 - 3), (int)((int)x / 5 + 4));
                    while (xRand < 0 || xRand > 11)
                    {
                        xRand = Random.Range((int)((int)x / 5 - 3), (int)((int)x / 5 + 4));
                    }
                    int yRand = Random.Range((int)(((int)y / 5) * -1) - 3, (int)(((int)y / 5) * -1) + 4);
                    while (yRand < 0 || yRand > 8)
                    {
                        yRand = Random.Range((int)(((int)y / 5) * -1) - 3, (int)(((int)y / 5) * -1) + 4);
                    }



                    point.tag = "Point";
                    point = tiles[xRand][yRand];
                    point.tag = "Untagged";
                    return new Vector3(tiles[xRand][yRand].transform.position.x, 0, tiles[xRand][yRand].transform.position.z);
                }
            default:
                return transform.position;
                break;
        }
    }

    private void OnDestroy()
    {
        if (point != null)
        {
            point.GetComponent<Highlight>().ResetTile();
            point.tag = "Point";
        }
    }

    private void ShootDefault()
    {
        GameObject piu = GameObject.Instantiate(shot, spawnpoint.position, transform.rotation);
        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 15;
    }

    private void ShootZyklop()
    {
        forward = transform.forward;
        right = transform.right;
        Invoke("ZyklopHelper", 0.125f);
        Invoke("ZyklopHelper", 0.25f);
        Invoke("ZyklopHelper", 0.375f);
        Invoke("ZyklopHelper", 0.5f);
        Invoke("ZyklopHelper", 0.625f);
        Invoke("ZyklopHelper", 0.75f);
        Invoke("ZyklopHelper", 0.875f);
        Invoke("ZyklopHelper", 1f);
    }

    private Vector3 forward;
    private Vector3 right;
    private int count = 0;

    private void ZyklopHelper()
    {
        //Debug.Log(count);
        Vector3 dir = new Vector3();
        Quaternion uWu = new Quaternion();
        switch (count)
        {
            case 0:
                dir = forward;
                break;
            case 1:
                dir = forward + right;
                break;
            case 2:
                dir = right;
                break;
            case 3:
                dir = -forward + right;
                break;
            case 4:
                dir = -forward;
                break;
            case 5:
                dir = -forward - right;
                break;
            case 6:
                dir = -right;
                break;
            case 7:
                dir = forward - right;
                break;
            default:
                break;
        }
        uWu.SetLookRotation(dir);
        //Debug.Log(dir + " " + uWu.eulerAngles);
        GameObject piu = GameObject.Instantiate(shot, new Vector3(transform.position.x, 1.4f, transform.position.z) + (dir * 3f), uWu);
        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 20;
        count = (count + 1) % 8;
    }

    private void ShootShooter()
    {
        GameObject piu = GameObject.Instantiate(shot, new Vector3(transform.position.x, 1.4f, transform.position.z) + (transform.forward * 3f), transform.rotation);
        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 20;


        Quaternion uWu = new Quaternion();
        uWu.SetLookRotation(-transform.forward);
        piu = GameObject.Instantiate(shot, new Vector3(transform.position.x, 1.4f, transform.position.z) - (transform.forward * 3f), uWu);
        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 20;
    }


    private bool hasDamaged = false;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(isInAnim + "  " + animStage + "  " + other.gameObject.tag + "  " + hasDamaged);
        if (isInAnim && animStage == 2 && other.gameObject.tag == "Player" && !hasDamaged)
        {
            player.GetComponent<Player_Manager>().Dmage(3);
            hasDamaged = true;
        }
    }
}
