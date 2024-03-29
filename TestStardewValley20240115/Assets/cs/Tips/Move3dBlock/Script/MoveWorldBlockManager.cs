using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorldBlockManager : MonoBehaviour
{

    private void Awake()
    {

    }

    private IEnumerator OnMouseDown()
    {
        //����ά��������ת������Ļ����
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //�����Ļ����
        Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
        //�������Ļ����ת������ά����
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace);
        //��������λ�������֮��ľ���
        Vector3 offset = transform.position - mouseWorldPosition;
        //��ǰ����÷���ֵ
        var cs = new WaitForFixedUpdate();
        //������������ʱ
        while (Input.GetMouseButton(0))
        {
            //���������Ļ����
            currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
            //�������Ļ����ת������ά����
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace);
            //�ƶ���������
            Vector3 currentPosition = mouseWorldPosition + offset;
            //�������������ó��ƶ��������
            transform.position = currentPosition;
            //���� (ֻ�е���һ��fixedUpdate��ʼʱ��ִ�к�������)
            yield return cs;
        }

    }

}
