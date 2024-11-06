using System.Collections;
using System.Collections.Generic;
using ThanhNK;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Text Text;
    void Start()
    {
        Text.text = $"Point:{PlayerData.Point}\nKill:{PlayerData.Kills}";
    }


    void Update()
    {
        Text.text = $"Point:{PlayerData.Point}\nKill:{PlayerData.Kills}";
    }
}
