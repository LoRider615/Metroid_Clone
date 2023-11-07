using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public Player player;
    public TMP_Text healthText;

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.healthPoints;
    }
}
