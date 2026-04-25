using Pawductivity.Models;

namespace Pawductivity.Managers;

/// <summary>
/// Holds all runtime state. Acts as the glue between forms.
/// Demonstrates Abstraction — forms don't know HOW things are stored.
/// </summary>
public class GameManager
{
    // Singleton-ish pattern: one instance passed between forms
    public Pet            Pet       { get; private set; }
    public List<TaskItem> Tasks     { get; private set; } = [];
    public List<ShopItem> ShopItems { get; private set; } = ShopItem.DefaultShop();
    public string         UserName  { get; set; } = string.Empty;

    // ── Stats ────────────────────────────────────────────────────────
    public int TotalCompleted { get; private set; }
    public int CurrentStreak  { get; private set; }
    public int LongestStreak  { get; private set; }
    private DateTime? _lastCompletionDate;

    public GameManager(Pet pet) => Pet = pet;

    // ── Task Operations ──────────────────────────────────────────────
    public void AddTask(TaskItem task) => Tasks.Add(task);

    public void EditTask(Guid id, string title, string desc, TaskPriority priority, DateTime due)
    {
        var t = Tasks.FirstOrDefault(t => t.Id == id);
        if (t is null) return;
        t.Title       = title;
        t.Description = desc;
        t.Priority    = priority;
        t.DueDate     = due;
    }

    public void DeleteTask(Guid id) => Tasks.RemoveAll(t => t.Id == id);

    public void CompleteTask(Guid id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id && !t.IsCompleted);
        if (task is null) return;

        task.Complete();
        Pet.ReactToTaskCompleted(task);
        TotalCompleted++;
        UpdateStreak();
    }

    public void ApplyOverduePenalties()
    {
        foreach (var t in Tasks.Where(t => t.IsOverdue))
            Pet.ReactToTaskMissed();
    }

    // ── Shop ─────────────────────────────────────────────────────────
    public bool BuyItem(ShopItem item)
    {
        if (Pet.Coins < item.Cost) return false;
        Pet.Coins   -= item.Cost;
        Pet.Health  += item.HealthBoost;
        Pet.Mood    += item.MoodBoost;
        return true;
    }

    // ── Analytics ────────────────────────────────────────────────────
    public int CompletedToday =>
        Tasks.Count(t => t.IsCompleted && t.CompletedAt?.Date == DateTime.Today);

    public int PendingCount =>
        Tasks.Count(t => !t.IsCompleted);

    public double CompletionRate =>
        Tasks.Count == 0 ? 0 : (double)Tasks.Count(t => t.IsCompleted) / Tasks.Count * 100;

    private void UpdateStreak()
    {
        var today = DateTime.Today;
        if (_lastCompletionDate == null || _lastCompletionDate.Value.Date < today.AddDays(-1))
            CurrentStreak = 1;
        else if (_lastCompletionDate.Value.Date == today.AddDays(-1))
            CurrentStreak++;
        // same day — no change
        _lastCompletionDate = DateTime.Now;
        if (CurrentStreak > LongestStreak) LongestStreak = CurrentStreak;
    }
}
