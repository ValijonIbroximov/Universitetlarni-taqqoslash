namespace Universitetlarni_taqqoslash
{
    partial class login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.linkdasturdanchiqish = new System.Windows.Forms.LinkLabel();
            this.linkparoltiklash = new System.Windows.Forms.LinkLabel();
            this.btnkirish = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtparol = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // linkdasturdanchiqish
            // 
            this.linkdasturdanchiqish.AutoSize = true;
            this.linkdasturdanchiqish.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkdasturdanchiqish.Location = new System.Drawing.Point(12, 151);
            this.linkdasturdanchiqish.Name = "linkdasturdanchiqish";
            this.linkdasturdanchiqish.Size = new System.Drawing.Size(145, 20);
            this.linkdasturdanchiqish.TabIndex = 8;
            this.linkdasturdanchiqish.TabStop = true;
            this.linkdasturdanchiqish.Text = "Dasturdan chiqish";
            this.linkdasturdanchiqish.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkdasturdanchiqish_LinkClicked);
            // 
            // linkparoltiklash
            // 
            this.linkparoltiklash.AutoSize = true;
            this.linkparoltiklash.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkparoltiklash.Location = new System.Drawing.Point(280, 151);
            this.linkparoltiklash.Name = "linkparoltiklash";
            this.linkparoltiklash.Size = new System.Drawing.Size(186, 20);
            this.linkparoltiklash.TabIndex = 9;
            this.linkparoltiklash.TabStop = true;
            this.linkparoltiklash.Text = "Parol esingizda yo\'qmi?";
            this.linkparoltiklash.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkparoltiklash_LinkClicked);
            // 
            // btnkirish
            // 
            this.btnkirish.Location = new System.Drawing.Point(161, 88);
            this.btnkirish.Name = "btnkirish";
            this.btnkirish.Size = new System.Drawing.Size(160, 38);
            this.btnkirish.TabIndex = 7;
            this.btnkirish.Text = "Kirish";
            this.btnkirish.UseVisualStyleBackColor = true;
            this.btnkirish.Click += new System.EventHandler(this.btnkirish_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Parolni kiriting:";
            // 
            // txtparol
            // 
            this.txtparol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtparol.Location = new System.Drawing.Point(64, 48);
            this.txtparol.Name = "txtparol";
            this.txtparol.PasswordChar = '*';
            this.txtparol.Size = new System.Drawing.Size(354, 34);
            this.txtparol.TabIndex = 4;
            this.txtparol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtparol_KeyDown);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Universitetlarni_taqqoslash.Properties.Resources.view;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(424, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 34);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 187);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.linkdasturdanchiqish);
            this.Controls.Add(this.linkparoltiklash);
            this.Controls.Add(this.btnkirish);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtparol);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.Load += new System.EventHandler(this.login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkdasturdanchiqish;
        private System.Windows.Forms.LinkLabel linkparoltiklash;
        private System.Windows.Forms.Button btnkirish;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtparol;
    }
}