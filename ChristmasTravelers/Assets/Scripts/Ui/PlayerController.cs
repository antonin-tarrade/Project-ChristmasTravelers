using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    private Button selectedButton;
    // Start is called before the first frame update
    void Start()
    {
        selectedButton = GameObject.Find("AllCharactersPool").transform.GetChild(0).GetComponent<Button>();
        GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(selectedButton.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked() {
        selectedButton.onClick.Invoke();
    }

    public void SwitchButtonRight()
    {

        selectedButton = selectedButton.transform.parent.GetChild(selectedButton.transform.GetSiblingIndex()).GetComponent<Button>();
    }
 
}
