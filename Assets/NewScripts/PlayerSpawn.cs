using System.Collections;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform spawnPoint; // 🚀 Inspector에서 `SpawnPoint` 연결

    // 🚨 게임 시작 시 자동으로 이동하지 않음!
    public void MovePlayerToSpawn()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null && spawnPoint != null)
        {
            Debug.Log($"🔹 PlayerSpawn: 포탈 이동 → SpawnPoint 위치 {spawnPoint.position}");

            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }

            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;

            if (cc != null)
            {
                cc.enabled = true;
            }

            Debug.Log($"🔹 PlayerSpawn: 이동 후 플레이어 위치 → {player.transform.position}");
        }
        else
        {
            Debug.LogError("❌ PlayerSpawn: 플레이어나 SpawnPoint를 찾을 수 없음!");
        }
    }
}









//public class PlayerSpawn : MonoBehaviour
//{
//    public Transform spawnPoint; //  Inspector에서 설정할 스폰 위치

//    private void Awake() //  씬이 로드되면 바로 실행 (플레이어 강제 이동)
//    {
//        MovePlayerToSpawn();
//    }

//    private void MovePlayerToSpawn()
//    {
//        GameObject player = GameObject.FindWithTag("Player");

//        if (player != null && spawnPoint != null)
//        {
//            Debug.Log(" Stage 씬에서 플레이어 스폰포인트로 강제 이동");
//            player.transform.position = spawnPoint.position;
//        }
//        else
//        {
//            Debug.LogError(" PlayerSpawn: 플레이어나 SpawnPoint를 찾을 수 없음!");
//        }
//    }
//}



