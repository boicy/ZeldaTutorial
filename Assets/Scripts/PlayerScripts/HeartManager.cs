using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [Header("Heart Sprite stuff")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;

    [Header("Health containers")]
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;


    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
        Debug.Log("Runtime Current Hearts: " + heartContainers.RuntimeValue);
        Debug.Log("Runtime Current Health: " + playerCurrentHealth.RuntimeValue);

    }

    public void InitHearts()
    {
        //for (int i=0; i < heartContainers.RuntimeValue; i++)
        //{
        //    if (i < hearts.Length) { 
        //        hearts[i].sprite = fullHeart;
        //        hearts[i].gameObject.SetActive(true);
        //    }
        //}


        Debug.Log("Current Hearts: " + heartContainers.RuntimeValue);
        Debug.Log("Current Health: " + playerCurrentHealth.RuntimeValue);
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        Debug.Log("Temp health: " + tempHealth);
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if (i <= tempHealth - 1)
            {
                //Full Heart
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                //empty
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                //half
                hearts[i].sprite = halfFullHeart;
            }
            hearts[i].gameObject.SetActive(true);
        }
    }

    public void UpdateHearts()
    {
        InitHearts();
        //Debug.Log("Current Hearts: " + heartContainers.RuntimeValue);
        //Debug.Log("Current Health: " + playerCurrentHealth.RuntimeValue);
        //float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        //Debug.Log("Temp health: " + tempHealth);
        //for (int i=0; i < heartContainers.RuntimeValue; i++)
        //{
        //    if (i <= tempHealth - 1)
        //    {
        //        //Full Heart
        //        hearts[i].sprite = fullHeart;
        //    }
        //    else if (i >= tempHealth)
        //    {
        //        //empty
        //        hearts[i].sprite = emptyHeart;
        //    }
        //    else
        //    {
        //        //half
        //        hearts[i].sprite = halfFullHeart;
        //    }
        //}
    }
}
