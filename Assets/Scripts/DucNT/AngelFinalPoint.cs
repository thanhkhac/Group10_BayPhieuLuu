using System.Collections;
using System.Collections.Generic;
using ThanhNK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AngelFinalPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("HitPlayer");
            LoadScene();
        }
    }


    void LoadScene()
    {
        var scene = SceneManager.GetActiveScene().name;

        switch (scene)
        {
            case "Level1":
                {
                    SceneManager.LoadScene("Level2");
                    PlayerData.CurrentLevel = 2;
                    PlayerData.OldPLayerHealth = PlayerData.PLayerHealth;
                    PlayerData.OldPlayerMana = PlayerData.PLayerMana;
                    break;
                }
            case "Level2":
                {
                    SceneManager.LoadScene("WinGame");
                    break;
                }
        }
    }
}
