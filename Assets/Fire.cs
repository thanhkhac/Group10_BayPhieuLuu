using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject BossFinal;
    public void playerDie()
    {
 
		this.gameObject.SetActive(false);
		BossFinal.SetActive(true);
		Animator bossAnimator = BossFinal.GetComponent<Animator>();
		bossAnimator.SetBool("Live",true);
	}
}
