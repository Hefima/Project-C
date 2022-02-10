using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    public ToolTip toolTip;

    RectTransform toolTipRect;

    private void Awake()
    {
        toolTipRect = toolTip.GetComponent<RectTransform>();
    }

    public void Show(string _content, string _header = "")
    {
        if(string.IsNullOrEmpty(_header) && string.IsNullOrEmpty(_content))
        {
            return;
        }

        toolTip.SetText(_content, _header);
        toolTip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        toolTip.gameObject.SetActive(false);
    }

    public void FollowMouse()
    {
        Vector2 mousePos = Input.mousePosition;

        float pivotX = Mathf.Round(mousePos.x / Screen.width);
        float pivotY = Mathf.Round(mousePos.y / Screen.height);

        toolTipRect.pivot = new Vector2(pivotX, pivotY);

        toolTip.transform.position = mousePos;
    }
}
