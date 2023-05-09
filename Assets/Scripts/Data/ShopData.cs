using DataStructs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopData : MonoBehaviour
{


    //상점에서 파는 5개의 카드 리스트
    public List<CardStruct> ShopCardsList { get; set; } = new List<CardStruct>(5);
    
    //카드 리롤 시 드는 비용
    public int RerollMoney { get; set; }
    
    //카드 제거 시 드는 비용
    public int DiscardMoney { get; set; }

    public void Awake()
    {
        ClearShopData();
    }

    public void OnEnable()
    {
        StageManager.Instance.OnLevelClear += ClearShopData; //레벨 클리어 시 이거 실행해서 상점 데이터 초기화
    }
    public void OnDisable()
    {
        StageManager.Instance.OnLevelClear -= ClearShopData;
    }

    //레벨 바뀔 때마다 상점 데이터 초기화
    public void ClearShopData()
    {

        //공격, 스킬 카드들을 쿼리.
        List<CardStruct> shopCardsPool = GameData.Instance.CardList
             .Where(card => (card.type == "Attack" || card.type == "Skill")
                 && card.attribute != "Normal" //기본 카드는 안나오게 수정
             ).ToList();

        //5개를 랜덤으로 가져오기
        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(0, shopCardsPool.Count);
            ShopCardsList.Add(shopCardsPool[index]); 
        }

        RerollMoney = 50;
        DiscardMoney = 75;
    }

}
