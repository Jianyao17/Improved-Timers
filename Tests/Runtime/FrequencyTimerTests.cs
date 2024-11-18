using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace ImprovedTimers.Tests
{
    public class FrequencyTimerTests
    {
        [UnityTest]
        public IEnumerator FrequencyTimer_TicksAtCorrectFrequency()
        {
            var frequencyTimer = new FrequencyTimer(2); // 2 ticks per second
            int tickCount = 0;

            frequencyTimer.OnTick += () => tickCount++;
            frequencyTimer.Start();

            yield return new WaitForSeconds(1f); // Tunggu 1 detik

            frequencyTimer.Tick();
            Assert.AreEqual(2, tickCount, "OnTick should be called twice after 1 second with 2 ticks per second.");
        }

        [UnityTest]
        public IEnumerator FrequencyTimer_ResetsWithNewFrequency()
        {
            var frequencyTimer = new FrequencyTimer(2); // 2 ticks per second
            int tickCount = 0;

            frequencyTimer.OnTick += () => tickCount++;
            frequencyTimer.Start();

            yield return new WaitForSeconds(1f); // Tunggu 1 detik

            frequencyTimer.Reset(4); // Ubah ke 4 ticks per second
            tickCount = 0;
            frequencyTimer.Start();

            yield return new WaitForSeconds(1f); // Tunggu 1 detik

            frequencyTimer.Tick();
            Assert.AreEqual(4, tickCount, "OnTick should be called four times after 1 second with 4 ticks per second.");
            Assert.AreEqual(4, frequencyTimer.TicksPerSecond, "TicksPerSecond should be updated correctly after reset.");
        }

        [UnityTest]
        public IEnumerator FrequencyTimer_StopsWhenFinished()
        {
            var frequencyTimer = new FrequencyTimer(2); // 2 ticks per second
            frequencyTimer.Start();

            yield return new WaitForSeconds(1f); // Tunggu 1 detik

            frequencyTimer.Stop();
            Assert.IsFalse(frequencyTimer.IsRunning, "FrequencyTimer should stop running after Stop is called.");

            frequencyTimer.Tick();
            Assert.IsFalse(frequencyTimer.IsRunning, "FrequencyTimer should remain stopped after Stop is called.");
        }
    }
}