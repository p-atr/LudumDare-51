using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stupidQuiz : MonoBehaviour
{
    [SerializeField]
    private GameObject next;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }


    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
        try {
            next.SetActive(true);
        } catch (System.Exception e) {
            Debug.Log(e); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
