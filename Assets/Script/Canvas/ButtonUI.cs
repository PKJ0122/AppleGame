using UnityEngine.UI;

public class ButtonUI : UIBase
{
    Button _main;


    protected override void Awake()
    {
        base.Awake();
        _main = transform.Find("Button - Home").GetComponent<Button>();
        _main.onClick.AddListener(() =>
        {
            GameManager.Instance.GameEnd(false);
            UIManager.Instance.Get<MainUI>().Show();
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            ShowEffect(_main.transform);
        });
    }
}