using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onenter : EventTrigger
{
    // Start is called before the first frame update

     Animator An;
    string start;
    private void Start()
    {
        An = GameObject.Find("M_Door").GetComponent<Animator>();
        start = "door_movement";
    }
    public override void OnPointerClick(PointerEventData data)
    {
        An.Play(start);
    }
}
