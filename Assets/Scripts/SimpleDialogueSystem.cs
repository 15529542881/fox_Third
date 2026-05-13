using MalbersAnimations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogueSystem : MonoBehaviour
{
    [Header("UI")]
    public Text dialogueText;       
    public GameObject dialogPanel;  

    public enum DialogueType
    {
        NULL,
        Ember_First,
        Bear_Task, Bear_Doing, Bear_Complete,
        Rabbit_Task, Rabbit_Doing, Rabbit_Complete,
        Owl_Task, Owl_Doing, Owl_Complete,
        Beaver_Task, Beaver_Doing, Beaver_Complete,
        Raccoon_Task, Raccoon_Doing, Raccoon_Complete,
        Campfire_End
    }

    private string[] currentDialogueLines; 
    private int currentLineIndex = 0;      


    public DialogueType currentDialogueType = DialogueType.NULL;
    void Start()
    {
        //dialogPanel.SetActive(false);

    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Return) )&& dialogPanel.activeInHierarchy)
        {
            ShowNextLine();
        }
    }

    public void ShowDialogue(DialogueType type)
    {
        Debug.Log(type);
        GameManager.Instance.SetCursorVisibility(true);
        currentLineIndex = 0;
        LoadDialogueContent(type);
        dialogPanel.SetActive(true);
        ShowCurrentLine();
    }

    private void LoadDialogueContent(DialogueType type)
    {
        currentDialogueType = type;
        switch (type)
        {
            case DialogueType.Ember_First:
                currentDialogueLines = new string[]
                {
            "Hey… it’s my birthday today.",
            "But… why is no one here?",
            "Hmm… something must be keeping everyone busy.",
            "I should go into the forest and find them."
                };
                break;
            case DialogueType.Bear_Task:
                currentDialogueLines = new string[]
                {
            "Bear: Oh! Ember! Happy birthday!",
            "Bear: I was going to bake you a huge birthday cake…",
            "Bear: But while I was preparing the ingredients, I… had a few bites.",
            "Bear: And, well… I ate the last basket of berries.",
            "Bear: If you could bring me 5 round, juicy red berries…",
            "Bear: I promise I won’t eat them this time!"
                };
                break;
            case DialogueType.Bear_Doing:
                currentDialogueLines = new string[]
                {
            "Bear: Mmm… I can almost smell berry pie already…",
            "Bear: Just a few more, right?"
                };
                break;
            case DialogueType.Bear_Complete:
                currentDialogueLines = new string[]
                {
            "Bear: Wonderful! These berries look even sweeter than I remember!",
            "Bear: You head back to the campsite and get the fire ready,the cake will be there in no time!"
                };
                break;

            case DialogueType.Rabbit_Task:
                currentDialogueLines = new string[]
                {
            "Rabbit: Ember! Shhh—keep your voice down!",
            "Rabbit: Do you see those things hanging in the trees?",
            "Rabbit: They’ve got these big eyes… staring at the path…",
            "Rabbit: I’m too scared to go past them!",
            "Rabbit: You’ve got that wooden slingshot, right?",
            "Rabbit: If you get rid of those 3 balloons,I can safely make it to your party."
                };
                break;
            case DialogueType.Rabbit_Doing:
                currentDialogueLines = new string[]
                {
            "Rabbit: Are they still there?",
            "Rabbit: I feel like they're secretly watching me..."
                };
                break;
            case DialogueType.Rabbit_Complete:
                currentDialogueLines = new string[]
                {
            "Rabbit: Pop! Pop!",
            "Rabbit: Oh… they were just balloons…",
            "Rabbit: You're so brave!",
            "Rabbit: I’ll head to the camp right now!"
                };
                break;

            case DialogueType.Owl_Task:
                currentDialogueLines = new string[]
                {
            "Owl: Ah… little fox. Happy birthday.",
            "Owl: I meant to head to the camp earlier…",
            "Owl: But once it gets dark, these old eyes can’t see a thing.",
            "Owl: And my lantern has long gone out.",
            "Owl: Do you see those little lights glowing in the grass?",
            "Owl: They’re fireflies. They’re drawn to warm-hearted foxes like you.",
            "Owl: If you lead them over to me, I’ll be able to find my way back to camp."
                };
                break;
            case DialogueType.Owl_Doing:
                currentDialogueLines = new string[]
                {
            "Owl: Take your time, little fox...",
            "Owl: I’m in no hurry."
                };
                break;
            case DialogueType.Owl_Complete:
                currentDialogueLines = new string[]
                {
            "Owl: Ah... What a warm light.",
            "Owl: Thank you, little fox.",
            "Owl: I’ll make my way to the camp."
                };
                break;

            case DialogueType.Beaver_Task:
                currentDialogueLines = new string[]
                {
            "Beaver: Hey! Happy birthday, kid!",
            "Beaver: I was building you a party stage…",
            "Beaver: But the wind knocked my hammer onto those floating logs in the river",
            "Beaver: My tail’s too clumsy to balance on those wobbly logs.",
            "Beaver: You’re quick on your feet—could you grab my hammer for me?"
                };
                break;
            case DialogueType.Beaver_Doing:
                currentDialogueLines = new string[]
                {
            "Beaver: Watch your step!",
            "Beaver: Don't fall into the water and get your beautiful fur wet!"
                };
                break;
            case DialogueType.Beaver_Complete:
                currentDialogueLines = new string[]
                {
            "Beaver: Haha! That’s my trusty hammer!",
            "Beaver: Now I can finish the stage.",
            "Beaver: You’ve gotta stand on it tonight for your birthday!"
                };
                break;

            case DialogueType.Raccoon_Task:
                currentDialogueLines = new string[]
                {
            "Raccoon: birthday star!",
            "Raccoon: I hear there’s a party tonight.",
            "Raccoon: Good thing I’ve got the finest fireworks in the forest.",
            "Raccoon: But a trader never deals for free.",
            "Raccoon: Bring me 5 shiny stones…this firework is yours."
                };
                break;
            case DialogueType.Raccoon_Doing:
                currentDialogueLines = new string[]
                {
            "Raccoon: Only the sparkliest things deserve a party.",
            "Raccoon: Got enough in your bag?"
                };
                break;
            case DialogueType.Raccoon_Complete:
                currentDialogueLines = new string[]
                {
            "Raccoon: Deal!",
            "Raccoon: This one explodes into a fox shape in the sky.",
            "Raccoon: Light it when the campfire’s at its brightest."
                };
                break;

            case DialogueType.Campfire_End:
                currentDialogueLines = new string[]
                {
            "Little Fox Ember: Everyone's here...",
            "Little Fox Ember: I guess the best birthday gift…is spending this night with friends."
                };
                break;
        }
    }

    private void ShowCurrentLine()
    {
        if (currentDialogueLines != null && currentLineIndex < currentDialogueLines.Length)
        {
            dialogueText.text = currentDialogueLines[currentLineIndex];
        }
    }

    private void ShowNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex < currentDialogueLines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            GameManager.Instance.SetCursorVisibility(false);
            dialogPanel.SetActive(false);
            currentLineIndex = 0; 
            switch (currentDialogueType)
            {
                case DialogueType.Ember_First:
                    GameManager.Instance.ShowHint("Go to the forest to look for friends");
                    break;
                case DialogueType.Bear_Task:
                    GameManager.Instance.ShowHint("New Task Received");
                    GameManager.Instance.jiangGuoObjs.SetActive(true);
                    GameManager.Instance.taskXiongObj.SetActive(true);
                    break;
                case DialogueType.Bear_Complete:
                    GameManager.Instance.ShowHint("Complete the task");
                    GameManager.Instance.bagControl.ClearJiangguo();
                    GameManager.Instance.taskXiongObj.GetComponent<Toggle>().isOn = true;
                    GameManager.Instance.allTasksDialogCompleted = true;
                    if (!GameManager.Instance.CheckAllTaskFinish())
                    {
                        GameManager.Instance.allTasksDialogCompleted = false;
                    }
                    break;
                case DialogueType.Rabbit_Task:
                    GameManager.Instance.ShowHint("New Task Received");
                    GameManager.Instance.qiQiuObjs.SetActive(true);
                    GameManager.Instance.zhunXingObj.SetActive(true);
                    GameManager.Instance.taskTuziObj.SetActive(true);
                    break;
                case DialogueType.Rabbit_Complete:
                    GameManager.Instance.ShowHint("Complete the task");
                    GameManager.Instance.taskTuziObj.GetComponent<Toggle>().isOn = true;
                    GameManager.Instance.allTasksDialogCompleted = true;
                    if (!GameManager.Instance.CheckAllTaskFinish())
                    {
                        GameManager.Instance.allTasksDialogCompleted = false;
                    }
                    break;
                case DialogueType.Owl_Task:
                    GameManager.Instance.ShowHint("New Task Received.");
                    GameManager.Instance.taskMaotouyingObj.SetActive(true);
                    GameManager.Instance.yinghuochongObj1.SetActive(true);
                    break;
                case DialogueType.Owl_Complete:
                    GameManager.Instance.ShowHint("Complete the task");
                    GameManager.Instance.taskMaotouyingObj.GetComponent<Toggle>().isOn = true;
                    GameManager.Instance.allTasksDialogCompleted = true;
                    if (!GameManager.Instance.CheckAllTaskFinish())
                    {
                        GameManager.Instance.allTasksDialogCompleted = false;
                    }
                    break;
                case DialogueType.Beaver_Task:
                    GameManager.Instance.ShowHint("New Task Received.");
                    GameManager.Instance.taskHailiObj.SetActive(true);
                    GameManager.Instance.chuizi.SetActive(true);
                    break;
                case DialogueType.Beaver_Complete:
                    GameManager.Instance.ShowHint("Complete the task");
                    GameManager.Instance.taskHailiObj.GetComponent<Toggle>().isOn = true;
                    GameManager.Instance.allTasksDialogCompleted = true;
                    if (!GameManager.Instance.CheckAllTaskFinish())
                    {
                        GameManager.Instance.allTasksDialogCompleted = false;
                    }
                    break;
                case DialogueType.Raccoon_Task:
                    GameManager.Instance.ShowHint("New Task Received.");
                    GameManager.Instance.baoshis.SetActive(true);
                    GameManager.Instance.taskHuanXiongObj.SetActive(true);
                    break;
                case DialogueType.Raccoon_Complete:
                    GameManager.Instance.ShowHint("Complete the task");
                    GameManager.Instance.taskHuanXiongObj.GetComponent<Toggle>().isOn = true;
                    GameManager.Instance.allTasksDialogCompleted = true;
                    if (!GameManager.Instance.CheckAllTaskFinish())
                    {
                        GameManager.Instance.allTasksDialogCompleted = false;
                    }
                    break;
                case DialogueType.Campfire_End:
                    GameManager.Instance.endBtn.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    public void ShowBearFirstDialogue() => ShowDialogue(DialogueType.Bear_Complete);
    public void ShowCampfireEndDialogue() => ShowDialogue(DialogueType.Campfire_End);
}