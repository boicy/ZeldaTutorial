using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pots : MonoBehaviour
{
    private const string IS_SMASHED = "smash";
    private const float THIRD_OF_A_SECOND = .3f;
    private Animator amimator;

    [Header("Death Effects")]
    public LootTable lootTable;

    // Start is called before the first frame update
    void Start()
    {
        amimator = GetComponent<Animator>();
    }

    public void Smash()
	{
        amimator.SetBool(IS_SMASHED, true);
        StartCoroutine(breakCoroutine());
        MakeLoot();
	}

    private void MakeLoot()
    {
        if (lootTable != null)
        {
            Powerup current = lootTable.LootPowerup();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator breakCoroutine()
    {
        yield return new WaitForSeconds(THIRD_OF_A_SECOND);
        this.gameObject.SetActive(false);
    }
}
