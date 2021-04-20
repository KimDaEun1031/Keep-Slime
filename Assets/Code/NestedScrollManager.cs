using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//IBeginDragHandler, IDragHandler, IEndDragHandler�� EventSystems�� ��� ����
//Ư�� �κ� ��ġ,Ŭ���� �ַ� ���

public class NestedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;
    public RectTransform[] BtnRect, BtnImageRect;

    const int SIZE = 4;
    float[] pos = new float[SIZE];
    float distance, targetPos, curPos;
    bool isDrag;
    int targetIndex; //������ ��

    //��ũ�� ���� ���� ��ũ�� ��

    void Start()
    {
        //�Ÿ��� ���� 0~1�� pos ����
        //���������� ������ ũ��� ���� ����ϰԲ�
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++) pos[i] = distance * i;
    }

    float SetPos()
    {
        //���� �Ÿ��� �������� ����� ��ġ�� ��ȯ
        for (int i = 0; i < SIZE; i++)
            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetIndex = i;
                return pos[i];
            }
        return 0; //���ǻ� �������� ����
    }

    //�巡�� �� ������ȭ��(�̺�)�� �� �� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        //print("�巡�� ����" + eventData.position);
        //print("�θ� �巡�� ����");
        curPos = SetPos();
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        //print("�θ� �巡�� ��");
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        targetPos = SetPos();
        //print("�θ� �巡�� ��");

        //print(curPos + "/" + targetPos + "/" + targetIndex);

        //������ �Ѱ��� ���
        //���ݰŸ��� �����ʾƵ� ���콺�� ������ �̵��ϸ�
        if (curPos == targetPos)
        {
            print(eventData.delta.x);
            //��ũ���� �������� ������ �̵��� ��ǥ�� �ϳ� ����
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }
            //��ũ���� ���������� ������ �̵��� ��ǥ�� �ϳ� ����
            else if (eventData.delta.x > -18 && curPos + distance <= 1.01f)
            {
                ++targetIndex;
                targetPos = curPos + distance;
            }
        }

        
    }
    void Update()
    {
        if (!isDrag) scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
    }

    public void TabClick(int n)
    {
        targetIndex = n;
        targetPos = pos[n];
    }
}
