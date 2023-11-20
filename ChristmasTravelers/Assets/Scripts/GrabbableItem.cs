using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

  public class GrabbableItem : MonoBehaviour, IGrabbable {

       [SerializeField] private DroppableItem item;
       private Vector3 initialPoisiton;

       private void Start() {
            initialPoisiton = transform.position;
            item.RegisterInitialState(this);
       }


       public void Set(DroppableItem item){
           this.item = item;
       }

        public void AcceptCollect(Character character)
        {
            character.GetComponent<Inventory>().Add(item);
            gameObject.SetActive(false);
        }
        

    }
