using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//IBeginDragHandler, IDragHandler, IEndDragHandler는 EventSystems를 상속 받음
//특정 부분 터치,클릭에 주로 사용

public class NestedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;
    public RectTransform[] BtnRect, BtnImageRect;

    const int SIZE = 4;
    float[] pos = new float[SIZE];
    float distance, targetPos, curPos;
    bool isDrag;
    int targetIndex; //페이지 수

    //스크롤 수직 수평 스크롤 뷰

    void Start()
    {
        //거리에 따라 0~1인 pos 대입
        //유동적으로 페이지 크기와 수를 계산하게끔
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++) pos[i] = distance * i;
    }

    float SetPos()
    {
        //절반 거리를 기준으로 가까운 위치를 반환
        for (int i = 0; i < SIZE; i++)
            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetIndex = i;
                return pos[i];
            }
        return 0; //예의상 실행하지 않음
    }

    //드래그 시 순간변화율(미분)을 알 수 있음
    public void OnBeginDrag(PointerEventData eventData)
    {
        //print("드래그 시작" + eventData.position);
        //print("부모 드래그 시작");
        curPos = SetPos();
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        //print("부모 드래그 중");
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        targetPos = SetPos();
        //print("부모 드래그 끝");

        //print(curPos + "/" + targetPos + "/" + targetIndex);

        //빠르게 넘겼을 경우
        //절반거리를 넘지않아도 마우스를 빠르게 이동하면
        if (curPos == targetPos)
        {
            print(eventData.delta.x);
            //스크롤이 왼쪽으로 빠르게 이동시 목표가 하나 감소
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }
            //스크롤이 오른쪽으로 빠르게 이동시 목표가 하나 증가
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
