using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComBatBoss : MonoBehaviour
{
	public Canvas canvas; // Canvas để hiển thị thông tin chiến đấu
	public GameObject boss; // Boss sẽ xuất hiện khi va chạm với người chơi

	void Start()
	{
		// Ẩn canvas và boss ban đầu
		if (canvas != null)
		{
			canvas.gameObject.SetActive(false);
		}
		if (boss != null)
		{
			boss.SetActive(false);
		}
	}

	void Update()
	{
		// Bạn có thể thêm logic khác tại đây nếu cần
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) // Kiểm tra xem collider có tag "Player" không
		{
			// Kích hoạt canvas và boss khi người chơi va chạm
			if (canvas != null)
			{
				canvas.gameObject.SetActive(true); // Kích hoạt canvas
			}

			if (boss != null)
			{
				boss.SetActive(true); // Kích hoạt boss
			}
		}
	}

}
