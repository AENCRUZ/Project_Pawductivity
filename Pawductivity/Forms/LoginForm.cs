using Pawductivity.Managers;
using Pawductivity.Models;

namespace Pawductivity.Forms;

public class LoginForm : Form
{
    private TextBox   _txtUsername  = null!;
    private TextBox   _txtPetName   = null!;
    private ComboBox  _cboPetType   = null!;
    private Button    _btnStart     = null!;
    private Label     _lblTitle     = null!;
    private Label     _lblEmoji     = null!;

    public LoginForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // ── Form ─────────────────────────────────────────────────────
        Text            = "Pawductivity 🐾";
        Size            = new Size(420, 520);
        StartPosition   = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        BackColor       = PawTheme.Background;
        Font            = PawTheme.FontBody;

        // ── Header emoji ─────────────────────────────────────────────
        _lblEmoji = new Label
        {
            Text      = "🐾",
            Font      = new Font("Segoe UI Emoji", 52f),
            AutoSize  = true,
            BackColor = Color.Transparent,
        };
        _lblEmoji.Location = new Point((420 - 90) / 2, 24);

        // ── Title ────────────────────────────────────────────────────
        _lblTitle = new Label
        {
            Text      = "Pawductivity",
            Font      = PawTheme.FontTitle,
            ForeColor = PawTheme.Primary,
            AutoSize  = true,
            BackColor = Color.Transparent,
        };
        _lblTitle.Location = new Point((420 - 200) / 2, 110);

        // ── Card panel ───────────────────────────────────────────────
        var card = new Panel
        {
            Location  = new Point(30, 170),
            Size      = new Size(340, 270),
            BackColor = PawTheme.Surface,
        };
        card.Paint += PaintCardBorder;

        // ── Username ─────────────────────────────────────────────────
        var lblUser = MakeLabel("Your Name  👤", new Point(15, 15));
        _txtUsername = MakeTextBox(new Point(15, 35), 310);
        _txtUsername.PlaceholderText = "e.g. Vianci";

        // ── Pet name ─────────────────────────────────────────────────
        var lblPet = MakeLabel("Pet Name  🐱", new Point(15, 85));
        _txtPetName = MakeTextBox(new Point(15, 105), 310);
        _txtPetName.PlaceholderText = "e.g. Strawberry";

        // ── Pet type ─────────────────────────────────────────────────
        var lblType = MakeLabel("Choose Your Pet  🐾", new Point(15, 155));
        _cboPetType = new ComboBox
        {
            Location      = new Point(15, 175),
            Width         = 310,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font          = PawTheme.FontBody,
            BackColor     = PawTheme.Background,
            ForeColor     = PawTheme.TextDark,
        };
        _cboPetType.Items.AddRange(["🐱 Cat  —  earns XP faster!", "🐶 Dog  —  more forgiving!"]);
        _cboPetType.SelectedIndex = 0;

        // ── Start button ─────────────────────────────────────────────
        _btnStart = new Button
        {
            Text     = "Start Pawductivity! 🌸",
            Location = new Point(30, 450),
            Width    = 340,
        };
        PawTheme.StyleButton(_btnStart);
        _btnStart.Click += BtnStart_Click;

        // ── Tagline ──────────────────────────────────────────────────
        var lbl = new Label
        {
            Text      = "Stay productive. Keep your pet happy! 💕",
            ForeColor = PawTheme.TextMuted,
            Font      = PawTheme.FontSmall,
            AutoSize  = true,
            BackColor = Color.Transparent,
        };
        lbl.Location = new Point((420 - 240) / 2, 490);

        card.Controls.AddRange([lblUser, _txtUsername, lblPet, _txtPetName, lblType, _cboPetType]);
        Controls.AddRange([_lblEmoji, _lblTitle, card, _btnStart, lbl]);
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

    // ── Helpers ──────────────────────────────────────────────────────
    private static Label MakeLabel(string text, Point loc) => new()
    {
        Text      = text,
        Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold),
        ForeColor = PawTheme.TextMuted,
        AutoSize  = true,
        Location  = loc,
        BackColor = Color.Transparent,
    };

    private static TextBox MakeTextBox(Point loc, int width) => new()
    {
        Location  = loc,
        Width     = width,
        Height    = 30,
        BackColor = PawTheme.Background,
        ForeColor = PawTheme.TextDark,
        BorderStyle = BorderStyle.FixedSingle,
        Font      = PawTheme.FontBody,
    };

    private static void PaintCardBorder(object? sender, PaintEventArgs e)
    {
        if (sender is not Panel p) return;
        using var pen = new Pen(PawTheme.CardBorder, 2f);
        e.Graphics.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
    }
}
