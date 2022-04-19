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
    public OptionsManager optionManager;

    private Queue<string> sentences;

    //used to store options to dialogue 
    public Dictionary<string, string[]> triggersToDialogue = new Dictionary<string, string[]>{
        {"What was that?", new [] {"Macbeth", "[Within] Who's there? what, ho!"}},
        {"[Within] Who's there? what, ho!", new [] {"Lady Macbeth", "Alack, I am afraid they have awaked,",
         "And 'tis not done. The attempt and not the deed", "Confounds us. Hark! I laid their daggers ready;",
         "He could not miss 'em. Had he not resembled", "My father as he slept, I had done't."}},
    };

    // Start is called before the first frame update
    void Start(){
        sentences = new Queue<string>();
    }

    public void startDialogue (Dialogue dialogue){ 

        animator.SetBool("isOpen", true); //needs to be removed or edited

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

        //if last sentence of dialogue is a key run displaySentence again
        //else open the options menu
        bool keyExists = triggersToDialogue.ContainsKey(dialogueText.text);   
        if (keyExists){
            Dialogue dialogue = optionManager.loadSentences(dialogueText.text);
            startDialogue(dialogue);
        }
        else {
            //opens the options box
            optionsAnimator.SetBool("isOpen", true);

            var tempOptions = new Option();
        
            //setting temp options variable to contain option dialogue from dictionary
            tempOptions.optionsList = optionManager.turnsToOps[optionManager.turnTracker];

            //use function to display all options from OptionsManager
            FindObjectOfType<OptionsManager>().displayOptions(tempOptions);
        }
        

    }


}