using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogAnimation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_Text;


    //create string to hold the full text
    private string m_TextToDisplay;

    [SerializeField]
    public GameObject nextDialog;
    // privat str m_TextToDisplay;
    // Start is called before the first frame update
    void Start()
    {
        m_TextToDisplay = m_Text.text;
        m_Text.text = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in m_TextToDisplay.ToCharArray())
        {
            m_Text.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        try {
            nextDialog.SetActive(true);
        } catch (System.Exception e) {
            Debug.Log(e); 
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
