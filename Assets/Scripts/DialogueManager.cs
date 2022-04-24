using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator dialogueAnimator;
    public Animator curLeftAnimator;
    public Animator curRightAnimator;
    public Animator optionsAnimator;
    public GameObject startButton;
    public GameObject quitButton;
    public GameObject contButton;
    public OptionsManager optionManager;
    private bool quitButtonOn = false; 

    private Queue<string> sentences;

    //used to store options to dialogue 
    public Dictionary<string, string[]> triggersToDialogue = new Dictionary<string, string[]>{
        //testing end
        // {"[Enter LADY MACBETH]", new [] {"MACBETH", "I don't want to see who I've become..."}},

        {"[Enter LADY MACBETH]", new [] {"LADY MACBETH", "That which hath made them drunk hath made me bold;", "What hath quench'd them hath given me fire.",
        "Hark! Peace!", "It was the owl that shriek'd, the fatal bellman,", "Which gives the stern'st good-night. He is about it:",
        "The doors are open; and the surfeited grooms", "Do mock their charge with snores: I have drugg'd", "their possets,", "That death and nature do contend about them,", 
        "Whether they live or die."}},
        {"What was that?", new [] {"MACBETH", "[Within] Who's there? what, ho!"}},
        {"[Within] Who's there? what, ho!", new [] {"LADY MACBETH", "Alack, I am afraid they have awaked,",
         "And 'tis not done. The attempt and not the deed", "Confounds us. Hark! I laid their daggers ready;",
         "He could not miss 'em. Had he not resembled", "My father as he slept, I had done't."}},
         {"My father as he slept, I had done't.", new [] {"", "[Enter MACBETH]"}},
         {"[Enter MACBETH]", new [] {"LADY MACBETH", "My husband!"}},
        {"It's done.", new [] {"MACBETH", "I have done the deed. Didst thou not hear a noise?"}},
        {"I have done the deed. Didst thou not hear a noise?", new [] {"LADY MACBETH", "I heard the owl scream and the crickets cry.", "Did not you speak?"}},
        {"What time?", new [] {"MACBETH", "When?"}}, {"When?", new [] {"LADY MACBETH", "Now."}},
        {"When I went down?", new [] {"MACBETH", "As I descended?"}}, {"As I descended?", new [] {"LADY MACBETH", "Ay."}},
        {"Listen!", new [] {"MACBETH", "Hark!", "Who lies i' the second chamber?"}}, {"Who lies i' the second chamber?", new [] {"LADY MACBETH", "Donalbain."}},
        {"What have I done?", new [] {"MACBETH", "This is a sorry sight.", "[Looking on his hands]"}},
        {"[Looking on his hands]", new [] {"LADY MACBETH", "A foolish thought, to say a sorry sight."}},
        {"I heard someone sleep talking!", new [] {"MACBETH", "There's one did laugh in's sleep, and one cried", "\'" + "Murder!" + "\'", 
        "That they did wake each other: I stood and heard them:", "But they did say their prayers, and address'd them", "Again to sleep."}}, 
        {"Again to sleep.", new [] {"LADY MACBETH","There are two lodged together."}},
        {"They said a prayer to ward off evil...", new [] {"MACBETH", "One cried 'God bless us!' and 'Amen' the other;", "As they had seen me with these hangman's hands.",
        "Listening their fear, I could not say 'Amen,'", "When they did say 'God bless us!'"}},
        {"When they did say 'God bless us!'", new [] {"LADY MACBETH", "Consider it not so deeply."}},
        {"Why couldn't I say a blessing?", new [] {"MACBETH", "But wherefore could not I pronounce 'Amen'?", "I had most need of blessing, and 'Amen'", "Stuck in my throat."}},
        {"Stuck in my throat.", new [] {"LADY MACBETH", "These deeds must not be thought", "After these ways; so, it will make us mad."}},
        {"I will never sleep again!", new [] {"MACBETH", "Methought I heard a voice cry 'Sleep no more!", "Macbeth does murder sleep', the innocent sleep,",
        "Sleep that knits up the ravell'd sleeve of care,", "The death of each day's life, sore labour's bath,", "Balm of hurt minds, great nature's second course,",
        "Chief nourisher in life's feast,--"}},
        {"Chief nourisher in life's feast,--", new [] {"LADY MACBETH", "What do you mean?"}},
        {"The ghosts will not let me rest!", new [] {"MACBETH", "Still it cried 'Sleep no more!' to all the house:", "'Glamis hath murder'd sleep, and therefore Cawdor", 
        "Shall sleep no more; Macbeth shall sleep no more.'"}},
        {"Shall sleep no more; Macbeth shall sleep no more.'", new [] {"LADY MACBETH", "Who was it that thus cried? Why, worthy thane,", "You do unbend your noble strength, to think",
        "So brainsickly of things. Go get some water,", "And wash this filthy witness from your hand.", "Why did you bring these daggers from the place?",
        "They must lie there: go carry them; and smear", "The sleepy grooms with blood."}},
        {"I can't do this anymore!", new [] {"MACBETH", "I'll go no more:", "I am afraid to think what I have done;", "Look on't again I dare not."}},
        {"Every noise sets me off... I don't even recognize myself anymore!", new [] {"LADY MACBETH", "Infirm of purpose!", "Give me the daggers: the sleeping and the dead", "Are but as pictures: 'tis the eye of childhood",
        "That fears a painted devil. If he do bleed,", "I'll gild the faces of the grooms withal;", "For it must seem their guilt."}},
        {"For it must seem their guilt.", new [] {"", "[Exit. Knocking within]"}}, 
        {"[Exit. Knocking within]", new [] {"MACBETH", "Whence is that knocking?", "How is't with me, when every noise appals me?", "What hands are here? ha! they pluck out mine eyes.",
        "Will all great Neptune's ocean wash this blood", "Clean from my hand? No, this my hand will rather", "The multitudinous seas in incarnadine,", "Making the green one red."}},
        {"Making the green one red.", new [] {"", "[Re-enter LADY MACBETH]"}},
        {"[Re-enter LADY MACBETH]", new [] {"LADY MACBETH", "My hands are of your colour; but I shame", "To wear a heart so white."}},
        {"...", new [] {"", "[Knocking within]"}},
        {"[Knocking within]", new [] {"LADY MACBETH", "I hear a knocking", "At the south entry: retire we to our chamber;", "A little water clears us of this deed:", "How easy is it, then! Your constancy",
        "Hath left you unattended."}},
        {"... I...", new [] {"", "[Knocking within] "}},
        {"[Knocking within] ", new [] {"LADY MACBETH", "Hark! more knocking.", "Get on your nightgown, lest occasion call us,", "And show us to be watchers. Be not lost",
        "So poorly in your thoughts."}},
        {"I don't want to see who I've become...", new [] {"MACBETH", "To know my deed, 'twere best not know myself."}}, 
        {"To know my deed, 'twere best not know myself.", new [] {"", "[Knocking within]  "}},
        {"[Knocking within]  ", new [] {"MACBETH", "Wake Duncan with thy knocking! I would thou couldst!"}} 
    };
    //should the scene transitions ie. [Exit. Knocking within] be separated from the dialogue and made more unique? are they going to be italicized?

    // Start is called before the first frame update
    void Start(){
        sentences = new Queue<string>();
        quitButton.gameObject.SetActive(false);
    }

    public void startDialogue (Dialogue dialogue){ 

        dialogueAnimator.SetBool("isOpen", true);
        curLeftAnimator.SetBool("isOpen", true);
        curRightAnimator.SetBool("isOpen", true);

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
        else if (dialogueText.text.Contains("Wake")){
            curLeftAnimator.SetBool("isOpen", false);
            curRightAnimator.SetBool("isOpen", false);
            contButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(true);
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

    public void ExitGame(){
        Application.Quit();
    }
}