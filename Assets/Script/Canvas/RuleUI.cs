using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class RuleUI : UIBase
{
    bool FirstGame
    {
        get
        {
            if (!PlayerPrefs.HasKey("FIRST"))
            {
                PlayerPrefs.SetInt("FIRST", 1);
                return true;
            }

            return false;
        }
    }

    RuleData _ruleData;
    public RuleData RuleData
    {
        get
        {
            if (_ruleData == null)
            {
                _ruleData = Resources.Load<RuleData>("RuleData");
            }

            return _ruleData;
        }
    }

    int _currentCount = 0;
    int _maxCount => RuleData.ruleSprites.Length - 1;

    Button _close;
    Button _left;
    Button _right;

    Image _rule;
    TMP_Text _ruleDescription;
    Image _back;

    Transform _panel;

    Locale _locale;


    protected override void Awake()
    {
        base.Awake();
        _panel = transform.Find("Panel");
        _close = transform.Find("Panel/Image - Rule/Button - Close").GetComponent<Button>();
        _left = transform.Find("Panel/Image - Rule/Button - Left").GetComponent<Button>();
        _right = transform.Find("Panel/Image - Rule/Button - Right").GetComponent<Button>();
        _rule = transform.Find("Panel/Image - Rule/Image - Rule").GetComponent<Image>();
        _ruleDescription = transform.Find("Panel/Image - Rule/Text (TMP) - Rule").GetComponent<TMP_Text>();
        _back = transform.Find("Panel/Image - Rule/Image - Back").GetComponent<Image>();

        _locale = LocalizationSettings.SelectedLocale;

        Setting.Instance.OnDarkModeChange += v =>
        {
            _back.color = v ? Setting.darkColor : Setting.baseColor;
        };
        SceneManager.sceneLoaded += (s, l) =>
        {
            if (FirstGame) Show();
        };
        _close.onClick.AddListener(() =>
        {
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            HideEffect(_panel);
            ShowEffect(_close.transform);
        });
        _left.onClick.AddListener(() =>
        {
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            ShowEffect(_left.transform);
            if (_currentCount == 0) return;
            _currentCount--;
            Refresh();
        });
        _right.onClick.AddListener(() =>
        {
            SoundManager.Instance.SFX_Play(SFX_List.ButtonClick);
            ShowEffect(_right.transform);
            if (_currentCount == _maxCount) return;
            _currentCount++;
            Refresh();
        });
    }

    public override void Show()
    {
        base.Show();
        _currentCount = 0;
        Refresh();
        ShowEffect(_panel);
    }

    void Refresh()
    {
        _rule.sprite = RuleData.ruleSprites[_currentCount];
        _locale = LocalizationSettings.SelectedLocale;
        _ruleDescription.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTable",$"Rule{_currentCount}", _locale);
    }
}
