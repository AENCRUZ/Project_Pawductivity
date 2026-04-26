using Pawductivity.Managers;
using Pawductivity.Models;

namespace Pawductivity.Forms;

public class DashboardForm : Form
{
    private readonly GameManager _gm;

    // Pet panel widgets
    private Label _lblPetEmoji = null!;
    private Label _lblPetName = null!;
    private Label _lblGreeting = null!;
    private Label _lblLevel = null!;
    private ProgressBar _pbHealth = null!;
    private ProgressBar _pbMood = null!;
    private ProgressBar _pbXp = null!;
    private Label _lblCoins = null!;

    // Task panel widgets
    private ListView _lvTasks = null!;
    private Button _btnAddTask = null!;
    private Button _btnComplete = null!;
    private Button _btnDelete = null!;
    private Button _btnEdit = null!;

    // Nav buttons
    private Button _btnShop = null!;
    private Button _btnStats = null!;

    // Stats labels
    private Label _lblToday = null!;
    private Label _lblStreak = null!;
    private Label _lblPending = null!;

    private System.Windows.Forms.Timer _decayTimer = null!;

    // ── Layout constants ────────────────────────────────────────────
    // Centralising all magic numbers here makes future tweaks trivial.
    private new int Margin = 16;  // outer gutter
    private const int InnerPad = 14;  // padding inside panels
    private const int TopBarH = 52;
    private const int PetPanelW = 288;
    private const int ButtonH = 34;
    private const int NavButtonW = 126;
    private const int ToolbarButtonW = 126;
    private const int StatBarH = 14;
    private const int StatBarLblGap = 4;   // gap between stat-bar label and bar

    public DashboardForm(GameManager gm)
    {
        _gm = gm;
        InitializeComponent();
        RefreshAll();
        StartDecayTimer();
    }

    private void InitializeComponent()
    {
        Text = "Pawductivity 🐾 — Dashboard";
        MinimumSize = new Size(970, 670);
        Size = new Size(930, 650);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = PawTheme.Background;
        Font = PawTheme.FontBody;
        FormClosed += (s, e) => Application.Exit();

        // ── TOP BAR ──────────────────────────────────────────────────
        // Uses DockStyle.Top so it stretches automatically on resize.
        var topBar = new Panel
        {
            Dock = DockStyle.Top,
            Height = TopBarH,
            BackColor = PawTheme.Primary,
        };

        var lblApp = new Label
        {
            Text = "🐾 Pawductivity",
            Font = new Font("Segoe UI", 16f, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            AutoSize = true,
            // Vertically centred within the top bar
            Location = new Point(Margin, (TopBarH - 28) / 2),
        };

        // User label is anchored right so it survives window resize.
        var lblUser = new Label
        {
            Text = $"Hi, {_gm.UserName}! 💕",
            Font = new Font("Segoe UI", 9f),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            AutoSize = true,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
        };
        topBar.Controls.AddRange([lblApp, lblUser]);

        // Position lblUser after AutoSize resolves (deferred via Load/Shown).
        // We approximate here; it will be corrected on first layout pass.
        lblUser.Location = new Point(topBar.Width - 200, (TopBarH - 18) / 2);

        // ── LEFT: PET PANEL ──────────────────────────────────────────
        // Panel sits Margin below the top bar.
        int panelTop = TopBarH + Margin;
        int panelBottom = ClientSize.Height - Margin; // bottom edge of both panels
        int petPanelH = panelBottom - panelTop;

        var petPanel = new Panel
        {
            Location = new Point(Margin, panelTop),
            Size = new Size(PetPanelW, petPanelH),
            BackColor = PawTheme.Surface,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom,
        };
        petPanel.Paint += (s, e) => PaintBorder(e, petPanel);

        // ── Pet emoji: large, centred, top of panel ──
        _lblPetEmoji = new Label
        {
            Font = new Font("Segoe UI Emoji", 52f),
            AutoSize = false,
            Size = new Size(PetPanelW - InnerPad * 2, 110),
            Location = new Point(InnerPad, InnerPad + 6),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
        };

        // ── Pet name: bold, centred ──
        _lblPetName = new Label
        {
            Font = new Font("Segoe UI", 13f, FontStyle.Bold),
            ForeColor = PawTheme.Primary,
            AutoSize = false,
            Size = new Size(PetPanelW - InnerPad * 2, 26),
            Location = new Point(InnerPad, _lblPetEmoji.Bottom + 8),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
        };

        // ── Greeting: muted, wraps to 2 lines ──
        _lblGreeting = new Label
        {
            Font = PawTheme.FontSmall,
            ForeColor = PawTheme.TextMuted,
            AutoSize = false,
            Size = new Size(PetPanelW - InnerPad * 2, 36),
            Location = new Point(InnerPad, _lblPetName.Bottom + 4),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
        };

        // ── Level: left-aligned, 10 px below greeting ──
        _lblLevel = new Label
        {
            Font = new Font("Segoe UI", 9f, FontStyle.Bold),
            ForeColor = PawTheme.TextDark,
            AutoSize = true,
            Location = new Point(InnerPad, _lblGreeting.Bottom + 10),
            BackColor = Color.Transparent,
        };

        // ── Stat bars: stacked with consistent spacing ──
        int barSlotH = 10 + StatBarLblGap + StatBarH + 8;  // 
        int firstBarY = _lblLevel.Location.Y + 20 + 10;      

        var (lblH, _pbHealthOut) = MakeStatBar("❤️ Health", firstBarY, PawTheme.HealthBar);
        var (lblM, _pbMoodOut) = MakeStatBar("😸 Mood", firstBarY + barSlotH, PawTheme.MoodBar);
        var (lblX, _pbXpOut) = MakeStatBar("⭐ XP", firstBarY + barSlotH * 2, PawTheme.XpBar);
        _pbHealth = _pbHealthOut;
        _pbMood = _pbMoodOut;
        _pbXp = _pbXpOut;

        // ── Coins: anchored to last bar's Bottom, not to AutoSize label ──
        _lblCoins = new Label
        {
            Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = PawTheme.Primary,
            AutoSize = true,
            Location = new Point(InnerPad, _pbXp.Bottom + 12),
            BackColor = Color.Transparent,
        };

        // ── Quick-stats sub-panel ──
        var statPanel = new Panel
        {
            Location = new Point(InnerPad, _lblCoins.Location.Y + 24 + 8),
            Size = new Size(PetPanelW - InnerPad * 2, 90),
            BackColor = PawTheme.Background,
        };
        // Rows spaced at 30 px intervals to add a touch more breathing room.
        _lblToday = MakeStatLabel("Tasks today: 0", new Point(6, 6));
        _lblStreak = MakeStatLabel("🔥 Streak: 0 days", new Point(6, 34));
        _lblPending = MakeStatLabel("📋 Pending: 0", new Point(6, 62));
        statPanel.Controls.AddRange([_lblToday, _lblStreak, _lblPending]);

        // ── Nav buttons: anchored to bottom of pet panel ──
        // ButtonH = 34; placed 14 px above the panel bottom, 10 px gap between.
        int navBtnY = statPanel.Bottom + 12;
        _btnShop = new Button { Text = "🛍️ Shop", Location = new Point(InnerPad, navBtnY), Width = NavButtonW, Height = ButtonH };
        _btnStats = new Button { Text = "📊 Stats", Location = new Point(InnerPad + NavButtonW + 8, navBtnY), Width = NavButtonW, Height = ButtonH };
        PawTheme.StyleButton(_btnShop, outlined: true);
        PawTheme.StyleButton(_btnStats, outlined: true);
        _btnShop.Click += (s, e) => new ShopForm(_gm, RefreshAll).ShowDialog(this);
        _btnStats.Click += (s, e) => new StatsForm(_gm).ShowDialog(this);

        petPanel.Controls.AddRange([
            _lblPetEmoji, _lblPetName, _lblGreeting, _lblLevel,
            lblH, _pbHealth, lblM, _pbMood, lblX, _pbXp,
            _lblCoins, statPanel, _btnShop, _btnStats,
        ]);

        // ── RIGHT: TASK PANEL ────────────────────────────────────────
        // Sits Margin to the right of the pet panel; stretches on resize.
        int taskPanelX = Margin + PetPanelW + Margin;
        int taskPanelW = Width - taskPanelX - Margin - 16; // 16 = scrollbar gutter estimate

        var taskPanel = new Panel
        {
            Location = new Point(taskPanelX, panelTop),
            Size = new Size(taskPanelW, petPanelH),
            BackColor = PawTheme.Surface,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
        };
        taskPanel.Paint += (s, e) => PaintBorder(e, taskPanel);

        var lblTaskTitle = new Label
        {
            Text = "📋 My Tasks",
            Font = PawTheme.FontHeading,
            ForeColor = PawTheme.Primary,
            AutoSize = true,
            Location = new Point(InnerPad, InnerPad),
            BackColor = Color.Transparent,
        };

        // ListView fills the panel between the title and the toolbar.
        // Anchoring all four sides lets it grow when the window is resized.
        int listTop = lblTaskTitle.Bottom + 10;
        int listH = petPanelH - listTop - ButtonH - InnerPad * 2 - 4;

        _lvTasks = new ListView
        {
            Location = new Point(InnerPad, listTop),
            Size = new Size(taskPanelW - InnerPad * 2, listH),
            View = View.Details,
            FullRowSelect = true,
            GridLines = false,
            BackColor = PawTheme.Background,
            ForeColor = PawTheme.TextDark,
            Font = PawTheme.FontBody,
            BorderStyle = BorderStyle.None,
            Anchor = AnchorStyles.Top | AnchorStyles.Left |
                            AnchorStyles.Bottom | AnchorStyles.Right,
        };

        // Columns: emoji | Task | Priority | Due Date | Status
        _lvTasks.Columns.Add("", 30);
        _lvTasks.Columns.Add("Task", 220);
        _lvTasks.Columns.Add("Priority", 96);
        _lvTasks.Columns.Add("Due Date", 96);
        _lvTasks.Columns.Add("Status", 96);

        _lvTasks.OwnerDraw = true;
        _lvTasks.ColumnWidthChanging += (s, e) =>
        {
            e.Cancel = true;
            e.NewWidth = _lvTasks.Columns[e.ColumnIndex].Width; // snap back to original
        };
        _lvTasks.Resize += (s, e) =>
        {
            int usedWidth = _lvTasks.Columns[0].Width  // emoji
                          + _lvTasks.Columns[2].Width  // Priority
                          + _lvTasks.Columns[3].Width  // Due Date
                          + _lvTasks.Columns[4].Width;  // Status
            _lvTasks.Columns[1].Width = _lvTasks.ClientSize.Width - usedWidth;
        };
        _lvTasks.DrawColumnHeader += (s, e) =>
        {
            using var bg = new SolidBrush(PawTheme.Secondary);
            e.Graphics.FillRectangle(bg, e.Bounds);

            // subtle bottom border instead of harsh seams
            using var pen = new Pen(Color.FromArgb(40, PawTheme.TextDark));
            e.Graphics.DrawLine(pen,
                e.Bounds.Left,
                e.Bounds.Bottom - 1,
                e.Bounds.Right,
                e.Bounds.Bottom - 1);

            // draw text centered vertically
            TextRenderer.DrawText(
                e.Graphics,
                e.Header.Text,
                _lvTasks.Font,
                e.Bounds,
                PawTheme.TextDark,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        };
        _lvTasks.DrawItem += LvTasks_DrawItem;
        _lvTasks.DrawSubItem += LvTasks_DrawSubItem;

        // ── Toolbar: four evenly-spaced buttons, anchored bottom-left ──
        // Total width used: 4 × ToolbarButtonW + 3 × gap(10) = 534 px
        int toolY = petPanelH - InnerPad - ButtonH;
        int toolGap = 10;

        _btnAddTask = new Button
        {
            Text = "+ Add Task",
            Width = ToolbarButtonW,
            Height = ButtonH,
            Location = new Point(InnerPad, toolY)
        };
        _btnComplete = new Button
        {
            Text = "✔ Complete",
            Width = ToolbarButtonW,
            Height = ButtonH,
            Location = new Point(InnerPad + (ToolbarButtonW + toolGap), toolY)
        };
        _btnEdit = new Button
        {
            Text = "✏️ Edit",
            Width = ToolbarButtonW,
            Height = ButtonH,
            Location = new Point(InnerPad + (ToolbarButtonW + toolGap) * 2, toolY)
        };
        _btnDelete = new Button
        {
            Text = "🗑 Delete",
            Width = ToolbarButtonW,
            Height = ButtonH,
            Location = new Point(InnerPad + (ToolbarButtonW + toolGap) * 3, toolY)
        };

        // Anchor toolbar buttons to bottom so they stay put on resize.
        foreach (var btn in new[] { _btnAddTask, _btnComplete, _btnEdit, _btnDelete })
            btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        PawTheme.StyleButton(_btnAddTask);
        PawTheme.StyleButton(_btnComplete);
        PawTheme.StyleButton(_btnEdit, outlined: true);
        PawTheme.StyleButton(_btnDelete, outlined: true);

        _btnAddTask.Click += BtnAdd_Click;
        _btnComplete.Click += BtnComplete_Click;
        _btnEdit.Click += BtnEdit_Click;
        _btnDelete.Click += BtnDelete_Click;

        taskPanel.Controls.AddRange([
            lblTaskTitle, _lvTasks,
            _btnAddTask, _btnComplete, _btnEdit, _btnDelete,
        ]);

        Controls.AddRange([topBar, petPanel, taskPanel]);
    }

    // ── CUSTOM LISTVIEW DRAW ─────────────────────────────────────────
    private void LvTasks_DrawItem(object? sender, DrawListViewItemEventArgs e)
    {
        e.DrawDefault = false;

        if (e.Item?.Tag is not TaskItem task) return;

        bool isSelected = (e.State & ListViewItemStates.Selected) != 0;

        Color bg = isSelected ? PawTheme.Secondary :
                   task.IsCompleted ? PawTheme.CompletedTask :
                   task.IsOverdue ? PawTheme.OverdueTask :
                   _lvTasks.BackColor;

        using (var brush = new SolidBrush(bg))
        {
            e.Graphics.FillRectangle(brush, e.Bounds);
        }
    }

    private void LvTasks_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
    {
        if (e.Item?.Tag is not TaskItem task) return;

        Color fg = task.IsCompleted ? PawTheme.TextGreen : PawTheme.TextDark;

        var flags = TextFormatFlags.Left |
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.EndEllipsis |
                    TextFormatFlags.NoPrefix;

        TextRenderer.DrawText(
            e.Graphics,
            e.SubItem.Text,
            _lvTasks.Font,
            e.Bounds,
            fg,
            flags
        );
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
        var selected = _lvTasks.SelectedItems[0];
        if (selected?.Tag is not TaskItem task)
        {
            ShowInfo("No task selected.");
            return;
        }
        if (task.IsCompleted) { ShowInfo("This task is already done! 🎉"); return; }

        _gm.CompleteTask(task.Id);

        var pet = _gm.Pet;
        var greeting = pet?.GetGreeting() ?? string.Empty;
        int coins = task.Priority switch
        {
            TaskPriority.High => 15,
            TaskPriority.Medium => 10,
            _ => 5,
        };
        ShowInfo($"{greeting}\n\n+XP gained! 🌟 Coins earned: {coins} 🪙");
        RefreshAll();
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (_lvTasks.SelectedItems.Count == 0) return;
        var task = (TaskItem)_lvTasks.SelectedItems[0].Tag!;
        using var dlg = new TaskEditForm(task);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            _gm.EditTask(task.Id, dlg.Result!.Title, dlg.Result.Description,
                         dlg.Result.Priority, dlg.Result.DueDate);
            RefreshAll();
        }
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (_lvTasks.SelectedItems.Count == 0) return;
        var task = (TaskItem)_lvTasks.SelectedItems[0].Tag!;
        if (MessageBox.Show($"Delete \"{task.Title}\"?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
        _lblPetName.Text = pet.Name;
        _lblGreeting.Text = pet.GetGreeting();
        _lblLevel.Text = $"Lv.{pet.Level}  •  Stage: {pet.Stage}";
        _lblCoins.Text = $"🪙 Coins: {pet.Coins}";

        _pbHealth.Value = pet.Health;
        _pbMood.Value = pet.Mood;
        _pbXp.Value = Math.Min(100, (int)((double)pet.XP / pet.XpForNextLevel * 100));

        _lblToday.Text = $"✅ Completed today: {_gm.CompletedToday}";
        _lblStreak.Text = $"🔥 Streak: {_gm.CurrentStreak} day(s)";
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
        _decayTimer = new System.Windows.Forms.Timer { Interval = 60_000 };
        _decayTimer.Tick += (s, e) => { _gm.ApplyOverduePenalties(); RefreshAll(); };
        _decayTimer.Start();
    }

    // ── Helpers ──────────────────────────────────────────────────────

    /// <summary>
    /// Creates a labelled progress-bar pair.
    /// <paramref name="y"/> is the top of the label; the bar sits
    /// <see cref="StatBarLblGap"/> px below it for tight but readable spacing.
    /// </summary>
    private static (Label lbl, ProgressBar pb) MakeStatBar(string label, int y, Color color)
    {
        var lbl = new Label
        {
            Text = label,
            Font = PawTheme.FontSmall,
            ForeColor = PawTheme.TextMuted,
            AutoSize = true,
            Location = new Point(InnerPad, y),
            BackColor = Color.Transparent,
        };
        var pb = new ProgressBar
        {
            Location = new Point(InnerPad, y + 15 + StatBarLblGap),
            Width = PetPanelW - InnerPad * 2,
            Height = StatBarH,
            Style = ProgressBarStyle.Continuous,
            Maximum = 100,
            Minimum = 0,
        };
        return (lbl, pb);
    }

    private static Label MakeStatLabel(string text, Point loc) => new()
    {
        Text = text,
        Font = PawTheme.FontSmall,
        ForeColor = PawTheme.TextDark,
        AutoSize = true,
        Location = loc,
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
