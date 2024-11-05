using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject BossFinal;
    public void playerDie()
    {
		Vector3 fireNow = this.gameObject.transform.position;
		fireNow.y -= 2.1f;
		BossFinal.transform.position = fireNow;
		BossFinal.SetActive(true);
		Animator bossAnimator = BossFinal.GetComponent<Animator>();
		bossAnimator.SetBool("Live",true);
	}
	public void end()
	{
		this.gameObject.SetActive(false);
	}
}
