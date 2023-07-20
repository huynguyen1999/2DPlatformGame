using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public Transform RespawnPoint;
    public GameObject player;
    public float RespawnDelay;
    private CinemachineVirtualCamera _virtualCamera;

    public static GameManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        _virtualCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
    }

    public void Respawn()
    {
        StartCoroutine(DelayRespawn());
    }

    private IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(RespawnDelay);
        var respawnedPlayer = Instantiate(player, RespawnPoint.position, RespawnPoint.rotation);
        _virtualCamera.m_Follow = respawnedPlayer.transform;
    }
}
