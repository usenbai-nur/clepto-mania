using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewelry : MonoBehaviour
{
    public string jewelName;
    public int cost;

    private void Start()
    {
        GameManager.instance.maxPossibleJewelry += cost;
    }
}
