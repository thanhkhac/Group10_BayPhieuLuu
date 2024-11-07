using System.Collections;
using System.Collections.Generic;
using ThanhNK;
using UnityEngine;
using UnityEngine.UI;
using static BossMove;
using UnityEngine.SceneManagement;

public class BossFinalMove : MonoBehaviour
{
	public float moveSpeed = 1f; // Tốc độ di chuyển
	public float stopDistance = 1f; // Khoảng cách dừng lại
	public GameObject response1;
	public GameObject response2;
	[SerializeField] GhostScript ghost;
	private Transform playerTransform; // Biến để lưu Transform của Player
	Animator animator;
	private float delayTime = 5f;
	float delayAtk = 0;
	bool checkRolateBoss = true;
	private System.Random random = new System.Random();
	public Image Health;
	float timeToResponse = 0f;
	bool Response = true;
	void Start()
	{
		BossMove.BossHealth.health = 1000f;
		animator = GetComponent<Animator>();
		// Tìm đối tượng có tag "Player"
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (player != null)
		{
			playerTransform = player.transform; // Lưu Transform của Player
		}
		else
		{
			Debug.LogWarning("No Player found with tag 'Player'.");
		}
	}

	void Update()
	{
		if(BossMove.BossHealth.health <= 0)
		{
			animator.SetTrigger("Die");
			moveSpeed = 0f;
			Response = false;
		}
		Vector3 relativePosition = playerTransform.position - transform.position;
		delayTime += Time.deltaTime;
		if (playerTransform != null)
		{
			// Tính toán hướng di chuyển (bỏ qua thành phần Y)
			Vector3 direction = new Vector3(
				playerTransform.position.x - transform.position.x,
				0, // Bỏ qua trục Y
				playerTransform.position.z - transform.position.z
			).normalized;

			float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
			// Kiểm tra khoảng cách đến Player

			moveWithPlayer(distanceToPlayer, direction, relativePosition);
			Acttack(distanceToPlayer);

			int randomNumber = random.Next(1, 4);
			timeToResponse += Time.deltaTime;
			if (timeToResponse >= 5f && Response == true)
			{
				if (randomNumber == 1)
				{
					SummonGhost(response1);
					timeToResponse = 0f;
				}
				if (randomNumber == 2)
				{
					SummonGhost(response2);
					timeToResponse = 0f;
				}
				if(randomNumber == 3)
				{
					SummonGhost(response1);
					SummonGhost(response2);
					timeToResponse = 0f;
				}
			}
			

		}
	}

	void SummonGhost(GameObject response)
	{
		var newGhost = Instantiate(ghost);
		newGhost.transform.position = response.transform.position;
		newGhost.SetDirection(new Vector2(transform.localScale.x, transform.localScale.y));
		Vector3 scale = newGhost.transform.localScale; // Lấy giá trị hiện tại của scale
		scale.x = 1.2974f; // Thay đổi giá trị của x
		scale.y = 1.1593f;
		scale.z = 1;
		newGhost.transform.localScale = scale;
		newGhost.gameObject.SetActive(true);
	}

	public void moveWithPlayer(float distanceToPlayer, Vector3 direction, Vector3 relativePosition)
	{
		if (distanceToPlayer > stopDistance)
		{
			// Di chuyển theo hướng của Player
			transform.position += direction * moveSpeed * Time.deltaTime;
			animator.SetBool("IsMove", true);
		}
		if (relativePosition.x < 0) // Nếu Player ở phía sau Boss
		{
			if (checkRolateBoss)
			{
				transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
			}
			checkRolateBoss = false;
		}
		if (relativePosition.x > 0) // Nếu Player ở phía sau Boss
		{
			if (!checkRolateBoss)
			{
				transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
			}
			checkRolateBoss = true;
		}
	}


	public void Acttack(float distanceToPlayer)
	{
		if (distanceToPlayer <= stopDistance)
		{
			animator.SetBool("IsMove", false);
		}
		if (distanceToPlayer <= stopDistance + 2f)
		{
			animator.SetBool("IsMove", false);
			if (delayAtk >= 2f)
			{
				BossMove.BossDame.dame = 25;
				animator.SetTrigger("Atk");
				delayAtk = 0f;
			}
			delayAtk += Time.deltaTime;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "PlayerAttack")
		{
			animator.SetTrigger("TakeHit");
			BossHealth.health -= 20f;
			Health.fillAmount = BossHealth.health / 1000f;
			Debug.Log(BossHealth.health);
		}
	}
	public void die()
	{
		PlayerData.Point +=100;
		PlayerData.Kills +=1;
		this.gameObject.SetActive(false);
		SceneManager.LoadScene("WinGame");
		PlayerData.CurrentLevel = 1;
		PlayerData.PLayerHealth = 100;
		PlayerData.PLayerMana = 100;
		PlayerData.OldPlayerMana = 100;
		PlayerData.OldPLayerHealth = 100;
		PlayerData.OldKills = 0;
		PlayerData.OldPoints = 0;
	}


}
