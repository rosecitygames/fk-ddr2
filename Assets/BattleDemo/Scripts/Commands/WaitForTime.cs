using RCG.Agents;
using RCG.Commands;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.BattleSimulator
{
    public class WaitForTime : AbstractCommand
    {
        MonoBehaviour monoBehaviour;
        float seconds;

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
            yield return new WaitForSeconds(seconds);
            Complete();
        }

        public static ICommand Create(MonoBehaviour monoBehaviour, float seconds)
        {
            return new WaitForTime
            {
                monoBehaviour = monoBehaviour,
                seconds = seconds
            };
        }
    }
}
