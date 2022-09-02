namespace QuanLyPhongNet
{
    partial class OptionGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionGUI));
            this.picReport = new System.Windows.Forms.PictureBox();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.picCategory = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llblSignOut = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // picReport
            // 
            this.picReport.Image = global::QuanLyPhongNet.Properties.Resources.vector_285_14_01_512;
            this.picReport.Location = new System.Drawing.Point(214, 316);
            this.picReport.Name = "picReport";
            this.picReport.Size = new System.Drawing.Size(184, 160);
            this.picReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picReport.TabIndex = 2;
            this.picReport.TabStop = false;
            this.picReport.Click += new System.EventHandler(this.PicReportClickEventHandler);
            this.picReport.MouseLeave += new System.EventHandler(this.PicReportMouseLeaveEventHandler);
            this.picReport.MouseHover += new System.EventHandler(this.PicReportMouseHoverEventHandler);
            // 
            // picHome
            // 
            this.picHome.BackColor = System.Drawing.Color.Transparent;
            this.picHome.Image = global::QuanLyPhongNet.Properties.Resources.trangchu;
            this.picHome.Location = new System.Drawing.Point(90, 98);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(184, 160);
            this.picHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHome.TabIndex = 1;
            this.picHome.TabStop = false;
            this.picHome.Click += new System.EventHandler(this.PicHomeClickEventHandler);
            this.picHome.MouseLeave += new System.EventHandler(this.PicHomeMouseLeaveEventHandler);
            this.picHome.MouseHover += new System.EventHandler(this.PicHomeMouseHoverEventHandler);
            // 
            // picCategory
            // 
            this.picCategory.Image = global::QuanLyPhongNet.Properties.Resources.google_sites_icon_large1;
            this.picCategory.Location = new System.Drawing.Point(345, 101);
            this.picCategory.Name = "picCategory";
            this.picCategory.Size = new System.Drawing.Size(183, 157);
            this.picCategory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCategory.TabIndex = 0;
            this.picCategory.TabStop = false;
            this.picCategory.Click += new System.EventHandler(this.PicCategoryClickEventHandler);
            this.picCategory.MouseLeave += new System.EventHandler(this.PicCategoryMouseLeaveEventHandler);
            this.picCategory.MouseHover += new System.EventHandler(this.PicCategoryMouseHoverEventHandler);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Các Tùy Chọn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(353, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Quản lý danh mục";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(227, 492);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Báo cáo, thống kê";
            // 
            // llblSignOut
            // 
            this.llblSignOut.AutoSize = true;
            this.llblSignOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblSignOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.llblSignOut.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.llblSignOut.Location = new System.Drawing.Point(480, 30);
            this.llblSignOut.Name = "llblSignOut";
            this.llblSignOut.Size = new System.Drawing.Size(113, 25);
            this.llblSignOut.TabIndex = 6;
            this.llblSignOut.TabStop = true;
            this.llblSignOut.Text = "Đăng Xuất";
            this.llblSignOut.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SignOutLinkClickedEventHandler);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(108, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Giao diện chính";
            // 
            // OptionGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 534);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.llblSignOut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picReport);
            this.Controls.Add(this.picHome);
            this.Controls.Add(this.picCategory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giao Diện Các Tùy Chọn";
            this.Load += new System.EventHandler(this.OptionGUILoadEventHandler);
            ((System.ComponentModel.ISupportInitialize)(this.picReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCategory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCategory;
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.PictureBox picReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llblSignOut;
        private System.Windows.Forms.Label label4;
    }
}