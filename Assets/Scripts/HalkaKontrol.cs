using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalkaKontrol : MonoBehaviour
{
    public float donmehizi;
    public bool soladon=true;

    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if (soladon)
        {
            transform.Rotate(0f, 0f, donmehizi*Time.deltaTime);
        }
        else
        {
            transform.Rotate(0f, 0f, -donmehizi * Time.deltaTime);
        }
    }
  

}
