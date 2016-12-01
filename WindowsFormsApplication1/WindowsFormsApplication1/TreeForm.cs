using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using TreeParts;

namespace WindowsFormsApplication1
{
    public partial class TreeForm : Form
    {
        NodeControl ctrl;
        public TreeForm()
        {
            InitializeComponent();
            Node n;
            using (FileStream fs = new FileStream("tree1.xml", FileMode.Open))
            {
                XmlSerializer xmlSerialize = new XmlSerializer(typeof(Node));
                n = (Node)xmlSerialize.Deserialize(fs);
            }
            ctrl = new NodeControl(n);
            Controls.Add(ctrl);
        }
    }
}
