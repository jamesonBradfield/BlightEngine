// Profiler.cs
using System.Diagnostics;
using ImGuiNET;
using System.Numerics;
using ByteSizeLib;

/// <summary>
/// Provides real-time performance profiling and visualization using ImGui.
/// Tracks execution time of different sections of code and displays metrics.
/// </summary>
public class ImGuiProfiler : IDisposable
{
    private Dictionary<string, ProfilerMetric> metrics;
    private bool isVisible;
    private float updateInterval;
    private float timeSinceLastUpdate;
    private Vector2 windowPosition;
    private Vector2 windowSize;
    private bool firstFrame = true;
    private Process proc = Process.GetCurrentProcess();

    /// <summary>
    /// Represents a single metric being tracked by the profiler.
    /// </summary>
    private class ProfilerMetric
    {
        /// <summary>
        /// Stopwatch for timing the metric.
        /// </summary>
        public Stopwatch Stopwatch { get; } = new Stopwatch();

        /// <summary>
        /// Historical timing data for the metric.
        /// </summary>
        public Queue<float> History { get; } = new Queue<float>();

        /// <summary>
        /// Running average execution time.
        /// </summary>
        public float AverageTime { get; set; }

        /// <summary>
        /// Minimum recorded execution time.
        /// </summary>
        public float MinTime { get; set; } = float.MaxValue;

        /// <summary>
        /// Maximum recorded execution time.
        /// </summary>
        public float MaxTime { get; set; }

        /// <summary>
        /// Number of samples collected.
        /// </summary>
        public int Samples { get; set; }

        /// <summary>
        /// Maximum number of historical samples to keep.
        /// </summary>
        public const int MaxHistorySize = 100;
    }

    /// <summary>
    /// Initializes a new instance of the ImGui profiler.
    /// </summary>
    /// <param name="updateIntervalSeconds">How often to update the display, in seconds.</param>
    public ImGuiProfiler(float updateIntervalSeconds = 0.5f)
    {
        metrics = new Dictionary<string, ProfilerMetric>();
        updateInterval = updateIntervalSeconds;
        timeSinceLastUpdate = 0;
        isVisible = true;
        windowPosition = new Vector2(10, 10);
        windowSize = new Vector2(300, 400);
    }

    /// <summary>
    /// Starts profiling a named section of code.
    /// </summary>
    /// <param name="name">The identifier for this profile section.</param>
    public void BeginProfile(string name)
    {
        if (!metrics.ContainsKey(name))
        {
            metrics[name] = new ProfilerMetric();
        }
        metrics[name].Stopwatch.Restart();
    }

    /// <summary>
    /// Ends profiling for a named section and records the metrics.
    /// </summary>
    /// <param name="name">The identifier for this profile section.</param>
    public void EndProfile(string name)
    {
        if (!metrics.ContainsKey(name)) return;

        var metric = metrics[name];
        metric.Stopwatch.Stop();

        float elapsed = (float)metric.Stopwatch.Elapsed.TotalMilliseconds;
        metric.History.Enqueue(elapsed);
        if (metric.History.Count > ProfilerMetric.MaxHistorySize)
        {
            metric.History.Dequeue();
        }

        metric.MinTime = Math.Min(metric.MinTime, elapsed);
        metric.MaxTime = Math.Max(metric.MaxTime, elapsed);
        metric.Samples++;

        metric.AverageTime = ((metric.AverageTime * (metric.Samples - 1)) + elapsed) / metric.Samples;
    }

    /// <summary>
    /// Updates the profiler display and processes new metrics.
    /// </summary>
    /// <param name="deltaTime">Time elapsed since last frame.</param>
    public void Update(float deltaTime)
    {
        // Update debug metric
        // BeginProfile("DEBUG");
        // Thread.Sleep(1); // Intentional delay to verify profiler is working
        // EndProfile("DEBUG");

        timeSinceLastUpdate += deltaTime;

        if (firstFrame)
        {
            ImGui.SetNextWindowPos(windowPosition);
            ImGui.SetNextWindowSize(windowSize);
            firstFrame = false;
        }
		proc.Refresh();
        UpdateProfilerWindow();
    }

    private void UpdateProfilerWindow()
    {
        if (!isVisible) return;

        var windowFlags = ImGuiWindowFlags.None;

        ImGui.SetNextWindowPos(windowPosition, ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSize(windowSize, ImGuiCond.FirstUseEver);

        if (ImGui.Begin("Performance Profiler###PerfProfiler", ref isVisible, windowFlags))
        {
            ImGui.Text($"Active Metrics: {metrics.Count}");
            ImGui.Text($"Profiler Status: Active");
			//TODO: add treenode and graphing for ram
			ImGui.Text($"Ram: {ByteSize.FromBytes(proc.PrivateMemorySize64).GigaBytes} Gb");

            foreach (var kvp in metrics)
            {
                var name = kvp.Key;
                var metric = kvp.Value;

                if (ImGui.TreeNode(name))
                {
                    ImGui.Text($"Average: {metric.AverageTime:F2}ms");
                    ImGui.Text($"Min: {metric.MinTime:F2}ms");
                    ImGui.Text($"Max: {metric.MaxTime:F2}ms");
                    ImGui.Text($"Samples: {metric.Samples}");
                    var history = metric.History.ToArray();
                    if (history.Length > 0)
                    {
                        ImGui.PlotLines($"##History{name}",
                            ref history[0],
                            history.Length,
                            0,
                            "History (ms)",
                            metric.MinTime,
                            metric.MaxTime,
                            new Vector2(ImGui.GetContentRegionAvail().X, 80));
                    }
                    ImGui.TreePop();
                }
            }

        }
        ImGui.End();
    }

    /// <summary>
    /// Profiles an action and records its execution time.
    /// </summary>
    /// <param name="name">Name of the profile section.</param>
    /// <param name="action">Action to profile.</param>
    public void ProfileAction(string name, Action action)
    {
        BeginProfile(name);
        action();
        EndProfile(name);
    }

    /// <summary>
    /// Toggles the visibility of the profiler window.
    /// </summary>
    public void ToggleVisibility()
    {
        isVisible = !isVisible;
    }

    /// <summary>
    /// Releases resources used by the profiler.
    /// </summary>
    public void Dispose()
    {
        metrics.Clear();
    }
}
