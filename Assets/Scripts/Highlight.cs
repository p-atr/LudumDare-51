using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField]
    private Material defaultMat;

    [SerializeField]
    private Material highlightMat;

    private Light light;
    private MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material = defaultMat;
        light = transform.GetChild(0).GetComponent<Light>();
        light.enabled = false;
    }

    public void HightlightTile()
    {
        light.enabled = true;
        rend.material = highlightMat;
    }

    public void ResetTile()
    {

        light.enabled = false;
        rend.material = defaultMat;
    }
}
