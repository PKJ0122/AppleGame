using UnityEngine.UI;

public class EndShowUI : UIBase
{
    Button _endShow;


    protected override void Awake()
    {
        base.Awake();
        _endShow = transform.Find("Panel/Button - EndShow").GetComponent<Button>();
        _endShow.onClick.AddListener(() =>
        {
            Hide();
            UIManager.Instance.Get<EndUI>().Show();
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            ShowEffect(_endShow.transform);
        });
    }
}
