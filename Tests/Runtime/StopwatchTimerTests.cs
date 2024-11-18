using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace ImprovedTimers.Tests
{
    public class StopwatchTimerTests
    {
        private StopwatchTimer timer;

        [SetUp]
        public void Setup()
        {
            timer = new StopwatchTimer();
        }

        [TearDown]
        public void Teardown()
        {
            timer.Dispose();
        }

        [UnityTest]
        public IEnumerator StopwatchTimer_CountsUpIndefinitely()
        {
            // Act
            timer.Start();
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            timer.Tick();

            // Assert
            Assert.AreEqual(3f, timer.CurrentTime, 0.1f, "Timer should count up indefinitely.");
        }
    }
}