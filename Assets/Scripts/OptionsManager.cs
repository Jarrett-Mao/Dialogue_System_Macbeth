using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Text nameText;
    public Text optionOne;
    public Text optionTwo;
    public Text optionThree;
    public GameObject buttonOne;
    public GameObject buttonTwo;
    public GameObject buttonThree;
    public Animator animator;
    public int turnTracker = 1;
    private int numOptions;

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

        //number of options each turn gotten by checking the turns dictionary
        numOptions = turnsToNumOps[turnTracker];
        buttonTwo.gameObject.SetActive(true);
        buttonThree.gameObject.SetActive(true);

        nameText.text = options.name;
        if (numOptions == 3){
            optionOne.text = options.optionsList[0];
            optionTwo.text = options.optionsList[1];
            optionThree.text = options.optionsList[2];
        }
        else if (numOptions == 2){
            buttonThree.gameObject.SetActive(false);
            optionOne.text = options.optionsList[0];
            optionTwo.text = options.optionsList[1];
        }     
        else {
            buttonTwo.gameObject.SetActive(false);
            buttonThree.gameObject.SetActive(false);
            optionOne.text = options.optionsList[0];
        }   
        

        // //debug turnsToOps string array
        // string[] test = turnsToOps[5];
        // foreach (string sentence in test){
        //     Debug.Log(sentence);
        // }
    }

    //will have to be reworked to include tracking dialogue 
    //should rename it when the time comes
    public void closeOptionsBox(){ 
        animator.SetBool("isOpen", false);
        turnTracker += 1; 
    }
}
