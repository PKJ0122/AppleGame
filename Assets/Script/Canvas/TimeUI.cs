using TMPro;
using UnityEngine.UI;

public class TimeUI : UIBase
{
    Slider _slider;
    TMP_Text _text;


    protected override void Awake()
    {
        base.Awake();
        _slider = transform.Find("Slider - Time").GetComponent<Slider>();
        _text = _slider.transform.Find("Text (TMP) - Time").GetComponent<TMP_Text>();

        GameManager.Instance.OnTickTimeChange += v =>
        {
            if (v <= 0) v = 0;
            _slider.value = v / GameManager.TICK_TIME;
            _text.text = v.ToString("0");
        };
    }
}
