using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SimpleDialogueSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    private bool isCursorVisible = false;

    public MalbersInput malbersInput;
    public MFreeLookCamera freeLookCamera;

    public SimpleDialogueSystem talkSystem;

    public BagControl bagControl;

    public GameObject hintObj;
    public Text hintText;

    public List<int> planState = new List<int>() { 1, 1, 1, 1, 1 };

    //renwu
    public int jiangGuoNum = 0;
    public int killNum = 0;// 
    public Text taskXiongText;
    public Text taskTuziText;
    public Text taskMaotouyingText;
    public Text taskHailiText;
    public Text taskHuanXiongText;

    public GameObject taskXiongObj;
    public GameObject taskTuziObj;
    public GameObject taskMaotouyingObj;
    public GameObject taskHailiObj;
    public GameObject taskHuanXiongObj;


    public GameObject miniMap;//
    //UI
    public GameObject setPanel;
    public GameObject bagPanel;
    public GameObject taskPanel;

    
    private List<GameObject> allJiangGuoObjects = new List<GameObject>();
    
    private GameObject currentInteractableJiangGuo = null;
    
    public Transform playerTransform;
    
    public float pickUpDistance = 1f;

      
    public GameObject chuiZiPickupTrigger; 
    public GameObject chuiZiDeliverTrigger; 
    public Transform respawnPoint; 
    private bool hasChuiZiInBag = false; 
    public float chuiZiPickDistance = 1f; 
    
    private bool showHammerPickHint = false;
    private bool showHammerGiveHint = false; 
    private bool isHintShowing = false;   
    private float hintDuration = 3f;      


    public GameObject stoneObjs;       
    private List<GameObject> allStones = new List<GameObject>();
    private GameObject currentInteractableStone = null;
    public int stoneCount = 0;         
    public int stoneNeed = 5;          
    public Transform raccoonTransform; 




    private GameObject currentInteractableYingHuoChong = null;

    public GameObject jiangGuoObjs;
    public GameObject qiQiuObjs;
    public GameObject zhunXingObj;
    public GameObject yinghuochongObj1;
    public GameObject yinghuochongObj2;
    public GameObject yinghuochongObj3;

    public GameObject baoshis;

    public GameObject chuizi;

    public Transform gameEndPlayerPos; 
    public GameObject campFireEndObj;  
    private bool isGameEnd = false;    

    bool CheckAllTaskFinish()
    {
        return planState[0] == 3
            && planState[1] == 3
            && planState[2] == 3
            && planState[3] == 3
            && planState[4] == 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetCursorVisibility(isCursorVisible);
        talkSystem.ShowDialogue(DialogueType.Ember_First);
        baoshis.SetActive(false);
        miniMap.SetActive(false);
        chuizi.SetActive(false);
        yinghuochongObj1.SetActive(false);
        yinghuochongObj2.SetActive(false);
        yinghuochongObj3.SetActive(false);
        zhunXingObj.SetActive(false);
        taskXiongObj.SetActive(false);
        qiQiuObjs.SetActive(false);
        taskTuziObj.SetActive(false);
        taskMaotouyingObj.SetActive(false);
        taskHailiObj.SetActive(false);
        taskHuanXiongObj.SetActive(false);
        jiangGuoObjs.SetActive(false);
        setPanel.SetActive(false);
        bagPanel.SetActive(false);
        taskPanel.SetActive(false);
        for (int i = 0; i < jiangGuoObjs.transform.childCount; i++)
        {
            allJiangGuoObjects.Add(jiangGuoObjs.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < stoneObjs.transform.childCount; i++)
        {
            allStones.Add(stoneObjs.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isCursorVisible = !isCursorVisible;
            SetCursorVisibility(isCursorVisible);
        }
  

        if (Input.GetKeyDown(KeyCode.M))
        {
            miniMap.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            miniMap.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!taskPanel.activeInHierarchy && !bagPanel.activeInHierarchy)
            {
                SetCursorVisibility(!setPanel.activeInHierarchy);
                setPanel.SetActive(!setPanel.activeInHierarchy);

            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!setPanel.activeInHierarchy && !bagPanel.activeInHierarchy)
            {
                SetCursorVisibility(!taskPanel.activeInHierarchy);
                taskPanel.SetActive(!taskPanel.activeInHierarchy);
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (!taskPanel.activeInHierarchy && !setPanel.activeInHierarchy)
            {
                SetCursorVisibility(!bagPanel.activeInHierarchy);
                bagPanel.SetActive(!bagPanel.activeInHierarchy);
            }
        }

        if (planState[0] != 3)
        {
            CheckJiangGuoDistance();
        }
        if (planState[2] != 3)
        {
            CheckYingHuoChongDistance();
        }
        if (Input.GetKeyDown(KeyCode.F) && currentInteractableJiangGuo != null)
        {
            PickUpJiangGuo(currentInteractableJiangGuo);
        }
        else if (Input.GetKeyDown(KeyCode.F) && currentInteractableYingHuoChong != null)
        {
            InteractJiangGuo();
        }
        if (yinghuochongObj2.activeInHierarchy)
        {
            float distance = Vector3.Distance(playerTransform.position, yinghuochongObj3.transform.position);
            Debug.Log("distance" + distance);
            if (distance<2)
            {
                yinghuochongObj2.SetActive(false);
                yinghuochongObj3.SetActive(true);
                planState[2] = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (chuizi.activeInHierarchy && !hasChuiZiInBag)
            {
                float dis = Vector3.Distance(playerTransform.position, chuizi.transform.position);
                if (dis <= chuiZiPickDistance)
                {
                    PickupChuiZi();
                }
            }
            else if (hasChuiZiInBag && chuiZiDeliverTrigger != null)
            {
                float disDeliver = Vector3.Distance(playerTransform.position, chuiZiDeliverTrigger.transform.position);
                if (disDeliver <= chuiZiPickDistance)
                {
                    DeliverChuiZi();
                }
            }
        }

        if (planState[4] != 3)
        {
            CheckStoneDistance();
        }

        if (Input.GetKeyDown(KeyCode.F) && currentInteractableStone != null)
        {
            PickUpStone(currentInteractableStone);
        }

        if (Input.GetKeyDown(KeyCode.F) && raccoonTransform != null)
        {
            float dis = Vector3.Distance(playerTransform.position, raccoonTransform.position);
            Debug.Log(dis + " + " + stoneCount + " + " + planState[4]);
            if (dis <= 2f && stoneCount >= stoneNeed && planState[4] == 2)
            {
                ExchangeFirework();
            }
        }

        if (!isGameEnd && CheckAllTaskFinish())
        {
            isGameEnd = true;
            playerTransform.position = gameEndPlayerPos.position;
            campFireEndObj.SetActive(true);
            talkSystem.ShowDialogue(DialogueType.Campfire_End);
        }
    }


    void CheckYingHuoChongDistance()
    {
        currentInteractableYingHuoChong = null;
        if (yinghuochongObj1 == null || !yinghuochongObj1.activeInHierarchy) return;

        float distance = Vector3.Distance(playerTransform.position, yinghuochongObj1.transform.position);

        if (distance < pickUpDistance)
        {
            currentInteractableYingHuoChong = yinghuochongObj1;

            ShowHint("Press F to interact with fireflies");
        }

        if (currentInteractableYingHuoChong == null && hintObj.activeInHierarchy)
        {
            hintObj.SetActive(false);
        }
    }

    void InteractJiangGuo()
    {
        yinghuochongObj1.SetActive(false);
        yinghuochongObj2.SetActive(true);
    }

    void CheckJiangGuoDistance()
    {
        currentInteractableJiangGuo = null; 

        foreach (GameObject jiangGuo in allJiangGuoObjects)
        {
            if (jiangGuo == null || !jiangGuo.activeInHierarchy) continue; 

            float distance = Vector3.Distance(playerTransform.position, jiangGuo.transform.position);

            if (distance < pickUpDistance)
            {
                currentInteractableJiangGuo = jiangGuo;
                ShowHint("Press F to pick berries");
                break; 
            }
        }

        if (currentInteractableJiangGuo == null && hintObj.activeInHierarchy)
        {
            hintObj.SetActive(false);
        }
    }

    void PickUpJiangGuo(GameObject jiangGuoObj)
    {
        jiangGuoObj.SetActive(false);
        jiangGuoNum++;
        bagControl.AddJiangguo();
        taskXiongText.text = "Sweet for the Bear 每 Collect Berries (" + jiangGuoNum + "/5)";

        if (jiangGuoNum >= 5)
        {
            currentInteractableJiangGuo = null;
            planState[0] = 3;
        }

        allJiangGuoObjects.Remove(jiangGuoObj);
    }


    public void KillBalloon()
    {
        killNum++;
        taskTuziText.text = "Scary Balloons 每 Pop the Balloons (" + killNum + "/3)";
        if (killNum >= 3)
        {
            planState[1] = 3;
            qiQiuObjs.SetActive(false);
            zhunXingObj.SetActive(false);
        }
    }

    public void PickupChuiZi()
    {
        if (hasChuiZiInBag) return;

        hasChuiZiInBag = true;
        chuizi.SetActive(false); 
        bagControl.AddChuizi(); 
        ShowHint("Got the Hammer!");
    }

    public void DeliverChuiZi()
    {
        if (!hasChuiZiInBag) return;

        hasChuiZiInBag = false;
        bagControl.ClearChuizi(); 
        ShowHint("Gave the Hammer to the Beaver!");
        planState[3] = 3; 
    }

    public void RespawnPlayer()
    {
        if (playerTransform != null && respawnPoint != null)
        {
            playerTransform.position = respawnPoint.position;
            ShowHint("Fell into water! Return to start.");
        }
    }

    public void SetCursorVisibility(bool isVisible)
    {
        malbersInput.enabled = !isVisible;
        freeLookCamera.enabled = !isVisible;
        Cursor.visible = isVisible;

        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;

    }


    void CheckStoneDistance()
    {
        currentInteractableStone = null;

        foreach (GameObject stone in allStones)
        {
            if (stone == null || !stone.activeInHierarchy) continue;

            float distance = Vector3.Distance(playerTransform.position, stone.transform.position);

            if (distance < pickUpDistance)
            {
                currentInteractableStone = stone;
                ShowHint("Press F to collect Shiny Stone");
                break;
            }
        }

        if (currentInteractableStone == null && hintObj.activeInHierarchy)
        {
            hintObj.SetActive(false);
        }
    }

    void PickUpStone(GameObject stoneObj)
    {
        stoneObj.SetActive(false);
        stoneCount++;
        bagControl.AddStone();        
        taskHuanXiongText.text = "A Fair Trade 每 Collect Stones (" + stoneCount + "/5)";

        allStones.Remove(stoneObj);
        ShowHint("Collected Shiny Stone!");

        if (stoneCount >= 5)
        {
            ShowHint("You collected all stones! Find Raccoon!");
        }
    }

    void ExchangeFirework()
    {
        stoneCount = 0;
        bagControl.ClearStone();
        planState[4] = 3; 

        bagControl.AddYanHua(); 
        taskHuanXiongText.text = "A Fair Trade 每 Completed!";
        ShowHint("Traded stones for Fireworks! Thank you!");
    }



    public void ShowHint(string hint)
    {
        StartCoroutine(ShowHintIE(hint));
    }

    IEnumerator ShowHintIE(string hint)
    {
        hintObj.SetActive(true);
        hintText.text = hint;
        yield return new WaitForSeconds(3);
        hintObj.SetActive(false);
    }
}