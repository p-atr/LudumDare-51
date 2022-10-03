using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flicker : MonoBehaviour
{
    private Light light;

    public float flickerspeed = 10f;
    public float minIntensity = 5f;
    public float maxIntensity = 7f;
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
        goalIntensity = Random.Range(minIntensity, maxIntensity);
    }

    float goalIntensity;
    void Update()
    {
        if (Mathf.Abs(light.intensity - goalIntensity) < 0.01f)
        {
            //lightIntensity ist immer zwischen low und high
            //Debug.Log("direction changed");
            goalIntensity = Random.Range(minIntensity, maxIntensity);
        }
        //lerp between 0 and 1
        light.intensity = Mathf.Lerp(light.intensity, goalIntensity, flickerspeed * Time.deltaTime);
    }
}
