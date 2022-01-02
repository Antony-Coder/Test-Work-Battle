using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using TMPro;
public class Bot : MonoBehaviour, IRayTarget
{
    [SerializeField] private MobSettings mobSettings;
    [SerializeField] private ObjectType objectType;
    private NavMeshAgent meshAgent;

    private int hp;
    private Transform target;
    private IRayTarget rayTarget;
 
    private UnityEvent fixedUpdate = new UnityEvent();

    public int Hp { get => hp; }

    private void Awake()
    {
        meshAgent = GetComponentInChildren<NavMeshAgent>();
    }

    public void Enable()
    {
        hp = mobSettings.hp;
        meshAgent.speed = mobSettings.speed;
        meshAgent.angularSpeed = mobSettings.angularSpeed;

        FindTarget();
    }

    private void FixedUpdate()
    {
        fixedUpdate.Invoke();
    }



    // Находит цель для нападения и записывает в поле target
    // Если целей на поле больше нет, то завершает игру
    private void FindTarget()
    {
        Bot targetBot = null;

        float minDistance = float.MaxValue;

        foreach (var bot in GameController.Get.Bots)
        {
            if (bot == null) continue;

            if (!bot.IsEnamy(objectType)) continue;

            if (Vector3.Distance(bot.transform.position, transform.position) < minDistance)
            {
                targetBot = bot;
            }
        }

        if (targetBot != null)
        {
            target = targetBot.transform;
            rayTarget = targetBot;
            meshAgent.isStopped = false;
            fixedUpdate.AddListener(Attack);
        }
        else
        {
            GameController.Get.Disable();
        }
    }



    private void Attack()
    {
        // Если цель не найдена, запускает пойск и завершает метод
        if (target == null)
        {         
            fixedUpdate.RemoveListener(Attack);
            meshAgent.isStopped = true;
            FindTarget();
            return;
        }

        // Двигаться к цели
        meshAgent.destination = target.position;

        //Атаковать цель
        if (Ray() != null)
        {
            if (rayTarget.IsEnamy(objectType))
            {
                rayTarget.Damage(mobSettings.damage);
            }
            
        }
    }

    // Выпускает луч, который возвращает объект с интерфейсом IRayTarget или null
    private Transform Ray()
    {
        Transform target = null;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, mobSettings.radius))
        {

            if (hit.transform.GetComponent<IRayTarget>() != null)
            {
                target = hit.transform;
            }
        }
        Debug.DrawRay(transform.position, transform.forward * mobSettings.radius, Color.green);

        return target;
    }

    //Нанесение урона
    public void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

    }

    // Распознавание свой/чужой
    public bool IsEnamy(ObjectType objectType)
    {
        return objectType != this.objectType;
    }
}
