using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using BoardCommands;
using UnityEditor;

public interface ISpawnable
{
    GameObject gameObject { get; }
    void Set(Character spawner, Vector3 position, Vector3 direction);
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
            GameManager.instance.Register(spawnable);
        }
        else
        {
            o.transform.position = param.position;
        }
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
