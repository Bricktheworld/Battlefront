using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldTactile : MonoBehaviour
{

    [SerializeField] private GameObject HoverBorder;

    public void Hover()
    {
        HoverBorder.SetActive(true);
    }

    public void NotHover()
    {
        HoverBorder.SetActive(false);
    }
}
