using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    private List<IItem> items;

    private Dictionary<IMapItem, MapItemInitialData> initialStates;

    public void Awake()
    {
        instance = this;
        items = new List<IItem>();
        initialStates = new Dictionary<IMapItem, MapItemInitialData>();
    }

    private void Start()
    {
        RoundManager.instance.OnTurnStart += OnTurnStart;
        RoundManager.instance.OnTurnEnd += OnTurnEnd;
    }

    public void Register(IMapItem item, MapItemInitialData data)
    {
        initialStates.Add(item, data);
    }


    private void RestoreInitialState(IMapItem item, MapItemInitialData data)
    {
        item.Initialise();
        if (data.initialState == IMapItem.MapItemState.Free)
        {
            if (item.mapState == IMapItem.MapItemState.Grabbed) item.Drop();
            item.gameObject.transform.position = data.initialPosition;
        }
    }


    private void OnTurnStart()
    {
        foreach (KeyValuePair<IMapItem, MapItemInitialData> kvp in initialStates)
        {
            RestoreInitialState(kvp.Key, kvp.Value);
        }
    }

    private void OnTurnEnd()
    {

    }

}
