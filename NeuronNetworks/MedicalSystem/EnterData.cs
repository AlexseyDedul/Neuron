using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem
{
    public partial class EnterData : Form
    {
        public EnterData()
        {
            InitializeComponent();

            var profInfo = typeof(Pacient).GetProperties();
            for(int i = 0; i < profInfo.Length; i++)
            {
                var property = profInfo[i];
                var textbox = CreateTextbox(i, property);
                Controls.Add(textbox);
            }
        }

        public bool? ShowForm()
        {
            var form = new EnterData();
            if(form.DialogResult == DialogResult.OK)
            {
                var patient = new Pacient();
                var result = Program.Controller.DataNetwork.Predict().Output;
                return result == 1.0;
            }
            return null;
        }

        private void EnterData_Load(object sender, EventArgs e)
        {

        }

        private TextBox CreateTextbox(int number, PropertyInfo property)
        {
            // 
            // textBox1
            // 
            var y = number * 25 + 12;
            var textbox = new TextBox();

            textbox.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            textbox.Location = new Point(12, y);
            textbox.Name = "textBox" + number;
            textbox.Size = new Size(250, 200);
            textbox.TabIndex = 0;

            return textbox;
        }
    }
}
