using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LionStudios.Suite.Analytics;
using GameAnalyticsSDK;

public class PlayerController : HumanSarface
{

    public static PlayerController playerController;

    [SerializeField] GameObject CharacterControlling;
    public bool isDragging;
    public bool isRocketAnim;
    public bool isLessHeadValue;


    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    public GameObject lastPos;
    public static bool dragChack;
    public GameObject dragIcon;
    

    // UI Elements ......... !!
    public GameObject gameFinish,_MainCamera;

    //Raw Elements Access From Unity.....
    SkinnedMeshRenderer playerSkin;
    Touch touches;
    private  Animator playerAnim;
    public bool isBlance, isTouch;

    //HumanSarface allHumanPart;
    [SerializeField] float playerDragMovement = 0.009f;
    //float playerMovingSpeed = 5f;
    [SerializeField] GameObject headSculpPlayer;
    [SerializeField] GameObject brainPlayer;

    [SerializeField] GameObject camPos;
    //Head Controlling.......
    //int playerHeadSize = 0;

    //Score AND UI........
   public int score;
    int money;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Text levelNumberIncrimental;
    [SerializeField] int levelNumberIncrimentalNo = 7;

    //FXS And Effects.... !!
    public GameObject playerFxShowingPoint;
    public GameObject gateFxPoint;

    public GameObject positiveBrainFx;
    public GameObject negetiveBrainFx;

    public GameObject rightChoiceGetFx;
    public GameObject wrongChoiceGetFx;

    public GameObject headPowerOrbFx;

    public GameObject bodyPossitiveFx,bodyNegitiveFx;

    public GameObject redFx,blueFx;

    public GameObject obstacolFx;
    public GameObject dollerEffect;

    public GameObject Angry;

    public GameObject levelFailBanner;

    public GameObject[] PopUpObjs;

    public GameObject startCamera,endCamera,sliderBar,SliderEffect;

    public Image _slider;

    public Sprite[] popSprits;

    public  Animator headEffectAnim;

    
  //  public GameObject winCanves;
    public GameObject[] stars;



    private bool isTouchX;
    int slideValue;
    

    //Controlling the head size and animations stage...

    float animMotion;//peramitter value..
    int perameterIdCountOfAnimation = 3;//take how maney animation clip...
    [SerializeField]
    public float blendShaphValue;//Head blend shapes...
    float maxAnim = 1.0f;
    float minAnim = 0.0f;
    float currentValue = 0;


    float start = 5.2f; // Could be any number.
    float end = 0;  // Could be any number too.

    float totalTime = 2; // The time it takes to transit from start to target.
    float t; // The variable holding the current time passed.



    [SerializeField]
    float glassHeadHeight,addPoint;
    float currentMotionValue;
    bool isIncressing;

    [SerializeField]
    float _maxHeadSize,_minHeadSize, headValue,playerSpeed;



    public int levelNumber;
    [Header("Lion Studio Perameters")]
    public int attemptNum;

    //Final Gate...
    public GameObject finalGate;


    private NavMeshAgent navMeshAgent;



    //Level Seveing..
    string levels = "levelNum";
    int next;

    void LastStageControlling(GameObject currentPlayer, string collaidedObjectOrCollisionStageBehaveour, float animationMotionForCurrentPlyer, int animationPeramitterToPlayForPlayer, GameObject collissonObj, float distanceFromBothObject)
    {
        currentPlayer = transform.gameObject;
        if (collaidedObjectOrCollisionStageBehaveour == "LastStage")
        {
            playerAnim.SetFloat(animationPeramitterToPlayForPlayer, animationMotionForCurrentPlyer);//set value for current player blend animation.
            if (Vector3.Distance(currentPlayer.transform.position, collissonObj.transform.position) <= distanceFromBothObject /* check the player collision tag , that he collided */ )
            {
                playerAnim.SetFloat(animationPeramitterToPlayForPlayer, 1f);

                //Here Call The corutine to wait for the task.... to do by player...
                //after that collaided obj will go for his animation stage...
                //camera will follow the object for a few moments...
                //control the rotations for the player too...
                //

                collissonObj.GetComponent<Animator>().enabled = true;
            }
        }
    }


    void Gate(string gateTag,GameObject gate)
    {
        if(gateTag == "finalGate")
        {
        //    Debug.Log("Play Anim For Final Gate");
            //gate.gameObject.GetComponent<Animator>().enabled = true;
            gate.GetComponent<Animator>().enabled = true;
        }
    }
    private void Awake()
    {

        if (playerController == null)
        {
            playerController = this;
        }

        _slider.fillAmount = 0f;
        if (dragIcon.activeSelf)
        {
          //  Debug.Log("Active");
            CharacterControlling.GetComponent<PathSystem.PathSystem_Object>().enabled = false;

        }


        for (int s = 0; s < stars.Length; s++)
        {
            stars[s].SetActive(false);
        }
    }

    void Start()
    {
        playerDragMovement = 0.009f;

        isDragging = true;

        // 
        SliderEffect.SetActive(false);


        Scene sceneLoaded = SceneManager.GetActiveScene();
       // //  SceneManager.LoadScene(sceneLoaded.buildIndex);
        PlayerPrefs.SetInt("SavedLevelNumber", sceneLoaded.buildIndex);


        levelNumber = PlayerPrefs.GetInt("levelNumber");



        attemptNum = PlayerPrefs.GetInt("Attempt");
        if (levelNumber == 0)
        {

            PlayerPrefs.SetInt("levelNumber", 1);
            levelNumber = PlayerPrefs.GetInt("levelNumber");
            levelNumberIncrimental.text = "" + levelNumber;
         //   Debug.Log(PlayerPrefs.GetInt("levelNumber"));
        }

        else
        {
            levelNumber = PlayerPrefs.GetInt("levelNumber");
            levelNumberIncrimental.text = "" + levelNumber;
        //    Debug.Log(PlayerPrefs.GetInt("levelNumber"));

        }





        playerAnim = GetComponent<Animator>();

        bodyPossitiveFx.SetActive(false);
        bodyNegitiveFx.SetActive(false);

        for (int i = 0; i < PopUpObjs.Length; i++)
        {
            PopUpObjs[i].SetActive(false);
        }

      
        Angry.SetActive(false);

        startCamera.SetActive(true);
        endCamera.SetActive(false);

        if (levelFailBanner.activeSelf)
        {
            levelFailBanner.SetActive(false);
        }

        if (gameFinish.activeSelf)
        {
            gameFinish.SetActive(false);
        }
        navMeshAgent = GetComponent<NavMeshAgent>();

        currentValue = 0f;
        blendShaphValue = 0;

     

        #region Accessing from Another scripts........!!
        //allHumanPart = FindObjectOfType<HumanSarface>();
        #endregion
        //Accessing raw elements...............!!
        playerAnim = GetComponent<Animator>();
        playerSkin = GetComponent<SkinnedMeshRenderer>();

        headPowerOrbFx.SetActive(false);

        headSculpPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue+ glassHeadHeight);
        brainPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue);
    }

    void Update()
    {

       // levelNumberIncrimental.text = LevelLoader.levelNo.ToString();

        scoreText.text = score.ToString();

        if (blendShaphValue > _maxHeadSize)
        {
            if (headPowerOrbFx.activeSelf == false)
            {

                headPowerOrbFx.SetActive(true);
            }

        }
        else
        {
            if (headPowerOrbFx.activeSelf)
            {
                headPowerOrbFx.SetActive(false);
            }
        }


        if (blendShaphValue >0 &&  blendShaphValue <= _maxHeadSize)
        {
            headSculpPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue + glassHeadHeight);
            brainPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue);

             headValue = (1.0f / _maxHeadSize) * blendShaphValue;
            headEffectAnim.SetFloat("HeadValue", headValue);
            _slider.fillAmount = headValue;

        }
      
        if (blendShaphValue < -3)
        {
           LevelFail();
        }

        if (Input.touchCount > 0 && Input.touchCount <= 1)
        {
            touches = Input.GetTouch(0);


            if (touches.phase == TouchPhase.Ended)
            {
                if (!isBlance)
                {
                    //isMove = 0.5f;
                    //_characterAnimator.GetComponent<Animator>().SetFloat("isMove", isMove);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                isTouch = false;
                Quaternion target = Quaternion.Euler(0, -1, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);
            }
            if (touches.phase == TouchPhase.Began)
            {
                if (!isBlance)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

                //   isTouch = true;
            }
            
            if (touches.phase == TouchPhase.Moved)
            {
                isTouch = true;
                if (dragIcon.activeSelf)
                {
                    dragIcon.gameObject.SetActive(false); 
                    CharacterControlling.GetComponent<PathSystem.PathSystem_Object>().enabled = true;
                    this.transform.gameObject.GetComponent<Animator>().SetBool("isRun", true);
                    
                }
                // This is working when player pressing the 
                if (dragIcon.activeSelf == false && isDragging)
                {
                    if (touches.deltaPosition.x > 0)
                    {

                        if (isBlance)
                        {
                            transform.Rotate(new Vector3(0, 0, -50f) * Time.deltaTime * 15f, Space.Self);
                            slideValue = 1;
                        }
                        else
                        {
                          //  transform.Translate(new Vector3(touches.deltaPosition.x, 0, 0) * Time.deltaTime * 0.5f, Space.Self);
                        }
                    }
                    if (touches.deltaPosition.x < 0)
                    {
                        slideValue = 0;

                        if (isBlance)
                        {
                            transform.Rotate(new Vector3(0, 0, 50f) * Time.deltaTime * 15f, Space.Self);
                        }
                        else
                        {
                           // transform.Translate(new Vector3(touches.deltaPosition.x, 0, 0) * Time.deltaTime , Space.Self);
                        }
                    }

                   // navMeshAgent.destination(new Vector3(touches.deltaPosition.x, 0, 0) * Time.deltaTime * 0.5f, Space.Self);

                    transform.Translate(new Vector3(touches.deltaPosition.x, 0, 0) * Time.deltaTime * playerSpeed, Space.Self);
                }
                
            }
        }
      
       

        if (isLessHeadValue)
        {
           
                t += Time.deltaTime;

                float T = t / totalTime; // The percentage of our "progress" towards totalTime.
                float currentFloat = Mathf.Lerp(start, end, T);
                blendShaphValue = currentFloat;
                playerAnim.SetFloat("LessHead", currentFloat);


                if (currentFloat == 0)
                {
                    StartCoroutine(BrainLess());
                }
            }
          
    }

    void ControllingAnimationsAndBlends(float animMotion, string animationPeramitterName, string objectTag)
    {
        if (objectTag == "posetive")
        {
           // if (blendShaphValue < _maxHeadSize)
          //  {
                blendShaphValue += addPoint;
                currentValue += 0.01f;

                if (blendShaphValue <= 100)
                {

                }
                if (currentValue <= maxAnim)
                {
                    playerAnim.SetFloat(animationPeramitterName, currentValue);
                }
           // }

        
        }
        if (objectTag == "negetive")
        {
         

                blendShaphValue -= addPoint;
                currentValue -= 0.01f;

                if (blendShaphValue >= 0)
                {
                    //headSculpPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue + glassHeadHeight);
                    //brainPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue);
                }
                if (currentValue <= maxAnim)
                {
                    playerAnim.SetFloat(animationPeramitterName, currentValue);
                }
           

        }
        if (objectTag == "choiceGatePositive")
        {
            //GameObject currentGet = GameObject.FindGameObjectWithTag("choiceGatePositive");
            //GameObject fxCurrent = GetComponentInChildren();
            //   FxShowingPoint(gateFxPoint, rightChoiceGetFx);


         
                blendShaphValue += addPoint;
                currentValue += 0.01f;
                blendShaphValue += addPoint * 3;

                if (blendShaphValue <= 100)
                {
                    //headSculpPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue + glassHeadHeight);
                    //brainPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue);
                }
                if (currentValue <= maxAnim)
                {
                    playerAnim.SetFloat(animationPeramitterName, currentValue);
                }
            
         
        }
        if (objectTag == "choiceGateNegetive")
        {

         
                blendShaphValue -= addPoint * 3;
                currentValue -= 0.01f;

                if (currentValue <= maxAnim)
                {
                    playerAnim.SetFloat(animationPeramitterName, currentValue);
                }
        

                
        }
    }



    IEnumerator startSlideEffect()
    {


        SliderEffect.SetActive(false);
        yield return new WaitForSeconds(0.001f);
        SliderEffect.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        SliderEffect.SetActive(false);
    }

    IEnumerator BrainLess()
    {
     //   if (blendShaphValue > 0 && blendShaphValue < _maxHeadSize)
        {
            isLessHeadValue = false;
        //    yield return new WaitForSeconds(0.38f);
        //   // currentValue -= 0.05f;
        ////    blendShaphValue -= addPoint + 3.7f * Time.deltaTime;
        //  //  playerAnim.SetFloat("LessHead", blendShaphValue);
        //    if (blendShaphValue > 0 && blendShaphValue < _maxHeadSize)
        //    {
        //        headSculpPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue + glassHeadHeight);
        //        brainPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue);
        //    }
        }


        if(blendShaphValue <0.001f)
        {
            isRocketAnim = true;
            yield return new WaitForSeconds(0.38f);
            if (endCamera.activeSelf == false)
            {
                startCamera.SetActive(false);
                endCamera.SetActive(true);
                StartCoroutine(LevelEndding());
            }
        }
   
        isLessHeadValue = true;
       

       
        //headSculpPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue + glassHeadHeight);
        //brainPlayer.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendShaphValue);
    }

    public void AddScre()
    {
        if (blendShaphValue > 0)
        {
          //  StartCoroutine(startSlideEffect());

            sliderBar.transform.position = new Vector3(sliderBar.transform.position.x , sliderBar.transform.position.y + 0.05f, sliderBar.transform.position.z);
        }
    }

    public void SubtractScore()
    {

        if (score > 0)
        {
            score -= 1;
        }

        if (blendShaphValue > 0)
        {
          // 
            sliderBar.transform.position = new Vector3(sliderBar.transform.position.x, sliderBar.transform.position.y - 0.05f, sliderBar.transform.position.z);
        }
    }

    public void positiveEffect()
    {
        bodyPossitiveFx.SetActive(false);
        bodyPossitiveFx.SetActive(true);

        for (int i = 0; i < PopUpObjs.Length; i++)
        {
            PopUpObjs[i].SetActive(false);
        }



        if (headValue > 0.0f)
        {

            for (int i = 0; i < PopUpObjs.Length; i++)
            {
                if (i == 0)
                {
                    PopUpObjs[i].SetActive(true);
                }
                else
                {
                    PopUpObjs[i].SetActive(false);
                }
               
            }


        }

        if (headValue > 0.33f)
        {

            for (int i = 0; i < PopUpObjs.Length; i++)
            {
                if (i == 1)
                {
                    PopUpObjs[i].SetActive(true);
                }
                else
                {
                    PopUpObjs[i].SetActive(false);
                }

            }

        }

        if (headValue > 0.88f)
        {

            for (int i = 0; i < PopUpObjs.Length; i++)
            {
                if (i == 2)
                {
                    PopUpObjs[i].SetActive(true);
                }
                else
                {
                    PopUpObjs[i].SetActive(false);
                }

            }

        }

    }

    public void negitiveEffec()
    {
        bodyNegitiveFx.SetActive(false);
        bodyNegitiveFx.SetActive(true);

        Angry.SetActive(false);
        Angry.SetActive(true);
    }


    IEnumerator LevelEndding()
    {
         sliderBar.SetActive(false);
         yield return new WaitForSeconds(4.3f);
         FindObjectOfType<MontiorTextureAnimation>().confitte.SetActive(true);
         yield return new WaitForSeconds(2.0f);
         gameFinish.SetActive(true);

        yield return new WaitForSeconds(0.8f);
        if (score < 25)
        {
            stars[0].SetActive(true);
        }
        else if (score < 50)
        {
            stars[0].SetActive(true);
            yield return new WaitForSeconds(0.8f);
            stars[1].SetActive(true);
        }
        else
        {
            stars[0].SetActive(true);
            yield return new WaitForSeconds(0.8f);
            stars[1].SetActive(true);
            yield return new WaitForSeconds(0.8f);
            stars[2].SetActive(true);
        }

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, levelNumber.ToString()); //GameAnal                                                                                                                // score = (int)curProgress;
        LionAnalytics.LevelComplete(levelNumber, attemptNum, score); //lionAnal

    }

    public void NextLevelLoading(GameObject panel)
    {

        //LevelLoader.levelNo++;
        //panel.SetActive(false);
        //next = SceneManager.GetActiveScene().buildIndex;
        //if(next < 8)
        //{
        //    Debug.Log(LevelLoader.levelNo);
        //    PlayerPrefs.SetInt(levels, next);
        //    SceneManager.LoadScene(next + 1);
        //    PlayerPrefs.Save();
        //    Debug.Log("Seved level number " + next);
        //}
        //if (next >= 7)
        //{
        //   // LevelLoader.levelNo++;

        //    levelNumberIncrimental.text = LevelLoader.levelNo.ToString();
        //    levelNumberIncrimentalNo += 1;
        //    levelNumberIncrimental.text = "Level " + levelNumberIncrimentalNo.ToString();
        //    int randomLode = Random.Range(0, 6);
        //    SceneManager.LoadScene(randomLode);
        //}
    }



    public void LevelFail()
    {
        if (!levelFailBanner.activeSelf)
        {
            levelFailBanner.SetActive(true);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, levelNumber.ToString()); //GameAnal                                                                                                 // score = (int)curProgress;
            LionAnalytics.LevelFail(levelNumber, attemptNum,score); //lionAnal

        }
    }


    void FxShowingPoint(GameObject positionToShowFx, GameObject fxToShow)
    {
        Instantiate(fxToShow, positionToShowFx.transform.position, positionToShowFx.transform.rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "posetive")
        {
           

            FxShowingPoint(playerFxShowingPoint,positiveBrainFx);

            GameObject BlueFx = Instantiate(blueFx, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation);
            BlueFx.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            score += 1;
            ControllingAnimationsAndBlends(currentValue, "HeadValue", other.gameObject.tag);
            Destroy(other.gameObject);

            StartCoroutine(startSlideEffect());

            AddScre();
        }
        if (other.gameObject.tag =="negetive")
        {
            StartCoroutine(startSlideEffect());

            GameObject RedFX = Instantiate(redFx, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation);
            RedFX.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            FxShowingPoint(playerFxShowingPoint,negetiveBrainFx);
            ControllingAnimationsAndBlends(currentValue, "HeadValue",other.gameObject.tag);
            Destroy(other.gameObject);
            SubtractScore();


        }
        if(other.gameObject.tag == "choiceGatePositive")
        {
            positiveEffect();
           // FxShowingPoint(gateFxPoint, rightChoiceGetFx);
            ControllingAnimationsAndBlends(currentValue,"HeadValue",other.gameObject.tag);//This function call for head size and anim motions
            score += 10;
            StartCoroutine(startSlideEffect());
            AddScre();
        }
        if (other.gameObject.tag == "choiceGateNegetive")
        {
            negitiveEffec();
            StartCoroutine(startSlideEffect());
            //FxShowingPoint(gateFxPoint,wrongChoiceGetFx);
            ControllingAnimationsAndBlends(currentValue, "HeadValue",other.gameObject.tag);
            SubtractScore();
        }
        if (other.gameObject.tag == "doller")
        {

            StartCoroutine(startSlideEffect());
            FxShowingPoint(other.gameObject, dollerEffect);
        }
        #region math door.......
        if (other.gameObject.tag == "plus")
        {
            // FxShowingPoint(playerFxShowingPoint, positiveBrainFx);

            if (blendShaphValue < _maxHeadSize)
            {

                StartCoroutine(startSlideEffect());
                
                blendShaphValue += 2;
                currentValue += currentValue;
            }
            score += 10;

            ControllingAnimationsAndBlends(currentValue, "HeadValue", other.gameObject.tag);
         //   Debug.Log("Plus 2");
          //  Debug.Log(blendShaphValue);
        }
        if(other.gameObject.tag == "plus10")
        {
            // FxShowingPoint(playerFxShowingPoint, positiveBrainFx);

            if (blendShaphValue < _maxHeadSize)
            {
                StartCoroutine(startSlideEffect());

                blendShaphValue += 10;
                currentValue += currentValue * 3;
            }
            score += 10;

            //score += 10;
            ControllingAnimationsAndBlends(currentValue, "HeadValue", other.gameObject.tag);
         //   Debug.Log("Plus 2");
         //   Debug.Log(blendShaphValue);
        }
        if (other.gameObject.tag == "minus10")
        {
            if (blendShaphValue >0)
            {
                StartCoroutine(startSlideEffect());

                blendShaphValue -= 10;
                currentValue -= currentValue * 2;
            }

            ControllingAnimationsAndBlends(currentValue, "HeadValue", other.gameObject.tag);
         //   Debug.Log("Plus 2");
         //   Debug.Log(blendShaphValue);
        }

        if (other.gameObject.tag == "minus")
        {
            //  FxShowingPoint(playerFxShowingPoint, positiveBrainFx);

            if (blendShaphValue > 0)
            {
                StartCoroutine(startSlideEffect());
                blendShaphValue -= 2;
                currentValue -= currentValue;
                ControllingAnimationsAndBlends(currentValue, "HeadValue", other.gameObject.tag);
             //   Debug.Log("Minuse 2");
             //   Debug.Log(blendShaphValue);
            }
            //score -= 10;
            //score += 10;

        }
        if (other.gameObject.tag == "into")
        {

            if (blendShaphValue < _maxHeadSize)
            {
                StartCoroutine(startSlideEffect());
                blendShaphValue *= 2;
                currentValue *= currentValue * currentValue;
                ControllingAnimationsAndBlends(currentValue, "HeadValue", other.gameObject.tag);
            //    Debug.Log("Into 2");
            }

            score += 10;
     
        }
        if (other.gameObject.tag == "dived")
        {
            // FxShowingPoint(playerFxShowingPoint, positiveBrainFx);

            if (blendShaphValue > 0)
            {

                StartCoroutine(startSlideEffect());

                blendShaphValue /= 2;
                currentValue /= currentValue / 2;
                ControllingAnimationsAndBlends(blendShaphValue, "HeadValue", other.gameObject.tag);
             //   Debug.Log("Devided 2");
            }

            score -= 10;

        }
        #endregion

        if(other.gameObject.tag == "lastPos")
        {
            isDragging = false;

           // Debug.Log("last pos");
            this.transform.position = lastPos.transform.position;
        }

        if (other.gameObject.tag == "finalTask")
        {
            CharacterControlling.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
            isLessHeadValue = true;
        
            CharacterControlling.GetComponent<PathSystem.PathSystem_Object>().psDefaultSpeed = 0;
            playerAnim.SetBool("isFinalTask", true);
            currentValue = 0.65f;
          //  blendShaphValue = 70;
            playerAnim.SetFloat("LessHead",currentValue);

            start = blendShaphValue;


        }


    }
    void CameraMovementControlling()
    {
        Quaternion playerRot = this.transform.rotation;
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation,playerRot,0.5f * Time.deltaTime);
    }
    //playerAnim.SetFloat((3, 1);
    //public void SetFloat(int id, float value, float dampTime, float deltaTime) { }



   
    
}