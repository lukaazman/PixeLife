using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleMinion : MonoBehaviour
{
    public GameObject Player;
    public bool isVisible;
    void Start()
    {
        isVisible = false;
        Player.SetActive(false);
    }

    void Update()
    {
        if (isVisible)
        {
            Player.SetActive(true);
        }

    }
}
