using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DataStructs;


//C# LInq 사용: 데이터 쿼리를 C#에서 스크립트로 사용할 수 있도록 하는 기술.
//배열 및 다른 컬렉션에서 쉽게 원하는 구역만 가져올 수 있음.

public class LibraryUI : BaseUI
{
    private bool showAllCards = true;
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

    private List<CardStruct> showedCardList= new List<CardStruct>();


    //전체 카드 리스트 가져오기
    public void Awake()
    {
       
    }

    private void OnEnable()
    {
        //켜지면 플레이어 조작 비활성화, UI 조작 활성화
        InputActions.keyActions.Player.Disable();
        InputActions.keyActions.UI.Enable();
        InputActions.keyActions.UI.Menu.started += Close;
    }

    private void OnDisable()
    {
        InputActions.keyActions.Player.Enable();
        InputActions.keyActions.UI.Disable();
        InputActions.keyActions.UI.Menu.started -= Close;
    }

    public void Init(bool showAllCards)
    {
        this.showAllCards = showAllCards;

        //카드 전체를 보여줄지, 플레이어의 카드를 보여줄지 택 1
        if (showAllCards)
        {
            showedCardList = GameData.Instance.CardList;
        }
        else
        {
            showedCardList = PlayerData.Instance.Deck;
        }

        ShowCards();
        SortByCostButtonClick();
    }

    public void ShowCards()
    {
        //Linq를 사용. 현재 페이지에 나올 분량만큼 카드 리스트에서 쿼리.
        List<CardStruct> cardList = showedCardList.Skip(currentPage * cardsPerPage).Take(cardsPerPage).ToList();

        for (int i = 0; i < cardList.Count; i++)
        {
            AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", deckDisplayer.transform)
                .GetComponent<CardUI>()
                .ShowCardData(cardList[i]);
        }

        UpdateButtons();

    }

    //표시중인 카드 제거
    private void ClearCards()
    {
        for (int i = 0; i < deckDisplayer.transform.childCount; i++)
        {
            AssetLoader.Instance.Destroy(deckDisplayer.transform.GetChild(i).gameObject);
        }
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
        ClearCards();
        ShowCards();
        UpdateButtons();

    }

    //이전 버튼 클릭시 발생할 이벤트
    public void PreviousButtonClick()
    {
        currentPage--;
        ClearCards();
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
        ClearCards();
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
        ClearCards();
        ShowCards();
        UpdateButtons();
    }

    //나가기 버튼, UI 닫기
    public void BackButtonClick()
    {
        UIManager.Instance.HideUI("LibraryUI");
    }

    //이건 수정해야 한다... 타이틀 화면에서도 적용되면 안되는데. 컨트롤러를 만들어서, 인게임에서는 특정 키로 열 수 있게 하는 게?
    private void Close(InputAction.CallbackContext context)
    {
        UIManager.Instance.HideUI("LibraryUI");
    }
}
