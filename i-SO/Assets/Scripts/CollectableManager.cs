using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
    public static int collectableAmount;        // The collectable amount
    public Text collectableText;                // The text displaying the collectable amount

    void Start()
    {
        // Setting up the references
        collectableText = GetComponent<Text>();
        collectableAmount = 0;
    }

    void Update()
    {
        // Convert the text amount into a number
        collectableText.text = collectableAmount.ToString();
    }
}
