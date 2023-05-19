using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerlength = headerField.text.Length;
        int contentlength = contentField.text.Length;

        layoutElement.enabled = (headerlength > characterWrapLimit || contentlength > characterWrapLimit) ? true : false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            int headerlength = headerField.text.Length;
            int contentlength = contentField.text.Length;

            layoutElement.enabled = (headerlength > characterWrapLimit || contentlength > characterWrapLimit) ? true : false;
        }
        
    }
}
