using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class BossMove : MonoBehaviour
{
    public float moveSpeed = 1f; // Tốc độ di chuyển
    public float stopDistance = 1f; // Khoảng cách dừng lại
    private Transform playerTransform; // Biến để lưu Transform của Player
    Animator animator;
    private float delayTime = 5f;
    float delayAtk = 0;
    bool checkRolateBoss = true;
    private System.Random random = new System.Random();
	public Image Health;
	public GameObject Fire;

	public class BossHealth
	{
		public static float health = 500f;
	}
	public class BossDame
	{
		public static int dame = 10;
	}
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

			if(BossHealth.health >= 0)
			{
				delayAtk += Time.deltaTime;
				moveWithPlayer(distanceToPlayer, direction, relativePosition);
				Acttack(distanceToPlayer);
			}
			checkHeathBoss();

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
		
		if (distanceToPlayer <= stopDistance + 0.5f)
		{
			animator.SetBool("IsMove", false);
		}
		if (distanceToPlayer <= stopDistance + 3f)
		{
			Debug.Log(delayAtk);
			if (delayAtk >= 2f)
			{
				
				int randomNumber = random.Next(1, 4);
				if (randomNumber == 1)
				{
					BossDame.dame = 15;
					animator.SetTrigger("Atk1");
				}
				if (randomNumber == 2)
				{
					BossDame.dame = 17;
					animator.SetTrigger("Atk2");
				}
				if (randomNumber == 3)
				{
					BossDame.dame = 19;
					animator.SetTrigger("Atk3");
				}
				if (randomNumber == 4)
				{
					BossDame.dame = 20;
					animator.SetTrigger("AtkSp");
				}
				delayAtk = 0f;
			}
			
		}
	}



	public void fireBossDie()
	{
		
		Fire.SetActive(true);
	}

	public void checkHeathBoss()
	{
		if(BossHealth.health <= 0)
		{
			Vector3 fireNow = this.gameObject.transform.position;
			fireNow.y += 3f;
			Fire.transform.position = fireNow;
			animator.SetTrigger("Death");
		}
	}
	public void bossCbDie()
	{
		moveSpeed = 0f;
	}
	public void bossDie()
	{
		this.gameObject.SetActive(false);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "PlayerAttack")
		{
			animator.SetTrigger("TakeHit");
			BossHealth.health -= 150;
			Health.fillAmount = BossHealth.health / 500f;
		}
	}


}
