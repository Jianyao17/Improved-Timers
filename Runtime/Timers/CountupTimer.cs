using UnityEngine;

namespace ImprovedTimers
{
    /// <summary>
    /// Timer that counts up from zero to a specific value.
    /// </summary>
    public class CountupTimer : Timer
    {
        private float maxThreshold;
        
        public CountupTimer(float maxThreshold) : base(0)
        {
            this.maxThreshold = maxThreshold;
        }

        public override bool IsFinished => CurrentTime >= maxThreshold;
        public override void Tick()
        {
            if (IsRunning && CurrentTime < maxThreshold)
                CurrentTime += Time.deltaTime;
            
            if (IsRunning && CurrentTime >= maxThreshold)
                Stop();
        }

        public override void Reset(float newMaxThreshold)
        {
            maxThreshold = newMaxThreshold;
            Reset();
        }
    }
}