using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceTest : MonoBehaviour
{
    public Button BtnCoroutine;
    public Button BtnUnitask;

    public Text TextCoroutine;
    public Text TextUnitask;

    public int LoopTimes;



    void Start()
    {
        BtnCoroutine.onClick.AddListener(OnClickCoroutineTest);
        BtnUnitask.onClick.AddListener(OnClickUniTaskTest);

    }
    private void OnClickCoroutineTest()
    {
        StartCoroutine(CoroutineTest());
    }

    private async void OnClickUniTaskTest()
    {
        int count = 0;
        float elapsedTime = 0;
        while (count < LoopTimes)
        {
            count += 1;
            float time = Time.realtimeSinceStartup;
            var unitask = EmptyUnitask();
            elapsedTime += (Time.realtimeSinceStartup - time);
            await unitask;
        }
        TextUnitask.text = string.Format("UniTask:{0}次: 耗時 {1:F6}ms", LoopTimes, elapsedTime * 1000);
    }

    async UniTask EmptyUnitask()
    {
        await UniTask.Yield(PlayerLoopTiming.Update);
    }



    IEnumerator CoroutineTest()
    {
        float elapsedTime = 0;
        for (int i = 0; i < LoopTimes; i++)
        {
            float time = Time.realtimeSinceStartup;
            var cor = StartCoroutine(EmptyCoroutine());
            elapsedTime += Time.realtimeSinceStartup - time;
            yield return cor;
        }
        TextCoroutine.text = string.Format("Coroutine:{0}次: 耗時 {1:F6}ms", LoopTimes, elapsedTime * 1000);
    }

    IEnumerator EmptyCoroutine() { yield return null; }











}
