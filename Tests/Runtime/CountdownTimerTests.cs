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
        
        [UnityTest]
        public IEnumerator CountdownTimer_TicksCorrectly()
        {
            var countdownTimer = new CountdownTimer(5f); // 5 detik
            countdownTimer.Start();

            float initialTime = countdownTimer.CurrentTime;
            float initialProgress = countdownTimer.Progress;

            yield return new WaitForSeconds(1f); // Tunggu 1 detik

            countdownTimer.Tick();
            Assert.Less(countdownTimer.CurrentTime, initialTime, "Current time should decrease after ticking.");
            Assert.Greater(countdownTimer.Progress, initialProgress, "Progress should increase as time decreases.");
        }

        [UnityTest]
        public IEnumerator CountdownTimer_FinishesCorrectly()
        {
            var countdownTimer = new CountdownTimer(1f); // 1 detik
            countdownTimer.Start();

            yield return new WaitForSeconds(1.1f); // Tunggu lebih dari durasi timer

            countdownTimer.Tick();
            Assert.IsTrue(countdownTimer.IsFinished, "Timer should finish when time runs out.");
            Assert.AreEqual(1f, countdownTimer.Progress, "Progress should be 1 when timer is finished.");
        }
    }
}