using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Animator optionsAnimator;
    public GameObject startButton;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start(){
        sentences = new Queue<string>();
    }

    public void startDialogue (Dialogue dialogue){ 

        animator.SetBool("isOpen", true); //needs to be removed or editedw

        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

        startButton.gameObject.SetActive(false);
    }

    public void DisplayNextSentence(){
        if (sentences.Count == 0){ 
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();  
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence){
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(.025f);
        }
    }

    void EndDialogue(){
        
        optionsAnimator.SetBool("isOpen", true);
        var options = new Option();
        options.optionsList = new string[] {"penis", "cock", "dick"};
        FindObjectOfType<OptionsManager>().displayOptions(options);
    }


}