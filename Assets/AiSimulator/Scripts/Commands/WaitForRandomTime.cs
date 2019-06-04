﻿using RCG.Agents;
using RCG.Commands;
using System.Collections;
using UnityEngine;

namespace RCG.Commands
{
    public class WaitForRandomTime : AbstractCommand
    {
        MonoBehaviour monoBehaviour;
        float minSeconds;
        float maxSeconds;

        Coroutine coroutine;

        protected override void OnStart()
        {
            StartWait();
        }

        protected override void OnStop()
        {
            StopWait();
        }

        protected override void OnDestroy()
        {
            StopWait();
        }

        void StartWait()
        {
            StopWait();
            coroutine = monoBehaviour.StartCoroutine(Wait());
        }

        void StopWait()
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
            }
        }

        IEnumerator Wait()
        {
            float seconds = Random.Range(minSeconds, maxSeconds);
            yield return new WaitForSeconds(seconds);
            Complete();
        }

        public static ICommand Create(MonoBehaviour monoBehaviour, float minSeconds, float maxSeconds)
        {
            return new WaitForRandomTime
            {
                monoBehaviour = monoBehaviour,
                minSeconds = minSeconds,
                maxSeconds = maxSeconds
            };
        }
    }
}