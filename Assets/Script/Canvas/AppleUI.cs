using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AppleUI : UIBase
{
    const int APPLE_DEL_AMOUNT = 10;

    bool _playing;

    List<Apple> _selectApple = new List<Apple>();
    Apple[] _apples;
    RectTransform _panel;
    Vector2 _startTouchPos;

    RectTransform _score;


    protected override void Awake()
    {
        base.Awake();
        _panel = transform.Find("Panel").GetComponent<RectTransform>();
        _apples = _panel.transform.GetComponentsInChildren<Apple>();

        GridLayoutGroup gridLayout = _panel.transform.GetComponent<GridLayoutGroup>();

        GameManager.Instance.OnPlayingCharge += v =>
        {
            _playing = v;

            if (v)
            {
                gridLayout.enabled = true;
                foreach (Apple apple in _apples)
                {
                    apple.enabled = true;
                    apple.gameObject.SetActive(true);
                    apple.Select = false;
                    apple.Amount = Random.Range(1, APPLE_DEL_AMOUNT);
                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(_panel);
                gridLayout.enabled = false;
            }
        };
    }

    void Start()
    {
        _score = UIManager.Instance.Get<ScoreUI>().Score;
    }

    void Update()
    {
        if (!_playing) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPos = ConvertToPanelLocalPosition(touch.position);
                    break;

                case TouchPhase.Moved:
                    CheckImages(ConvertToPanelLocalPosition(touch.position));
                    break;

                case TouchPhase.Ended:
                    CheckImagesInsideRect(ConvertToPanelLocalPosition(touch.position));
                    break;
            }
        }
    }

    Vector2 ConvertToPanelLocalPosition(Vector2 screenPos)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_panel, screenPos, null, out localPoint);
        return localPoint;
    }

    void CheckImages(Vector2 touch)
    {
        float xMin = Mathf.Min(_startTouchPos.x, touch.x);
        float xMax = Mathf.Max(_startTouchPos.x, touch.x);
        float yMin = Mathf.Min(_startTouchPos.y, touch.y);
        float yMax = Mathf.Max(_startTouchPos.y, touch.y);
        Rect selectionRect = new Rect(xMin, yMin, xMax - xMin, yMax - yMin);

        foreach (Apple apple in _apples)
        {
            RectTransform imgRect = apple.GetComponent<RectTransform>();

            Vector2 imgPos = imgRect.localPosition;
            Vector2 imgSize = imgRect.sizeDelta;

            Rect imgBounds = new Rect(
                imgPos.x - imgSize.x * 0.5f,
                imgPos.y - imgSize.y * 0.5f,
                imgSize.x,
                imgSize.y
            );

            apple.Select = selectionRect.Overlaps(imgBounds);
        }
    }

    private void CheckImagesInsideRect(Vector2 touch)
    {
        float xMin = Mathf.Min(_startTouchPos.x, touch.x);
        float xMax = Mathf.Max(_startTouchPos.x, touch.x);
        float yMin = Mathf.Min(_startTouchPos.y, touch.y);
        float yMax = Mathf.Max(_startTouchPos.y, touch.y);
        Rect selectionRect = new Rect(xMin, yMin, xMax - xMin, yMax - yMin);

        _selectApple.Clear();

        foreach (Apple apple in _apples)
        {
            RectTransform imgRect = apple.GetComponent<RectTransform>();

            Vector2 imgPos = imgRect.localPosition;
            Vector2 imgSize = imgRect.sizeDelta;

            Rect imgBounds = new Rect(
                imgPos.x - imgSize.x * 0.5f,
                imgPos.y - imgSize.y * 0.5f,
                imgSize.x,
                imgSize.y
            );

            if (selectionRect.Overlaps(imgBounds) && apple.enabled)
            {
                _selectApple.Add(apple);
            }
        }

        int amount = 0;

        foreach (Apple apple in _selectApple)
        {
            amount += apple.Amount;
        }

        if (amount == APPLE_DEL_AMOUNT)
        {
            SoundManager.Instance.SFX_Play(SFX_List.AppleDelete);
            List<Apple> apples = _selectApple.ToList();
            AppleDelEffect(apples);
            _selectApple.Clear();
        }
        else
        {
            foreach (Apple apple in _selectApple)
            {
                apple.Select = false;
            }
            _selectApple.Clear();
        }
    }

    void AppleDelEffect(List<Apple> apple)
    {
        Sequence seq = DOTween.Sequence();

        foreach (Apple item in apple)
        {
            RectTransform appleRect = item.GetComponent<RectTransform>();

            Vector2 localPoint = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _panel,
                _score.position,
                null,
                out localPoint
            );

            Vector2 panelSize = _panel.rect.size;

            localPoint.x += panelSize.x * 0.5f;
            localPoint.y -= panelSize.y * 0.5f;

            item.Select = false;
            item.enabled = false;

            seq.Join(appleRect.DOAnchorPos(localPoint, 0.8f)
                              .SetEase(Ease.OutQuad));
        }

        seq.Play().OnComplete(() =>
        {
            foreach (Apple item in apple)
            {
                item.gameObject.SetActive(false);
            }
            GameManager.Instance.PlusScore(apple.Count);
        });
    }
}
