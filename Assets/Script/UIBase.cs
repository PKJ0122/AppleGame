using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIBase : MonoBehaviour
{
    public int SortingOrder
    {
        get => _canvas.sortingOrder;
        set => _canvas.sortingOrder = value;
    }

    protected Canvas _canvas;


    protected virtual void Awake()
    {
        _canvas = GetComponent<Canvas>();
        UIManager.Instance.Register(this);
    }

    public virtual void Show()
    {
        if (_canvas.enabled) return;

        _canvas.enabled = true;
        UIManager.Instance.PushUI(this);
    }

    public virtual void Hide()
    {
        if (!_canvas.enabled) return;

        _canvas.enabled = false;
        UIManager.Instance.PopUI(this);
    }

    protected void ShowEffect(Transform transform)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(1.1f, 0.2f));
        seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play();
    }

    protected void HideEffect(Transform transform)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(1.1f, 0.1f));
        seq.Append(transform.DOScale(0.2f, 0.2f));

        seq.Play().OnComplete(() =>
        {
            Hide();
        });
    }
}
