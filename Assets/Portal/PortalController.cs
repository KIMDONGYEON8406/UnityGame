using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PortalController : MonoBehaviour
{
    public GameObject portalUI;
    public Transform targetPosition;
    public PortalTransport portalTransport;
    private bool isPortalActive = false;
    private SkeletonSpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<SkeletonSpawner>(); // ������ ã��

        UIManager.instance.SetUIVisibility(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenPortalUI();
        }
    }

    private void Update()
    {
        if (isPortalActive)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                GameManager.instance.FadeInOut(() =>
                {
                    PortalTransition();
                });
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                ClosePortalUI();
            }
        }
    }

    private void OpenPortalUI()
    {
        portalUI.SetActive(true);
        isPortalActive = true;
    }

    private void ClosePortalUI()
    {
        portalUI.SetActive(false);
        isPortalActive = false;
    }

    private void PortalTransition()
    {
        if (portalTransport != null)
        {
            portalTransport.MovePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), targetPosition);
        }

        ClosePortalUI();

        if (spawner != null)
        {
            spawner.ActivateEnemies(); // ��Ż�� ����ϸ� ���� Ȱ��ȭ
        }

        UIManager.instance.SetUIVisibility(true);
    }
}




//public class PortalController : MonoBehaviour
//{
//    public GameObject portalUI; // PortalUI �г�
//    public Transform targetPosition; // �̵��� ������ ��ġ (Inspector���� ����)
//    public PortalTransport portalTransport; // ��Ż �̵� ó�� ��ũ��Ʈ ����

//    private bool isPortalActive = false; // PortalUI Ȱ��ȭ ����

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player")) // �÷��̾ ��Ż�� ����� ���� UI Ȱ��ȭ
//        {
//            OpenPortalUI();
//        }
//    }

//    private void Update()
//    {
//        if (isPortalActive)
//        {
//            if (Input.GetKeyDown(KeyCode.C))
//            {
//                //  Fade ȿ�� ���� �� �̵� ����
//                GameManager.instance.FadeInOut(() =>
//                {
//                    PortalTransition();
//                });
//            }
//            else if (Input.GetKeyDown(KeyCode.X))
//            {
//                ClosePortalUI();
//            }
//        }
//    }

//    private void OpenPortalUI()
//    {
//        portalUI.SetActive(true);
//        isPortalActive = true;
//    }

//    private void ClosePortalUI()
//    {
//        portalUI.SetActive(false);
//        isPortalActive = false;
//    }

//    private void PortalTransition()
//    {
//        //  UI �ݱ� (Fade �Ŀ��� �����ִ� ���� �ذ�)
//        ClosePortalUI();

//        //  �÷��̾� �̵� ����
//        if (portalTransport != null)
//        {
//            portalTransport.MovePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), targetPosition);
//        }
//        else
//        {
//            Debug.LogError("PortalController: portalTransport�� �������� �ʾҽ��ϴ�.");
//        }
//    }
//}



