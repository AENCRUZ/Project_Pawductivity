using Pawductivity.Managers;
using Pawductivity.Models;

namespace Pawductivity.Forms;

public class DashboardForm : Form
{
    private readonly GameManager _gm;

    // Pet panel widgets
    private Label      _lblPetEmoji  = null!;
    private Label      _lblPetName   = null!;
    private Label      _lblGreeting  = null!;
    private Label      _lblLevel     = null!;
    private ProgressBar _pbHealth    = null!;
    private ProgressBar _pbMood      = null!;
    private ProgressBar _pbXp        = null!;
    private Label      _lblCoins     = null!;

    // Task panel widgets
    private ListView   _lvTasks      = null!;
    private Button     _btnAddTask   = null!;
    private Button     _btnComplete  = null!;
    private Button     _btnDelete    = null!;
    private Button     _btnEdit      = null!;

    // Nav buttons
    private Button _btnShop  = null!;
    private Button _btnStats = null!;

    // Stats labels
    private Label _lblToday   = null!;
    private Label _lblStreak  = null!;
    private Label _lblPending = null!;

    private System.Windows.Forms.Timer _decayTimer = null!;

    public DashboardForm(GameManager gm)
    {
        _gm = gm;
        InitializeComponent();
        RefreshAll();
        StartDecayTimer();
    }

    private void InitializeComponent()
    {
        Text            = "Pawductivity 🐾 — Dashboard";
        Size            = new Size(900, 620);
        StartPosition   = FormStartPosition.CenterScreen;
        BackColor       = PawTheme.Background;
        Font            = PawTheme.FontBody;
        FormClosed      += (s, e) => Application.Exit();

        // ── TOP BAR ──────────────────────────────────────────────────
        var topBar = new Panel { Dock = DockStyle.Top, Height = 52, BackColor = PawTheme.Primary };
        var lblApp = new Label
        {
            Text      = "🐾 Pawductivity",
            Font      = new Font("Segoe UI", 16f, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            AutoSize  = true,
            Location  = new Point(16, 12),
        };
        var lblUser = new Label
        {
            Text      = $"Hi, {_gm.UserName}! 💕",
            Font      = new Font("Segoe UI", 9f),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            AutoSize  = true,
            Location  = new Point(700, 18),
        };
        topBar.Controls.AddRange([lblApp, lblUser]);

        // ── LEFT: PET PANEL ──────────────────────────────────────────
        var petPanel = new Panel
        {
            Location  = new Point(12, 64),
            Size      = new Size(280, 510),
            BackColor = PawTheme.Surface,
        };
        petPanel.Paint += (s, e) => PaintBorder(e, petPanel);

        _lblPetEmoji = new Label
        {
            Font      = new Font("Segoe UI Emoji", 56f),
            AutoSize  = false,
            Size      = new Size(280, 90),
            Location  = new Point(0, 12),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
        };

        _lblPetName = new Label
        {
            Font      = new Font("Segoe UI", 14f, FontStyle.Bold),
            ForeColor = PawTheme.Primary,
            AutoSize  = false,
            Width     = 260,
            Height    = 28,
            Location  = new Point(10, 108),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
        };

        _lblGreeting = new Label
        {
            Font      = PawTheme.FontSmall,
            ForeColor = PawTheme.TextMuted,
            AutoSize  = false,
            Size      = new Size(260, 36),
            Location  = new Point(10, 138),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
        };

        _lblLevel = new Label
        {
            Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
            ForeColor = PawTheme.TextDark,
            AutoSize  = true,
            Location  = new Point(10, 180),
            BackColor = Color.Transparent,
        };

        // Stat bars
        var (lblH, _pbHealthOut) = MakeStatBar("❤️ Health",  195, PawTheme.HealthBar);
        var (lblM, _pbMoodOut)   = MakeStatBar("😸 Mood",    237, PawTheme.MoodBar);
        var (lblX, _pbXpOut)     = MakeStatBar("⭐ XP",      279, PawTheme.XpBar);
        _pbHealth = _pbHealthOut;
        _pbMood   = _pbMoodOut;
        _pbXp     = _pbXpOut;

        _lblCoins = new Label
        {
            Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = PawTheme.Primary,
            AutoSize  = true,
            Location  = new Point(10, 318),
            BackColor = Color.Transparent,
        };

        // Quick stats
        var statPanel = new Panel
        {
            Location  = new Point(10, 345),
            Size      = new Size(260, 100),
            BackColor = PawTheme.Background,
        };
        _lblToday   = MakeStatLabel("Tasks today: 0",     new Point(6, 6));
        _lblStreak  = MakeStatLabel("🔥 Streak: 0 days",  new Point(6, 32));
        _lblPending = MakeStatLabel("📋 Pending: 0",      new Point(6, 58));
        statPanel.Controls.AddRange([_lblToday, _lblStreak, _lblPending]);

        // Nav buttons
        _btnShop = new Button { Text = "🛍️ Shop", Location = new Point(10, 460), Width = 120 };
        _btnStats = new Button { Text = "📊 Stats", Location = new Point(145, 460), Width = 120 };
        PawTheme.StyleButton(_btnShop, outlined: true);
        PawTheme.StyleButton(_btnStats, outlined: true);
        _btnShop.Click  += (s, e) => new ShopForm(_gm, RefreshAll).ShowDialog(this);
        _btnStats.Click += (s, e) => new StatsForm(_gm).ShowDialog(this);

        petPanel.Controls.AddRange([
            _lblPetEmoji, _lblPetName, _lblGreeting, _lblLevel,
            lblH, _pbHealth, lblM, _pbMood, lblX, _pbXp,
            _lblCoins, statPanel, _btnShop, _btnStats
        ]);

        // ── RIGHT: TASK PANEL ────────────────────────────────────────
        var taskPanel = new Panel
        {
            Location  = new Point(304, 64),
            Size      = new Size(568, 510),
            BackColor = PawTheme.Surface,
        };
        taskPanel.Paint += (s, e) => PaintBorder(e, taskPanel);

        var lblTaskTitle = new Label
        {
            Text      = "📋 My Tasks",
            Font      = PawTheme.FontHeading,
            ForeColor = PawTheme.Primary,
            AutoSize  = true,
            Location  = new Point(12, 12),
            BackColor = Color.Transparent,
        };

        _lvTasks = new ListView
        {
            Location      = new Point(12, 45),
            Size          = new Size(544, 380),
            View          = View.Details,
            FullRowSelect = true,
            GridLines     = false,
            BackColor     = PawTheme.Background,
            ForeColor     = PawTheme.TextDark,
            Font          = PawTheme.FontBody,
            BorderStyle   = BorderStyle.None,
        };
        _lvTasks.Columns.Add("",     30);   // emoji
        _lvTasks.Columns.Add("Task", 210);
        _lvTasks.Columns.Add("Priority", 80);
        _lvTasks.Columns.Add("Due Date", 100);
        _lvTasks.Columns.Add("Status",   100);
        _lvTasks.OwnerDraw = true;
        _lvTasks.DrawColumnHeader += (s, e) => {
            e.Graphics.FillRectangle(new SolidBrush(PawTheme.Secondary), e.Bounds);
            e.DrawText();
        };
        _lvTasks.DrawItem += LvTasks_DrawItem;
        _lvTasks.DrawSubItem += LvTasks_DrawSubItem;

        // Toolbar buttons
        _btnAddTask  = new Button { Text = "+ Add Task",      Location = new Point(12, 440), Width = 110 };
        _btnComplete = new Button { Text = "✔ Complete",      Location = new Point(134, 440), Width = 110 };
        _btnEdit     = new Button { Text = "✏️ Edit",         Location = new Point(256, 440), Width = 110 };
        _btnDelete   = new Button { Text = "🗑 Delete",        Location = new Point(378, 440), Width = 110 };

        PawTheme.StyleButton(_btnAddTask);
        PawTheme.StyleButton(_btnComplete);
        PawTheme.StyleButton(_btnEdit, outlined: true);
        PawTheme.StyleButton(_btnDelete, outlined: true);

        _btnAddTask.Click  += BtnAdd_Click;
        _btnComplete.Click += BtnComplete_Click;
        _btnEdit.Click     += BtnEdit_Click;
        _btnDelete.Click   += BtnDelete_Click;

        taskPanel.Controls.AddRange([
            lblTaskTitle, _lvTasks,
            _btnAddTask, _btnComplete, _btnEdit, _btnDelete
        ]);

        Controls.AddRange([topBar, petPanel, taskPanel]);
    }

    // ── CUSTOM LISTVIEW DRAW ─────────────────────────────────────────
    private void LvTasks_DrawItem(object? sender, DrawListViewItemEventArgs e)
    {
        var task = (TaskItem)e.Item.Tag!;
        Color bg = e.Item.Selected
            ? PawTheme.Secondary
            : task.IsCompleted
                ? PawTheme.CompletedTask
                : task.IsOverdue
                    ? Color.FromArgb(255, 220, 220)
                    : PawTheme.Background;

        e.Graphics.FillRectangle(new SolidBrush(bg), e.Bounds);
        e.DrawFocusRectangle();
    }

    private void LvTasks_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
    {
        var task = (TaskItem)e.Item.Tag!;
        Color fg = task.IsCompleted ? Color.Gray : PawTheme.TextDark;
        var flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
        TextRenderer.DrawText(e.Graphics, e.SubItem!.Text, _lvTasks.Font, e.Bounds, fg, flags);
    }

    // ── BUTTON HANDLERS ──────────────────────────────────────────────
    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dlg = new TaskEditForm();
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            _gm.AddTask(dlg.Result!);
            RefreshAll();
        }
    }

    private void BtnComplete_Click(object? sender, EventArgs e)
    {
        if (_lvTasks.SelectedItems.Count == 0) return;
        var task = (TaskItem)_lvTasks.SelectedItems[0].Tag!;
        if (task.IsCompleted) { ShowInfo("This task is already done! 🎉"); return; }
        _gm.CompleteTask(task.Id);
        ShowInfo($"{_gm.Pet.GetGreeting()}\n\n+XP gained! 🌟 Coins earned: {task.Priority switch { TaskPriority.High => 15, TaskPriority.Medium => 10, _ => 5 }} 🪙");
        RefreshAll();
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (_lvTasks.SelectedItems.Count == 0) return;
        var task = (TaskItem)_lvTasks.SelectedItems[0].Tag!;
        using var dlg = new TaskEditForm(task);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            _gm.EditTask(task.Id, dlg.Result!.Title, dlg.Result.Description, dlg.Result.Priority, dlg.Result.DueDate);
            RefreshAll();
        }
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (_lvTasks.SelectedItems.Count == 0) return;
        var task = (TaskItem)_lvTasks.SelectedItems[0].Tag!;
        if (MessageBox.Show($"Delete \"{task.Title}\"?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            _gm.DeleteTask(task.Id);
            RefreshAll();
        }
    }

    // ── REFRESH ───────────────────────────────────────────────────────
    public void RefreshAll()
    {
        var pet = _gm.Pet;

        _lblPetEmoji.Text = $"{pet.StageEmoji}\n{pet.MoodEmoji}";
        _lblPetName.Text  = pet.Name;
        _lblGreeting.Text = pet.GetGreeting();
        _lblLevel.Text    = $"Lv.{pet.Level}  •  Stage: {pet.Stage}";
        _lblCoins.Text    = $"🪙 Coins: {pet.Coins}";

        _pbHealth.Value = pet.Health;
        _pbMood.Value   = pet.Mood;
        _pbXp.Value     = Math.Min(100, (int)((double)pet.XP / pet.XpForNextLevel * 100));

        _lblToday.Text   = $"✅ Completed today: {_gm.CompletedToday}";
        _lblStreak.Text  = $"🔥 Streak: {_gm.CurrentStreak} day(s)";
        _lblPending.Text = $"📋 Pending: {_gm.PendingCount}";

        _lvTasks.Items.Clear();
        foreach (var t in _gm.Tasks.OrderBy(t => t.IsCompleted).ThenBy(t => t.DueDate))
        {
            var item = new ListViewItem(t.IsCompleted ? "✅" : t.IsOverdue ? "⚠️" : "⬜");
            item.SubItems.Add(t.Title);
            item.SubItems.Add($"{t.PriorityEmoji} {t.Priority}");
            item.SubItems.Add(t.DueDate.ToString("MMM dd"));
            item.SubItems.Add(t.IsCompleted ? "Done 🎉" : t.IsOverdue ? "Overdue!" : "Pending");
            item.Tag = t;
            _lvTasks.Items.Add(item);
        }
    }

    // ── Decay timer (mood drops if tasks are overdue) ─────────────────
    private void StartDecayTimer()
    {
        _decayTimer = new System.Windows.Forms.Timer { Interval = 60_000 }; // every 1 min
        _decayTimer.Tick += (s, e) => { _gm.ApplyOverduePenalties(); RefreshAll(); };
        _decayTimer.Start();
    }

    // ── Helpers ──────────────────────────────────────────────────────
    private static (Label lbl, ProgressBar pb) MakeStatBar(string label, int y, Color color)
    {
        var lbl = new Label
        {
            Text      = label,
            Font      = PawTheme.FontSmall,
            ForeColor = PawTheme.TextMuted,
            AutoSize  = true,
            Location  = new Point(10, y),
            BackColor = Color.Transparent,
        };
        var pb = new ProgressBar
        {
            Location = new Point(10, y + 16),
            Width    = 260,
            Height   = 14,
            Style    = ProgressBarStyle.Continuous,
            Maximum  = 100,
            Minimum  = 0,
        };
        return (lbl, pb);
    }

    private static Label MakeStatLabel(string text, Point loc) => new()
    {
        Text      = text,
        Font      = PawTheme.FontSmall,
        ForeColor = PawTheme.TextDark,
        AutoSize  = true,
        Location  = loc,
        BackColor = Color.Transparent,
    };

    private static void PaintBorder(PaintEventArgs e, Control c)
    {
        using var pen = new Pen(PawTheme.CardBorder, 1.5f);
        e.Graphics.DrawRectangle(pen, 0, 0, c.Width - 1, c.Height - 1);
    }

    private void ShowInfo(string msg) =>
        MessageBox.Show(msg, "Pawductivity 🐾", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
