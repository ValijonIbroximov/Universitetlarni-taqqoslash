using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universitetlarni_taqqoslash.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Universitetlarni_taqqoslash
{
    public partial class Universitetlar : Form
    {
        public static string ConnectionString { get; private set; } = string.Empty;
        private static bool _loginparol = false;

        public static bool loginparol
        {
            get { return _loginparol; }
            set { _loginparol = value; }
        }

        public static int idLeft { get; private set; } = -1;
        public static int idRight { get; private set; } = -1;


        string selectId = string.Empty;
        string id = string.Empty;
        string nom = string.Empty;
        System.Drawing.Image rasm = null;
        byte[] imageByte1 = null;

        public Universitetlar()
        {
            InitializeComponent();
        }

        private void taqqoslashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(idLeft==-1 || idRight == -1)
            {
                MessageBox.Show("Taqqoslash uchun oliy ta'lim muassasalari tanlanmagan.\n" +
                        "Ikkala tomon uchun ham oliy ta'lim muassasasi tanlang!",
                        "Taqqoslash uchun tayyor emas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Taqqoslash taqqoslash = new Taqqoslash();
            taqqoslash.ShowDialog();
        }

        private void sozlamalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginparol = false;
            login signin = new login();
            signin.ShowDialog();
            if (loginparol)
            {
                Sozlamalar sozlamalar = new Sozlamalar();
                sozlamalar.ShowDialog();
                Universitetlar_Load(sender, e);
            }
            Universitetlar_Load(this, EventArgs.Empty);
        }

        private void chiqishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Universitetlar_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ConnectionString = loadConnectionString();
            //MessageBox.Show(ConnectionString, "cstr");
            if (!checkDatabaseConnection(ConnectionString))
            {
                MessageBox.Show("Ma'lumotlar bazasi joylashuvi aniqlanmadi.\n" +
                        "Sozlamalar oynasidan ma'lumotlar bazasini qayta bog'lang!",
                        "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sozlamalarToolStripMenuItem_Click(sender, e);
            }
            addOTM(flowLayoutPanelOTM);
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
                    MessageBox.Show("Ma'lumotlar bazasi joylashuvi aniqlanmadi.\n" +
                        "Sozlamalar oynasidan ma'lumotlar bazasini qayta bog'lang!",
                        "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sozlamalarToolStripMenuItem_Click(null, EventArgs.Empty);
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

        private void pictureBoxVS_Click(object sender, EventArgs e)
        {
            taqqoslashToolStripMenuItem_Click(null, EventArgs.Empty);
        }

        private void addOTM(Panel OTMlar_panel)
        {
            try
            {
                // Ma'lumotlar bazasidagi ma'lumotlarni panelga joylash
                OTMlar_panel.Controls.Clear(); // Obyektlarni tozalash

                using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                            panelOTM.Size = new System.Drawing.Size(270, 230);
                            panelOTM.TabIndex = 1;
                            panelOTM.BorderStyle = BorderStyle.Fixed3D;
                            // 
                            // picRasm
                            // 
                            picRasm.Dock = System.Windows.Forms.DockStyle.Fill;
                            picRasm.Location = new System.Drawing.Point(0, 86);
                            picRasm.Name = "picRasm";
                            picRasm.Size = new System.Drawing.Size(270, 170);
                            picRasm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                            picRasm.TabIndex = 1;
                            picRasm.TabStop = false;
                            picRasm.Image = rasm;
                            toolTip1.SetToolTip(picRasm, "Tanlash uchun 2 marta bosing");
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
                            toolTip1.SetToolTip(labelNom, "Tanlash uchun 2 marta bosing");

                            foreach (Control control in panelOTM.Controls)
                            {
                                // Har bir kontrol uchun click hodisasini qo'shish
                                control.DoubleClick += (sender, e) =>
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
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                        imageByte1 = (byte[])reader["Rasm"];
                        selectOTM();
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

        private void labelNomL_DoubleClick(object sender, EventArgs e)
        {
            idLeft = -1;
            labelNomL.Text = "Tanlanmagan";
            picRasmL.Image = Resources.photo;
        }

        private void labelNomR_DoubleClick(object sender, EventArgs e)
        {
            idRight = -1;
            labelNomR.Text = "Tanlanmagan";
            picRasmR.Image = Resources.photo;
        }

        private void selectOTM()
        {
            if (idLeft == -1)
            {
                idLeft = Convert.ToInt32(selectId);
                labelNomL.Text = nom;
                picRasmL.Image = ByteArrayToImage(imageByte1);
            }
            else if (idRight == -1)
            {
                idRight = Convert.ToInt32(selectId);
                labelNomR.Text = nom;
                picRasmR.Image = ByteArrayToImage(imageByte1);
            }
            else
            {
                MessageBox.Show("Oliy ta'lim muassasalari tanlab bo'lingan." +
                    "\nTaqqoslashni boshlang yoki oliy ta'lim muassasasini almashtiring.",
                    "No'to'gri harakat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
