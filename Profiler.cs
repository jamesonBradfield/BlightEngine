using System.Diagnostics;
using ImGuiNET;
using System.Numerics;

public class ImGuiProfiler : IDisposable
{
    private Dictionary<string, ProfilerMetric> metrics;
    private bool isVisible;
    private float updateInterval;
    private float timeSinceLastUpdate;
    private Vector2 windowPosition;
    private Vector2 windowSize;
    private bool firstFrame = true;

    private class ProfilerMetric
    {
        public Stopwatch Stopwatch { get; } = new Stopwatch();
        public Queue<float> History { get; } = new Queue<float>();
        public float AverageTime { get; set; }
        public float MinTime { get; set; } = float.MaxValue;
        public float MaxTime { get; set; }
        public int Samples { get; set; }

        public const int MaxHistorySize = 100;
    }

    public ImGuiProfiler(float updateIntervalSeconds = 0.5f)
    {
        metrics = new Dictionary<string, ProfilerMetric>();
        updateInterval = updateIntervalSeconds;
        timeSinceLastUpdate = 0;
        isVisible = true;
        windowPosition = new Vector2(10, 10);
        windowSize = new Vector2(300, 400);

        // Add a debug metric
        // metrics["DEBUG"] = new ProfilerMetric();
    }

    public void BeginProfile(string name)
    {
        if (!metrics.ContainsKey(name))
        {
            metrics[name] = new ProfilerMetric();
        }
        metrics[name].Stopwatch.Restart();
    }

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

    public void ToggleVisibility()
    {
        isVisible = !isVisible;
    }

    public void Dispose()
    {
        metrics.Clear();
    }

    public void ProfileAction(string name, Action action)
    {
        BeginProfile(name);
        action();
        EndProfile(name);
    }
}
