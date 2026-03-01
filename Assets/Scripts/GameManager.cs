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
    // 记录当前鼠标的显示状态（true显示，false隐藏）
    private bool isCursorVisible = false;

    public MalbersInput malbersInput;
    public MFreeLookCamera freeLookCamera;

    public SimpleDialogueSystem talkSystem;

    public BagControl bagControl;

    public GameObject hintObj;
    public Text hintText;

    public List<int> planState = new List<int>() { 1, 1, 1, 1, 1 };//1 2 3  xiong tuzi maotouying haili huanxiong

    //renwu
    public int jiangGuoNum = 0;
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

    //UI
    public GameObject setPanel;
    public GameObject bagPanel;
    public GameObject taskPanel;

    // 新增：存储场景中所有的浆果对象
    private List<GameObject> allJiangGuoObjects = new List<GameObject>();
    // 新增：记录当前可交互的浆果对象（距离<1的那个）
    private GameObject currentInteractableJiangGuo = null;
    // 新增：玩家Transform（需要在Inspector面板赋值）
    public Transform playerTransform;
    // 新增：拾取距离阈值
    public float pickUpDistance = 1f;

    public GameObject jiangGuoObjs;

    // Start is called before the first frame update
    void Start()
    {
        // 初始化时设置鼠标为默认显示状态
        SetCursorVisibility(isCursorVisible);
        talkSystem.ShowDialogue(DialogueType.Ember_First);
        taskXiongObj.SetActive(false);
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
    }

    // Update is called once per frame
    void Update()
    {
        // 检测Q键按下（按下一次切换，而非持续按住）
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 切换鼠标状态（取反）
            isCursorVisible = !isCursorVisible;
            // 调用方法更新鼠标显隐
            SetCursorVisibility(isCursorVisible);
        }
        // 检测鼠标左键点击（按下瞬间）
        //if (Input.GetMouseButtonDown(0))
        //{
        //    DetectClickedObject();
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 调用方法更新鼠标显隐
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
                // 调用方法更新鼠标显隐
                SetCursorVisibility(!taskPanel.activeInHierarchy);
                taskPanel.SetActive(!taskPanel.activeInHierarchy);
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (!taskPanel.activeInHierarchy && !setPanel.activeInHierarchy)
            {
                // 调用方法更新鼠标显隐
                SetCursorVisibility(!bagPanel.activeInHierarchy);
                bagPanel.SetActive(!bagPanel.activeInHierarchy);
            }
        }

        // 每帧检测玩家与所有浆果的距离
        CheckJiangGuoDistance();
        // 检测F键按下，拾取当前可交互的浆果
        if (Input.GetKeyDown(KeyCode.F) && currentInteractableJiangGuo != null)
        {
            PickUpJiangGuo(currentInteractableJiangGuo);
        }
    }
    /// <summary>
    /// 检测玩家与所有浆果的距离，更新可交互浆果
    /// </summary>
    void CheckJiangGuoDistance()
    {
        currentInteractableJiangGuo = null; // 重置当前可交互对象

        foreach (GameObject jiangGuo in allJiangGuoObjects)
        {
            if (jiangGuo == null || !jiangGuo.activeInHierarchy) continue; // 跳过已被拾取的浆果

            // 计算玩家与浆果的距离
            float distance = Vector3.Distance(playerTransform.position, jiangGuo.transform.position);

            // 如果距离小于阈值，标记为可交互
            if (distance < pickUpDistance)
            {
                currentInteractableJiangGuo = jiangGuo;
                // 可选：显示拾取提示（比如"按F拾取浆果"）
                ShowHint("Press F to pick berries");
                break; // 只取最近的一个浆果
            }
        }

        // 如果没有可交互的浆果，隐藏提示（可选）
        if (currentInteractableJiangGuo == null && hintObj.activeInHierarchy)
        {
            hintObj.SetActive(false);
        }
    }

    /// <summary>
    /// 拾取浆果的核心逻辑
    /// </summary>
    /// <param name="jiangGuoObj">要拾取的浆果对象</param>
    void PickUpJiangGuo(GameObject jiangGuoObj)
    {
        jiangGuoObj.SetActive(false);
        jiangGuoNum++;
        bagControl.AddJiangguo();
        taskXiongText.text = "Sweet for the Bear – Collect Berries (" + jiangGuoNum + "/5)";

        if (jiangGuoNum >= 5)
        {
            planState[0] = 3;
        }

        // 移除已拾取的浆果（可选，防止重复检测）
        allJiangGuoObjects.Remove(jiangGuoObj);
        Debug.Log("成功拾取浆果，当前数量：" + jiangGuoNum);
    }

    ///// <summary>
    ///// 核心逻辑：检测点击的物体并获取名称
    ///// </summary>
    //void DetectClickedObject()
    //{
    //    // 1. 创建从摄像机到鼠标位置的射线
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    // 2. 射线碰撞信息存储
    //    RaycastHit hitInfo;

    //    // 3. 发射射线检测（参数：射线、碰撞信息、检测最大距离）
    //    if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
    //    {
    //        // 4. 获取点击到的物体
    //        GameObject clickedObject = hitInfo.collider.gameObject;
    //        // 5. 获取物体名称（核心：objectName是Unity物体的默认属性）
    //        string objectName = clickedObject.name;

    //        // ========== 核心逻辑：识别到名称后的处理 ==========
    //        // 方式1：控制台打印（调试用）
    //        Debug.Log($"点击到的物体名称：{objectName}");
    //        if (objectName == "jiangguo")
    //        {
    //            clickedObject.SetActive(false);
    //            jiangGuoNum++;
    //            bagControl.AddJiangguo();
    //            taskXiongText.text = "Sweet for the Bear – Collect Berries (" + jiangGuoNum  + "/5)";
    //            if (jiangGuoNum>=5)
    //            {
    //                planState[0] = 3;
    //            }
    //        }
    //    }
    //}
    /// <summary>
    /// 控制鼠标显示/隐藏的方法
    /// </summary>
    /// <param name="isVisible">true显示鼠标，false隐藏鼠标</param>
    public void SetCursorVisibility(bool isVisible)
    {
        malbersInput.enabled = !isVisible;
        freeLookCamera.enabled = !isVisible;
        // 设置鼠标是否可见
        Cursor.visible = isVisible;

        // 设置鼠标锁定模式：
        // 可见时解锁（自由移动），隐藏时锁定到屏幕中心
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;

        // 可选：打印日志，方便调试
        Debug.Log($"鼠标状态已切换：{(isVisible ? "显示" : "隐藏")}");
    }

    /// <summary>
    /// 提示
    /// </summary>
    /// <param name="hint"></param>
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