using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] List<Sprite> healthList;
    [SerializeField] Image healthImage;
    public PlayerControllerV2 player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerControllerV2>();
    }

    private void Update()
    {
        healthImage.sprite = healthList[player.curHealth];
    }
}
