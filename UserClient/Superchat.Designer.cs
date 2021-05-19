namespace UserClient
{
    partial class Superchat
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
            this.chat = new System.Windows.Forms.TextBox();
            this.Usermessage = new System.Windows.Forms.TextBox();
            this.selectuser = new System.Windows.Forms.ListBox();
            this.Send_button = new System.Windows.Forms.Button();
            this.Usernamebox = new System.Windows.Forms.TextBox();
            this.Connect_button = new System.Windows.Forms.Button();
            this.timelabel = new System.Windows.Forms.Label();
            this.InternalEvent = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chat
            // 
            this.chat.Enabled = false;
            this.chat.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chat.Location = new System.Drawing.Point(13, 13);
            this.chat.Multiline = true;
            this.chat.Name = "chat";
            this.chat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chat.Size = new System.Drawing.Size(541, 321);
            this.chat.TabIndex = 0;
            // 
            // Usermessage
            // 
            this.Usermessage.Enabled = false;
            this.Usermessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Usermessage.Location = new System.Drawing.Point(13, 351);
            this.Usermessage.Multiline = true;
            this.Usermessage.Name = "Usermessage";
            this.Usermessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Usermessage.Size = new System.Drawing.Size(541, 87);
            this.Usermessage.TabIndex = 1;
            // 
            // selectuser
            // 
            this.selectuser.Enabled = false;
            this.selectuser.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectuser.FormattingEnabled = true;
            this.selectuser.ItemHeight = 29;
            this.selectuser.Items.AddRange(new object[] {
            "all"});
            this.selectuser.Location = new System.Drawing.Point(561, 351);
            this.selectuser.Name = "selectuser";
            this.selectuser.Size = new System.Drawing.Size(227, 33);
            this.selectuser.TabIndex = 2;
            // 
            // Send_button
            // 
            this.Send_button.Enabled = false;
            this.Send_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Send_button.Location = new System.Drawing.Point(561, 391);
            this.Send_button.Name = "Send_button";
            this.Send_button.Size = new System.Drawing.Size(227, 47);
            this.Send_button.TabIndex = 3;
            this.Send_button.Text = "Send message";
            this.Send_button.UseVisualStyleBackColor = true;
            this.Send_button.Click += new System.EventHandler(this.Send_button_Click);
            // 
            // Usernamebox
            // 
            this.Usernamebox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Usernamebox.Location = new System.Drawing.Point(561, 13);
            this.Usernamebox.Name = "Usernamebox";
            this.Usernamebox.Size = new System.Drawing.Size(227, 34);
            this.Usernamebox.TabIndex = 4;
            this.Usernamebox.Text = "username";
            // 
            // Connect_button
            // 
            this.Connect_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Connect_button.Location = new System.Drawing.Point(561, 54);
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.Size = new System.Drawing.Size(227, 39);
            this.Connect_button.TabIndex = 5;
            this.Connect_button.Text = "Connect";
            this.Connect_button.UseVisualStyleBackColor = true;
            this.Connect_button.Click += new System.EventHandler(this.Connect_button_Click);
            // 
            // timelabel
            // 
            this.timelabel.AutoSize = true;
            this.timelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timelabel.Location = new System.Drawing.Point(561, 154);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(193, 29);
            this.timelabel.TabIndex = 6;
            this.timelabel.Text = "Lamport time: 1";
            // 
            // InternalEvent
            // 
            this.InternalEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InternalEvent.Location = new System.Drawing.Point(561, 186);
            this.InternalEvent.Name = "InternalEvent";
            this.InternalEvent.Size = new System.Drawing.Size(227, 75);
            this.InternalEvent.TabIndex = 7;
            this.InternalEvent.Text = "Internal event";
            this.InternalEvent.UseVisualStyleBackColor = true;
            this.InternalEvent.Click += new System.EventHandler(this.InternalEvent_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(561, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 64);
            this.button1.TabIndex = 8;
            this.button1.Text = "Critical process";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Superchat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.InternalEvent);
            this.Controls.Add(this.timelabel);
            this.Controls.Add(this.Connect_button);
            this.Controls.Add(this.Usernamebox);
            this.Controls.Add(this.Send_button);
            this.Controls.Add(this.selectuser);
            this.Controls.Add(this.Usermessage);
            this.Controls.Add(this.chat);
            this.Name = "Superchat";
            this.Text = "Superchat";
            this.Load += new System.EventHandler(this.Superchat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chat;
        private System.Windows.Forms.TextBox Usermessage;
        private System.Windows.Forms.ListBox selectuser;
        private System.Windows.Forms.Button Send_button;
        private System.Windows.Forms.TextBox Usernamebox;
        private System.Windows.Forms.Button Connect_button;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.Button InternalEvent;
        private System.Windows.Forms.Button button1;
    }
}