using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSupplies : MonoBehaviour
{
    public GameObject restore1, restore2, upgrade1, upgrade2;

    void Start()
    {
        int r = Random.Range(0, 2);

        if(r == 1)
        {
            restore1.SetActive(true);
        }
        else
        {
            restore2.SetActive(true);
        }

        int u = Random.Range(0, 2);

        if (u == 1)
        {
            upgrade1.SetActive(true);
        }
        else
        {
            upgrade2.SetActive(true);
        }
    }

}
