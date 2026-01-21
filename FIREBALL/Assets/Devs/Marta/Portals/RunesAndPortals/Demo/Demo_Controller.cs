using System.Collections;
using UnityEngine;

public class Demo_Controller : MonoBehaviour
{
    [SerializeField] private Portal_Controller portalSimpleScripts;
    [SerializeField] private Transform camBaseTF;
    [SerializeField] private Light lightMain;
    [SerializeField] private GameObject[] SSCameras;
    private int ssCamNR, ssCamsMax;

    private void Start()
    {
        ssCamsMax = SSCameras.Length;

        lightMain.intensity = 0f;
        StartCoroutine(DemoRoutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    if (lightMain.intensity == 0)
        //        lightMain.intensity = 1f;
        //    else lightMain.intensity = 0f;
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SSCameras[ssCamNR].SetActive(false);
        //
        //    ssCamNR += 1;
        //    if (ssCamNR >= ssCamsMax)
        //        ssCamNR = 0;
        //
        //    SSCameras[ssCamNR].SetActive(true);
        //}
    }

    private IEnumerator DemoRoutine()
    {
        //yield return new WaitForSeconds(1f);
        //runestoneScript.ToggleRuneStone(true);
        //portalRoundScripts.F_TogglePortalRound(true);
        //portalSimpleScripts.TogglePortal(true);
        //portalGateScript.F_TogglePortalGate(true);
        //vortexScript.F_ToggleVortex(true);
        //riftScript.F_ToggleRift(true);
        
        //On
        //start delay
        yield return new WaitForSeconds(1f);
        
        yield return new WaitForSeconds(3f);
        StartCoroutine(CameraRoutine());
        
        yield return new WaitForSeconds(2f);
        
        yield return new WaitForSeconds(4f);
        
        yield return new WaitForSeconds(10f);
        
        yield return new WaitForSeconds(7f);
        portalSimpleScripts.TogglePortal(true);
        
        yield return new WaitForSeconds(9f);
        
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeLight());
        
        yield return new WaitForSeconds(45f);
        
        //Off
        yield return new WaitForSeconds(8f);
        
        yield return new WaitForSeconds(2f);
        
        yield return new WaitForSeconds(5f);
        
        yield return new WaitForSeconds(9f);
        
        yield return new WaitForSeconds(7f);
        portalSimpleScripts.TogglePortal(false);
        
        yield return new WaitForSeconds(10f);
    }

    private IEnumerator CameraRoutine()
    {
        while (true)
        {
            camBaseTF.Rotate(Vector3.up, Time.deltaTime * -8f);
            yield return null;
        }
    }

    private IEnumerator FadeLight()
    {
        float fadeFloat = 0f;

        while (fadeFloat < 1f)
        {
            fadeFloat = Mathf.MoveTowards(fadeFloat, 1f, Time.deltaTime * 0.02f);
            lightMain.intensity = fadeFloat;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
