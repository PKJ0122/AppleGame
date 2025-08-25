using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUI : UIBase
{
    TMP_Text _score;

    public RectTransform Score => _score.rectTransform;


    protected override void Awake()
    {
        base.Awake();
        _score = transform.Find("Text (TMP) - Score").GetComponent<TMP_Text>();
        GameManager.Instance.OnScoreCharge += v =>
        {
            _score.text = v.ToString();
            
            Sequence seq = DOTween.Sequence();

            seq.Append(_score.transform.DOScale(1.3f, 0.3f));
            seq.Append(_score.transform.DOScale(1f, 0.15f));

            seq.Play();
        };
    }
}