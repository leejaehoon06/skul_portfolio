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
            //���Ŀ� ��Ƽ ���� �߰�
            PlayerManager[] players = FindObjectsOfType<PlayerManager>();
            virtualCamera.Follow = players[0].playerObjs[0].transform;
        }
    }
}
