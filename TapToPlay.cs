using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlay : MonoBehaviour
{

    public GameObject dragIcon;
    Touch touch;
    [SerializeField] GameObject CharacterControlling;
    // Start is called before the first frame update
    void Awake()
    {
        CharacterControlling.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
        if (PlayerController.dragChack == true)
        {
            dragIcon.SetActive(false);
            CharacterControlling.GetComponent<PathSystem.PathSystem_Object>().enabled = true;
            this.transform.gameObject.GetComponent<Animator>().SetBool("isRun",false);
            
        }
    }
    private void OnMouseEnter()
    {
        Debug.Log("Entered");
        
    }
    
    void Update()
    {
        

        //}
        //if (dragIcon.gameObject.activeSelf)
        //{
        //    CharacterControlling.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
        //    this.transform.gameObject.GetComponent<Animator>().enabled = false;
        //}
        //else
        //{
        //    CharacterControlling.GetComponent<PathSystem.PathSystem_Object>().enabled = true;
        //    this.transform.gameObject.GetComponent<Animator>().enabled = true;
        //}
    }
}
