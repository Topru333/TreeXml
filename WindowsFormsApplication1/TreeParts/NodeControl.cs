using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeParts
{
    public partial class NodeControl : UserControl
    {
        private static List<List<NodeControl>> nodeLayers = new List<List<NodeControl>>();
        private Node data;
        public int layer = 0;
        public int pos = 1;
        private bool IsNodeOpen = false;
        private NodeControl ncParent;
        public NodeControl(Node data)
        {
            InitializeComponent();
            this.data = data;
            ValueText.Text = data.GetValue().ToString();
        }
        private void RemoveChilds()
        {
            if(nodeLayers.Count > layer + 1)
            {
                nodeLayers[layer + 1].RemoveAll(this.IsChildPred);
            }
        }
        private bool IsChildPred(NodeControl nc)
        {
            bool isChild = (nc.ncParent == this);
            if(isChild)
            {
                nc.RemoveChilds();
                TopLevelControl.Controls.Remove(nc);
            }
            return isChild;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsNodeOpen)
            {
                RemoveChilds();
                RebuildTreeDown();
                TopLevelControl.CreateGraphics().Clear(Color.White);
                IsNodeOpen = false;
                button1.Text = "Open";
                return;
            }
            //Добавление веток(правый , левый)
            if(data.GetLeft()!=null)
            {
                NodeControl left = new NodeControl(data.GetLeft())
                {
                    Top = Top + 200,
                    Left = Left - 100,
                    layer = layer + 1
                };
                if(nodeLayers.Count < layer + 1)
                {
                    nodeLayers.Add(new List<NodeControl>());
                    nodeLayers[0].Add(this);
                }
                if(nodeLayers.Count < layer + 2)
                {
                    nodeLayers.Add(new List<NodeControl>());
                }
                left.ncParent = this;
                left.pos = -1;
                nodeLayers[layer + 1].Add(left);
                TopLevelControl.Controls.Add(left);
            }
            if (data.GetRight() != null)
            {
                NodeControl Right = new NodeControl(data.GetLeft())
                {
                    Top = Top + 200,
                    Left = Left + 100,
                    layer = layer + 1
                };
                if (nodeLayers.Count < layer + 1)
                {
                    nodeLayers.Add(new List<NodeControl>());
                    nodeLayers[0].Add(this);
                }
                if (nodeLayers.Count < layer + 2)
                {
                    nodeLayers.Add(new List<NodeControl>());
                }
                Right.ncParent = this;
                Right.pos = 1;
                nodeLayers[layer + 1].Add(Right);
                TopLevelControl.Controls.Add(Right);
            }
            RebuildTreeDown();
            IsNodeOpen = true;
            button1.Text = "Close";
        }

        private void RebuildTreeDown()
        {
            for (int i = 0; i < nodeLayers.Count;i++)
            {
                RebuildMarkUp(i);
            }
        }
        int CompareNC(NodeControl a,NodeControl b)
        {
            return a.pos - b.pos;
        }

        private void RebuildMarkUp(int lvl)
        {
            for(int i = 0;i<nodeLayers[lvl].Count;i++)
            {
                NodeControl ctrl = nodeLayers[lvl][i];
                NodeControl prevCtrl = ctrl.ncParent;
                if (prevCtrl != null)
                {
                    ctrl.Left = ctrl.pos * prevCtrl.Width * (nodeLayers.Count - lvl) + prevCtrl.Left;
                }
                else
                {
                    ctrl.Left = nodeLayers[nodeLayers.Count - 1].Count * Width * 3 / 2;
                }
            }
        }

        private void NodeControll_Paint(object sender, PaintEventArgs e)
        {
            if (ncParent != null)
            {
                Pen pen = new Pen(Color.Gold);
                Point A = new Point(Location.X + Size.Width / 2, Location.Y),
                      D = new Point(ncParent.Location.X + ncParent.Size.Width / 2, ncParent.Location.Y + ncParent.Size.Height);
                int PointOffset = (D.Y - A.Y) / 2 + 10;
                Point B = new Point(A.X, A.Y + PointOffset);
                Point C = new Point(D.X, D.Y - PointOffset);
                Graphics MainCtrlGraphics = TopLevelControl.CreateGraphics();
                MainCtrlGraphics.DrawLine(pen, A, B);
                MainCtrlGraphics.DrawLine(pen, B, C);
                MainCtrlGraphics.DrawLine(pen, C, D);
            }
        }
    }
}
