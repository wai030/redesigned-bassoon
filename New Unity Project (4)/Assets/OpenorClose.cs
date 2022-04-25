using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenorClose : MonoBehaviour
{
    [SerializeField] Animator An;
    [SerializeField] string start, revstart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        An.Play(start);
    }
    public void RevPlay()
    {
        An.Play(revstart);
    }
}
