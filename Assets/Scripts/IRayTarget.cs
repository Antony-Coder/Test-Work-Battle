
// добавляю возможность выполнять разные действия при попадании луча в цель
// например можно доавить void Heal(), чтобы лечить моба
interface IRayTarget
{
    bool IsEnamy(ObjectType objectType);
    void Damage(int damage);
}
