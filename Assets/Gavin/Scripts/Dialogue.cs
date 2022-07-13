using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    public static Dialogue currentDialogueSystem;
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
    }

    /// <summary>
    /// Input list of dialogues, the dialogue box will pop up and run through the list of dialogue
    /// </summary>
    /// <param name="dialogue"></param>
    void DisplayDialogue(List<string> dialogue)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(DisplayText(dialogue));
    }

    IEnumerator DisplayText(List<string> dialogue)
    {
        TextMeshProUGUI text = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < dialogue.Count; i++)
        {
            text.text = dialogue[i];
            print(text.text);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return null;
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
