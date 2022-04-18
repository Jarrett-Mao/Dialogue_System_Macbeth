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
    public Animator animator;

    // private Queue<string> sentences;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void displayOptions(Option dialogue){

        nameText.text = dialogue.name;
        
        optionOne.text = dialogue.optionsList[0];
        optionTwo.text = dialogue.optionsList[1];
        optionThree.text = dialogue.optionsList[2];

    }
}
