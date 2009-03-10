using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NEnhancer
{
    public partial class CodeConverter : AddInForm
    {
        public CodeConverter()
        {
            InitializeComponent();
        }

        private void CodeConverter_Load(object sender, EventArgs e)
        {
            if (!HasSolution)
            {
                lblMessage.Text = "Solution not opened";
            }
        }

        private bool HasSolution
        {
            get
            {
                return DTEObject.Solution.IsOpen;
            }
        }
    }
}
