namespace PeonLib.forms
{
    partial class AdminUser
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminUser));
            this.button_add = new System.Windows.Forms.Button();
            this.button_remove = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.button_select = new System.Windows.Forms.Button();
            this.label_user_tittle = new System.Windows.Forms.Label();
            this.label_user_value = new System.Windows.Forms.Label();
            this.listbox1 = new PeonLib.COM.listbox(this.components);
            this.SuspendLayout();
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(234, 22);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(57, 19);
            this.button_add.TabIndex = 1;
            this.button_add.Text = "&Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_remove
            // 
            this.button_remove.Location = new System.Drawing.Point(234, 47);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(57, 19);
            this.button_remove.TabIndex = 2;
            this.button_remove.Text = "&Remove";
            this.button_remove.UseVisualStyleBackColor = true;
            this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(276, 184);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(82, 44);
            this.button_save.TabIndex = 3;
            this.button_save.Text = "&Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_select
            // 
            this.button_select.Location = new System.Drawing.Point(234, 72);
            this.button_select.Name = "button_select";
            this.button_select.Size = new System.Drawing.Size(57, 19);
            this.button_select.TabIndex = 4;
            this.button_select.Text = "&Select";
            this.button_select.UseVisualStyleBackColor = true;
            // 
            // label_user_tittle
            // 
            this.label_user_tittle.AutoSize = true;
            this.label_user_tittle.Location = new System.Drawing.Point(13, 13);
            this.label_user_tittle.Name = "label_user_tittle";
            this.label_user_tittle.Size = new System.Drawing.Size(32, 13);
            this.label_user_tittle.TabIndex = 5;
            this.label_user_tittle.Text = "User:";
            // 
            // label_user_value
            // 
            this.label_user_value.AutoSize = true;
            this.label_user_value.Location = new System.Drawing.Point(51, 13);
            this.label_user_value.Name = "label_user_value";
            this.label_user_value.Size = new System.Drawing.Size(22, 13);
            this.label_user_value.TabIndex = 6;
            this.label_user_value.Text = "xxx";
            // 
            // listbox1
            // 
            this.listbox1.FormattingEnabled = true;
            this.listbox1.Location = new System.Drawing.Point(36, 43);
            this.listbox1.Name = "listbox1";
            this.listbox1.Size = new System.Drawing.Size(168, 186);
            this.listbox1.TabIndex = 0;
            // 
            // AdminUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 262);
            this.Controls.Add(this.label_user_value);
            this.Controls.Add(this.label_user_tittle);
            this.Controls.Add(this.button_select);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_remove);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.listbox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminUser";
            this.Text = "AdminUser";
            this.Load += new System.EventHandler(this.AdminUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PeonLib.COM.listbox listbox1;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_remove;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_select;
        private System.Windows.Forms.Label label_user_tittle;
        private System.Windows.Forms.Label label_user_value;

    }
}