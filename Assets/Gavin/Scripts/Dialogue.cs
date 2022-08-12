using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Dialogue : MonoBehaviour
{
    public static Dialogue currentDialogueSystem;
    [SerializeField] float textSpeed;
    bool skipTextPrintOut = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            List<string> dialogue = new List<string>();
            dialogue.Add("This is some wonky text");
            dialogue.Add("2nd wonkey text");
            dialogue.Add("3rd wonkey text");
            dialogue.Add("4th wonkey text");
            DisplayDialogue(dialogue);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipTextPrintOut = true;
        }
    }

    /// <summary>
    /// Input list of dialogues, the dialogue box will pop up and run through the list of dialogue
    /// </summary>
    /// <param name="dialogue"></param>
    void DisplayDialogue(List<string> dialogue)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(DisplayText(dialogue, () => { return true; }));
    }

    void DisplayDialogue(List<string> dialogue, Func<bool> FinishFunc)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(DisplayText(dialogue, FinishFunc));
    }

    IEnumerator DisplayText(List<string> dialogue, Func<bool> FinishFunc)
    {
        TextMeshProUGUI text = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < dialogue.Count; i++)
        {
            text.text = "";
            skipTextPrintOut = false;
            for (int j = 0; j < dialogue[i].Length; j++)
            {
                text.text += dialogue[i][j];
                if (!skipTextPrintOut)
                {
                    yield return new WaitForSeconds(textSpeed);
                }
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return null;
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
