using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        if(virtualCamera.Follow == null || virtualCamera.Follow.gameObject.activeSelf == false)
        {
            //추후에 멀티 조건 추가
            PlayerManager[] players = FindObjectsOfType<PlayerManager>();
            virtualCamera.Follow = players[0].playerObjs[0].transform;
        }
    }
}
