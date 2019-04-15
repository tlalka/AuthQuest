using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverHUD : MonoBehaviour, IPointerEnterHandler
{
    public bool isRange;
    public GameObject player;
    //public GameObject parent;
    public Canvas canvas;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        canvas = GameObject.Find("HUD").GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GameObject textGO = new GameObject();
        textGO.transform.parent = this.transform;
        textGO.AddComponent<Text>();

        text = textGO.GetComponent<Text>();
        text.font = arial;
        text.text = "Press space key";
        text.fontSize = 22;
        text.fontStyle = FontStyle.Bold;
        //text.alignment = TextAnchor.LowerLeft;

        //parent = GameObject.Find("Inventory 1");
        RectTransform rectTransform;
        //rectTransform = this.GetComponent<RectTransform>();
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(120, 0, 0);
        rectTransform.sizeDelta = new Vector2(100, 100);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject weapon;
        Debug.Log("Mouse is over GameObject.");
        if (isRange)
        {
            weapon = player.GetComponent<PlayerController>().rangeWeapon;
        }
        else
        {
            weapon = player.GetComponent<PlayerController>().meleeWeapon;
        }

        int attack = weapon.GetComponent<WeaponStats>().weaponAttack;
        int speed = weapon.GetComponent<WeaponStats>().weaponSpeed;

    }
}
