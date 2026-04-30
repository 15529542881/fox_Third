using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagControl : MonoBehaviour
{
    public Transform[] bagCell;
    public Sprite jiangguoSpr;
    public Sprite chuiziSpr;
    public Sprite storeSpr;
    public Sprite yanHuaSpr;

    void Start()
    {
        InitBag();
    }

    void Update()
    {

    }

    private void InitBag()
    {
        foreach (var item in bagCell)
        {
            if (item.childCount > 0)
            {
                item.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void AddJiangguo()
    {
        AddItemToBag(jiangguoSpr);
    }

    public void AddChuizi()
    {
        AddItemToBag(chuiziSpr);
    }

    public void AddStone()
    {
        AddItemToBag(storeSpr);
    }

    public void AddYanHua()
    {
        AddItemToBag(yanHuaSpr);
    }

    private void AddItemToBag(Sprite itemSprite)
    {
        if (itemSprite == null)
        {
            return;
        }

        foreach (var cell in bagCell)
        {
            Transform itemIcon = cell.GetChild(0);
            if (!itemIcon.gameObject.activeSelf)
            {
                Image img = itemIcon.GetComponent<Image>();
                if (img != null)
                {
                    img.sprite = itemSprite;
                }
                else
                {
                    return;
                }

                itemIcon.gameObject.SetActive(true);
                return;
            }
        }
    }

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
    }

    public void ClearSpecifiedItem(Sprite targetSprite)
    {
        if (targetSprite == null)
        {
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
    }

    public void ClearJiangguo() => ClearSpecifiedItem(jiangguoSpr);
    public void ClearChuizi() => ClearSpecifiedItem(chuiziSpr);
    public void ClearStone() => ClearSpecifiedItem(storeSpr);
}