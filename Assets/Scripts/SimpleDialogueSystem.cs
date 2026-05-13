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
            "Hey¡­ it¡¯s my birthday today.",
            "But why is no one here by the campfire?",
            "Hmm¡­ they must be held up by something.",
            "I¡¯ll go into the forest and find them."
                };
                break;
            case DialogueType.Bear_Task:
                currentDialogueLines = new string[]
                {
            "Bear: Oh! Little Fox! Happy Birthday!",
            "Bear: I was going to bake you a huge birthday cake¡­",
            "Bear: But while I was preparing the ingredients, I couldn¡¯t help tasting a few¡­",
            "Bear: And well¡­ I ended up eating the last basket of berries.",
            "Bear: If you could help me gather 5 nice, plump berries",
            "Bear: I promise I won¡¯t sneak a bite this time!"
                };
                break;
            case DialogueType.Bear_Doing:
                currentDialogueLines = new string[]
                {
            "Bear: I can almost smell the berry pie already¡­",
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
            "Rabbit: Ember! Shhh¡ªkeep your voice down!",
            "Rabbit: Did you see those things hanging from the tree?",
            "Rabbit: They're staring at the path with their big eyes...",
            "Rabbit: I dare not go over there at all.",
            "Rabbit: You have that wooden slingshot with you, right?",
            "Rabbit: If you get rid of those 3 balloons, I can safely go to your birthday party."
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
            "Rabbit: They were just balloons that pop...",
            "Rabbit: You're so brave, little fox!",
            "Rabbit: I'll come to the campsite to find you right away!"
                };
                break;

            case DialogueType.Owl_Task:
                currentDialogueLines = new string[]
                {
            "Owl: Oh... Little Fox, happy birthday.",
            "Owl: This old man was going to get to the campsite early.",
            "Owl: But once it gets dark, these old eyes of mine can't see a thing.",
            "Owl: The light in my lantern has long since gone out.",
            "Owl: Did you see those little glowing guys in the grass over there? Those are fireflies.",
            "Owl: They love warm little foxes, and they'll follow you as long as you get close.",
            "Owl: Help me lead them to my side, and I'll be able to see the way back to the campsite."
                };
                break;
            case DialogueType.Owl_Doing:
                currentDialogueLines = new string[]
                {
            "Owl: Take your time, little fox...",
            "Owl: Old friends don't like being rushed."
                };
                break;
            case DialogueType.Owl_Complete:
                currentDialogueLines = new string[]
                {
            "Owl: Ah... What a warm light.",
            "Owl: Thank you, little fox.",
            "Owl: I'll come to the campsite to find you."
                };
                break;

            case DialogueType.Beaver_Task:
                currentDialogueLines = new string[]
                {
            "Beaver: Hey! Happy birthday, little one!",
            "Beaver: I was just about to build a party stage for you.",
            "Beaver: But a gust of wind blew my hammer onto the driftwood in the middle of the river.",
            "Beaver: My tail is too clumsy to jump across those wobbly logs.",
            "Beaver: You're quick and nimble¡ªcan you get my hammer back for me?"
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
            "Beaver: Hahaha! There's my old buddy!",
            "Beaver: The stage will be finished in no time.",
            "Beaver: Tonight, you must stand on it to celebrate your birthday!"
                };
                break;

            case DialogueType.Raccoon_Task:
                currentDialogueLines = new string[]
                {
            "Raccoon: Hehe, little birthday star!",
            "Raccoon: I heard there's a party tonight.",
            "Raccoon: Perfect timing¡ªI have the most dazzling fireworks in the forest right here.",
            "Raccoon: But a merchant never trades for nothing.",
            "Raccoon: If you exchange 3 shiny stones for it, this firework is yours."
                };
                break;
            case DialogueType.Raccoon_Doing:
                currentDialogueLines = new string[]
                {
            "Raccoon: Shiny things are the only fit for a party.",
            "Raccoon: Do you have enough in your bag?"
                };
                break;
            case DialogueType.Raccoon_Complete:
                currentDialogueLines = new string[]
                {
            "Raccoon: Deal!",
            "Raccoon: This firework will burst into the shape of a fox in the night sky.",
            "Raccoon: Remember to light it when the bonfire is at its brightest."
                };
                break;

            case DialogueType.Campfire_End:
                currentDialogueLines = new string[]
                {
            "Little Fox Ember: Everyone's here...",
            "Little Fox Ember: Turns out, the best birthday gift is lighting up this night together with friends."
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
                    GameManager.Instance.ShowHint("Received a new task.");
                    GameManager.Instance.jiangGuoObjs.SetActive(true);
                    GameManager.Instance.taskXiongObj.SetActive(true);
                    break;
                case DialogueType.Bear_Complete:
                    GameManager.Instance.ShowHint("fulfil a task");
                    GameManager.Instance.bagControl.ClearJiangguo();
                    GameManager.Instance.taskXiongObj.GetComponent<Toggle>().isOn = true;
                    GameManager.Instance.allTasksDialogCompleted = true;
                    if (!GameManager.Instance.CheckAllTaskFinish())
                    {
                        GameManager.Instance.allTasksDialogCompleted = false;
                    }
                    break;
                case DialogueType.Rabbit_Task:
                    GameManager.Instance.ShowHint("Received a new task.");
                    GameManager.Instance.qiQiuObjs.SetActive(true);
                    GameManager.Instance.zhunXingObj.SetActive(true);
                    GameManager.Instance.taskTuziObj.SetActive(true);
                    break;
                case DialogueType.Rabbit_Complete:
                    GameManager.Instance.ShowHint("fulfil a task");
                    GameManager.Instance.taskTuziObj.GetComponent<Toggle>().isOn = true;
                    GameManager.Instance.allTasksDialogCompleted = true;
                    if (!GameManager.Instance.CheckAllTaskFinish())
                    {
                        GameManager.Instance.allTasksDialogCompleted = false;
                    }
                    break;
                case DialogueType.Owl_Task:
                    GameManager.Instance.ShowHint("Received a new task.");
                    GameManager.Instance.taskMaotouyingObj.SetActive(true);
                    GameManager.Instance.yinghuochongObj1.SetActive(true);
                    break;
                case DialogueType.Owl_Complete:
                    GameManager.Instance.ShowHint("fulfil a task");
                    GameManager.Instance.taskMaotouyingObj.GetComponent<Toggle>().isOn = true;
                    GameManager.Instance.allTasksDialogCompleted = true;
                    if (!GameManager.Instance.CheckAllTaskFinish())
                    {
                        GameManager.Instance.allTasksDialogCompleted = false;
                    }
                    break;
                case DialogueType.Beaver_Task:
                    GameManager.Instance.ShowHint("Received a new task.");
                    GameManager.Instance.taskHailiObj.SetActive(true);
                    GameManager.Instance.chuizi.SetActive(true);
                    break;
                case DialogueType.Beaver_Complete:
                    GameManager.Instance.ShowHint("fulfil a task");
                    GameManager.Instance.taskHailiObj.GetComponent<Toggle>().isOn = true;
                    GameManager.Instance.allTasksDialogCompleted = true;
                    if (!GameManager.Instance.CheckAllTaskFinish())
                    {
                        GameManager.Instance.allTasksDialogCompleted = false;
                    }
                    break;
                case DialogueType.Raccoon_Task:
                    GameManager.Instance.ShowHint("Received a new task.");
                    GameManager.Instance.baoshis.SetActive(true);
                    GameManager.Instance.taskHuanXiongObj.SetActive(true);
                    break;
                case DialogueType.Raccoon_Complete:
                    GameManager.Instance.ShowHint("fulfil a task");
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