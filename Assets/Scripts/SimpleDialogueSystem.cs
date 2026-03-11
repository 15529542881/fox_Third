using MalbersAnimations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 简易对话系统 - 所有对话内容整合在一个脚本中
/// </summary>
public class SimpleDialogueSystem : MonoBehaviour
{
    [Header("UI组件引用")]
    public Text dialogueText;       // 显示对话的Text组件
    public GameObject dialogPanel;  // 对话框面板

    // 对话类型枚举（所有可触发的对话）
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

    private string[] currentDialogueLines; // 当前要显示的对话内容
    private int currentLineIndex = 0;      // 当前显示的行索引


    public DialogueType currentDialogueType = DialogueType.NULL;
    void Start()
    {
        // 初始隐藏对话框
        //dialogPanel.SetActive(false);

    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Return) )&& dialogPanel.activeInHierarchy)
        {
            ShowNextLine();
        }
    }

    /// <summary>
    /// 外部调用：显示指定类型的对话
    /// </summary>
    /// <param name="type">要显示的对话类型</param>
    public void ShowDialogue(DialogueType type)
    {
        GameManager.Instance.SetCursorVisibility(true);
        // 重置索引
        currentLineIndex = 0;
        // 根据类型加载对应的对话内容
        LoadDialogueContent(type);
        // 显示对话框和第一句对话
        dialogPanel.SetActive(true);
        ShowCurrentLine();
    }

    /// <summary>
    /// 加载指定类型的对话内容
    /// </summary>
    private void LoadDialogueContent(DialogueType type)
    {
        currentDialogueType = type;
        switch (type)
        {
            // 贪吃的熊
            case DialogueType.Ember_First:
                currentDialogueLines = new string[]
                {
            "Hey… it’s my birthday today.",
            "But why is no one here by the campfire?",
            "Hmm… they must be held up by something.",
            "I’ll go into the forest and find them."
                };
                break;
            case DialogueType.Bear_Task:
                currentDialogueLines = new string[]
                {
            "Bear: Oh! Little Fox! Happy Birthday!",
            "Bear: I was going to bake you a huge birthday cake…",
            "Bear: But while I was preparing the ingredients, I couldn’t help tasting a few…",
            "Bear: And well… I ended up eating the last basket of berries.",
            "Bear: If you could help me gather 5 nice, plump berries",
            "Bear: I promise I won’t sneak a bite this time!"
                };
                break;
            case DialogueType.Bear_Doing:
                currentDialogueLines = new string[]
                {
            "Bear: I can almost smell the berry pie already…",
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

            // 胆小的兔子
            case DialogueType.Rabbit_Task:
                currentDialogueLines = new string[]
                {
            "Rabbit: Ember! Shhh—keep your voice down!",
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

            // 迷路的猫头鹰
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

            // 忙碌的海狸
            case DialogueType.Beaver_Task:
                currentDialogueLines = new string[]
                {
            "Beaver: Hey! Happy birthday, little one!",
            "Beaver: I was just about to build a party stage for you.",
            "Beaver: But a gust of wind blew my hammer onto the driftwood in the middle of the river.",
            "Beaver: My tail is too clumsy to jump across those wobbly logs.",
            "Beaver: You're quick and nimble—can you get my hammer back for me?"
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

            // 浣熊商人
            case DialogueType.Raccoon_Task:
                currentDialogueLines = new string[]
                {
            "Raccoon: Hehe, little birthday star!",
            "Raccoon: I heard there's a party tonight.",
            "Raccoon: Perfect timing—I have the most dazzling fireworks in the forest right here.",
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

            // 篝火结尾
            case DialogueType.Campfire_End:
                currentDialogueLines = new string[]
                {
            "Little Fox Ember: Everyone's here...",
            "Little Fox Ember: Turns out, the best birthday gift is lighting up this night together with friends."
                };
                break;
        }
    }

    /// <summary>
    /// 显示当前行的对话
    /// </summary>
    private void ShowCurrentLine()
    {
        if (currentDialogueLines != null && currentLineIndex < currentDialogueLines.Length)
        {
            dialogueText.text = currentDialogueLines[currentLineIndex];
        }
    }

    /// <summary>
    /// 点击按钮显示下一句
    /// </summary>
    private void ShowNextLine()
    {
        currentLineIndex++;

        // 如果还有对话就显示，没有就关闭对话框
        if (currentLineIndex < currentDialogueLines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            GameManager.Instance.SetCursorVisibility(false);
            dialogPanel.SetActive(false);
            currentLineIndex = 0; // 重置索引，方便下次使用
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
                    break;
                case DialogueType.Owl_Task:
                    GameManager.Instance.ShowHint("Received a new task.");
                    break;
                case DialogueType.Owl_Complete:
                    GameManager.Instance.ShowHint("fulfil a task");
                    break;
                case DialogueType.Beaver_Task:
                    GameManager.Instance.ShowHint("Received a new task.");
                    break;
                case DialogueType.Beaver_Complete:
                    GameManager.Instance.ShowHint("fulfil a task");
                    break;
                case DialogueType.Raccoon_Task:
                    GameManager.Instance.ShowHint("Received a new task.");
                    break;
                case DialogueType.Raccoon_Complete:
                    GameManager.Instance.ShowHint("fulfil a task");
                    break;
                case DialogueType.Campfire_End:
                    break;
                default:
                    break;
            }
        }
    }

    // 快捷调用示例（可以直接在Inspector绑定按钮点击）
    public void ShowBearFirstDialogue() => ShowDialogue(DialogueType.Bear_Complete);
    public void ShowCampfireEndDialogue() => ShowDialogue(DialogueType.Campfire_End);
}