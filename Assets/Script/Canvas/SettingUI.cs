using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIBase
{
    const int TRUE = 1;

    Button _close;
    Button _bgmMute;
    Image _bgmMuteImg;
    Button _sfxMute;
    Image _sfxMuteImg;
    Button _darkMode;
    Image _darkModeImg;

    Transform _panel;


    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 60;

        _panel = transform.Find("Panel");
        _close = transform.Find("Panel/Image - Setting/Button - ESC").GetComponent<Button>();
        _bgmMute = transform.Find("Panel/Image - Setting/Image - BGM/Button - BGM").GetComponent<Button>();
        _bgmMuteImg = transform.Find("Panel/Image - Setting/Image - BGM/Image - Mute").GetComponent<Image>();
        _sfxMute = transform.Find("Panel/Image - Setting/Image - SFX/Button - SFX").GetComponent<Button>();
        _sfxMuteImg = transform.Find("Panel/Image - Setting/Image - SFX/Image - Mute").GetComponent<Image>();
        _darkMode = transform.Find("Panel/Image - Setting/Image - DrakMode/Button - DarkMode").GetComponent<Button>();
        _darkModeImg = transform.Find("Panel/Image - Setting/Image - DrakMode/Image - DarkOn").GetComponent<Image>();

        _close.onClick.AddListener(() =>
        {
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            ShowEffect(_close.transform);
            HideEffect(_panel);
        });

        _bgmMute.onClick.AddListener(() =>
        {
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            int mute = Setting.Instance.BgmMute == TRUE ? 0 : 1;
            Setting.Instance.BgmMute = mute;
            ShowEffect(_bgmMute.transform);
        });
        _sfxMute.onClick.AddListener(() =>
        {
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            int mute = Setting.Instance.SfxMute == TRUE ? 0 : 1;
            Setting.Instance.SfxMute = mute;
            ShowEffect(_sfxMute.transform);
        });
        _darkMode.onClick.AddListener(() =>
        {
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            int on = Setting.Instance.DarkMode == TRUE ? 0 : 1;
            Setting.Instance.DarkMode = on;
            ShowEffect(_darkMode.transform);
        });

        Setting setting = Setting.Instance;

        setting.OnBgmMuteChange += v =>
        {
            _bgmMuteImg.gameObject.SetActive(v);
        };
        setting.OnSfxMuteChange += v =>
        {
            _sfxMuteImg.gameObject.SetActive(v);
        };
        setting.OnDarkModeChange += v =>
        {
            _darkModeImg.gameObject.SetActive(v);
        };
    }

    public override void Show()
    {
        base.Show();
        ShowEffect(_panel.transform);
    }
}