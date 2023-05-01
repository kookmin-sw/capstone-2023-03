using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DataStructs;


//C# LInq ���: ������ ������ C#���� ��ũ��Ʈ�� ����� �� �ֵ��� �ϴ� ���.
//�迭 �� �ٸ� �÷��ǿ��� ���� ���ϴ� ������ ������ �� ����.

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


    //��ü ī�� ����Ʈ ��������
    public void Awake()
    {
       
    }

    private void OnEnable()
    {
        //������ �÷��̾� ���� ��Ȱ��ȭ, UI ���� Ȱ��ȭ
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

        //ī�� ��ü�� ��������, �÷��̾��� ī�带 �������� �� 1
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
        //Linq�� ���. ���� �������� ���� �з���ŭ ī�� ����Ʈ���� ����.
        List<CardStruct> cardList = showedCardList.Skip(currentPage * cardsPerPage).Take(cardsPerPage).ToList();

        for (int i = 0; i < cardList.Count; i++)
        {
            AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", deckDisplayer.transform)
                .GetComponent<CardUI>()
                .ShowCardData(cardList[i], CardMode.Library);
        }

        UpdateButtons();

    }

    //ǥ������ ī�� ����
    private void ClearCards()
    {
        for (int i = 0; i < deckDisplayer.transform.childCount; i++)
        {
            AssetLoader.Instance.Destroy(deckDisplayer.transform.GetChild(i).gameObject);
        }
    }
    
    // ����/���� ��ư Ȱ��ȭ
    private void UpdateButtons()
    {
        prevButton.gameObject.SetActive(currentPage > 0);
        nextButton.gameObject.SetActive((currentPage + 1) * cardsPerPage < showedCardList.Count);
    }

    //���� ��ư Ŭ���� �߻��� �̺�Ʈ
    public void NextButtonClick()
    {
        currentPage++;
        ClearCards();
        ShowCards();
        UpdateButtons();

    }

    //���� ��ư Ŭ���� �߻��� �̺�Ʈ
    public void PreviousButtonClick()
    {
        currentPage--;
        ClearCards();
        ShowCards();
        UpdateButtons();
    }

    //�ڽ�Ʈ�� ���� ��ư. 
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

    //�̸��� ���� ��ư. 
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

    //������ ��ư, UI �ݱ�
    public void BackButtonClick()
    {
        UIManager.Instance.HideUI("LibraryUI");
    }

    //�̰� �����ؾ� �Ѵ�... Ÿ��Ʋ ȭ�鿡���� ����Ǹ� �ȵǴµ�. ��Ʈ�ѷ��� ����, �ΰ��ӿ����� Ư�� Ű�� �� �� �ְ� �ϴ� ��?
    private void Close(InputAction.CallbackContext context)
    {
        UIManager.Instance.HideUI("LibraryUI");
    }
}
