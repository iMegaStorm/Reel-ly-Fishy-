using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{

    [SerializeField] Transform topFishBar;
    [SerializeField] Transform bottomFishBar;
    [SerializeField] Transform hookTop;
    [SerializeField] Transform hookBottom;
    [SerializeField] Transform fish;

    float fishPosition;
    float fishDestination;

    float fishTimer;
    [SerializeField] float timeMultiplicator = 3f;

    float fishSpeed;
    [SerializeField] float smoothMotion = 1f;

    [SerializeField] Transform hook;
    float hookPosition;
    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float newHookSize = 32.0f;
    [SerializeField] float hookPower = 0.5f;
    float hookProgress;
    float hookPullVelocity;
    [SerializeField] float hookPushPower = 0.025f;
    [SerializeField] float hookGravityPower = 0.005f;
    [SerializeField] float hookProgressDegradationPower = 0.1f;

    [SerializeField] GameObject hookImage;
    [SerializeField] Transform progressBar;
    public Image progressBarFill;
    bool pause = true;
    public bool isFishing = false;

    public GameObject miniGame;

    [SerializeField] float failTimer = 5f;

    private void Start()
    {
        hookImage = GameObject.Find("Hook");
        miniGame = GameObject.Find("MiniGame");
        //miniGame.SetActive(false);
    }

    private void Update()
    {
        if (isFishing)
            pause = false;


        if (pause)
            return;

        var hookImageRectTransform = hookImage.transform as RectTransform;
        hookImageRectTransform.sizeDelta = new Vector2(hookImageRectTransform.sizeDelta.x, hookSize * 100);
        Fish();
        Hook();
        ProgressCheck();
    }

    void ProgressCheck()
    {
        //Vector3 ls = progressBar.localScale;
        //ls.y = hookProgress;
        //progressBar.localScale = ls;
        progressBarFill.fillAmount = hookProgress;


        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            //hookProgress += hookPower * Time.deltaTime;
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= hookProgressDegradationPower * Time.deltaTime;
            //hookProgress -= hookPower * Time.deltaTime;
            failTimer -= Time.deltaTime;

            if (failTimer <= 0f)
                Lose();
        }

        if(hookProgress >= 1f)
        {
            Win();
        }
        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }

    void Win()
    {
        pause = true;
        Debug.Log("Fish caught");
    }

    void Lose()
    {
        pause = true;
        Debug.Log("Fish fled");
    }

    void Hook()
    {
        if (Input.GetMouseButton(0))
        {
            hookPullVelocity += hookPushPower * Time.deltaTime;
        }
        hookPullVelocity -= hookGravityPower * Time.deltaTime;

        if (hookPosition - hookSize / 2 <= 0f && hookPullVelocity < 0f)
        {
            hookPullVelocity = 0f;
        }
        if (hookPosition + hookSize / 2 >= 1f && hookPullVelocity > 0f)
        {
            hookPullVelocity = 0f;
        }

        hookPosition += hookPullVelocity;
        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);
        hook.position = Vector3.Lerp(hookBottom.position, hookTop.position, hookPosition);
    }


    void Fish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = UnityEngine.Random.value * timeMultiplicator;

            fishDestination = UnityEngine.Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomFishBar.position, topFishBar.position, fishPosition);
    }
}
