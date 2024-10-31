using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject BossFinal;
    public void playerDie()
    {
		BossFinal.transform.position = this.gameObject.transform.position;
		this.gameObject.SetActive(false);
		BossFinal.SetActive(true);
		Animator bossAnimator = BossFinal.GetComponent<Animator>();
		bossAnimator.SetBool("Live",true);
	}
}
