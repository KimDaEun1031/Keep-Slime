using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


public class ButtonClickScript : MonoBehaviour
{

    public GameObject Slime, Box;
    public RectTransform SlimeIcon, InvenIcon;

    void Update()
    {
        
    }

    void OnClickSlime()
    {
        // activeInHierachy = 활성화 감지
        if (Slime.activeInHierarchy)  Slime.SetActive(false);   
    }

    void OnClickBox()
    {
        if (Box.activeInHierarchy) Box.SetActive(false);
    }

    public void OnclickSlimePanel(string panelName)
    {
        if (panelName == "SlimeIcon")
        {
            if (Box.activeInHierarchy)
            {
                if (InvenIcon.localPosition.y == -289)
                {                
                    InvenIcon.DOLocalMoveY(-1460, 0.5f).SetEase(Ease.InBack);
                    Invoke("OnClickBox", 0.6f);
                    Slime.SetActive(true);
                    if (SlimeIcon.localPosition.y == -1550)
                    {
                        SlimeIcon.DOLocalMoveY(-375, 0.5f).SetEase(Ease.OutBack);
                    }
                }                     
            } 
            else if (SlimeIcon.localPosition.y == -1550)
            {
                Slime.SetActive(true);
                SlimeIcon.DOLocalMoveY(-375, 0.5f).SetEase(Ease.OutBack);
            }
        }
        else if (panelName == "SlimeUpgrade Panel")
        {           
            if (SlimeIcon.localPosition.y == -375)
            {
                SlimeIcon.DOLocalMoveY(-1550, 0.5f).SetEase(Ease.InBack);
                Invoke("OnClickSlime", 0.6f);                   
            }
        } 
    }

    public void OnclickInvenPanel(string panelName)
    {
        if (panelName == "InventoryIcon")
        {
            if (Slime.activeInHierarchy)
            {
                if (SlimeIcon.localPosition.y == -375)
                {
                    SlimeIcon.DOLocalMoveY(-1550, 0.5f).SetEase(Ease.InBack);
                    Invoke("OnClickSlime", 0.6f);
                    Box.SetActive(true);
                    if (InvenIcon.localPosition.y == -1460)
                    {
                        InvenIcon.DOLocalMoveY(-289, 0.5f).SetEase(Ease.OutBack);
                    }
                }              
            }
            else if (InvenIcon.localPosition.y == -1460)
            {
                 Box.SetActive(true);
                InvenIcon.DOLocalMoveY(-289, 0.5f).SetEase(Ease.OutBack);
            }
        }
        else if (panelName == "Inventory Panel")
        {          
            if (InvenIcon.localPosition.y == -289)
            {
                InvenIcon.DOLocalMoveY(-1460, 0.5f).SetEase(Ease.InBack);
                Invoke("OnClickBox", 0.6f);
            }
        }
    }


}

 
