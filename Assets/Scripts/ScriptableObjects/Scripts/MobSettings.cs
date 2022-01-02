using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobSettings", menuName = "ScriptableObjects/MobSettings")]
public class MobSettings : ScriptableObject
{

    public float speed; //�������� �������� ����
    public float angularSpeed; //�������� �������� ����
    public float radius; //��������� ����, ���������� ����
    public int damage; //����, ��������� ���� ��� ��������� � ���� ����
    public int hp; // �� ���� � ������ ����

}


