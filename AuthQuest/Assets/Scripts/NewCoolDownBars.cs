using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCoolDownBars : MonoBehaviour
{
    private Image barImage;

    private void Awake() {
        barImage = transform.Find("bar").GetComponent<Image>();

        barImage.fillAmount = .3f;
    }

}
