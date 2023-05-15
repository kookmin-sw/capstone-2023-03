using DataStructs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : BaseUI
{
    [SerializeField]
    private GameObject HandUI;
    [SerializeField]
    private Button TrashUI;
    [SerializeField]
    private Button DeckUI;
    [SerializeField]
    private Button EndUI;

    TextMeshProUGUI DeckNum;
    TextMeshProUGUI TrashNum;
    TextMeshProUGUI EnergyText;
    TextMeshProUGUI TurnText;

    NoticeUI noticeUI;
    // Start is called before the first frame update
    void Start()
    {

        DeckNum = GameObject.Find("Deck").GetComponentInChildren<TextMeshProUGUI>();
        TrashNum = GameObject.Find("Trash").GetComponentInChildren<TextMeshProUGUI>();
        EnergyText = GameObject.Find("Energy").GetComponentInChildren<TextMeshProUGUI>();
        TurnText = GameObject.Find("Turn").GetComponentInChildren<TextMeshProUGUI>();

        noticeUI = FindObjectOfType<NoticeUI>();

        //Canvas�� ī�޶� BattleCamera�� ����, �׷� ī�޶� ���ٸ� ���� ī�޶�� ����
        Canvas canvas = GetComponent<Canvas>();
        GameObject battleCameraParent = GameObject.Find("BattleCameraParent");
        Camera mainCamera = Camera.main;
        if (battleCameraParent != null)
        {
            canvas.worldCamera = battleCameraParent.transform.GetChild(0).GetComponent<Camera>();
        }
        else
        {
            canvas.worldCamera = mainCamera;
        }

        StartCoroutine(Turn_Start());
    }

    private void Update()
    {
        HandSpacingChange();
        UpdateDeckTrashNum();
        UpdateEnergy();
        UpdateTurn();

        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            Draw();
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            StartCoroutine(Turn_Start());
        }
    }

    public void TrashClick()
    {
        UIManager.Instance.ShowUI("LibraryUI")
            .GetComponent<LibraryUI>()
            .Init(LibraryMode.Battle_Trash);
    }

    public void DeckClick()
    {
        UIManager.Instance.ShowUI("LibraryUI")
            .GetComponent<LibraryUI>()
            .Init(LibraryMode.Battle_Deck);
    }

    //Hand UI�� �ڽ��� ���ڿ� ���� Hand UI�� Horizontal Layout Group�� Spacing�� ����
    public void HandSpacingChange()
    {

        int childCount = transform.GetChild(3).GetChild(2).childCount;
        float spacing = 0;
        switch (childCount)
        {
            case < 1:
                spacing = 0;
                break;
            case 2:
                spacing = -500;
                break;
            case 3:
                spacing = -390;
                break;
            case 4:
                spacing = -275;
                break;
            case 5:
                spacing = -165;
                break;
            case 6:
                spacing = -250;
                break;
            case > 6:
                spacing = -225;
                break;
        }
        transform.GetChild(3).GetChild(2).GetComponent<UnityEngine.UI.HorizontalLayoutGroup>().spacing = spacing;
    }

    //������ ������ ī�带 �̾Ƽ� �տ� �߰�
    public void Draw()
    {
        bool CanDraw;
        CardUI cardUI;

        CanDraw = Battle.Draw();

        if (CanDraw)
        {
            cardUI = AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", HandUI.transform).GetComponent<CardUI>();
            cardUI.ShowCardData(BattleData.Instance.Hand[BattleData.Instance.Hand.Count - 1]);

            Vector3 pos = cardUI.transform.localPosition;
            pos.z = 43.25f;
            cardUI.transform.localPosition = pos;
            cardUI.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            cardUI.AddComponent<Draggable>();

            SoundManager.Instance.Play("Sounds/DrawBgm");
        }
        else
        {
            noticeUI.ShowNotice("뽑을 카드가 없습니다!");
        }
    }
    // �Է¹��� ī�带 Hand UI���� ����
    public void Discard(CardStruct card)
    {

        Battle.Discard(card);
        
        for (int i = 0; i < HandUI.transform.childCount; i++)
        {
            if (HandUI.transform.GetChild(i).GetComponent<CardUI>().Card.name == card.name)
            {
                Destroy(HandUI.transform.GetChild(i).gameObject);
                break;
            }
        }
    }

    //DeckNum�� TrashNum�� ����
    public void UpdateDeckTrashNum()
    {
        DeckNum.text = "남은 카드\n" + BattleData.Instance.Deck.Count.ToString();
        TrashNum.text = "버려진 카드\n" + BattleData.Instance.Trash.Count.ToString();
    }

    public void UpdateEnergy()
    {
        EnergyText.text = BattleData.Instance.CurrentEnergy.ToString() + "/" + BattleData.Instance.MaxEnergy.ToString();
    }

    public void UpdateTurn()
    {
        TurnText.text = "턴      " + BattleData.Instance.CurrentTurn.ToString();
    }

    //�� ���� ��ư�� ������ ����
    public void EndClick()
    {
        Battle.End_turn();
        for (int i = 0; i < HandUI.transform.childCount; i++)
        {
            Destroy(HandUI.transform.GetChild(i).gameObject);          
        }
    }

    public IEnumerator Turn_Start()
    {
        DeckUI.interactable = false;
        TrashUI.interactable = false;
        EndUI.interactable = false;
        Battle.Start_turn();
        for (int i = 0; i < BattleData.Instance.StartHand; i++)
        {

            yield return new WaitForSecondsRealtime(0.5f);
            Draw();
        }
        DeckUI.interactable = true;
        TrashUI.interactable = true;
        EndUI.interactable = true;
    }
}
