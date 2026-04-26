using Pawductivity.Managers;
using Pawductivity.Models;

namespace Pawductivity.Forms;

public class LoginForm : Form
{
    private TextBox _txtUsername = null!;
    private TextBox _txtPetName = null!;
    private ComboBox _cboPetType = null!;
    private Button _btnStart = null!;
    private Label _lblTitle = null!;
    private Label _lblEmoji = null!;

    public LoginForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // ── Form ─────────────────────────────────────────
        Text = "Pawductivity 🐾";
        Size = new Size(420, 520);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        BackColor = PawTheme.Background;
        Font = PawTheme.FontBody;

        // ── Main layout ──────────────────────────────────
        var main = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 5,
            Padding = new Padding(14),
        };

        main.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // emoji
        main.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // title
        main.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // card
        main.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // button
        main.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // tagline

        // ── Emoji ────────────────────────────────────────
        _lblEmoji = new Label
        {
            Text = "🐾",
            Font = new Font("Segoe UI Emoji", 52f),
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        // ── Title ────────────────────────────────────────
        _lblTitle = new Label
        {
            Text = "Pawductivity",
            Font = PawTheme.FontTitle,
            ForeColor = PawTheme.Primary,
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        // ── Card panel ───────────────────────────────────
        var card = new Panel
        {
            Dock = DockStyle.Top,
            BackColor = PawTheme.Surface,
            Padding = new Padding(5),
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
        };
        card.Paint += PaintCardBorder;

        var cardLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = 1,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
        };

        // ── Username ─────────────────────────────────────
        var lblUser = MakeLabel("Your Name  👤");
        _txtUsername = MakeTextBox();
        _txtUsername.PlaceholderText = "e.g. Marie";

        // ── Pet name ─────────────────────────────────────
        var lblPet = MakeLabel("Pet Name  🐱");
        _txtPetName = MakeTextBox();
        _txtPetName.PlaceholderText = "e.g. Strawberry";

        // ── Pet type ─────────────────────────────────────
        var lblType = MakeLabel("Choose Your Pet  🐾");
        _cboPetType = new ComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = PawTheme.FontBody,
            BackColor = PawTheme.Background,
            ForeColor = PawTheme.TextDark,
        };
        _cboPetType.Items.AddRange(new object[]
        {
            "🐱 Cat  —  earns XP faster!",
            "🐶 Dog  —  more forgiving!"
        });
        _cboPetType.SelectedIndex = 0;
        _cboPetType.Height = 40;

        // Add to card layout
        cardLayout.Controls.Add(lblUser);
        cardLayout.Controls.Add(_txtUsername);
        cardLayout.Controls.Add(lblPet);
        cardLayout.Controls.Add(_txtPetName);
        cardLayout.Controls.Add(lblType);
        cardLayout.Controls.Add(_cboPetType);

        card.Controls.Add(cardLayout);

        // ── Start button ─────────────────────────────────
        _btnStart = new Button
        {
            Text = "Start Pawductivity! 🌸",
            Dock = DockStyle.Top,
            Height = 40,
            Margin = new Padding(0, 10, 0, 0),
        };
        PawTheme.StyleButton(_btnStart);
        _btnStart.Click += BtnStart_Click;

        // ── Tagline ──────────────────────────────────────
        var lbl = new Label
        {
            Text = "Stay productive. Keep your pet happy! 💕",
            ForeColor = PawTheme.TextMuted,
            Font = PawTheme.FontSmall,
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        // ── Add everything ───────────────────────────────
        main.Controls.Add(_lblEmoji);
        main.Controls.Add(_lblTitle);
        main.Controls.Add(card);
        main.Controls.Add(_btnStart);
        main.Controls.Add(lbl);

        Controls.Add(main);
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtUsername.Text) ||
            string.IsNullOrWhiteSpace(_txtPetName.Text))
        {
            MessageBox.Show("Please fill in your name and a pet name! 🐾",
                            "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        Pet pet = _cboPetType.SelectedIndex == 0
            ? new CatPet(_txtPetName.Text.Trim())
            : new DogPet(_txtPetName.Text.Trim());

        var manager = new GameManager(pet) { UserName = _txtUsername.Text.Trim() };

        var dashboard = new DashboardForm(manager);
        dashboard.Show();
        Hide();
    }

    // ── Helpers ────────────────────────────────────────
    private static Label MakeLabel(string text) => new()
    {
        Text = text,
        Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
        ForeColor = PawTheme.TextMuted,
        AutoSize = true,
        Margin = new Padding(0, 10, 0, 2),
    };

    private static TextBox MakeTextBox() => new()
    {
        Dock = DockStyle.Top,
        Height = 30,
        BackColor = PawTheme.Background,
        ForeColor = PawTheme.TextDark,
        BorderStyle = BorderStyle.FixedSingle,
        Font = PawTheme.FontBody,
        Margin = new Padding(0, 0, 0, 5),
    };

    private static void PaintCardBorder(object? sender, PaintEventArgs e)
    {
        if (sender is not Panel p) return;
        using var pen = new Pen(PawTheme.CardBorder, 2f);
        e.Graphics.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
    }
}