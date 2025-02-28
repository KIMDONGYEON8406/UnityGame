using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElectricEffect : MonoBehaviour
{
    public GameObject electricEffectPrefab; // ���� ����Ʈ ������
    private GameObject currentEffect;       // ���� Ȱ��ȭ�� ����Ʈ
    private CharacterController controller; // �÷��̾� �̵� ����

    void Start()
    {
        controller = GetComponent<CharacterController>(); // CharacterController ��������
    }

    void Update()
    {
        // �÷��̾��� �̵� �ӵ��� üũ
        if (controller.velocity.magnitude > 0.1f) // �̵� ���̸�
        {
            if (currentEffect == null) // ���� ����Ʈ�� ���� ���� ����
            {
                currentEffect = Instantiate(electricEffectPrefab, GetFootPosition(), Quaternion.identity);
                currentEffect.transform.parent = transform; // �÷��̾ ����ٴϰ� ����
            }
        }
        else // �÷��̾ ���߸�
        {
            if (currentEffect != null) // ����Ʈ ����
            {
                Destroy(currentEffect);
                currentEffect = null;
            }
        }

        // ����Ʈ�� ����ؼ� �� �ؿ� ��ġ�ϵ��� ������Ʈ
        if (currentEffect != null)
        {
            currentEffect.transform.position = GetFootPosition();
        }
    }

    // �÷��̾� �� ��ġ�� ��ȯ�ϴ� �Լ�
    Vector3 GetFootPosition()
    {
        return new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
    }
}

