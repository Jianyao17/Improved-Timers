using UnityEngine;

namespace ImprovedTimers
{
    /// <summary>
    /// Timer that counts down from a specific value to zero.
    /// </summary>
    public class CountdownTimer : Timer
    {
        public CountdownTimer(float initialTime) : base(initialTime) { }

        public float Progress => Mathf.Clamp(1f - (CurrentTime / initialTime), 0, 1);
        public override bool IsFinished => CurrentTime <= 0;
        
        public override void Tick()
        {
            if (IsRunning && CurrentTime > 0)
                CurrentTime -= Time.deltaTime;
            
            if (IsRunning && CurrentTime <= 0)
                Stop();
        }
    }
}