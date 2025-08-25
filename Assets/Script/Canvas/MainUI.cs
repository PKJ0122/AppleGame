using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : UIBase
{
    Button _play;
    Button _rule;
    Button _setting;


    protected override void Awake()
    {
        base.Awake();
        _play = transform.Find("Panel/Button - Play").GetComponent<Button>();
        _rule = transform.Find("Panel/Button - Rule").GetComponent <Button>();
        _setting = transform.Find("Panel/Button - Setting").GetComponent<Button>();
        _play.onClick.AddListener(() =>
        {
            Hide();
            GameManager.Instance.GameStart();
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
        });
        _rule.onClick.AddListener(() =>
        {
            UIManager.Instance.Get<RuleUI>().Show();
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
        });
        _setting.onClick.AddListener(() =>
        {
            UIManager.Instance.Get<SettingUI>().Show();
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
        });
        Show();
    }

    public override void Show()
    {
        base.Show();
    }
}
