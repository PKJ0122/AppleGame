using UnityEngine;
using UnityEngine.UI;

public class ESCUI : UIBase
{
    Button _esc;
    Button _hide;

    Transform _panel;


    protected override void Awake()
    {
        base.Awake();
        _panel = transform.Find("Panel");
        _esc = transform.Find("Panel/Image - ESC/Button - ESC").GetComponent<Button>();
        _hide = transform.Find("Panel/Image - ESC/Button - Hide").GetComponent<Button>();
        _esc.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        _hide.onClick.AddListener(() =>
        {
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            HideEffect(_panel);
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Show();
    }

    public override void Show()
    {
        base.Show();
        SoundManager.Instance.SFX_Play(SFX_List.UiShowHide);
        ShowEffect(_panel);
    }
}
