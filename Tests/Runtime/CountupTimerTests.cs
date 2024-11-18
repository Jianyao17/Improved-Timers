using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace ImprovedTimers.Tests
{
    public class CountupTimerTests
    {
        private CountupTimer timer;

        [SetUp]
        public void Setup()
        {
            timer = new CountupTimer(10f); // Max threshold of 10 seconds
        }

        [TearDown]
        public void Teardown()
        {
            timer.Dispose();
        }

        [UnityTest]
        public IEnumerator CountupTimer_IncreasesOverTime()
        {
            // Act
            timer.Start();
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            timer.Tick();

            // Assert
            Assert.AreEqual(3f, timer.CurrentTime, 0.1f, "Timer should have counted up to 3 seconds.");
        }

        [UnityTest]
        public IEnumerator CountupTimer_StopsAtMaxThreshold()
        {
            // Act
            timer.Start();
            yield return new WaitForSeconds(10f); // Wait for 10 seconds
            timer.Tick();

            // Assert
            Assert.AreEqual(10f, timer.CurrentTime, 0.1f, "Timer should stop at the max threshold.");
            Assert.IsFalse(timer.IsRunning, "Timer should stop running when it reaches the max threshold.");
        }
    }
}