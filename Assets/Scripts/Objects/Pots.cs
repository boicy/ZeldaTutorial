using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pots : MonoBehaviour
{
    private Animator amimator;

    // Start is called before the first frame update
    void Start()
    {
        amimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash()
	{
        amimator.SetBool("smash", true);
        StartCoroutine(breakCoroutine());
	}

    IEnumerator breakCoroutine()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
