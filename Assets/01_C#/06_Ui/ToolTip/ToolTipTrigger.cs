using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;
    [TextArea]
    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.acc.UI.toolTip.Show(content,header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.acc.UI.toolTip.Hide();
    }
}
