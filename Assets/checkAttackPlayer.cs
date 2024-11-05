using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BossMove;

public class checkAttackPlayer : MonoBehaviour
{
	public Image Health;
	 float count = 0f;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		count += Time.deltaTime;
		Health.fillAmount = BossHealth.health / 500f;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" && count >=1.2)
		{
			Debug.Log(1);
			BossHealth.health -= BossDame.dame;
			Health.fillAmount = BossHealth.health / 500f;
		}
	}
	

}
