using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Universitetlarni_taqqoslash
{
    public partial class Taqqoslash : Form
    {
        string connectionString = string.Empty;
        int leftwidth = 550;
        int idLeft = -1, idRight = -1;
        int resultLeft = 0, resultRight = 0;

        public Taqqoslash()
        {
            InitializeComponent();
        }

        private void Taqqoslash_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            leftwidth = Convert.ToInt32(this.Width / 2);
            leftOTMwidth();
            panelOTMData.VerticalScroll.Value = panelVS.VerticalScroll.Maximum;
            panelOTMData.PerformLayout();
            idLeft = Universitetlar.idLeft;
            idRight = Universitetlar.idRight;
            connectionString = loadConnectionString();
            //MessageBox.Show(ConnectionString, "cstr");
            if (!checkDatabaseConnection(connectionString))
            {
                MessageBox.Show("Ma'lumotlar bazasi joylashuvi aniqlanmadi.\n" +
                        "Sozlamalar oynasidan ma'lumotlar bazasini qayta bog'lang!",
                        "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            OTMtaqqoslash(idLeft, idRight);
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
                    this.Close();
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
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
            }

            return isConnected;
        }

        private void leftOTMwidth()
        {
            resultl.Width = leftwidth;
            z1l.Width = leftwidth;
            z2l.Width = leftwidth;
            z3l.Width = leftwidth;
            z4l.Width = leftwidth;
            z5l.Width = leftwidth;
            z6l.Width = leftwidth;
            z7l.Width = leftwidth;
            z8l.Width = leftwidth;
            z9l.Width = leftwidth;
            z10l.Width = leftwidth;
            z11l.Width = leftwidth;
            z12l.Width = leftwidth;
            z13l.Width = leftwidth;
            z14l.Width = leftwidth;
            z15l.Width = leftwidth;
            z16l.Width = leftwidth;
            z17l.Width = leftwidth;
            z18l.Width = leftwidth;
            z19l.Width = leftwidth;
            z20l.Width = leftwidth;
            z21l.Width = leftwidth;
            z22l.Width = leftwidth;
            z23l.Width = leftwidth;
            z24l.Width = leftwidth;
            z25l.Width = leftwidth;
            z26l.Width = leftwidth;
            z27l.Width = leftwidth;
            z28l.Width = leftwidth;
            z29l.Width = leftwidth;
            z30l.Width = leftwidth;
            z31l.Width = leftwidth;
            z32l.Width = leftwidth;
            z33l.Width = leftwidth;
            z34l.Width = leftwidth;
            z35l.Width = leftwidth;
            z36l.Width = leftwidth;
            z37l.Width = leftwidth;
            z38l.Width = leftwidth;
            z39l.Width = leftwidth;
        }

        private void OTMtaqqoslash(int idLeft, int idRight)
        {
            string lnom = string.Empty, rnom = string.Empty;
            System.Drawing.Image lrasm = null, rrasm = null;
            string lviloyat = string.Empty, rviloyat = string.Empty;
            string lshahar = string.Empty, rshahar = string.Empty;
            string lholat = string.Empty, rholat = string.Empty;
            string ltashkil_etilgan_yili = string.Empty, rtashkil_etilgan_yili = string.Empty;
            string ltelefon = string.Empty, rtelefon = string.Empty;
            string lemail = string.Empty, remail = string.Empty;
            string lveb_sayti = string.Empty, rveb_sayti = string.Empty;
            string loqituvchilar_soni = string.Empty, roqituvchilar_soni = string.Empty;
            string ldontsentlar_soni = string.Empty, rdontsentlar_soni = string.Empty;
            string lprofessorlar_soni = string.Empty, rprofessorlar_soni = string.Empty;
            string lakademiklar_soni = string.Empty, rakademiklar_soni = string.Empty;
            string lxorijiy_oqituvchilar_soni = string.Empty, rxorijiy_oqituvchilar_soni = string.Empty;
            string lilmiy_maqolalar_soni = string.Empty, rilmiy_maqolalar_soni = string.Empty;
            string lilmiy_iqtiboslar_soni = string.Empty, rilmiy_iqtiboslar_soni = string.Empty;
            string ltadqiqot_mablaglari = string.Empty, rtadqiqot_mablaglari = string.Empty;
            string loqituvchilar_KPI = string.Empty, roqituvchilar_KPI = string.Empty;
            string ltalabalar_soni = string.Empty, rtalabalar_soni = string.Empty;
            string lerkak_talabalar_soni = string.Empty, rerkak_talabalar_soni = string.Empty;
            string layol_talabalar_soni = string.Empty, rayol_talabalar_soni = string.Empty;
            string lxorijiy_talabalar_soni = string.Empty, rxorijiy_talabalar_soni = string.Empty;
            string lbitiruvchilar_soni = string.Empty, rbitiruvchilar_soni = string.Empty;
            string lish_bilan_bandlik = string.Empty, rish_bilan_bandlik = string.Empty;
            string lqarzdor_talabalar_soni = string.Empty, rqarzdor_talabalar_soni = string.Empty;
            string lumumiy_qarz = string.Empty, rumumiy_qarz = string.Empty;
            string lortacha_GPA = string.Empty, rortacha_GPA = string.Empty;
            string laxborot_resurslari_soni = string.Empty, raxborot_resurslari_soni = string.Empty;
            string loquv_bino_sigimi = string.Empty, roquv_bino_sigimi = string.Empty;
            string lyotoqxona_sigimi = string.Empty, ryotoqxona_sigimi = string.Empty;
            string lortacha_stipendiya = string.Empty, rortacha_stipendiya = string.Empty;
            string lortacha_kontrakt_tolovi = string.Empty, rortacha_kontrakt_tolovi = string.Empty;
            string lyotoqxona_tolovi = string.Empty, ryotoqxona_tolovi = string.Empty;
            string lajratilgan_joylar = string.Empty, rajratilgan_joylar = string.Empty;
            string lgrant_joylar = string.Empty, rgrant_joylar = string.Empty;
            string lkontrakt_joylar = string.Empty, rkontrakt_joylar = string.Empty;
            string lfakultetlar_soni = string.Empty, rfakultetlar_soni = string.Empty;
            string ltalim_yonalishlari_ids = string.Empty, rtalim_yonalishlari_ids = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Ma'lumotlarni olish uchun SQL so'rov
                    string lselectQuery = "SELECT * FROM OTM WHERE Id = @Id";
                    SqlCommand lcommand = new SqlCommand(lselectQuery, connection);
                    lcommand.Parameters.AddWithValue("@Id", idLeft);

                    // Ma'lumotlarni olish
                    SqlDataReader lreader = lcommand.ExecuteReader();
                    if (lreader.Read())
                    {
                        // Ma'lumotlarni olish
                        lnom = lreader["Nom"].ToString();
                        byte[] limageByte = (byte[])lreader["Rasm"];
                        lrasm = ByteArrayToImage(limageByte);
                        lviloyat = lreader["Viloyat"].ToString();
                        lshahar = lreader["Shahar"].ToString();
                        lholat = lreader["Holat"].ToString();
                        ltashkil_etilgan_yili = lreader["Tashkil_etilgan_yili"].ToString();
                        ltelefon = lreader["Telefon"].ToString();
                        lemail = lreader["Email"].ToString();
                        lveb_sayti = lreader["Veb_sayti"].ToString();
                        loqituvchilar_soni = lreader["Oqituvchilar_soni"].ToString();
                        ldontsentlar_soni = lreader["Dontsentlar_soni"].ToString();
                        lprofessorlar_soni = lreader["Professorlar_soni"].ToString();
                        lakademiklar_soni = lreader["Akademiklar_soni"].ToString();
                        lxorijiy_oqituvchilar_soni = lreader["Xorijiy_oqituvchilar_soni"].ToString();
                        lilmiy_maqolalar_soni = lreader["Ilmiy_maqolalar_soni"].ToString();
                        lilmiy_iqtiboslar_soni = lreader["Ilmiy_iqtiboslar_soni"].ToString();
                        ltadqiqot_mablaglari = lreader["Tadqiqot_mablaglari"].ToString();
                        loqituvchilar_KPI = lreader["Oqituvchilar_KPI"].ToString();
                        ltalabalar_soni = lreader["Talabalar_soni"].ToString();
                        lerkak_talabalar_soni = lreader["Erkak_talabalar_soni"].ToString();
                        layol_talabalar_soni = lreader["Ayol_talabalar_soni"].ToString();
                        lxorijiy_talabalar_soni = lreader["Xorijiy_talabalar_soni"].ToString();
                        lbitiruvchilar_soni = lreader["Bitiruvchilar_soni"].ToString();
                        lish_bilan_bandlik = lreader["Ish_bilan_bandlik"].ToString();
                        lqarzdor_talabalar_soni = lreader["Qarzdor_talabalar_soni"].ToString();
                        lumumiy_qarz = lreader["Umumiy_qarz"].ToString();
                        lortacha_GPA = lreader["Ortacha_GPA"].ToString();
                        laxborot_resurslari_soni = lreader["Axborot_resurslari_soni"].ToString();
                        loquv_bino_sigimi = lreader["Oquv_bino_sigimi"].ToString();
                        lyotoqxona_sigimi = lreader["Yotoqxona_sigimi"].ToString();
                        lortacha_stipendiya = lreader["Ortacha_stipendiya"].ToString();
                        lortacha_kontrakt_tolovi = lreader["Ortacha_kontrakt_tolovi"].ToString();
                        lyotoqxona_tolovi = lreader["Yotoqxona_tolovi"].ToString();
                        lajratilgan_joylar = lreader["Ajratilgan_joylar"].ToString();
                        lgrant_joylar = lreader["Grant_joylar"].ToString();
                        lkontrakt_joylar = lreader["Kontrakt_joylar"].ToString();
                        lfakultetlar_soni = lreader["Fakultetlar_soni"].ToString();
                        ltalim_yonalishlari_ids = lreader["Talim_yonalishlari_ids"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Ma'lumot topilmadi.", "Xatolik");
                    }

                    lreader.Close();

                    // Ma'lumotlarni olish uchun SQL so'rov
                    string rselectQuery = "SELECT * FROM OTM WHERE Id = @Id";
                    SqlCommand rcommand = new SqlCommand(rselectQuery, connection);
                    rcommand.Parameters.AddWithValue("@Id", idRight);

                    // Ma'lumotlarni olish
                    SqlDataReader rreader = rcommand.ExecuteReader();
                    if (rreader.Read())
                    {
                        // Ma'lumotlarni olish
                        rnom = rreader["Nom"].ToString();
                        byte[] rimageByte = (byte[])rreader["Rasm"];
                        rrasm = ByteArrayToImage(rimageByte);
                        rviloyat = rreader["Viloyat"].ToString();
                        rshahar = rreader["Shahar"].ToString();
                        rholat = rreader["Holat"].ToString();
                        rtashkil_etilgan_yili = rreader["Tashkil_etilgan_yili"].ToString();
                        rtelefon = rreader["Telefon"].ToString();
                        remail = rreader["Email"].ToString();
                        rveb_sayti = rreader["Veb_sayti"].ToString();
                        roqituvchilar_soni = rreader["Oqituvchilar_soni"].ToString();
                        rdontsentlar_soni = rreader["Dontsentlar_soni"].ToString();
                        rprofessorlar_soni = rreader["Professorlar_soni"].ToString();
                        rakademiklar_soni = rreader["Akademiklar_soni"].ToString();
                        rxorijiy_oqituvchilar_soni = rreader["Xorijiy_oqituvchilar_soni"].ToString();
                        rilmiy_maqolalar_soni = rreader["Ilmiy_maqolalar_soni"].ToString();
                        rilmiy_iqtiboslar_soni = rreader["Ilmiy_iqtiboslar_soni"].ToString();
                        rtadqiqot_mablaglari = rreader["Tadqiqot_mablaglari"].ToString();
                        roqituvchilar_KPI = rreader["Oqituvchilar_KPI"].ToString();
                        rtalabalar_soni = rreader["Talabalar_soni"].ToString();
                        rerkak_talabalar_soni = rreader["Erkak_talabalar_soni"].ToString();
                        rayol_talabalar_soni = rreader["Ayol_talabalar_soni"].ToString();
                        rxorijiy_talabalar_soni = rreader["Xorijiy_talabalar_soni"].ToString();
                        rbitiruvchilar_soni = rreader["Bitiruvchilar_soni"].ToString();
                        rish_bilan_bandlik = rreader["Ish_bilan_bandlik"].ToString();
                        rqarzdor_talabalar_soni = rreader["Qarzdor_talabalar_soni"].ToString();
                        rumumiy_qarz = rreader["Umumiy_qarz"].ToString();
                        rortacha_GPA = rreader["Ortacha_GPA"].ToString();
                        raxborot_resurslari_soni = rreader["Axborot_resurslari_soni"].ToString();
                        roquv_bino_sigimi = rreader["Oquv_bino_sigimi"].ToString();
                        ryotoqxona_sigimi = rreader["Yotoqxona_sigimi"].ToString();
                        rortacha_stipendiya = rreader["Ortacha_stipendiya"].ToString();
                        rortacha_kontrakt_tolovi = rreader["Ortacha_kontrakt_tolovi"].ToString();
                        ryotoqxona_tolovi = rreader["Yotoqxona_tolovi"].ToString();
                        rajratilgan_joylar = rreader["Ajratilgan_joylar"].ToString();
                        rgrant_joylar = rreader["Grant_joylar"].ToString();
                        rkontrakt_joylar = rreader["Kontrakt_joylar"].ToString();
                        rfakultetlar_soni = rreader["Fakultetlar_soni"].ToString();
                        rtalim_yonalishlari_ids = rreader["Talim_yonalishlari_ids"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Ma'lumot topilmadi.", "Xatolik");
                    }

                    rreader.Close();


                    z1l.Text = idLeft.ToString();
                    z2l.Text = lnom;
                    z3l.Image = lrasm;
                    z4l.Text = lviloyat;
                    z5l.Text = lshahar;
                    z6l.Text = lholat;
                    z7l.Text = ltashkil_etilgan_yili;
                    z8l.Text = ltelefon;
                    z9l.Text = lemail;
                    z10l.Text = lveb_sayti;
                    z11l.Text = loqituvchilar_soni;
                    z12l.Text = ldontsentlar_soni;
                    z13l.Text = lprofessorlar_soni;
                    z14l.Text = lakademiklar_soni;
                    z15l.Text = lxorijiy_oqituvchilar_soni;
                    z16l.Text = lilmiy_maqolalar_soni;
                    z17l.Text = lilmiy_iqtiboslar_soni;
                    z18l.Text = ltadqiqot_mablaglari;
                    z19l.Text = loqituvchilar_KPI;
                    z10l.Text = ltalabalar_soni;
                    z21l.Text = lerkak_talabalar_soni;
                    z22l.Text = layol_talabalar_soni;
                    z23l.Text = lxorijiy_talabalar_soni;
                    z24l.Text = lbitiruvchilar_soni;
                    z25l.Text = lish_bilan_bandlik;
                    z26l.Text = lqarzdor_talabalar_soni;
                    z27l.Text = lumumiy_qarz;
                    z28l.Text = lortacha_GPA;
                    z29l.Text = laxborot_resurslari_soni;
                    z30l.Text = loquv_bino_sigimi;
                    z31l.Text = lyotoqxona_sigimi;
                    z32l.Text = lortacha_stipendiya;
                    z33l.Text = lortacha_kontrakt_tolovi;
                    z34l.Text = lyotoqxona_tolovi;
                    z35l.Text = lajratilgan_joylar;
                    z36l.Text = lgrant_joylar;
                    z37l.Text = lkontrakt_joylar;
                    z38l.Text = lfakultetlar_soni;
                    SetYonalishNamesFromIds(ltalim_yonalishlari_ids, z39l);



                    z1l.Text = idRight.ToString();
                    z2r.Text = rnom;
                    z3r.Image = rrasm;
                    z4r.Text = rviloyat;
                    z5r.Text = rshahar;
                    z6r.Text = rholat;
                    z7r.Text = rtashkil_etilgan_yili;
                    z8r.Text = rtelefon;
                    z9r.Text = remail;
                    z10r.Text = rveb_sayti;
                    z11r.Text = roqituvchilar_soni;
                    z12r.Text = rdontsentlar_soni;
                    z13r.Text = rprofessorlar_soni;
                    z14r.Text = rakademiklar_soni;
                    z15r.Text = rxorijiy_oqituvchilar_soni;
                    z16r.Text = rilmiy_maqolalar_soni;
                    z17r.Text = rilmiy_iqtiboslar_soni;
                    z18r.Text = rtadqiqot_mablaglari;
                    z19r.Text = roqituvchilar_KPI;
                    z10r.Text = rtalabalar_soni;
                    z21r.Text = rerkak_talabalar_soni;
                    z22r.Text = rayol_talabalar_soni;
                    z23r.Text = rxorijiy_talabalar_soni;
                    z24r.Text = rbitiruvchilar_soni;
                    z25r.Text = rish_bilan_bandlik;
                    z26r.Text = rqarzdor_talabalar_soni;
                    z27r.Text = rumumiy_qarz;
                    z28r.Text = rortacha_GPA;
                    z29r.Text = raxborot_resurslari_soni;
                    z30r.Text = roquv_bino_sigimi;
                    z31r.Text = ryotoqxona_sigimi;
                    z32r.Text = rortacha_stipendiya;
                    z33r.Text = rortacha_kontrakt_tolovi;
                    z34r.Text = ryotoqxona_tolovi;
                    z35r.Text = rajratilgan_joylar;
                    z36r.Text = rgrant_joylar;
                    z37r.Text = rkontrakt_joylar;
                    z38r.Text = rfakultetlar_soni;
                    SetYonalishNamesFromIds(rtalim_yonalishlari_ids, z39r);

                    kattaplus(z11l, z11r);
                    kattaplus(z12l, z12r);
                    kattaplus(z13l, z13r);
                    kattaplus(z14l, z14r);
                    kattaplus(z15l, z15r);
                    kattaplus(z16l, z16r);
                    kattaplus(z17l, z17r);
                    kattaplus(z18l, z18r);
                    kattaplus(z19l, z19r);
                    kattaplus(z20l, z20r);
                    kattaplus(z21l, z21r);
                    kattaplus(z22l, z22r);
                    kattaplus(z23l, z23r);
                    kattaplus(z24l, z24r);
                    kattaplus(z25l, z25r);
                    kichikplus(z26l, z26r);
                    kichikplus(z27l, z27r);
                    kattaplus(z28l, z28r);
                    kattaplus(z29l, z29r);
                    kattaplus(z30l, z30r);
                    kattaplus(z31l, z31r);
                    kattaplus(z32l, z32r);
                    kichikplus(z33l, z33r);
                    kichikplus(z34l, z34r);
                    kattaplus(z35l, z35r);
                    kattaplus(z36l, z36r);
                    kattaplus(z37l, z37r);
                    kattaplus(z38l, z38r);
                    compareListBox(z39l, z39r);

                    resultl.Text = resultLeft.ToString();
                    resultr.Text = resultRight.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message, "Xatolik");
                }
            }
        }

        private void compareListBox(ListBox left, ListBox right)
        {
            if (left.Items.Count > right.Items.Count)
            {
                resultLeft++;
                left.BackColor = System.Drawing.Color.Lime;
            }
            else if (left.Items.Count < right.Items.Count)
            {
                resultRight++;
                right.BackColor = System.Drawing.Color.Lime;
            }
            else
            {
                left.BackColor = System.Drawing.Color.Lime;
                right.BackColor = System.Drawing.Color.Lime;
            }
        }

        private void kattaplus(Label left, Label right)
        {
            if (Convert.ToInt32(left.Text) > Convert.ToInt32(right.Text))
            {
                resultLeft++;
                left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            }
            else if (Convert.ToInt32(left.Text) < Convert.ToInt32(right.Text))
            {
                resultRight++;
                right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            }
            else
            {
                left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
                right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            }
        }
        private void kichikplus(Label left, Label right)
        {
            if (Convert.ToInt32(left.Text) < Convert.ToInt32(right.Text))
            {
                resultLeft++;
                left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            }
            else if (Convert.ToInt32(left.Text) > Convert.ToInt32(right.Text))
            {
                resultRight++;
                right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            }
            else
            {
                left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
                right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
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

        private void SetYonalishNamesFromIds(string talimYonalishlariIds, ListBox listbox)
        {
            // zTalim_yonalishlari_ids ni tozalash
            listbox.Items.Clear();

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
                        listbox.Items.Add(yonalishNom);
                    }
                }
            }
        }
    }
}
