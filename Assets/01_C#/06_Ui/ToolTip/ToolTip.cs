using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public Text header;
    public Text content;

    public void SetText(string _content, string _header = "")
    {
        if (string.IsNullOrEmpty(_header))
        {
            header.gameObject.SetActive(false);
        }
        else
        {
            header.gameObject.SetActive(true);
            header.text = _header;
        }
        content.text = _content;
    }
}
