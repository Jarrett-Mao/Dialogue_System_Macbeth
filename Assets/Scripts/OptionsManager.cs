﻿using System.Collections;
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
    public Button buttonOne;
    public Button buttonTwo;
    public Button buttonThree;
    public Animator animator;
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

    //used to store what is said in the turns
    public Dictionary<int, string[]> turnsToOps = new Dictionary<int, string[]>{
        {1, new [] {"What was that?"}},
        {2, new [] {"It's done."}},
        {3, new [] {"What time?", "When I went down?", "Listen!"}},
        {4, new [] {"What have I done?"}},
        {5, new [] {"I heard someone talking in their sleep!", "They said a prayer to ward off evil..."}},
        {6, new [] {"Why couldn't I say a blessing?"}},
        {7, new [] {"I will never sleep again!", "The ghosts will not let me rest!"}},
        {8, new [] {"I can't do this anymore!"}},
        {9, new [] {"Every noise sets me off... I don't even recognize myself anymore!"}},
        {10, new [] {"...", "..."}},
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
        
        //needs to be fixed later when dialogue box is 
        // implemented needs to account for options closing

        if (numClicked == numOptions){
            closeOptionsBox();
        }
        // Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        button.interactable = false;
    }

    //will have to be reworked to include tracking dialogue 
    //should rename it when the time comes
    public void closeOptionsBox(){ 
        animator.SetBool("isOpen", false);
        turnTracker += 1; 
        numClicked = 0;

    }

    private void resetButtons(){
        //reset buttons after disabling
        buttonOne.interactable = true;
        buttonTwo.interactable = true;
        buttonThree.interactable = true;
    }
}
