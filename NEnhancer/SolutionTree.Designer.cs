namespace NEnhancer
{
    partial class SolutionTree
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Doc1.txt");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("SfProject1(C#)");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("SfProject1(VB)");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("SfProject1(DB)");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("SolutionFolder", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("References");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Class1.cs");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Class2.cs");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Folder1", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Project1(C#)", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Project2(VB)");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Project3(DB)");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Solution", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode10,
            treeNode11,
            treeNode12});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node2";
            treeNode1.Text = "Doc1.txt";
            treeNode2.Name = "Node3";
            treeNode2.Text = "SfProject1(C#)";
            treeNode3.Name = "Node7";
            treeNode3.Text = "SfProject1(VB)";
            treeNode4.Name = "Node8";
            treeNode4.Text = "SfProject1(DB)";
            treeNode5.Name = "Node1";
            treeNode5.Text = "SolutionFolder";
            treeNode6.Name = "Node12";
            treeNode6.Text = "References";
            treeNode7.Name = "Node9";
            treeNode7.Text = "Class1.cs";
            treeNode8.Name = "Node11";
            treeNode8.Text = "Class2.cs";
            treeNode9.Name = "Node10";
            treeNode9.Text = "Folder1";
            treeNode10.Name = "Node4";
            treeNode10.Text = "Project1(C#)";
            treeNode11.Name = "Node5";
            treeNode11.Text = "Project2(VB)";
            treeNode12.Name = "Node6";
            treeNode12.Text = "Project3(DB)";
            treeNode13.Name = "Node0";
            treeNode13.Text = "Solution";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13});
            this.treeView1.Size = new System.Drawing.Size(345, 302);
            this.treeView1.TabIndex = 0;
            // 
            // SolutionTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 342);
            this.Controls.Add(this.treeView1);
            this.Name = "SolutionTree";
            this.Text = "SolutionTree";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
    }
}