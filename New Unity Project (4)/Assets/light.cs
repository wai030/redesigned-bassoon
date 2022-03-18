using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class light : MonoBehaviour
{
    // Start is called before the first frame update
    Light self;
    bool on = true;
    CancellationTokenSource c;
    private void Awake()
    {
        self = gameObject.GetComponent<Light>();
        c = new CancellationTokenSource();
    }
    void Start()
    {
        seton(c);
    }

    async void seton(CancellationTokenSource c)
    {
       
        try
        {
            while (on)
            {

                self.enabled = true;
                await Task.Delay(200);
                self.enabled = false;
                await Task.Delay(200);
                self.enabled = true;
                await Task.Delay(200);
                self.enabled = false;
                await Task.Delay(400);
                self.enabled = true;
                await Task.Delay(1000);
                self.enabled = false;
                await Task.Delay(2000);
                self.enabled = true;
                await Task.Delay(5000);

                if (on == false)
                {
                    break;
                }
            }
        }
        catch (System.OperationCanceledException) when (c.IsCancellationRequested)
        {
            on = false;
        }
    }
    async void blink(int first, int second)
    {
        self.enabled = true;
        await Task.Delay(first);
        self.enabled = false;
        await Task.Delay(second);
    }
    private void OnApplicationQuit()
    {
        c.Cancel();
    }
}
