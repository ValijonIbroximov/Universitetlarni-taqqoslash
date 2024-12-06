using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Universitetlarni_taqqoslash.Properties;

namespace Universitetlarni_taqqoslash
{
    public partial class login : Form
    {
        string parolfayl = string.Empty;
        string paroltxt = string.Empty;

        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtparol.PasswordChar == '*')
            {
                txtparol.PasswordChar = '\0';
                button1.BackgroundImage = Resources.hide;
            }
            else
            {
                txtparol.PasswordChar = '*';
                button1.BackgroundImage = Resources.view;
            }
        }

        private void btnkirish_Click(object sender, EventArgs e)
        {
            if (txtparol.Text != parolfayl)
            {
                MessageBox.Show($"Parol xato");
            }
            else
            {
                Universitetlar.loginparol = true;
                this.Close();
            }
        }

        private void linkdasturdanchiqish_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide(); // Formani yopib olish
            Application.Exit(); // Dastur tugatish
        }

        private void linkparoltiklash_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Agar parolingiz yodingizda bo'lmasa standart parolga qaytish mumkin\n"
                + "Bunda siz standart parolni bilishingiz kerak bo'ladi.\n\n"
                + "Rostdan ham standart parolga qaytishni istaysizmi?"
                , "Standart parolga qaytish", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                File.WriteAllText("parol.txt", "standartparol");
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            // parol.txt fayl nomi
            string fileName = "parol.txt";
            string defaultPassword = "standartparol";

            try
            {
                // Faylni tekshirish
                if (!File.Exists(fileName))
                {
                    // Fayl mavjud emas, yangi fayl yaratish va standartparol yozish
                    File.WriteAllText(fileName, defaultPassword);
                    //MessageBox.Show($"'{fileName}' fayli yaratildi va standart parol ('{defaultPassword}') bilan to'ldirildi.");
                }
                // Fayldan parolni o'qish
                parolfayl = File.ReadAllText(fileName);
                //MessageBox.Show($"'{fileName}' fayli o'qildi. Parol: {parolfayl}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}");
            }
        }

        private void txtparol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnkirish_Click(sender, e);
            }
        }
    }
}
