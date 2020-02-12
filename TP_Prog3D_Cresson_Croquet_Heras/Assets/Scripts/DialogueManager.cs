using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Animator anim = default;
    [SerializeField] private TextMeshProUGUI text = default;
    [SerializeField] private Image image = default;

    private Queue<string> sentences;

    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("Open", true);
        Debug.Log("Start of conv");
        sentences = new Queue<string>();
        image.sprite = dialogue.image;
        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            EndDialogue();
            return;
        }
        
        text.text = sentences.Dequeue();
    }

    private void EndDialogue()
    {
        anim.SetBool("Open", false);
        Debug.Log("end of conv");
    }
}
