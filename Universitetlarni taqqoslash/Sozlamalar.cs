using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Universitetlarni_taqqoslash
{
    public partial class Sozlamalar : Form
    {
        string connectionString = string.Empty;

        string selectId = string.Empty;

        string id = string.Empty;
        string nom = string.Empty;
        System.Drawing.Image rasm = null;
        string viloyat = string.Empty;
        string shahar = string.Empty;
        string holat = string.Empty;
        string tashkil_etilgan_yili = string.Empty;
        string telefon = string.Empty;
        string email = string.Empty;
        string veb_sayti = string.Empty;
        string oqituvchilar_soni = string.Empty;
        string dontsentlar_soni = string.Empty;
        string professorlar_soni = string.Empty;
        string akademiklar_soni = string.Empty;
        string xorijiy_oqituvchilar_soni = string.Empty;
        string ilmiy_maqolalar_soni = string.Empty;
        string ilmiy_iqtiboslar_soni = string.Empty;
        string tadqiqot_mablaglari = string.Empty;
        string oqituvchilar_KPI = string.Empty;
        string talabalar_soni = string.Empty;
        string erkak_talabalar_soni = string.Empty;
        string ayol_talabalar_soni = string.Empty;
        string xorijiy_talabalar_soni = string.Empty;
        string bitiruvchilar_soni = string.Empty;
        string ish_bilan_bandlik = string.Empty;
        string qarzdor_talabalar_soni = string.Empty;
        string umumiy_qarz = string.Empty;
        string ortacha_GPA = string.Empty;
        string axborot_resurslari_soni = string.Empty;
        string oquv_bino_sigimi = string.Empty;
        string yotoqxona_sigimi = string.Empty;
        string ortacha_stipendiya = string.Empty;
        string ortacha_kontrakt_tolovi = string.Empty;
        string yotoqxona_tolovi = string.Empty;
        string ajratilgan_joylar = string.Empty;
        string grant_joylar = string.Empty;
        string kontrakt_joylar = string.Empty;
        string fakultetlar_soni = string.Empty;
        string talim_yonalishlari_ids = string.Empty;


        public Sozlamalar()
        {
            InitializeComponent();
        }

        private void chiqishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void parolniAlmashtirishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Prompt the user to enter the old password
            string oldPassword = Microsoft.VisualBasic.Interaction.InputBox("Eski parolni kiriting:", "Parolni almashtirish");

            // Read the current password from the parol.txt file
            string currentPassword = ReadPasswordFromFile();

            if (oldPassword != currentPassword)
            {
                MessageBox.Show("Eski parol noto'g'ri. Parolni almashtirish bekor qilindi.", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Prompt the user to enter the new password
            string newPassword = Microsoft.VisualBasic.Interaction.InputBox("Yangi parolni kiriting:", "Parolni almashtirish");

            // Prompt the user to confirm the new password
            string confirmNewPassword = Microsoft.VisualBasic.Interaction.InputBox("Yangi parolni qaytadan kiriting:", "Parolni almashtirish");

            // Check if the new password matches the confirmed password
            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Yangi parollar mos kelmadi. Parolni almashtirish bekor qilindi.", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Write the new password to the parol.txt file
            WritePasswordToFile(newPassword);

            MessageBox.Show("Yangi parol muvaffaqiyatli saqlandi.", "Tasdiq", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private string ReadPasswordFromFile()
        {
            string fileName = "parol.txt";
            string password = "";

            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    password = System.IO.File.ReadAllText(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return password;
        }

        private void WritePasswordToFile(string newPassword)
        {
            string fileName = "parol.txt";

            try
            {
                System.IO.File.WriteAllText(fileName, newPassword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void malumotlarBazasiJoylashuviniKorishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(connectionString, "Ma'lumotlar bazasi joylashuvi");
        }

        private void yangiMalumotlarBazasiYaratishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Papka joylashuvini tanlash
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowser.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                {
                    string folderPath = folderBrowser.SelectedPath;

                    // Ma'lumotlar bazasi nomini so'rash
                    string databaseName = Interaction.InputBox("Ma'lumotlar bazasi nomini kiriting:", "Ma'lumotlar bazasi nomi", "OTMlar");
                    if (string.IsNullOrWhiteSpace(databaseName))
                        return; // Foydalanuvchi nom kiritmagan

                    // Ma'lumotlar bazasini yaratish
                    string databaseFileName = Path.Combine(folderPath, databaseName + ".mdf");
                    createDatabase(databaseFileName);
                }
            }
            Sozlamalar_Load(this, EventArgs.Empty);
        }

        private void createDatabase(string databaseFileName)
        {
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;";
            string databaseName = System.IO.Path.GetFileNameWithoutExtension(databaseFileName);
            string connectionString1 = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // CREATE DATABASE so'rovi
                    string createDatabaseQuery = $"CREATE DATABASE {databaseName} ON PRIMARY (NAME={databaseName}, FILENAME='{databaseFileName}')";
                    SqlCommand command = new SqlCommand(createDatabaseQuery, connection);
                    command.ExecuteNonQuery();

                    //MessageBox.Show("Ma'lumotlar bazasi muvaffaqiyatli yaratildi.");

                    // Ma'lumotlar bazasi yaratildi, endi bog'lanish stringini saqlash
                    connectionString1 = $"Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Initial Catalog={databaseName}";

                    // saveConnectionString funksiyasiga connectionString1 ni yuborish
                    saveConnectionString(connectionString1);

                    createTables(connectionString1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
        }

        private void malumotlarBazasiBilanBoglashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "MDF files (*.mdf)|*.mdf|All files (*.*)|*.*";
            openFileDialog1.Title = "Ma'lumotlar bazasini tanlang";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string databaseFileName = openFileDialog1.FileName;

                // Tanlangan fayl mavjudligini tekshirish
                if (System.IO.File.Exists(databaseFileName))
                {
                    // Ma'lumotlar bazasiga ulanish
                    connectToDatabase(databaseFileName);
                    connectionString = databaseFileName;
                }
                else
                {
                    // Fayl mavjud emas, yangi ma'lumotlar bazasini yaratish
                    DialogResult result = MessageBox.Show("Tanlangan ma'lumotlar bazasi fayli topilmadi. Yangi ma'lumotlar bazasini yaratishni xohlaysizmi?", "Ma'lumotlar bazasi mavjud emas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Trigger creation of a new database
                        yangiMalumotlarBazasiYaratishToolStripMenuItem_Click(sender, e);
                    }
                }
            }
            Sozlamalar_Load(this, EventArgs.Empty);
        }

        private void connectToDatabase(string databaseFileName)
        {
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFileName};Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    saveConnectionString(connectionString);
                    //MessageBox.Show("Ma'lumotlar bazasiga muvaffaqiyatli ulanildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
        }

        private void saveConnectionString(string connectionString)
        {
            string fileName = "connectstring.txt";

            try
            {
                // Faylni yaratish yoki ochish
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                // Faylga connectionString yozish
                sw.WriteLine(connectionString);

                // Resurslarni tozalash
                sw.Close();
                fs.Close();

                //MessageBox.Show("ConnectionString faylga saqlandi: " + fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik yuz berdi: " + ex.Message);
            }
        }

        private void Sozlamalar_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            connectionString = loadConnectionString();

            // Ma'lumotlar bazasiga ulanishni tekshirish
            if (!checkDatabaseConnection(connectionString))
            {
                MessageBox.Show("Ma'lumotlar bazasi joylashuvi aniqlanmadi.\n" +
                        "Ma'lumotlar bazasini qayta bog'lang yoki yarating!",
                        "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //malumotlarBazasiBilanBoglashToolStripMenuItem_Click(sender, e);
            }
            else
            {
                // Bog'lanish muvaffaqiyatli, ehtiyotqismlar jadvalini tekshirish
                if (!checkTablesExistence(connectionString))
                {
                    MessageBox.Show("Ma'lumotlar bazasida 'OTM' yoki 'Yonalishlar' jadvali mavjud emas. Jadvalni yaratilmoqda...");
                    createTables(connectionString);
                }
            }
            connectionString = loadConnectionString();
            addYonalishlar(connectionString);
            addOTM(flowLayoutPanelOTMlar);
        }

        private void addYonalishlar(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Yo'nalishlarni olish uchun so'rov
                    string query = "SELECT Id, Nom FROM Yonalishlar";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    dataGridViewYonalish.DataSource = dataTable;

                    // dataGridViewYonalish ustunlarining kengliklarini sozlash
                    if (dataGridViewYonalish.Columns.Count > 0)
                    {
                        dataGridViewYonalish.Columns["Id"].Width = 100; // Standart kenglik

                        // Nom ustunini qolgan qismini to'ldiradigan qilib kengaytirish
                        dataGridViewYonalish.Columns["Nom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    // zTalim_yonalishlari_ids ni tozalash va yangi yo'nalishlarni qo'shish
                    zTalim_yonalishlari_ids.Items.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        zTalim_yonalishlari_ids.Items.Add(row["Nom"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string loadConnectionString()
        {
            string fileName = "connectstring.txt";
            string connectionString = "";

            try
            {
                // Faylni tekshirish
                if (File.Exists(fileName))
                {
                    // Faylni ochish
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);

                    // Fayldan ConnectionString o'qish
                    connectionString = sr.ReadLine();

                    // Resurslarni tozalash
                    sr.Close();
                    fs.Close();

                    //MessageBox.Show("ConnectionString fayldan yuklandi: " + fileName);
                }
                else
                {
                    MessageBox.Show("Fayl mavjud emas: " + fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik yuz berdi: " + ex.Message);
            }

            return connectionString;
        }

        private bool checkDatabaseConnection(string connectionString)
        {
            bool isConnected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    isConnected = true;
                    //MessageBox.Show("Ma'lumotlar bazasiga muvaffaqiyatli ulanildi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
            }

            return isConnected;
        }

        private bool checkTablesExistence(string connectionString)
        {
            bool tablesExist = false;
            string[] tableNames = { "OTM", "Yonalishlar" };

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (string tableName in tableNames)
                    {
                        // Jadval mavjudligini tekshirish
                        string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                        SqlCommand command = new SqlCommand(query, connection);
                        int count = (int)command.ExecuteScalar();

                        if (count == 0)
                        {
                            tablesExist = false;
                            MessageBox.Show($"Jadval mavjud emas: {tableName}", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return tablesExist;
                        }
                    }

                    tablesExist = true; // Agar barcha jadvallar mavjud bo'lsa
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tablesExist;
        }


        private void createTables(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // OTM (Universities) jadvali yaratish
                    string createUniversitiesTableQuery = @"
CREATE TABLE [dbo].[OTM]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nom] NVARCHAR(100) NOT NULL, 
    [Rasm] VARBINARY(MAX) NULL, 
    [Viloyat] NVARCHAR(100) NULL, 
    [Shahar] NVARCHAR(100) NULL, 
    [Holat] NVARCHAR(50) NULL, 
    [Tashkil_etilgan_yili] INT NULL, 
    [Telefon] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(100) NULL, 
    [Veb_sayti] NVARCHAR(100) NULL, 
    [Oqituvchilar_soni] INT NULL, 
    [Dontsentlar_soni] INT NULL, 
    [Professorlar_soni] INT NULL, 
    [Akademiklar_soni] INT NULL, 
    [Xorijiy_oqituvchilar_soni] INT NULL, 
    [Ilmiy_maqolalar_soni] INT NULL, 
    [Ilmiy_iqtiboslar_soni] INT NULL, 
    [Tadqiqot_mablaglari] FLOAT NULL, 
    [Oqituvchilar_KPI] FLOAT NULL, 
    [Talabalar_soni] INT NULL, 
    [Erkak_talabalar_soni] INT NULL, 
    [Ayol_talabalar_soni] INT NULL, 
    [Xorijiy_talabalar_soni] INT NULL, 
    [Bitiruvchilar_soni] INT NULL, 
    [Ish_bilan_bandlik] FLOAT NULL, 
    [Qarzdor_talabalar_soni] INT NULL, 
    [Umumiy_qarz] FLOAT NULL, 
    [Ortacha_GPA] FLOAT NULL, 
    [Axborot_resurslari_soni] INT NULL, 
    [Oquv_bino_sigimi] INT NULL, 
    [Yotoqxona_sigimi] INT NULL, 
    [Ortacha_stipendiya] FLOAT NULL, 
    [Ortacha_kontrakt_tolovi] FLOAT NULL, 
    [Yotoqxona_tolovi] FLOAT NULL, 
    [Ajratilgan_joylar] INT NULL, 
    [Grant_joylar] INT NULL, 
    [Kontrakt_joylar] INT NULL, 
    [Fakultetlar_soni] INT NULL, 
    [Talim_yonalishlari_ids] NVARCHAR(MAX) NULL
)";

                    SqlCommand createUniversitiesCommand = new SqlCommand(createUniversitiesTableQuery, connection);
                    createUniversitiesCommand.ExecuteNonQuery();

                    // Yonalishlar (Programs) jadvali yaratish
                    string createProgramsTableQuery = @"
CREATE TABLE [dbo].[Yonalishlar]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nom] NVARCHAR(100) NOT NULL
)";
                    SqlCommand createProgramsCommand = new SqlCommand(createProgramsTableQuery, connection);
                    createProgramsCommand.ExecuteNonQuery();

                    MessageBox.Show("OTM va Yonalishlar jadvallari muvaffaqiyatli yaratildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
        }

        private void addOTM(Panel OTMlar_panel)
        {
            try
            {
                // Ma'lumotlar bazasidagi ma'lumotlarni panelga joylash
                OTMlar_panel.Controls.Clear(); // Obyektlarni tozalash

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "SELECT * FROM OTM"; // Barcha ma'lumotlarni olish so'rovi
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            // Har bir ma'lumot uchun panelga yangi obyekt qo'shish
                            string id = reader["Id"].ToString();
                            string nom = reader["Nom"].ToString();
                            byte[] imageByte = (byte[])reader["Rasm"]; // "Rasm" ustunining byte[] qiymatini olamiz
                            System.Drawing.Image rasm = ByteArrayToImage(imageByte); // ByteArrayToImage funktsiyasini chaqiramiz

                            Panel panelOTM = new System.Windows.Forms.Panel();
                            PictureBox picRasm = new System.Windows.Forms.PictureBox();
                            Label labelNom = new System.Windows.Forms.Label();
                            Label labelID = new System.Windows.Forms.Label();

                            // 
                            // panelOTM
                            // 
                            panelOTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                            panelOTM.Controls.Add(picRasm);
                            panelOTM.Controls.Add(labelNom);
                            panelOTM.Controls.Add(labelID);
                            panelOTM.Location = new System.Drawing.Point(409, 148);
                            panelOTM.Name = "panelOTM";
                            panelOTM.Size = new System.Drawing.Size(270, 270);
                            panelOTM.TabIndex = 1;
                            panelOTM.BorderStyle = BorderStyle.Fixed3D;
                            // 
                            // picRasm
                            // 
                            picRasm.Dock = System.Windows.Forms.DockStyle.Fill;
                            picRasm.Location = new System.Drawing.Point(0, 86);
                            picRasm.Name = "picRasm";
                            picRasm.Size = new System.Drawing.Size(270, 184);
                            picRasm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                            picRasm.TabIndex = 1;
                            picRasm.TabStop = false;
                            picRasm.Image = rasm;
                            // 
                            // labelNom
                            // 
                            labelNom.Dock = System.Windows.Forms.DockStyle.Top;
                            labelNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            labelNom.ForeColor = System.Drawing.Color.White;
                            labelNom.Location = new System.Drawing.Point(0, 26);
                            labelNom.Name = "labelNom";
                            labelNom.Size = new System.Drawing.Size(270, 60);
                            labelNom.TabIndex = 2;
                            labelNom.Text = nom;
                            labelNom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                            // 
                            // labelID
                            // 
                            labelID.Dock = System.Windows.Forms.DockStyle.Top;
                            labelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                            labelID.ForeColor = System.Drawing.Color.White;
                            labelID.Location = new System.Drawing.Point(0, 0);
                            labelID.Name = "labelID";
                            labelID.Size = new System.Drawing.Size(270, 26);
                            labelID.TabIndex = 0;
                            labelID.Text = "ID: " + id;
                            labelID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                            foreach (Control control in panelOTM.Controls)
                            {
                                // Har bir kontrol uchun click hodisasini qo'shish
                                control.Click += (sender, e) =>
                                {
                                    OTMClick(id);
                                };
                            }

                            OTMlar_panel.Controls.Add(panelOTM);
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xatolik: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
            }
        }

        private System.Drawing.Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);
                return image;
            }
        }

        private void OTMClick(string textId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Ma'lumotlarni olish uchun SQL so'rov
                    string selectQuery = "SELECT * FROM OTM WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@Id", textId);
                    selectId = textId;

                    // Ma'lumotlarni olish
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Ma'lumotlarni olish
                        nom = reader["Nom"].ToString();
                        byte[] imageByte1 = (byte[])reader["Rasm"];
                        viloyat = reader["Viloyat"].ToString();
                        shahar = reader["Shahar"].ToString();
                        holat = reader["Holat"].ToString();
                        tashkil_etilgan_yili = reader["Tashkil_etilgan_yili"].ToString();
                        telefon = reader["Telefon"].ToString();
                        email = reader["Email"].ToString();
                        veb_sayti = reader["Veb_sayti"].ToString();
                        oqituvchilar_soni = reader["Oqituvchilar_soni"].ToString();
                        dontsentlar_soni = reader["Dontsentlar_soni"].ToString();
                        professorlar_soni = reader["Professorlar_soni"].ToString();
                        akademiklar_soni = reader["Akademiklar_soni"].ToString();
                        xorijiy_oqituvchilar_soni = reader["Xorijiy_oqituvchilar_soni"].ToString();
                        ilmiy_maqolalar_soni = reader["Ilmiy_maqolalar_soni"].ToString();
                        ilmiy_iqtiboslar_soni = reader["Ilmiy_iqtiboslar_soni"].ToString();
                        tadqiqot_mablaglari = reader["Tadqiqot_mablaglari"].ToString();
                        oqituvchilar_KPI = reader["Oqituvchilar_KPI"].ToString();
                        talabalar_soni = reader["Talabalar_soni"].ToString();
                        erkak_talabalar_soni = reader["Erkak_talabalar_soni"].ToString();
                        ayol_talabalar_soni = reader["Ayol_talabalar_soni"].ToString();
                        xorijiy_talabalar_soni = reader["Xorijiy_talabalar_soni"].ToString();
                        bitiruvchilar_soni = reader["Bitiruvchilar_soni"].ToString();
                        ish_bilan_bandlik = reader["Ish_bilan_bandlik"].ToString();
                        qarzdor_talabalar_soni = reader["Qarzdor_talabalar_soni"].ToString();
                        umumiy_qarz = reader["Umumiy_qarz"].ToString();
                        ortacha_GPA = reader["Ortacha_GPA"].ToString();
                        axborot_resurslari_soni = reader["Axborot_resurslari_soni"].ToString();
                        oquv_bino_sigimi = reader["Oquv_bino_sigimi"].ToString();
                        yotoqxona_sigimi = reader["Yotoqxona_sigimi"].ToString();
                        ortacha_stipendiya = reader["Ortacha_stipendiya"].ToString();
                        ortacha_kontrakt_tolovi = reader["Ortacha_kontrakt_tolovi"].ToString();
                        yotoqxona_tolovi = reader["Yotoqxona_tolovi"].ToString();
                        ajratilgan_joylar = reader["Ajratilgan_joylar"].ToString();
                        grant_joylar = reader["Grant_joylar"].ToString();
                        kontrakt_joylar = reader["Kontrakt_joylar"].ToString();
                        fakultetlar_soni = reader["Fakultetlar_soni"].ToString();
                        talim_yonalishlari_ids = reader["Talim_yonalishlari_ids"].ToString();

                        zId.Text = "ID: " + textId;
                        zNom.Text = nom;
                        zRasm.Image = ByteArrayToImage(imageByte1);
                        zViloyat.Text = viloyat;
                        zShahar.Text = shahar;
                        zHolat.Text = holat;
                        zTashkil_etilgan_yili.Value = Convert.ToInt32(tashkil_etilgan_yili);
                        zTelefon.Text = telefon;
                        zEmail.Text = email;
                        zVeb_sayti.Text = veb_sayti;
                        zOqituvchilar_soni.Value = Convert.ToInt32(oqituvchilar_soni);
                        zDontsentlar_soni.Value = Convert.ToInt32(dontsentlar_soni);
                        zProfessorlar_soni.Value = Convert.ToInt32(professorlar_soni);
                        zAkademiklar_soni.Value = Convert.ToInt32(akademiklar_soni);
                        zXorijiy_oqituvchilar_soni.Value = Convert.ToInt32(xorijiy_oqituvchilar_soni);
                        zIlmiy_maqolalar_soni.Value = Convert.ToInt32(ilmiy_maqolalar_soni);
                        zIlmiy_iqtiboslar_soni.Value = Convert.ToInt32(ilmiy_iqtiboslar_soni);
                        zTadqiqot_mablaglari.Value = Convert.ToDecimal(tadqiqot_mablaglari);
                        zOqituvchilar_KPI.Value = Convert.ToDecimal(oqituvchilar_KPI);
                        zTalabalar_soni.Value = Convert.ToInt32(talabalar_soni);
                        zErkak_talabalar_soni.Value = Convert.ToInt32(erkak_talabalar_soni);
                        zAyol_talabalar_soni.Value = Convert.ToInt32(ayol_talabalar_soni);
                        zXorijiy_talabalar_soni.Value = Convert.ToInt32(xorijiy_talabalar_soni);
                        zBitiruvchilar_soni.Value = Convert.ToInt32(bitiruvchilar_soni);
                        zIsh_bilan_bandlik.Value = Convert.ToDecimal(ish_bilan_bandlik);
                        zQarzdor_talabalar_soni.Value = Convert.ToInt32(qarzdor_talabalar_soni);
                        zUmumiy_qarz.Value = Convert.ToDecimal(umumiy_qarz);
                        zOrtacha_GPA.Value = Convert.ToDecimal(ortacha_GPA);
                        zAxborot_resurslari_soni.Value = Convert.ToInt32(axborot_resurslari_soni);
                        zOquv_bino_sigimi.Value = Convert.ToInt32(oquv_bino_sigimi);
                        zYotoqxona_sigimi.Value = Convert.ToInt32(yotoqxona_sigimi);
                        zOrtacha_stipendiya.Value = Convert.ToDecimal(ortacha_stipendiya);
                        zOrtacha_kontrakt_tolovi.Value = Convert.ToDecimal(ortacha_kontrakt_tolovi);
                        zYotoqxona_tolovi.Value = Convert.ToDecimal(yotoqxona_tolovi);
                        zAjratilgan_joylar.Value = Convert.ToInt32(ajratilgan_joylar);
                        zGrant_joylar.Value = Convert.ToInt32(grant_joylar);
                        zKontrakt_joylar.Value = Convert.ToInt32(kontrakt_joylar);
                        zFakultetlar_soni.Value = Convert.ToInt32(fakultetlar_soni);
                        SetYonalishNamesFromIds(talim_yonalishlari_ids);
                    }
                    else
                    {
                        MessageBox.Show("Ma'lumot topilmadi.", "Xatolik");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message, "Xatolik");
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string yonalishNomi = textBoxYonalish.Text.Trim();

            if (string.IsNullOrEmpty(yonalishNomi))
            {
                MessageBox.Show("Iltimos, yo'nalish nomini kiriting.", "Ogohlantirish", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Yo'nalish nomi mavjudligini tekshirish uchun SELECT so'rovi
                    string checkQuery = "SELECT Id FROM Yonalishlar WHERE Nom = @Nom";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@Nom", yonalishNomi);

                    object result = checkCommand.ExecuteScalar();

                    if (result != null)
                    {
                        int existingId = (int)result;
                        MessageBox.Show($"Ushbu yo'nalish nomi allaqachon mavjud. ID: {existingId}", "Ogohlantirish", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Yo'nalishni qo'shish uchun INSERT so'rovi
                        string insertQuery = "INSERT INTO Yonalishlar (Nom) VALUES (@Nom)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@Nom", yonalishNomi);
                        insertCommand.ExecuteNonQuery();

                        MessageBox.Show("Yo'nalish muvaffaqiyatli qo'shildi.", "Ma'lumot", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Yangi yo'nalishni qo'shgandan keyin jadvalni yangilash
                        toolStripMenuItem2_Click(null, EventArgs.Empty);
                        Sozlamalar_Load(this, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewYonalish_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewYonalish.Rows[e.RowIndex];
                labelYonalishID.Text = "ID: " + row.Cells["Id"].Value.ToString();
                textBoxYonalish.Text = row.Cells["Nom"].Value.ToString();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            labelYonalishID.Text = "ID: ";
            textBoxYonalish.Clear();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                // Id ni labelYonalishID dan olish
                if (labelYonalishID.Text.StartsWith("ID: "))
                {
                    string idText = labelYonalishID.Text.Substring(4);
                    if (int.TryParse(idText, out int id))
                    {
                        string yangiNom = textBoxYonalish.Text;

                        // Ma'lumotlar bazasida yo'nalish nomini yangilash uchun UPDATE so'rovi
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string query = "UPDATE Yonalishlar SET Nom = @Nom WHERE Id = @Id";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@Nom", yangiNom);
                            command.Parameters.AddWithValue("@Id", id);
                            command.ExecuteNonQuery();

                            MessageBox.Show("Yo'nalish nomi muvaffaqiyatli yangilandi.", "Ma'lumot", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Yangilangan yo'nalishni qo'shgandan keyin jadvalni yangilash
                            toolStripMenuItem2_Click(null, EventArgs.Empty);
                            Sozlamalar_Load(this, EventArgs.Empty);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Noto'g'ri ID format.", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Yo'nalish ID topilmadi.", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            // Yo'nalish ID ni olish
            string idText = labelYonalishID.Text.Replace("ID: ", "").Trim();
            if (!int.TryParse(idText, out int yonalishId))
            {
                MessageBox.Show("Iltimos, to'g'ri yo'nalish ID ni tanlang.", "Ogohlantirish", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yo'nalish nomini olish
            string yonalishNomi = textBoxYonalish.Text.Trim();
            if (string.IsNullOrEmpty(yonalishNomi))
            {
                MessageBox.Show("Iltimos, yo'nalish nomini kiriting.", "Ogohlantirish", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yo'nalishni o'chirishni tasdiqlash uchun xabar ko'rsatish
            DialogResult result = MessageBox.Show("Haqiqatan ham bu yo'nalishni o'chirmoqchimisiz?" +
                "\n" + labelYonalishID.Text + "\nYonalish: " + yonalishNomi,
                "Yo'nalishni o'chirish", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Yo'nalishni o'chirish uchun DELETE so'rovi
                        string deleteQuery = "DELETE FROM Yonalishlar WHERE Id = @Id AND Nom = @Nom";
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@Id", yonalishId);
                        deleteCommand.Parameters.AddWithValue("@Nom", yonalishNomi);

                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Yo'nalish muvaffaqiyatli o'chirildi.", "Ma'lumot", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Yonalish o'chirildikdan keyin labelYonalishID va textBoxYonalish tozalash
                            labelYonalishID.Text = "ID: ";
                            textBoxYonalish.Clear();

                            // Jadvalni yangilash
                            toolStripMenuItem2_Click(null, EventArgs.Empty);
                            Sozlamalar_Load(this, EventArgs.Empty);
                        }
                        else
                        {
                            MessageBox.Show("Yo'nalishni o'chirishda xatolik yuz berdi. Iltimos, qayta urinib ko'ring.", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Yo'nalish o'chirish bekor qilindi.", "Bekor qilindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void zRasm_DoubleClick(object sender, EventArgs e)
        {
            // OpenFileDialog obyekti yaratish
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Fayllarni filtrlash uchun parametrlar
            openFileDialog.Filter = "Rasm fayllari|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            // Dialog sarlavhasini belgilash
            openFileDialog.Title = "Rasm faylini tanlang";

            // Faylni tanlash va foydalanuvchi faylni tanlaganini tekshirish
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Tanlangan faylning manzili
                    string rasmManzili = openFileDialog.FileName;

                    // Tanlangan fayl rasm fayli ekanligini tekshirish
                    if (RasmFayliEkanliginiTekshir(rasmManzili))
                    {
                        // Faylni yuklash
                        System.Drawing.Image tanlanganRasm = System.Drawing.Image.FromFile(rasmManzili);

                        // PictureBoxning Image xususiyatiga tanlangan rasmni joylash
                        zRasm.Image = tanlanganRasm;
                    }
                    else
                    {
                        MessageBox.Show("Iltimos, mavjud rasm formatida fayl tanlang.", "Noto'g'ri Fayl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool RasmFayliEkanliginiTekshir(string faylManzili)
        {
            string kengaytma = Path.GetExtension(faylManzili)?.ToLower();
            return kengaytma == ".jpg" || kengaytma == ".jpeg" || kengaytma == ".png" || kengaytma == ".gif" || kengaytma == ".bmp";
        }

        //OTM add
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            // Barcha ma'lumotlarni yig'ish
            nom = zNom.Text;
            if (zRasm.Image == null)
            {
                MessageBox.Show("Rasm kiritilmagan!", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            rasm = zRasm.Image;
            byte[] imageBytes = ImageToByteArray(rasm);
            viloyat = zViloyat.Text;
            shahar = zShahar.Text;
            holat = zHolat.Text;
            tashkil_etilgan_yili = zTashkil_etilgan_yili.Value.ToString();
            telefon = zTelefon.Text;
            email = zEmail.Text;
            veb_sayti = zVeb_sayti.Text;
            oqituvchilar_soni = zOqituvchilar_soni.Value.ToString();
            dontsentlar_soni = zDontsentlar_soni.Value.ToString();
            professorlar_soni = zProfessorlar_soni.Value.ToString();
            akademiklar_soni = zAkademiklar_soni.Value.ToString();
            xorijiy_oqituvchilar_soni = zXorijiy_oqituvchilar_soni.Value.ToString();
            ilmiy_maqolalar_soni = zIlmiy_maqolalar_soni.Value.ToString();
            ilmiy_iqtiboslar_soni = zIlmiy_iqtiboslar_soni.Value.ToString();
            tadqiqot_mablaglari = zTadqiqot_mablaglari.Value.ToString();
            oqituvchilar_KPI = zOqituvchilar_KPI.Value.ToString();
            talabalar_soni = zTalabalar_soni.Value.ToString();
            erkak_talabalar_soni = zErkak_talabalar_soni.Value.ToString();
            ayol_talabalar_soni = zAyol_talabalar_soni.Value.ToString();
            xorijiy_talabalar_soni = zXorijiy_talabalar_soni.Value.ToString();
            bitiruvchilar_soni = zBitiruvchilar_soni.Value.ToString();
            ish_bilan_bandlik = zIsh_bilan_bandlik.Value.ToString();
            qarzdor_talabalar_soni = zQarzdor_talabalar_soni.Value.ToString();
            umumiy_qarz = zUmumiy_qarz.Value.ToString();
            ortacha_GPA = zOrtacha_GPA.Value.ToString();
            axborot_resurslari_soni = zAxborot_resurslari_soni.Value.ToString();
            oquv_bino_sigimi = zOquv_bino_sigimi.Value.ToString();
            yotoqxona_sigimi = zYotoqxona_sigimi.Value.ToString();
            ortacha_stipendiya = zOrtacha_stipendiya.Value.ToString();
            ortacha_kontrakt_tolovi = zOrtacha_kontrakt_tolovi.Value.ToString();
            yotoqxona_tolovi = zYotoqxona_tolovi.Value.ToString();
            ajratilgan_joylar = zAjratilgan_joylar.Value.ToString();
            grant_joylar = zGrant_joylar.Value.ToString();
            kontrakt_joylar = zKontrakt_joylar.Value.ToString();
            fakultetlar_soni = zFakultetlar_soni.Value.ToString();
            talim_yonalishlari_ids = GetYonalishIdsFromNames();


            // Bo'sh maydonlarni tekshirish
            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(viloyat) || string.IsNullOrEmpty(shahar) || string.IsNullOrEmpty(holat) || string.IsNullOrEmpty(telefon) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(veb_sayti))
            {
                MessageBox.Show("Barcha maydonlarni to'ldiring va kamida bitta yo'nalishni tanlang!", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (zTalim_yonalishlari_ids.CheckedItems.Count < 1)
            {
                MessageBox.Show("Iltimos, kamida bitta ta'lim yo'nalish tanlang.", "Ogohlantirish", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // OTM jadvaliga ma'lumot qo'shish
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO OTM (Nom, Rasm, Viloyat, Shahar, Holat, Tashkil_etilgan_yili, Telefon, Email, Veb_sayti, Oqituvchilar_soni, Dontsentlar_soni, Professorlar_soni, Akademiklar_soni, Xorijiy_oqituvchilar_soni, Ilmiy_maqolalar_soni, Ilmiy_iqtiboslar_soni, Tadqiqot_mablaglari, Oqituvchilar_KPI, Talabalar_soni, Erkak_talabalar_soni, Ayol_talabalar_soni, Xorijiy_talabalar_soni, Bitiruvchilar_soni, Ish_bilan_bandlik, Qarzdor_talabalar_soni, Umumiy_qarz, Ortacha_GPA, Axborot_resurslari_soni, Oquv_bino_sigimi, Yotoqxona_sigimi, Ortacha_stipendiya, Ortacha_kontrakt_tolovi, Yotoqxona_tolovi, Ajratilgan_joylar, Grant_joylar, Kontrakt_joylar, Fakultetlar_soni, Talim_yonalishlari_ids) " +
                        "VALUES (@Nom, @Rasm, @Viloyat, @Shahar, @Holat, @Tashkil_etilgan_yili, @Telefon, @Email, @Veb_sayti, @Oqituvchilar_soni, @Dontsentlar_soni, @Professorlar_soni, @Akademiklar_soni, @Xorijiy_oqituvchilar_soni, @Ilmiy_maqolalar_soni, @Ilmiy_iqtiboslar_soni, @Tadqiqot_mablaglari, @Oqituvchilar_KPI, @Talabalar_soni, @Erkak_talabalar_soni, @Ayol_talabalar_soni, @Xorijiy_talabalar_soni, @Bitiruvchilar_soni, @Ish_bilan_bandlik, @Qarzdor_talabalar_soni, @Umumiy_qarz, @Ortacha_GPA, @Axborot_resurslari_soni, @Oquv_bino_sigimi, @Yotoqxona_sigimi, @Ortacha_stipendiya, @Ortacha_kontrakt_tolovi, @Yotoqxona_tolovi, @Ajratilgan_joylar, @Grant_joylar, @Kontrakt_joylar, @Fakultetlar_soni, @Talim_yonalishlari_ids)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nom", nom);
                    command.Parameters.AddWithValue("@Rasm", imageBytes);
                    command.Parameters.AddWithValue("@Viloyat", viloyat);
                    command.Parameters.AddWithValue("@Shahar", shahar);
                    command.Parameters.AddWithValue("@Holat", holat);
                    command.Parameters.AddWithValue("@Tashkil_etilgan_yili", tashkil_etilgan_yili);
                    command.Parameters.AddWithValue("@Telefon", telefon);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Veb_sayti", veb_sayti);
                    command.Parameters.AddWithValue("@Oqituvchilar_soni", oqituvchilar_soni);
                    command.Parameters.AddWithValue("@Dontsentlar_soni", dontsentlar_soni);
                    command.Parameters.AddWithValue("@Professorlar_soni", professorlar_soni);
                    command.Parameters.AddWithValue("@Akademiklar_soni", akademiklar_soni);
                    command.Parameters.AddWithValue("@Xorijiy_oqituvchilar_soni", xorijiy_oqituvchilar_soni);
                    command.Parameters.AddWithValue("@Ilmiy_maqolalar_soni", ilmiy_maqolalar_soni);
                    command.Parameters.AddWithValue("@Ilmiy_iqtiboslar_soni", ilmiy_iqtiboslar_soni);
                    command.Parameters.AddWithValue("@Tadqiqot_mablaglari", tadqiqot_mablaglari);
                    command.Parameters.AddWithValue("@Oqituvchilar_KPI", oqituvchilar_KPI);
                    command.Parameters.AddWithValue("@Talabalar_soni", talabalar_soni);
                    command.Parameters.AddWithValue("@Erkak_talabalar_soni", erkak_talabalar_soni);
                    command.Parameters.AddWithValue("@Ayol_talabalar_soni", ayol_talabalar_soni);
                    command.Parameters.AddWithValue("@Xorijiy_talabalar_soni", xorijiy_talabalar_soni);
                    command.Parameters.AddWithValue("@Bitiruvchilar_soni", bitiruvchilar_soni);
                    command.Parameters.AddWithValue("@Ish_bilan_bandlik", ish_bilan_bandlik);
                    command.Parameters.AddWithValue("@Qarzdor_talabalar_soni", qarzdor_talabalar_soni);
                    command.Parameters.AddWithValue("@Umumiy_qarz", umumiy_qarz);
                    command.Parameters.AddWithValue("@Ortacha_GPA", ortacha_GPA);
                    command.Parameters.AddWithValue("@Axborot_resurslari_soni", axborot_resurslari_soni);
                    command.Parameters.AddWithValue("@Oquv_bino_sigimi", oquv_bino_sigimi);
                    command.Parameters.AddWithValue("@Yotoqxona_sigimi", yotoqxona_sigimi);
                    command.Parameters.AddWithValue("@Ortacha_stipendiya", ortacha_stipendiya);
                    command.Parameters.AddWithValue("@Ortacha_kontrakt_tolovi", ortacha_kontrakt_tolovi);
                    command.Parameters.AddWithValue("@Yotoqxona_tolovi", yotoqxona_tolovi);
                    command.Parameters.AddWithValue("@Ajratilgan_joylar", ajratilgan_joylar);
                    command.Parameters.AddWithValue("@Grant_joylar", grant_joylar);
                    command.Parameters.AddWithValue("@Kontrakt_joylar", kontrakt_joylar);
                    command.Parameters.AddWithValue("@Fakultetlar_soni", fakultetlar_soni);
                    command.Parameters.AddWithValue("@Talim_yonalishlari_ids", talim_yonalishlari_ids);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Ma'lumotlar muvaffaqiyatli qo'shildi!", "Muvaffaqiyat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ma'lumotlarni qo'shishda xatolik yuz berdi: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            toolStripMenuItem6_Click(null, EventArgs.Empty);
            Sozlamalar_Load(this, EventArgs.Empty);
        }

        private byte[] ImageToByteArray(System.Drawing.Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }

        private string GetYonalishIdsFromNames()
        {
            StringBuilder yonalishIds = new StringBuilder();

            foreach (string yonalishNom in zTalim_yonalishlari_ids.CheckedItems)
            {
                // Ta'lim yo'nalishlar jadvalidan yonalish nomiga mos keluvchi yonalish ID sini olish
                string query = "SELECT Id FROM Yonalishlar WHERE Nom = @Nom";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nom", yonalishNom);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        int yonalishId = (int)result;
                        yonalishIds.Append(yonalishId);
                        yonalishIds.Append(",");
                    }
                }
            }

            // Vergul bilan ajratilgan yonalish ID larining oxiridagi vergulni olib tashlash
            if (yonalishIds.Length > 0)
            {
                yonalishIds.Remove(yonalishIds.Length - 1, 1); // Oxiridagi vergulni olib tashlash
            }

            return yonalishIds.ToString();
        }

        private void SetYonalishNamesFromIds(string talimYonalishlariIds)
        {
            // zTalim_yonalishlari_ids ni tozalash
            for (int i = 0; i < zTalim_yonalishlari_ids.Items.Count; i++)
            {
                zTalim_yonalishlari_ids.SetItemChecked(i, false);
            }

            // Id larni vergul bilan ajratib olish
            string[] ids = talimYonalishlariIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                // Ta'lim yo'nalishlar jadvalidan ID ga mos keluvchi yonalish nomini olish
                string query = "SELECT Nom FROM Yonalishlar WHERE Id = @Id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string yonalishNom = result.ToString();

                        // Find the index of the item in zTalim_yonalishlari_ids that matches the name
                        for (int i = 0; i < zTalim_yonalishlari_ids.Items.Count; i++)
                        {
                            if (zTalim_yonalishlari_ids.Items[i].ToString() == yonalishNom)
                            {
                                zTalim_yonalishlari_ids.SetItemChecked(i, true);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            zId.Text = "ID: ";
            zNom.Clear();
            zRasm.Image = null;
            zViloyat.SelectedItem = null;
            zShahar.Clear();
            zHolat.SelectedItem = null;
            zTashkil_etilgan_yili.Value = 2000;
            zTelefon.Clear();
            zEmail.Clear();
            zVeb_sayti.Clear();
            zOqituvchilar_soni.Value = 0;
            zDontsentlar_soni.Value = 0;
            zProfessorlar_soni.Value = 0;
            zAkademiklar_soni.Value = 0;
            zXorijiy_oqituvchilar_soni.Value = 0;
            zIlmiy_maqolalar_soni.Value = 0;
            zIlmiy_iqtiboslar_soni.Value = 0;
            zTadqiqot_mablaglari.Value = 0;
            zOqituvchilar_KPI.Value = 0;
            zTalabalar_soni.Value = 0;
            zErkak_talabalar_soni.Value = 0;
            zAyol_talabalar_soni.Value = 0;
            zXorijiy_talabalar_soni.Value = 0;
            zBitiruvchilar_soni.Value = 0;
            zIsh_bilan_bandlik.Value = 0;
            zQarzdor_talabalar_soni.Value = 0;
            zUmumiy_qarz.Value = 0;
            zOrtacha_GPA.Value = 0;
            zAxborot_resurslari_soni.Value = 0;
            zOquv_bino_sigimi.Value = 0;
            zYotoqxona_sigimi.Value = 0;
            zOrtacha_stipendiya.Value = 0;
            zOrtacha_kontrakt_tolovi.Value = 0;
            zYotoqxona_tolovi.Value = 0;
            zAjratilgan_joylar.Value = 0;
            zGrant_joylar.Value = 0;
            zKontrakt_joylar.Value = 0;
            zFakultetlar_soni.Value = 0;
            for (int i = 0; i < zTalim_yonalishlari_ids.Items.Count; i++)
            {
                zTalim_yonalishlari_ids.SetItemChecked(i, false);
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (zId.Text == "ID: ")
            {
                MessageBox.Show("Iltimos, birorta mahsulot tanlang!");
                return;
            }

            nom = zNom.Text;
            if (zRasm.Image == null)
            {
                MessageBox.Show("Rasm kiritilmagan!", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            rasm = zRasm.Image;
            byte[] imageBytes = ImageToByteArray(rasm);
            viloyat = zViloyat.Text;
            shahar = zShahar.Text;
            holat = zHolat.Text;
            tashkil_etilgan_yili = zTashkil_etilgan_yili.Value.ToString();
            telefon = zTelefon.Text;
            email = zEmail.Text;
            veb_sayti = zVeb_sayti.Text;
            oqituvchilar_soni = zOqituvchilar_soni.Value.ToString();
            dontsentlar_soni = zDontsentlar_soni.Value.ToString();
            professorlar_soni = zProfessorlar_soni.Value.ToString();
            akademiklar_soni = zAkademiklar_soni.Value.ToString();
            xorijiy_oqituvchilar_soni = zXorijiy_oqituvchilar_soni.Value.ToString();
            ilmiy_maqolalar_soni = zIlmiy_maqolalar_soni.Value.ToString();
            ilmiy_iqtiboslar_soni = zIlmiy_iqtiboslar_soni.Value.ToString();
            tadqiqot_mablaglari = zTadqiqot_mablaglari.Value.ToString();
            oqituvchilar_KPI = zOqituvchilar_KPI.Value.ToString();
            talabalar_soni = zTalabalar_soni.Value.ToString();
            erkak_talabalar_soni = zErkak_talabalar_soni.Value.ToString();
            ayol_talabalar_soni = zAyol_talabalar_soni.Value.ToString();
            xorijiy_talabalar_soni = zXorijiy_talabalar_soni.Value.ToString();
            bitiruvchilar_soni = zBitiruvchilar_soni.Value.ToString();
            ish_bilan_bandlik = zIsh_bilan_bandlik.Value.ToString();
            qarzdor_talabalar_soni = zQarzdor_talabalar_soni.Value.ToString();
            umumiy_qarz = zUmumiy_qarz.Value.ToString();
            ortacha_GPA = zOrtacha_GPA.Value.ToString();
            axborot_resurslari_soni = zAxborot_resurslari_soni.Value.ToString();
            oquv_bino_sigimi = zOquv_bino_sigimi.Value.ToString();
            yotoqxona_sigimi = zYotoqxona_sigimi.Value.ToString();
            ortacha_stipendiya = zOrtacha_stipendiya.Value.ToString();
            ortacha_kontrakt_tolovi = zOrtacha_kontrakt_tolovi.Value.ToString();
            yotoqxona_tolovi = zYotoqxona_tolovi.Value.ToString();
            ajratilgan_joylar = zAjratilgan_joylar.Value.ToString();
            grant_joylar = zGrant_joylar.Value.ToString();
            kontrakt_joylar = zKontrakt_joylar.Value.ToString();
            fakultetlar_soni = zFakultetlar_soni.Value.ToString();
            talim_yonalishlari_ids = GetYonalishIdsFromNames();

            // Bo'sh maydonlarni tekshirish
            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(viloyat) || string.IsNullOrEmpty(shahar) || string.IsNullOrEmpty(holat) || string.IsNullOrEmpty(telefon) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(veb_sayti))
            {
                MessageBox.Show("Barcha maydonlarni to'ldiring va kamida bitta yo'nalishni tanlang!", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (zTalim_yonalishlari_ids.CheckedItems.Count < 1)
            {
                MessageBox.Show("Iltimos, kamida bitta ta'lim yo'nalish tanlang.", "Ogohlantirish", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // OTM jadvaliga ma'lumot yangilash
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE OTM SET Nom = @Nom, Rasm = @Rasm, Viloyat = @Viloyat, Shahar = @Shahar, Holat = @Holat, Tashkil_etilgan_yili = @Tashkil_etilgan_yili, Telefon = @Telefon, Email = @Email, Veb_sayti = @Veb_sayti, Oqituvchilar_soni = @Oqituvchilar_soni, Dontsentlar_soni = @Dontsentlar_soni, Professorlar_soni = @Professorlar_soni, Akademiklar_soni = @Akademiklar_soni, Xorijiy_oqituvchilar_soni = @Xorijiy_oqituvchilar_soni, Ilmiy_maqolalar_soni = @Ilmiy_maqolalar_soni, Ilmiy_iqtiboslar_soni = @Ilmiy_iqtiboslar_soni, Tadqiqot_mablaglari = @Tadqiqot_mablaglari, Oqituvchilar_KPI = @Oqituvchilar_KPI, Talabalar_soni = @Talabalar_soni, Erkak_talabalar_soni = @Erkak_talabalar_soni, Ayol_talabalar_soni = @Ayol_talabalar_soni, Xorijiy_talabalar_soni = @Xorijiy_talabalar_soni, Bitiruvchilar_soni = @Bitiruvchilar_soni, Ish_bilan_bandlik = @Ish_bilan_bandlik, Qarzdor_talabalar_soni = @Qarzdor_talabalar_soni, Umumiy_qarz = @Umumiy_qarz, Ortacha_GPA = @Ortacha_GPA, Axborot_resurslari_soni = @Axborot_resurslari_soni, Oquv_bino_sigimi = @Oquv_bino_sigimi, Yotoqxona_sigimi = @Yotoqxona_sigimi, Ortacha_stipendiya = @Ortacha_stipendiya, Ortacha_kontrakt_tolovi = @Ortacha_kontrakt_tolovi, Yotoqxona_tolovi = @Yotoqxona_tolovi, Ajratilgan_joylar = @Ajratilgan_joylar, Grant_joylar = @Grant_joylar, Kontrakt_joylar = @Kontrakt_joylar, Fakultetlar_soni = @Fakultetlar_soni, Talim_yonalishlari_ids = @Talim_yonalishlari_ids WHERE Id = @Id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", selectId);
                    command.Parameters.AddWithValue("@Nom", nom);
                    command.Parameters.AddWithValue("@Rasm", imageBytes);
                    command.Parameters.AddWithValue("@Viloyat", viloyat);
                    command.Parameters.AddWithValue("@Shahar", shahar);
                    command.Parameters.AddWithValue("@Holat", holat);
                    command.Parameters.AddWithValue("@Tashkil_etilgan_yili", tashkil_etilgan_yili);
                    command.Parameters.AddWithValue("@Telefon", telefon);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Veb_sayti", veb_sayti);
                    command.Parameters.AddWithValue("@Oqituvchilar_soni", oqituvchilar_soni);
                    command.Parameters.AddWithValue("@Dontsentlar_soni", dontsentlar_soni);
                    command.Parameters.AddWithValue("@Professorlar_soni", professorlar_soni);
                    command.Parameters.AddWithValue("@Akademiklar_soni", akademiklar_soni);
                    command.Parameters.AddWithValue("@Xorijiy_oqituvchilar_soni", xorijiy_oqituvchilar_soni);
                    command.Parameters.AddWithValue("@Ilmiy_maqolalar_soni", ilmiy_maqolalar_soni);
                    command.Parameters.AddWithValue("@Ilmiy_iqtiboslar_soni", ilmiy_iqtiboslar_soni);
                    command.Parameters.AddWithValue("@Tadqiqot_mablaglari", tadqiqot_mablaglari);
                    command.Parameters.AddWithValue("@Oqituvchilar_KPI", oqituvchilar_KPI);
                    command.Parameters.AddWithValue("@Talabalar_soni", talabalar_soni);
                    command.Parameters.AddWithValue("@Erkak_talabalar_soni", erkak_talabalar_soni);
                    command.Parameters.AddWithValue("@Ayol_talabalar_soni", ayol_talabalar_soni);
                    command.Parameters.AddWithValue("@Xorijiy_talabalar_soni", xorijiy_talabalar_soni);
                    command.Parameters.AddWithValue("@Bitiruvchilar_soni", bitiruvchilar_soni);
                    command.Parameters.AddWithValue("@Ish_bilan_bandlik", ish_bilan_bandlik);
                    command.Parameters.AddWithValue("@Qarzdor_talabalar_soni", qarzdor_talabalar_soni);
                    command.Parameters.AddWithValue("@Umumiy_qarz", umumiy_qarz);
                    command.Parameters.AddWithValue("@Ortacha_GPA", ortacha_GPA);
                    command.Parameters.AddWithValue("@Axborot_resurslari_soni", axborot_resurslari_soni);
                    command.Parameters.AddWithValue("@Oquv_bino_sigimi", oquv_bino_sigimi);
                    command.Parameters.AddWithValue("@Yotoqxona_sigimi", yotoqxona_sigimi);
                    command.Parameters.AddWithValue("@Ortacha_stipendiya", ortacha_stipendiya);
                    command.Parameters.AddWithValue("@Ortacha_kontrakt_tolovi", ortacha_kontrakt_tolovi);
                    command.Parameters.AddWithValue("@Yotoqxona_tolovi", yotoqxona_tolovi);
                    command.Parameters.AddWithValue("@Ajratilgan_joylar", ajratilgan_joylar);
                    command.Parameters.AddWithValue("@Grant_joylar", grant_joylar);
                    command.Parameters.AddWithValue("@Kontrakt_joylar", kontrakt_joylar);
                    command.Parameters.AddWithValue("@Fakultetlar_soni", fakultetlar_soni);
                    command.Parameters.AddWithValue("@Talim_yonalishlari_ids", talim_yonalishlari_ids);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Ma'lumotlar muvaffaqiyatli yangilandi.");
                        // Yangilangan ma'lumotlarni ko'rsatish uchun kerakli amallar
                    }
                    else
                    {
                        MessageBox.Show("Yangilashda xatolik yuz berdi. Ma'lumot topilmadi yoki yangilanmadi.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yangilashda xatolik yuz berdi: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            toolStripMenuItem6_Click(null, EventArgs.Empty);
            Sozlamalar_Load(this, EventArgs.Empty);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectId))
            {
                if (zId.Text == "ID: ")
                {
                    MessageBox.Show("Iltimos, birorta mahsulot tanlang!");
                    return;
                }
                // Ma'lumotlar bazasidan selectId ga mos ma'lumotni o'chirish
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // DELETE so'rovi tayyorlash
                        string deleteQuery = "DELETE FROM OTM WHERE Id = @Id";

                        SqlCommand command = new SqlCommand(deleteQuery, connection);
                        command.Parameters.AddWithValue("@Id", selectId);

                        // So'rovni bajaring
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Ma'lumot muvaffaqiyatli o'chirildi.");
                            // O'chirilgan ma'lumotni ko'rsatish uchun kerakli amallar
                        }
                        else
                        {
                            MessageBox.Show("O'chirishda xatolik yuz berdi. Ma'lumot topilmadi yoki o'chirilmadi.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xatolik: " + ex.Message);
                    }
                }
                toolStripMenuItem6_Click(null, EventArgs.Empty);
                Sozlamalar_Load(this, EventArgs.Empty);
            }
        }
    }
}
