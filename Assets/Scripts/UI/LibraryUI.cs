using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//C# LInq 사용: 데이터 쿼리를 C#에서 스크립트로 사용할 수 있도록 하는 기술.
//배열 및 다른 컬렉션에서 쉽게 원하는 구역만 가져올 수 있음.

public class LibraryUI : BaseUI
{
    private bool showAllCards = true;
    private int cardsPerPage = 2;
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

    private List<CardData> showedCardList= new List<CardData>();


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

        ShowCards();

    }

    public void ShowCards()
    {
        //카드 전체를 보여줄지, 플레이어의 카드를 보여줄지 택 1
        if(showAllCards)
        {
            showedCardList = GameDataCon.Instance.CardList;
        }
        else
        {
            showedCardList = PlayDataCon.Instance.PlayData.playerCardData;
        }

        //Linq를 사용. 현재 페이지에 나올 분량만큼 카드 리스트에서 쿼리.
        var cardList = showedCardList.Skip(currentPage * cardsPerPage).Take(cardsPerPage);

        foreach (CardData cardData in cardList)
        {
            CardUI cardUI = AssetLoader.Instance.Instantiate("Prefabs/Button/CardButton").GetComponent<CardUI>();
            cardUI.transform.SetParent(deckDisplayer.transform);   
            cardUI.ShowCardData(cardData);
        }

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
        prevButton.interactable = currentPage > 0;
        nextButton.interactable = (currentPage + 1) * cardsPerPage < showedCardList.Count;
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


    private void Close(InputAction.CallbackContext context)
    {
        PanelManager.Instance.ClosePanel("LibraryUI");
    }
}
