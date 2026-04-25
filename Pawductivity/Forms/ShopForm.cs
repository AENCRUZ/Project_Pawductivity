using Pawductivity.Managers;
using Pawductivity.Models;

namespace Pawductivity.Forms;

public class ShopForm : Form
{
    private readonly GameManager _gm;
    private readonly Action      _onBuy;
    private Label _lblCoins = null!;

    public ShopForm(GameManager gm, Action onBuy)
    {
        _gm   = gm;
        _onBuy = onBuy;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text            = "🛍️ Pawductivity Shop";
        Size            = new Size(540, 540);
        StartPosition   = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        BackColor       = PawTheme.Background;

        var title = new Label
        {
            Text      = "🛍️ Shop",
            Font      = PawTheme.FontTitle,
            ForeColor = PawTheme.Primary,
            AutoSize  = true,
            Location  = new Point(20, 15),
            BackColor = Color.Transparent,
        };

        _lblCoins = new Label
        {
            Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = PawTheme.Primary,
            AutoSize  = true,
            Location  = new Point(20, 60),
            BackColor = Color.Transparent,
        };

        Controls.AddRange([title, _lblCoins]);
        UpdateCoinsLabel();

        int y = 90;
        foreach (var item in _gm.ShopItems)
        {
            var card = BuildItemCard(item, y);
            Controls.Add(card);
            y += 72;
        }
    }

    private Panel BuildItemCard(ShopItem item, int y)
    {
        var card = new Panel
        {
            Location  = new Point(20, y),
            Size      = new Size(480, 62),
            BackColor = PawTheme.Surface,
        };
        card.Paint += (s, e) =>
        {
            using var pen = new Pen(PawTheme.CardBorder, 1.5f);
            e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
        };

        var lblEmoji = new Label
        {
            Text      = item.Emoji,
            Font      = new Font("Segoe UI Emoji", 22f),
            AutoSize  = true,
            Location  = new Point(10, 12),
            BackColor = Color.Transparent,
        };

        var lblName = new Label
        {
            Text      = item.Name,
            Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = PawTheme.TextDark,
            AutoSize  = true,
            Location  = new Point(58, 8),
            BackColor = Color.Transparent,
        };

        var lblDesc = new Label
        {
            Text      = $"{item.Description}   ❤️+{item.HealthBoost}  😸+{item.MoodBoost}",
            Font      = PawTheme.FontSmall,
            ForeColor = PawTheme.TextMuted,
            AutoSize  = true,
            Location  = new Point(58, 30),
            BackColor = Color.Transparent,
        };

        var btnBuy = new Button
        {
            Text     = $"🪙 {item.Cost}",
            Location = new Point(380, 14),
            Width    = 85,
        };
        PawTheme.StyleButton(btnBuy);
        btnBuy.Click += (s, e) =>
        {
            if (_gm.BuyItem(item))
            {
                MessageBox.Show($"You bought {item.Emoji} {item.Name}!\nYour pet loves it! 💕",
                                "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _onBuy();
                UpdateCoinsLabel();
            }
            else
            {
                MessageBox.Show("Not enough coins! 🪙 Complete more tasks to earn coins.",
                                "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        };

        card.Controls.AddRange([lblEmoji, lblName, lblDesc, btnBuy]);
        return card;
    }

    private void UpdateCoinsLabel() =>
        _lblCoins.Text = $"🪙 Your coins: {_gm.Pet.Coins}";
}
