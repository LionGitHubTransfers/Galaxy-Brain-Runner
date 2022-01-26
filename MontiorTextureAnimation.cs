using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontiorTextureAnimation : MonoBehaviour
{
    [SerializeField]
    Material textureMat;
    [SerializeField]
    Texture[] monitorTextures;
    [SerializeField]
   public GameObject mathEffect,confitte;

    int i;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(startTextureAnimation());
        mathEffect.SetActive(false);
        confitte.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

     

        if (FindObjectOfType<PlayerController>().isLessHeadValue)
        {

            if (FindObjectOfType<PlayerController>().blendShaphValue > 0.1f)
            {
                mathEffect.SetActive(true);
            }
            else
            {
                mathEffect.SetActive(false);
               // confitte.SetActive(true);
            }
           
            StartCoroutine(startTextureAnimation());

        }

        if (i == monitorTextures.Length)
        {
            StartCoroutine(startTextureAnimation());
            i = 0;
        }
    }


    IEnumerator startTextureAnimation()
    {

        for ( i = 0; i < monitorTextures.Length; i++)
        {
            yield return new WaitForSeconds(0.01f);
            textureMat.SetTexture("_BaseMap", monitorTextures[i]);
        }

      
       
    }
}
