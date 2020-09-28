using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetail
{
    public string title { get; set; }
    public string image { get; set; }
    public bool available { get; set; }
    public string address { get; set; }
}

public class Items
{
    public string messageOfTheDay { get; set; }
    public string imagePath { get; set; }
    public List<ItemDetail> items { get; set; }
}


public class Manager : MonoBehaviour
{
    [SerializeField]
    private Text m_Title;

    [SerializeField]
    private Item m_ItemPrefab;

    [SerializeField]
    private Transform m_ItemsParent;

    [SerializeField]
    private ScrollRect m_ScrollRect;

    private void Awake()
    {
        // load json
        Items l_Items = ParseJson();

        if (l_Items != null)
        {
            InitilizeItems(l_Items);
        }
    }

    private void InitilizeItems(Items items)
    {
        m_Title.text = items.messageOfTheDay;

        string l_ImagePath = items.imagePath;

        Item l_GeneratedItem;
        foreach (var item in items.items)
        {
            l_GeneratedItem = Instantiate(m_ItemPrefab, m_ItemsParent);
            l_GeneratedItem.Title = item.title;
            l_GeneratedItem.IsAvailable = item.available;
            l_GeneratedItem.URL = Path.Combine(l_ImagePath, item.image);
            l_GeneratedItem.Address = item.address;
        }

        m_ScrollRect.verticalNormalizedPosition = 1;
    }

    private Items ParseJson()
    {
        TextAsset l_TextAsset = Resources.Load<TextAsset>("Items");
        if (l_TextAsset == null)
        {
            Debug.LogError("Items.json file not found at Resources");
            return null;
        }

        if (string.IsNullOrEmpty(l_TextAsset.text))
        {
            Debug.LogError("Items.json file is empty");
            return null;
        }

        Items l_Items = Newtonsoft.Json.JsonConvert.DeserializeObject<Items>(l_TextAsset.text);
        if (l_Items == null)
        {
            Debug.LogError("Items.json parse error");
            return null;
        }

        return l_Items;
    }
}
