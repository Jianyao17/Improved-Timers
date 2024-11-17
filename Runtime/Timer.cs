using System;
using UnityEngine;

namespace ImprovedTimers
{
    public abstract class Timer : IDisposable
    {
        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; protected set; }
        public float Progress => Mathf.Clamp(CurrentTime / initialTime, 0, 1);

        protected float initialTime;

        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float initialTime)
        {
            this.initialTime = initialTime;
        }

        public void Start()
        {
            CurrentTime = initialTime;
            if (!IsRunning)
            {
                IsRunning = true;
                TimerManager.RegisterTimer(this);
                OnTimerStart?.Invoke();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                TimerManager.DeregisterTimer(this);
                OnTimerStop?.Invoke();
            }
        }

        public abstract void Tick();
        public abstract bool IsFinished { get; }
        
        public void Pause() => IsRunning = false;
        public void Resume() => IsRunning = true;
        
        public virtual void Reset() => CurrentTime = initialTime;
        public virtual void Reset(float value)
        {
            CurrentTime = initialTime;
            Reset();
        }

        #region IDisposable

        bool disposed;
        
        ~Timer()
        {
            Dispose(false);
        }
        
        /// <summary>
        /// Call Dispose to ensure deregistration of the timer from TimerManager
        /// when consumer is done with the timer or being destroyed
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) TimerManager.DeregisterTimer(this);
            disposed = true;
        }

        #endregion
    }
}