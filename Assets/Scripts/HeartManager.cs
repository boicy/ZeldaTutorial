using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("initalising hearts");
        InitHearts();
    }

    public void InitHearts()
    {
        Debug.Log("Number of containers to initalise: " + heartContainers.initialValue);
        for (int i=0; i < heartContainers.initialValue; i++)
        {
            Debug.Log("initing heart: " + i);
            hearts[i].sprite = fullHeart;
            hearts[i].gameObject.SetActive(true);
        }
    }
}
