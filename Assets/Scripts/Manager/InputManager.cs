
//인풋 액션들을 관리
public class InputManager : Singleton<InputManager>
{
    public GameActions KeyActions { get; set; }

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);

        KeyActions = new GameActions();
        KeyActions.Enable();
    }
}
