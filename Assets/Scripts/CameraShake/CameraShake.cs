using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private IEnumerator shakeCoroutine;
	private bool isShaking = false;
	public CinemachineVirtualCamera virtualCamera;
	private CinemachineBasicMultiChannelPerlin perlinNoise;

	void Start()
	{
		if (virtualCamera != null)
		{
			perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		}
		else
		{
			Debug.LogError("Null");
		}
	}

	public IEnumerator Shake(float duration, float magnitude)
	{
		if (perlinNoise == null)
		{
			Debug.LogError("Null");
			yield break;
		}

		isShaking = true;
		perlinNoise.m_AmplitudeGain = magnitude;
		perlinNoise.m_FrequencyGain = magnitude;
		float elapsed = 0.0f;

		while (elapsed < duration && isShaking)
		{
			elapsed += Time.deltaTime;
			yield return null;
		}

		perlinNoise.m_AmplitudeGain = 0;
		isShaking = false;
	}

	// Hàm gọi để bắt đầu rung camera
	public void StartShake()
	{
		if (isShaking)
		{
			StopShake();
		}
		shakeCoroutine = Shake(2.5f, 3f);
		StartCoroutine(shakeCoroutine);
	}

	public void StartShakeAttack()
	{
		if (isShaking)
		{
			StopShake();
		}
		shakeCoroutine = Shake(0.5f, 3f);
		StartCoroutine(shakeCoroutine);
	}

	public void StartNightmareShakeAttack()
	{
		if (isShaking)
		{
			StopShake();
		}
		shakeCoroutine = Shake(1f, 3f);
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

			if (perlinNoise != null)
			{
				perlinNoise.m_AmplitudeGain = 0;
			}
		}
	}
}
