using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : UIBase
{
    TMP_Text _score;
    Button _rePlay;
    Button _main;
    Button _endHide;

    Transform _panel;


    protected override void Awake()
    {
        base.Awake();
        _panel = transform.Find("Panel");
        _score = transform.Find("Panel/Image - Score/Text (TMP) - Score").GetComponent<TMP_Text>();
        _rePlay = transform.Find("Panel/Button - RePlay").GetComponent<Button>();
        _main = transform.Find("Panel/Button - Main").GetComponent<Button>();
        _endHide = transform.Find("Panel/Button - EndHide").GetComponent<Button>();

        _rePlay.onClick.AddListener(() =>
        {
            Hide();
            GameManager.Instance.GameReStart();
            ShowEffect(_rePlay.transform);
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
        });
        _main.onClick.AddListener(() =>
        {
            Hide();
            UIManager.Instance.Get<MainUI>().Show();
            ShowEffect(_main.transform);
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
        });
        _endHide.onClick.AddListener(() =>
        {
            Hide();
            UIManager.Instance.Get<EndShowUI>().Show();
            ShowEffect(_endHide.transform);
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
        });
    }

    public override void Show()
    {
        base.Show();
        Refresh(GameManager.Instance.Score);
        SoundManager.Instance.SFX_Play(SFX_List.GameEnd);
        ShowEffect(_panel.transform);

        GameManager.Instance.OnScoreCharge += Refresh;
    }

    public override void Hide()
    {
        base.Hide();
        GameManager.Instance.OnScoreCharge -= Refresh;
    }

    void Refresh(int score)
    {
        _score.text = $"Score\n<size=100>{score}";
    }
}