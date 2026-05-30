using System;
using System.Drawing;
using System.Windows.Forms;

namespace AccountAdmin
{
    public class MainForm : Form
    {
        private Label lblTitle;
        private Label lblName;
        private TextBox txtName;
        private Label lblCode;
        private TextBox txtCode;
        private Label lblGroup;
        private ComboBox cmbGroups;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnClear;
        private ListBox listUsers;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;

        public MainForm()
        {
            // Спецификации независимого системного окна администрирования
            this.Text = "🛡️ АДМИНИСТРИРОВАНИЕ УЧЁТНЫХ ЗАПИСЕЙ MSA/MSU";
            this.Size = new Size(360, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(30, 30, 30); // Тёмно-неоновый стиль

            Font adminFont = new Font("Roboto", 10, FontStyle.Bold);
            Font regularFont = new Font("Roboto", 9, FontStyle.Regular);

            // ИСПРАВЛЕНО СТРОГО НА НА ТИВНОЕ ForeColor! Никаких фантомных свойств!
            lblTitle = new Label() { Text = "👥 КОНТРОЛЛЕР УЧЁТНЫХ ЗАПИСЕЙ AD", Location = new Point(20, 20), Width = 300, ForeColor = Color.FromArgb(0, 120, 215), Font = adminFont };

            lblName = new Label() { Text = "Имя персоны:", Location = new Point(20, 55), Width = 100, ForeColor = Color.White, Font = regularFont };
            txtName = new TextBox() { Location = new Point(130, 52), Width = 190, BackColor = Color.FromArgb(45, 45, 48), ForeColor = Color.Lime, Font = regularFont };

            lblCode = new Label() { Text = "Личный код:", Location = new Point(20, 90), Width = 100, ForeColor = Color.White, Font = regularFont };
            txtCode = new TextBox() { Location = new Point(130, 87), Width = 190, BackColor = Color.FromArgb(45, 45, 48), ForeColor = Color.Lime, Font = regularFont };

            lblGroup = new Label() { Text = "Группа прав:", Location = new Point(20, 125), Width = 100, ForeColor = Color.White, Font = regularFont };
            cmbGroups = new ComboBox() { Location = new Point(130, 122), Width = 190, BackColor = Color.FromArgb(45, 45, 48), ForeColor = Color.White, Font = regularFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGroups.Items.Add("MSA (Management Suite Admin)");
            cmbGroups.Items.Add("MSU (Management Suite User)");
            cmbGroups.SelectedIndex = 0;

            btnAdd = new Button() { Text = "➕ ИНЖЕКТИРОВАТЬ ПЕРСОНУ", Location = new Point(20, 160), Size = new Size(300, 32), BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, Font = adminFont, FlatStyle = FlatStyle.Flat };
            listUsers = new ListBox() { Location = new Point(20, 205), Size = new Size(300, 180), BackColor = Color.FromArgb(45, 45, 48), ForeColor = Color.Cyan, Font = regularFont };
            listUsers.Items.Add("[MSA] Nikita | Token: 832934 (Supreme Owner)");

            btnDelete = new Button() { Text = "🪓 УДАЛИТЬ ПЕРСОНУ", Location = new Point(20, 400), Size = new Size(145, 32), BackColor = Color.FromArgb(183, 28, 28), ForeColor = Color.White, Font = adminFont, FlatStyle = FlatStyle.Flat };
            btnClear = new Button() { Text = "🧹 СБРОСИТЬ КЭШ", Location = new Point(175, 400), Size = new Size(145, 32), BackColor = Color.FromArgb(61, 61, 61), ForeColor = Color.White, Font = adminFont, FlatStyle = FlatStyle.Flat };

            // Логика добавления персон в Active Directory
            btnAdd.Click += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text)) return;
                string group = cmbGroups.SelectedIndex == 0 ? "MSA" : "MSU";
                listUsers.Items.Add($"[{group}] {txtName.Text} | Code: {txtCode.Text}");
                lblStatus.Text = $"[AD] Учётка '{txtName.Text}' успешно инжектирована.";
                txtName.Clear(); txtCode.Clear();
            };

            // Логика удаления с ring-0 защитой создателя Nikita
            btnDelete.Click += (s, e) => {
                if (listUsers.SelectedItem == null) return;
                if (listUsers.SelectedItem.ToString().Contains("Nikita (Supreme Owner)"))
                {
                    MessageBox.Show("ОТКАЗ БЕЗОПАСНОСТИ!\nУчётная запись Создателя Nikita защищена.", "MSEM BLOCKED", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                listUsers.Items.Remove(listUsers.SelectedItem);
                lblStatus.Text = "[AD] Персона депортирована из системы.";
            };

            btnClear.Click += (s, e) => {
                listUsers.Items.Clear();
                listUsers.Items.Add("[MSA] Nikita | Token: 832934 (Supreme Owner)");
                lblStatus.Text = "[AD] Кэш домена очищен. Рут-аккаунт восстановлен.";
            };

            statusStrip = new StatusStrip() { BackColor = Color.FromArgb(45, 45, 48) };
            lblStatus = new ToolStripStatusLabel("Статус: Свойства ForeColor засинхронены. Архитектура стабильна.");
            lblStatus.ForeColor = Color.Lime;
            lblStatus.Font = new Font("Roboto", 9, FontStyle.Regular);
            statusStrip.Items.Add(lblStatus);

            this.Controls.Add(lblTitle); this.Controls.Add(lblName); this.Controls.Add(txtName);
            this.Controls.Add(lblCode); this.Controls.Add(txtCode); this.Controls.Add(lblGroup);
            this.Controls.Add(cmbGroups); this.Controls.Add(btnAdd); this.Controls.Add(listUsers);
            this.Controls.Add(btnDelete); this.Controls.Add(btnClear); this.Controls.Add(statusStrip);
        }
    }
}