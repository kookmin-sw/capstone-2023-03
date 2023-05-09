using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DataStructs;



public enum LibraryMode
{
    Library, //일반 라이브러리 모드
    Deck, //플레이어 덱 보여주기 모드
    EventDiscard, //플레이어 덱 보여주기 + 이벤트로 카드 버리기 모드
    ShopDiscard //플레이어 덱 보여주기 + 상점에서 카드 버리기 모드
}

//C# LInq 사용: 데이터 쿼리를 C#에서 스크립트로 사용할 수 있도록 하는 기술.
//배열 및 다른 컬렉션에서 쉽게 원하는 구역만 가져올 수 있음.

public class LibraryUI : BaseUI
{
    private LibraryMode libraryMode;
    private int cardsPerPage = 8;
    private int currentPage = 0;

    [SerializeField]
    private GameObject deckDisplayer;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button prevButton;
    [SerializeField]
    private Button sortByCostButton;
    [SerializeField]
    private Button sortByNameButton;
    [SerializeField]
    private Button BackButton;

    private List<CardStruct> showedCardList= new List<CardStruct>();


    private void OnEnable()
    {
        InputActions.keyActions.UI.Menu.started += Close;
        PlayerData.Instance.OnDataChange += RefreshLibrary; //이거는 덱이 바뀔 때마다 그것을 감지하여 카드 UI를 새로고침하기 위함.
    }

    private void OnDisable()
    {
        InputActions.keyActions.UI.Menu.started -= Close;
        PlayerData.Instance.OnDataChange -= RefreshLibrary;
    }

    public void Init(LibraryMode libraryMode = LibraryMode.Library)
    {
        this.libraryMode = libraryMode;

        switch (libraryMode) //공격, 스킬, 애청자 카드 전부 보여주기
        {
            case LibraryMode.Library:
                showedCardList = GameData.Instance.CardList
                .Where(card => card.type == "Attack" || card.type == "Skill" || card.type == "Viewer")
                .ToList();
                break;
            case LibraryMode.Deck: //현재 덱 보여주기
                showedCardList = PlayerData.Instance.Deck;
                break;
            case LibraryMode.EventDiscard: //현재 덱 보여주기 + 이벤트로 카드 한 장 버리기
                showedCardList = PlayerData.Instance.Deck;
                BackButton.gameObject.SetActive(false);
                break;
            case LibraryMode.ShopDiscard: //현재 덱 보여주기 + 이벤트로 클릭하는 만큼 버리기
                showedCardList = PlayerData.Instance.Deck;
                break;

        }
        ShowCards();
        SortByCostButtonClick();
    }

    public void RefreshLibrary() //덱이 바뀌었을 때 호출되어, 카드 UI를 새로고침한다.
    {
        switch (libraryMode) //공격, 스킬, 애청자 카드 전부 보여주기
        {
            case LibraryMode.Library:
                showedCardList = GameData.Instance.CardList
                .Where(card => card.type == "Attack" || card.type == "Skill" || card.type == "Viewer")
                .ToList();
                break;
            case LibraryMode.Deck: //현재 덱 보여주기
                showedCardList = PlayerData.Instance.Deck;
                break;
            case LibraryMode.EventDiscard: //현재 덱 보여주기 + 이벤트로 카드 한 장 버리기
                showedCardList = PlayerData.Instance.Deck;
                BackButton.gameObject.SetActive(false);
                break;
            case LibraryMode.ShopDiscard: //현재 덱 보여주기 + 이벤트로 클릭하는 만큼 버리기
                showedCardList = PlayerData.Instance.Deck;
                break;
        }
        ShowCards();
        SortByCostButtonClick();
    }

    //표시중인 카드 제거
    private void ClearCards()
    {
        for (int i = 0; i < deckDisplayer.transform.childCount; i++)
        {
            AssetLoader.Instance.Destroy(deckDisplayer.transform.GetChild(i).gameObject);
        }
    }


    public void ShowCards()
    {

        ClearCards(); //전에 표시되던 카드 제거

        //Linq를 사용. 현재 페이지에 나올 분량만큼 카드 리스트에서 쿼리해서 보여주기
        List<CardStruct> cardList = showedCardList.Skip(currentPage * cardsPerPage).Take(cardsPerPage).ToList();

        for (int i = 0; i < cardList.Count; i++)
        {
            switch (libraryMode)
            {
                case LibraryMode.Library: //공격, 스킬, 애청자 카드 전부 보여주기
                case LibraryMode.Deck: //현재 덱 보여주기
                    AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", deckDisplayer.transform)
                        .GetComponent<CardUI>()
                        .ShowCardData(cardList[i], CardMode.Library); //카드를 라이브러리 용으로 소환(클릭 이벤트 X)
                    break;
                case LibraryMode.EventDiscard: //현재 덱 보여주기 + 카드 버리기
                    AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", deckDisplayer.transform)
                        .GetComponent<CardUI>()
                        .ShowCardData(cardList[i], CardMode.EventDiscard); //카드를 버리기 모드로 소환(클릭 시 버리기 이벤트)
                    break;
            }
        }
        UpdateButtons();
    }
    
    // 이전/다음 버튼 활성화
    private void UpdateButtons()
    {
        prevButton.gameObject.SetActive(currentPage > 0);
        nextButton.gameObject.SetActive((currentPage + 1) * cardsPerPage < showedCardList.Count);
    }

    //다음 버튼 클릭시 발생할 이벤트
    public void NextButtonClick()
    {
        currentPage++;
        ShowCards();
        UpdateButtons();

    }

    //이전 버튼 클릭시 발생할 이벤트
    public void PreviousButtonClick()
    {
        currentPage--;
        ShowCards();
        UpdateButtons();
    }

    //코스트순 정렬 버튼. 
    public void SortByCostButtonClick()
    {
        sortByCostButton.interactable = false;
        sortByNameButton.interactable = true;

        sortByCostButton.GetComponentInChildren<TMP_Text>().color = Color.grey;
        sortByNameButton.GetComponentInChildren<TMP_Text>().color = Color.white;

        showedCardList = showedCardList.OrderBy(card => card.cost).ToList();
        currentPage = 0;
        ShowCards();
        UpdateButtons();
    }

    //이름순 정렬 버튼. 
    public void SortByNameButtonClick()
    {
        sortByNameButton.interactable = false;
        sortByCostButton.interactable = true;

        sortByNameButton.GetComponentInChildren<TMP_Text>().color = Color.grey;
        sortByCostButton.GetComponentInChildren<TMP_Text>().color = Color.white;

        showedCardList = showedCardList.OrderBy(card => card.name).ToList();
        currentPage = 0;
        ShowCards();
        UpdateButtons();
    }

    //나가기 버튼, UI 닫기
    public void BackButtonClick()
    {
        UIManager.Instance.HideUI("LibraryUI");
    }

    private void Close(InputAction.CallbackContext context)
    {
        UIManager.Instance.HideUI("LibraryUI");
    }
}
