using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class co : MonoBehaviour
{
	public GameObject objectToSpawn; // Đối tượng sẽ được tạo ra
	public float spawnInterval = 5.0f; // Khoảng thời gian giữa các lần tạo đối tượng
	public float fallHeight = 10f; // Chiều cao rơi từ vị trí của GameObject này
	public float fallSpeed = 2f; // Tốc độ rơi của đối tượng

	private GameObject player;
	private float timer;

	void Start()
	{
		// Tìm đối tượng có tag "Player"
		player = GameObject.FindGameObjectWithTag("Player");
		if (player == null)
		{
			Debug.LogError("Không tìm thấy đối tượng có tag 'Player'.");
		}
		timer = 0;
	}

	void Update()
	{
		if (player != null)
		{
			timer += Time.deltaTime;
			if (timer >= spawnInterval)
			{
				// Tạo một đối tượng mới tại vị trí phía trên đối tượng Player
				Vector3 spawnPosition = player.transform.position + new Vector3(0, fallHeight, 0);
				GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

				// Kiểm tra nếu đối tượng được tạo có Rigidbody, nếu không thì thêm vào
				Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
				if (rb == null)
				{
					rb = spawnedObject.AddComponent<Rigidbody>();
				}

				// Thiết lập vận tốc rơi của đối tượng
				rb.velocity = new Vector3(0, -fallSpeed, 0); // Đặt vận tốc rơi xuống với tốc độ fallSpeed
				rb.useGravity = false; // Vô hiệu hóa trọng lực để chỉ sử dụng vận tốc do script điều khiển

				timer = 0; // Reset lại bộ đếm
			}
		}
	}
}
