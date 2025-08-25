using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : Singleton<Setting>
{
    public const int TRUE = 1;

    public static Color baseColor = new Color(0.9058824f, 0.9607844f, 0.8705883f);
    public static Color darkColor = new Color(0.2735849f, 0.2735849f, 0.2735849f);

    public int BgmMute
    {
        get
        {
            if (!PlayerPrefs.HasKey("BGM"))
            {
                PlayerPrefs.SetInt("BGM", 0);
            }
            return PlayerPrefs.GetInt("BGM");
        }
        set
        {
            PlayerPrefs.SetInt("BGM", value);
            OnBgmMuteChange?.Invoke(value == TRUE);
        }
    }

    public int SfxMute
    {
        get
        {
            if (!PlayerPrefs.HasKey("SFX"))
            {
                PlayerPrefs.SetInt("SFX", 0);
            }
            return PlayerPrefs.GetInt("SFX");
        }
        set
        {
            PlayerPrefs.SetInt("SFX", value);
            OnSfxMuteChange?.Invoke(value == TRUE);
        }
    }

    public int DarkMode
    {
        get
        {
            if (!PlayerPrefs.HasKey("DRAKMODE"))
            {
                PlayerPrefs.SetInt("DRAKMODE", 0);
            }
            return PlayerPrefs.GetInt("DRAKMODE");
        }
        set
        {
            PlayerPrefs.SetInt("DRAKMODE", value);
            OnDarkModeChange?.Invoke(value == TRUE);
        }
    }

    public event Action<bool> OnBgmMuteChange;
    public event Action<bool> OnSfxMuteChange;
    public event Action<bool> OnDarkModeChange;


    protected override void Awake()
    {
        base.Awake();
        OnDarkModeChange += v =>
        {
            Camera.main.backgroundColor = v ? darkColor : baseColor;
        };
        SceneManager.sceneLoaded += (s, l) =>
        {
            OnBgmMuteChange?.Invoke(BgmMute == TRUE);
            OnSfxMuteChange?.Invoke(SfxMute == TRUE);
            OnDarkModeChange?.Invoke(DarkMode == TRUE);
        };
    }
}
