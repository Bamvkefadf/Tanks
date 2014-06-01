namespace tanks
{
    partial class SaveResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
            this.NameTB = new System.Windows.Forms.TextBox();
            this.SaveScores = new System.Windows.Forms.Button();
            this.resultsTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(12, 270);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(164, 20);
            this.NameTB.TabIndex = 0;
            // 
            // SaveScores
            // 
            this.SaveScores.Enabled = false;
            this.SaveScores.Location = new System.Drawing.Point(197, 268);
            this.SaveScores.Name = "SaveScores";
            this.SaveScores.Size = new System.Drawing.Size(75, 23);
            this.SaveScores.TabIndex = 1;
            this.SaveScores.Text = " OK";
            this.SaveScores.UseVisualStyleBackColor = true;
            this.SaveScores.Click += new System.EventHandler(this.SaveResult_Click);
            // 
            // resultsTB
            // 
            this.resultsTB.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultsTB.Location = new System.Drawing.Point(12, 12);
            this.resultsTB.Multiline = true;
            this.resultsTB.Name = "resultsTB";
            this.resultsTB.ReadOnly = true;
            this.resultsTB.Size = new System.Drawing.Size(260, 252);
            this.resultsTB.TabIndex = 2;
            // 
            // SaveResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 295);
            this.Controls.Add(this.resultsTB);
            this.Controls.Add(this.SaveScores);
            this.Controls.Add(this.NameTB);
            this.MaximizeBox = false;
            this.Name = "SaveResultForm";
            this.Load += new System.EventHandler(this.SaveResultForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox NameTB;
        public System.Windows.Forms.Button SaveScores;
        private System.Windows.Forms.TextBox resultsTB;
    }
}