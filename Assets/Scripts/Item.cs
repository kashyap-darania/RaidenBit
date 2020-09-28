using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Item : MonoBehaviour
{
    [SerializeField]
    private Image m_Icon;
    [SerializeField]
    private Image m_PadLock;
    [SerializeField]
    private Text m_TitleText;
    [SerializeField]
    private Loading m_Loading;

    private Button m_Button;

    private string m_Address;

    private void Awake()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (!string.IsNullOrEmpty(m_Address))
        {
            Application.OpenURL(m_Address);
        }
    }

    public string Title
    {
        get
        {
            return m_TitleText.text;
        }
        set
        {
            m_TitleText.text = value;
        }
    }

    public bool IsAvailable
    {
        get
        {
            return !m_PadLock.gameObject.activeInHierarchy;
        }
        set
        {
            bool l_Available = value;
            m_PadLock.gameObject.SetActive(!l_Available);
            m_Button.interactable = l_Available;
        }
    }

    public string URL
    {
        set
        {
            Utilities.DownloadImage(value, OnDownloadCompletes);
        }
    }

    public string Address
    {
        get
        {
            return m_Address;
        }
        set
        {
            m_Address = value;
        }
    }

    private void OnDownloadCompletes(Sprite sprite)
    {
        m_Icon.sprite = sprite;
        m_Loading.gameObject.SetActive(false);
        m_Icon.gameObject.SetActive(true);
    }
}
