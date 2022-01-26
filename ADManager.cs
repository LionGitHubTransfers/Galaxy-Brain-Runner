using UnityEngine;
using LionStudios.Suite.Analytics;
using GameAnalyticsSDK;
using LionStudios.Suite.Debugging;
public class ADManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LionDebugger.Hide();
        GameAnalytics.Initialize();
        LionAnalytics.GameStart();


        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads

            Debug.Log("AppLovin SDK is initialized, start loading ads ");
        };

        MaxSdk.SetSdkKey("CgG1BtqwUb8gNyhBVM-6AoTTU-yyGD9UyFS4QZzB7qdKR94hTICWTvRbNbGfmkw9VEQ8cUSDZFXLFELip15EZB");
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();

        LionAnalytics.GameStart();
    }


   


// Update is called once per frame
void Update()
    {
        
    }
}
