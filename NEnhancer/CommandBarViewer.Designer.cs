namespace NEnhancer
{
    partial class CommandBarViewer
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tvCommandBars = new System.Windows.Forms.TreeView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvCommandBars);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 407);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CommandBars";
            // 
            // tvCommandBars
            // 
            this.tvCommandBars.Location = new System.Drawing.Point(18, 20);
            this.tvCommandBars.Name = "tvCommandBars";
            this.tvCommandBars.Size = new System.Drawing.Size(393, 368);
            this.tvCommandBars.TabIndex = 0;
            this.tvCommandBars.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvCommandBars_BeforeExpand);
            // 
            // CommandBarViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 471);
            this.Controls.Add(this.groupBox1);
            this.Name = "CommandBarViewer";
            this.Text = "CommandBar Viewer";
            this.Load += new System.EventHandler(this.CommandBarViewer_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView tvCommandBars;
    }
}