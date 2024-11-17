# Improved Timers
An extensible Timer solution for Unity Game Development. Timers are 
self-managing by injecting a Timer Manager class into Unity's Update loop.
This is Personal Implementation of git-amend works as describe in his video

## Install
Import this library with Unity Package Manager using this git URL :
<br> (Window > Package Manager > + Add package from git URL)

```
https://github.com/Jianyao17/Improved-Timers.git
```

## How To Use
### Example Usage :
```csharp
CountdownTimer timer = new CountdownTimer(5f);

void Start() {
    timer.OnTimerStart += () => Debug.Log("Timer started");
    timer.OnTimerStop += () => Debug.Log("Timer stopped");
    timer.Start();
    
    timer.Pause();
    timer.Resume();
    
    timer.Reset();
    timer.Reset(10f);
    
    Debug.Log(timer.IsRunning ? "Timer is running" : "Timer is not running");
    Debug.Log(timer.IsFinished ? "Timer is finished" : "Timer is not finished");
    
    timer.Stop();
}

void Update() {
    Debug.Log(timer.CurrentTime);
    Debug.Log(timer.Progress);
}

void OnDestroy() {
    timer.Dispose();
}
```

## How It Works
### Initialization
  The TimerBootstrapper automatically injects the TimerManager 
  into the Player Loop when the game starts. 
  - This integration occurs in the Update phase, 
    ensuring minimal performance overhead.
  - In the Unity Editor, timers are safely removed when exiting 
    Play mode to prevent unexpected behavior during testing.

### 1. Timer Class
Represents an abstract base for timers. Derived classes can implement 
specific behaviors, such as countdown timers or repeatable timers.

- **Key Properties:**
  - `CurrentTime` : Tracks the current progress of the timer.
  - `IsRunning`: Indicates whether the timer is active.
  - `Progress`: A normalized value (0 to 1) representing the completion percentage.
  
- **Core Methods:**
  - `Start()`: Activates the timer and registers it with the TimerManager.
  - `Stop()`: Deactivates the timer and deregisters it.
  - `Tick()`: Updates the timer logic (implemented in derived classes).
  - `Dispose()`: Ensures proper cleanup and deregistration.

### 2. TimerManager
  A static class responsible for maintaining active timers and invoking 
  their`Tick()` methods during each update cycle.

### 3. TimerBootstrapper
  Integrates the TimerManager into Unity's Player Loop system, 
  allowing timers to update automatically during gameplay.

### 4. PlayerLoopUtils
  Utility functions to dynamically modify the Unity Player Loop, 
  enabling insertion, removal, or inspection of custom systems like the TimerManager.