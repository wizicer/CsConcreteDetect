namespace ConcreteDetect
{
    partial class frmMain
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
            this.picMain = new System.Windows.Forms.PictureBox();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnGradient = new System.Windows.Forms.Button();
            this.btnMarkArea = new System.Windows.Forms.Button();
            this.lstLines = new System.Windows.Forms.ListBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtLines = new System.Windows.Forms.TextBox();
            this.txtRegions = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.SuspendLayout();
            // 
            // picMain
            // 
            this.picMain.Location = new System.Drawing.Point(12, 12);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(510, 392);
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(528, 12);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(75, 23);
            this.btnMinus.TabIndex = 1;
            this.btnMinus.Text = "Minus";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnGradient
            // 
            this.btnGradient.Location = new System.Drawing.Point(528, 41);
            this.btnGradient.Name = "btnGradient";
            this.btnGradient.Size = new System.Drawing.Size(75, 23);
            this.btnGradient.TabIndex = 2;
            this.btnGradient.Text = "gradient";
            this.btnGradient.UseVisualStyleBackColor = true;
            this.btnGradient.Click += new System.EventHandler(this.btnGradient_Click);
            // 
            // btnMarkArea
            // 
            this.btnMarkArea.Location = new System.Drawing.Point(528, 70);
            this.btnMarkArea.Name = "btnMarkArea";
            this.btnMarkArea.Size = new System.Drawing.Size(75, 23);
            this.btnMarkArea.TabIndex = 3;
            this.btnMarkArea.Text = "markarea";
            this.btnMarkArea.UseVisualStyleBackColor = true;
            this.btnMarkArea.Click += new System.EventHandler(this.btnMarkArea_Click);
            // 
            // lstLines
            // 
            this.lstLines.FormattingEnabled = true;
            this.lstLines.ItemHeight = 12;
            this.lstLines.Location = new System.Drawing.Point(528, 279);
            this.lstLines.Name = "lstLines";
            this.lstLines.Size = new System.Drawing.Size(206, 100);
            this.lstLines.TabIndex = 4;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(528, 99);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtLines
            // 
            this.txtLines.Location = new System.Drawing.Point(528, 383);
            this.txtLines.Name = "txtLines";
            this.txtLines.Size = new System.Drawing.Size(206, 21);
            this.txtLines.TabIndex = 6;
            this.txtLines.Click += new System.EventHandler(this.txtLines_Click);
            // 
            // txtRegions
            // 
            this.txtRegions.Location = new System.Drawing.Point(528, 128);
            this.txtRegions.Multiline = true;
            this.txtRegions.Name = "txtRegions";
            this.txtRegions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRegions.Size = new System.Drawing.Size(206, 145);
            this.txtRegions.TabIndex = 7;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 416);
            this.Controls.Add(this.txtRegions);
            this.Controls.Add(this.txtLines);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lstLines);
            this.Controls.Add(this.btnMarkArea);
            this.Controls.Add(this.btnGradient);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.picMain);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnGradient;
        private System.Windows.Forms.Button btnMarkArea;
        private System.Windows.Forms.ListBox lstLines;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtLines;
        private System.Windows.Forms.TextBox txtRegions;
    }
}

