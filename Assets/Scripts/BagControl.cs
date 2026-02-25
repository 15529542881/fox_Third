using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagControl : MonoBehaviour
{
    // 背包格子数组
    public Transform[] bagCell;
    // 三种物品的精灵图
    public Sprite jiangguoSpr;
    public Sprite chuiziSpr;
    public Sprite storeSpr;

    void Start()
    {
        // 初始化背包，隐藏所有格子的物品显示
        InitBag();
    }

    void Update()
    {

    }

    /// <summary>
    /// 初始化背包，隐藏所有格子的物品显示
    /// </summary>
    private void InitBag()
    {
        foreach (var item in bagCell)
        {
            // 确保格子有子对象
            if (item.childCount > 0)
            {
                item.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 添加浆果物品到背包第一个空格子
    /// </summary>
    public void AddJiangguo()
    {
        AddItemToBag(jiangguoSpr);
    }

    /// <summary>
    /// 添加锤子物品到背包第一个空格子
    /// </summary>
    public void AddChuizi()
    {
        AddItemToBag(chuiziSpr);
    }

    /// <summary>
    /// 添加商店物品到背包第一个空格子
    /// </summary>
    public void AddStore()
    {
        AddItemToBag(storeSpr);
    }

    /// <summary>
    /// 通用添加物品方法
    /// </summary>
    /// <param name="itemSprite">要添加的物品精灵图</param>
    private void AddItemToBag(Sprite itemSprite)
    {
        // 空精灵图直接返回
        if (itemSprite == null)
        {
            Debug.LogWarning("要添加的物品精灵图为空！");
            return;
        }

        // 遍历背包格子，找到第一个空格子
        foreach (var cell in bagCell)
        {
            Transform itemIcon = cell.GetChild(0);
            // 检查格子是否为空（物品图标未激活）
            if (!itemIcon.gameObject.activeSelf)
            {
                // 获取Image组件并设置精灵图
                Image img = itemIcon.GetComponent<Image>();
                if (img != null)
                {
                    img.sprite = itemSprite;
                }
                else
                {
                    Debug.LogError($"格子{cell.name}的子对象缺少Image组件！");
                    return;
                }

                // 激活物品图标
                itemIcon.gameObject.SetActive(true);
                Debug.Log($"成功添加{itemSprite.name}到背包格子{cell.name}");
                return;
            }
        }

        // 没有找到空格子
        Debug.LogWarning("背包已满，无法添加新物品！");
    }

    /// <summary>
    /// 清空背包所有物品
    /// </summary>
    public void ClearAllItems()
    {
        int clearCount = 0;
        foreach (var cell in bagCell)
        {
            Transform itemIcon = cell.GetChild(0);
            if (itemIcon.gameObject.activeSelf)
            {
                itemIcon.gameObject.SetActive(false);
                clearCount++;
            }
        }
        Debug.Log($"已清空背包中{clearCount}个物品");
    }

    /// <summary>
    /// 清空背包中指定精灵图的物品
    /// </summary>
    /// <param name="targetSprite">要清空的目标精灵图</param>
    public void ClearSpecifiedItem(Sprite targetSprite)
    {
        // 空精灵图直接返回
        if (targetSprite == null)
        {
            Debug.LogWarning("要清空的目标精灵图为空！");
            return;
        }

        int clearCount = 0;
        foreach (var cell in bagCell)
        {
            Transform itemIcon = cell.GetChild(0);
            if (itemIcon.gameObject.activeSelf)
            {
                Image img = itemIcon.GetComponent<Image>();
                if (img != null && img.sprite == targetSprite)
                {
                    itemIcon.gameObject.SetActive(false);
                    clearCount++;
                }
            }
        }

        Debug.Log($"已清空背包中{clearCount}个{targetSprite.name}物品");
    }

    // 快捷清空方法（可以直接调用）
    public void ClearJiangguo() => ClearSpecifiedItem(jiangguoSpr);
    public void ClearChuizi() => ClearSpecifiedItem(chuiziSpr);
    public void ClearStore() => ClearSpecifiedItem(storeSpr);
}