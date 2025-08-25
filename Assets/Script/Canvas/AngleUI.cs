using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AngleUI : UIBase
{
    bool _playing;

    RectTransform _rect;
    Image _angle;
    Vector2 startTouchPosition; // 터치 시작 위치
    Vector2 endTouchPosition; // 터치 끝 위치


    protected override void Awake()
    {
        base.Awake();
        _angle = transform.Find("Panel/Image - Angle").GetComponent<Image>();
        _rect = _canvas.GetComponent<RectTransform>();
        GameManager.Instance.OnPlayingCharge += v =>
        {
            _playing = v;
        };
    }

    void Update()
    {
        if (!_playing) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rect, touch.position, null, out touchPos);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touchPos;
                    CreateRectangle();
                    break;

                case TouchPhase.Moved:
                    endTouchPosition = touchPos;
                    UpdateRectangle();
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touchPos;
                    UpdateRectangle();
                    DisableRectangle();
                    break;
            }
        }
    }

    void CreateRectangle()
    {
        _angle.gameObject.SetActive(true);
        _angle.GetComponent<RectTransform>().anchoredPosition = startTouchPosition;
    }

    void UpdateRectangle()
    {
        if (_angle != null)
        {
            Vector2 size = endTouchPosition - startTouchPosition;
            _angle.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
            _angle.GetComponent<RectTransform>().anchoredPosition = startTouchPosition + (size / 2);
        }
    }

    private void DisableRectangle()
    {
        _angle.gameObject.SetActive(false);
        _angle.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
    }
}