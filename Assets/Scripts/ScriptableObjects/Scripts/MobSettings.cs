using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobSettings", menuName = "ScriptableObjects/MobSettings")]
public class MobSettings : ScriptableObject
{

    public float speed; //скорость движения моба
    public float angularSpeed; //скорость вращения моба
    public float radius; //дистанция луча, наносящего урон
    public int damage; //урон, наносимый мобу при попадании в него луча
    public int hp; // хп моба в начале игры

}


