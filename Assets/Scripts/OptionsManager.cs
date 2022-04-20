using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsManager : MonoBehaviour
{
    public Text nameText;
    public Text optionOne;
    public Text optionTwo;
    public Text optionThree;
    public GameObject gameButtonOne;
    public GameObject gameButtonTwo;
    public GameObject gameButtonThree;
    public Animator animator;
    public DialogueManager dialogueManager;
    public int turnTracker = 1;
    private int numOptions; 
    private int numClicked = 0;

    // used to track the number of options every turn
    public Dictionary<int, int> turnsToNumOps = new Dictionary<int, int>{
        {1, 1},
        {2, 1},
        {3, 3},
        {4, 1},
        {5, 2},
        {6, 1},
        {7, 2},
        {8, 1},
        {9, 1},
        {10, 2},
        {11, 1}
    };

    //used to store what options is said in the turns
    public Dictionary<int, string[]> turnsToOps = new Dictionary<int, string[]>{
        {1, new [] {"What was that?"}},
        {2, new [] {"It's done."}},
        {3, new [] {"What time?", "When I went down?", "Listen!"}},
        {4, new [] {"What have I done?"}},
        {5, new [] {"I heard someone sleep talking!", "They said a prayer to ward off evil..."}},
        {6, new [] {"Why couldn't I say a blessing?"}},
        {7, new [] {"I will never sleep again!", "The ghosts will not let me rest!"}},
        {8, new [] {"I can't do this anymore!"}},
        {9, new [] {"Every noise sets me off... I don't even recognize myself anymore!"}},
        {10, new [] {"...", "... I..."}},
        {11, new [] {"I don't want to see who I've become..."}},
    }; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void displayOptions(Option options){
        //needs to be fixed when dialogue box is implemented
        resetButtons();

        //number of options each turn gotten by checking the turns dictionary
        numOptions = turnsToNumOps[turnTracker];

        //resets buttons after they are turned invisible
        gameButtonTwo.gameObject.SetActive(true);
        gameButtonThree.gameObject.SetActive(true);

        nameText.text = options.name;
        if (numOptions == 3){
            optionOne.text = options.optionsList[0];
            optionTwo.text = options.optionsList[1];
            optionThree.text = options.optionsList[2];
        }
        else if (numOptions == 2){
            gameButtonThree.gameObject.SetActive(false);
            optionOne.text = options.optionsList[0];
            optionTwo.text = options.optionsList[1];
        }     
        else {
            gameButtonTwo.gameObject.SetActive(false);
            gameButtonThree.gameObject.SetActive(false);
            optionOne.text = options.optionsList[0];
        }   
        

        // //debug turnsToOps string array
        // string[] test = turnsToOps[5];
        // foreach (string sentence in test){
        //     Debug.Log(sentence);
        // }
    }

    public void buttonClicked(Button button){
        
        numClicked += 1;
        button.interactable = false;

        // Debug.Log(button.GetComponentInChildren<Text>().text);
        string currButtonText = button.GetComponentInChildren<Text>().text;

        //needs to be fixed later when dialogue box is 
        // implemented needs to account for options closing
        if (numClicked == numOptions){
            closeOptionsBox();
        }

        Dialogue dialogue = loadSentences(currButtonText);

        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }

    //will have to be reworked to include tracking dialogue 
    //should rename it when the time comes
    public void closeOptionsBox(){ 
        animator.SetBool("isOpen", false);
        turnTracker += 1; 

        //this will need to be changed to account for dialogue box
        numClicked = 0;

    }

    private void resetButtons(){
        //reset buttons after disabling
        Button buttonOne = gameButtonOne.GetComponent<Button>();
        Button buttonTwo = gameButtonTwo.GetComponent<Button>();
        Button buttonThree = gameButtonThree.GetComponent<Button>();

        buttonOne.interactable = true;
        buttonTwo.interactable = true;
        buttonThree.interactable = true;
    }

    //loads the next sentences will be used to either load the last dialogue text
    //or the options text
    public Dialogue loadSentences(string prevText){
        Dialogue dialogue = new Dialogue();

        //temp holds the name of the speaker and their dialogue using the current
        //button's text
        string[] nameSentence = dialogueManager.triggersToDialogue[prevText]; 
        dialogue.name = nameSentence[0];
        
        // initializes the string array in dialogue to correct size
        dialogue.sentences = new string[nameSentence.Length-1]; 
        
        //enqueues all of the dialogue in the triggersToDialogue dictionary 
        for (int i = 1; i < nameSentence.Length; i++){
            dialogue.sentences[i-1] = nameSentence[i]; 
        }

        return dialogue;
    }
}
