using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace ImprovedTimers.Tests
{
    public class CountdownTimerTests
    {
        private CountdownTimer timer;

        [SetUp]
        public void Setup()
        {
            timer = new CountdownTimer(5f); // 5 seconds timer
        }

        [TearDown]
        public void Teardown()
        {
            timer.Dispose();
        }

        [UnityTest]
        public IEnumerator CountdownTimer_DecreasesOverTime()
        {
            // Act
            timer.Start();
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            timer.Tick();

            // Assert
            Assert.AreEqual(2f, timer.CurrentTime, 0.1f, "Timer should have 2 seconds remaining.");
        }

        [UnityTest]
        public IEnumerator CountdownTimer_StopsAtZero()
        {
            // Act
            timer.Start();
            yield return new WaitForSeconds(5f); // Wait for 5 seconds
            timer.Tick();

            // Assert
            Assert.AreEqual(0f, timer.CurrentTime, 0.1f, "Timer should stop at 0.");
            Assert.IsFalse(timer.IsRunning, "Timer should stop running when it reaches 0.");
        }
    }
}