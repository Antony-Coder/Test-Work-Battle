
// �������� ����������� ��������� ������ �������� ��� ��������� ���� � ����
// �������� ����� ������� void Heal(), ����� ������ ����
interface IRayTarget
{
    bool IsEnamy(ObjectType objectType);
    void Damage(int damage);
}
