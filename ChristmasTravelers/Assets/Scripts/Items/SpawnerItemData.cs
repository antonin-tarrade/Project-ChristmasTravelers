using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using BoardCommands;
using UnityEditor;
using System;

public interface ISpawnable
{
    void Set(Character spawner, Vector3 position, Vector3 direction);
    void Destroy();
}

/// <summary>
/// Used to encapsulate a simple gameobject into a basic spawnable, 
/// so that the game object does not need to implement ISpawnable
/// </summary>
public class SpawnableObject : ISpawnable
{
    private readonly GameObject gameObject;
    private readonly Action<Character, Vector3, Vector3> setFunc;
    public SpawnableObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
        setFunc = null;
    }

    public SpawnableObject(GameObject gameObject, Action<Character, Vector3, Vector3> setFunc)
    {
        this.gameObject = gameObject;
        this.setFunc = setFunc;
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }

    public void Set(Character spawner, Vector3 position, Vector3 direction)
    {
        setFunc?.Invoke(spawner, position, direction);
    }
}

[CreateAssetMenu(fileName = "SpawnerItem", menuName = "Scriptables/Items/Spawner")]
public class SpawnerItemData : ScriptableItemData
{
    public GameObject obj;

    public override IItem GetInstance()
    {
        return new SpawnerItem(this);
    }
}

public class SpawnerItem : Item
{
    protected new SpawnerItemData data;

    public SpawnerItem(SpawnerItemData data) : base(data)
    {
        this.data = data;
    }

    public override UseItemCommand GenerateCommand(Character character)
    {
        Vector3 position = character.transform.position;
        Vector3 direction = character.GetComponent<IAttack>().shootDirection;
        return new UseItemCommand(character, this, new SpawnerItemParameters(position, direction));
    }

    public override string GetName()
    {
        return "Spawner";
    }

    public override void OnUse(Inventory inventory, IItemParameters parameters)
    {
        SpawnerItemParameters param = (SpawnerItemParameters)parameters;
        GameObject o = GameObject.Instantiate(data.obj);
        if (o.TryGetComponent<ISpawnable>(out ISpawnable spawnable))
        {
            spawnable.Set(inventory.GetComponent<Character>(), param.position, param.direction);
        }
        else
        {
            o.transform.position = param.position;
        }
        GameManager.instance.ScheduleDestroy(new SpawnableObject(o));
    }
}

public struct SpawnerItemParameters : IItemParameters
{
    public Vector3 position;
    public Vector3 direction;

    public SpawnerItemParameters(Vector3 position, Vector3 direction)
    {
        this.position = position;
        this.direction = direction;
    }
}
