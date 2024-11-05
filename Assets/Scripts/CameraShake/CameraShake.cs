using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private IEnumerator shakeCoroutine;
	private bool isShaking = false;
	public Camera camera;
	// Hàm coroutine thực hiện rung camera
	public IEnumerator Shake(float duration, float magnitude)
	{
		if (camera == null)
		{
			Debug.LogError("Camera không được truyền vào.");
			yield break;
		}

		Vector3 originalPosition = camera.transform.localPosition;
		float elapsed = 0.0f;
		isShaking = true;

		while (elapsed < duration && isShaking)
		{
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;

			camera.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		camera.transform.localPosition = originalPosition;
		isShaking = false;
	}

	// Hàm gọi để bắt đầu rung camera trong 1 giây
	public void StartShake()
	{
		if (isShaking)
		{
			StopShake();
		}
		shakeCoroutine = Shake(3.0f, 0.15f); // Rung trong 1 giây với cường độ 0.1
		StartCoroutine(shakeCoroutine);
	}

	public void StartShakeAttack()
	{
		if (isShaking)
		{
			StopShake();
		}
		shakeCoroutine = Shake(0.5f, 0.15f); // Rung trong 1 giây với cường độ 0.1
		StartCoroutine(shakeCoroutine);
	}
    public void StartNightmareShakeAttack()
    {
        if (isShaking)
        {
            StopShake();
        }
        shakeCoroutine = Shake(1f, 0.15f); // Rung trong 1 giây với cường độ 0.1
        StartCoroutine(shakeCoroutine);
    }


    // Hàm gọi để dừng rung camera
    public void StopShake()
	{
		if (shakeCoroutine != null)
		{
			isShaking = false;
			StopCoroutine(shakeCoroutine);
			shakeCoroutine = null;
		}
	}
}
