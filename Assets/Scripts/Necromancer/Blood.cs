using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    public Image blood;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateBlood(float currentBlood, float maxBlood)
    {
        blood.fillAmount = currentBlood / maxBlood;
    }
}
