using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ű�����������2D��Ϸ����Ļ���ƥ��
// ����Ǻ��������Ϸ������Ҫ������ű�
public class CamRatioAdapter : MonoBehaviour
{
    // ���Խ������Ļ��߱�3:4 = 0.75��Ӧ�����size3.75
    // ��Ļ��߱�9:16 = 0.5625��Ӧ�������С5

    [Tooltip("��Ļ��/��")]
    [SerializeField]
    float ratio1 = 0.75f;

    [Tooltip("���������Size")]
    [SerializeField]
    float size1 = 3.75f;

    [Tooltip("��Ļ��/��")]
    [SerializeField]
    float ratio2 = 0.5625f;

    [Tooltip("���������Size")]
    [SerializeField]
    float size2 = 5f;

    Camera cam;

    void Update()
    {
        // ��ȡ��Ļ���������������������size
        float curRatio = (float)Screen.width / Screen.height;

        // �����Ĭ������߶ȡ�����߱��������size�ɷ���ʱ���ܵõ������ȵĽ��
        float a = ratio1 * size1;
        float size = a / curRatio;
        
        cam = GetComponent<Camera>();
        cam.orthographicSize = size;
    }

}
