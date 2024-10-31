using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFinalMove : MonoBehaviour
{
	public float moveSpeed = 1f; // Tốc độ di chuyển
	public float stopDistance = 1f; // Khoảng cách dừng lại
	private Transform playerTransform; // Biến để lưu Transform của Player
	Animator animator;
	private float delayTime = 5f;
	float delayAtk = 0;
	bool checkRolateBoss = true;
	private System.Random random = new System.Random();
	public Transform AttackPoint;
	public float attackRange;
	public LayerMask attackPlayer;
	public float BossHealth = 500f;
	public Image Health;
	public Image Mana;

	void Start()
	{

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


		}
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
			if (delayAtk >= 3f)
			{		
				animator.SetTrigger("Atk");
				BossHealth -= 10f;
				Health.fillAmount = BossHealth / 500f;
				Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, attackPlayer);
				Debug.Log(hitPlayer.Length);
				foreach (Collider2D attack in hitPlayer)
				{
					Debug.Log(attack.name);
				}
				delayAtk = 0f;
			}
			delayAtk += Time.deltaTime;
		}
	}
	void OnDrawGizmos()
	{
		if (AttackPoint == null)
			return;

		Gizmos.color = Color.red;  // Đặt màu cho vòng tròn Gizmos
		Gizmos.DrawWireSphere(AttackPoint.position, attackRange);  // Vẽ vòng tròn tại AttackPoint
	}


}
