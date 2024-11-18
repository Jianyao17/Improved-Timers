using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace ImprovedTimers.Tests
{
    public class FrequencyTimerTests
    {
        private FrequencyTimer timer;
        private int tickCount;

        [SetUp]
        public void Setup()
        {
            tickCount = 0;
            timer = new FrequencyTimer(2); // 2 ticks per second
            timer.OnTick += () => tickCount++;
        }

        [TearDown]
        public void Teardown()
        {
            timer.Dispose();
        }

        [UnityTest]
        public IEnumerator FrequencyTimer_TicksAtCorrectFrequency()
        {
            // Act
            timer.Start();
            yield return new WaitForSeconds(1f); // Wait for 1 second
            timer.Tick();

            // Assert
            Assert.AreEqual(2, tickCount, "Timer should have ticked twice in 1 second.");
        }
    }
}