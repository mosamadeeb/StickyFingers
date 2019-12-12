using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StickyFingers.Variables;
using static StickyFingers.Load;
using static StickyFingers.Save;

namespace StickyFingers
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void EnableButtons()
        {
            if (xfbin1Open)
            {
                exportNud1.Enabled = true;
                if (xfbin2Open || nudOpen) replaceButton.Enabled = true;
                else replaceButton.Enabled = false;
            }
            else exportNud1.Enabled = false;
            if (xfbin2Open) exportNud2.Enabled = true;
            else exportNud2.Enabled = false;
        }
        private void Xfbin1Browse_Click(object sender, EventArgs e)
        {
            if (openXfbin1Dialog.ShowDialog() == DialogResult.OK)
            {
                XfbinClose(1);
                if (XfbinOpen(1, openXfbin1Dialog.FileName))
                {
                    xfbin1Box.Text = xfbin1Path;
                    foreach (var nameInList in meshList1)
                    {
                        mesh1Box.Items.Add(nameInList.MeshName);
                    }
                    foreach (var group in xfbin1Groups)
                    {
                        ListViewItem item = new ListViewItem(group.Name);
                        item.SubItems.Add(BitConverter.ToString(BitConverter.GetBytes(group.EndByte)).Substring(0, 2));
                        groupsBox.Items.Add(item);
                    }
                }
                mesh1Box.SelectedIndex = 0;
                mesh1Box.Focus();
                EnableButtons();
            }
        }
        private void Xfbin2Browse_Click(object sender, EventArgs e)
        {
            if (openXfbin2Dialog.ShowDialog() == DialogResult.OK)
            {
                XfbinClose(2);
                FileInfo file = new FileInfo(openXfbin2Dialog.FileName);
                if (file.Extension == ".nud")
                {
                    nudPath = openXfbin2Dialog.FileName;
                    nudOpen = true;
                    xfbin2Box.Text = nudPath;
                    externalMesh = LoadNud(File.ReadAllBytes(nudPath), 0, false);
                    mesh2Box.Items.Add(externalMesh.MeshName);
                }
                else
                {
                    if (XfbinOpen(2, openXfbin2Dialog.FileName))
                    {
                        xfbin2Box.Text = xfbin2Path;
                        foreach (var nameInList in meshList2)
                        {
                            mesh2Box.Items.Add(nameInList.MeshName);
                        }
                    }
                }
                mesh2Box.SelectedIndex = 0;
                mesh2Box.Focus();
                EnableButtons();
            }
        }
        private void Mesh1Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperties(1);
        }
        private void Mesh2Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperties(2);
        }
        public void RefreshProperties(int xfbinNo)
        {
            int x;
            string groupsText = "";
            if (xfbinNo == 1)
            {
                x = mesh1Box.SelectedIndex;
                mesh1IndexLabel.Text = meshList1[x].MeshIndex.ToString();
                group1Label.Text = meshList1[x].GroupCount.ToString();
                foreach (var group in meshList1[x].GroupBytes)
                {
                    groupsText = groupsText + BitConverter.ToString(BitConverter.GetBytes(group.EndByte)).Substring(0, 2) + ", ";
                }
                groups1Label.Text = groupsText.Remove(groupsText.Length - 2);
                mat1Label.Text = meshList1[x].Material;
                formatByte1Label.Text = meshList1[x].MeshFormat;

            }
            else if (xfbinNo == 2)
            {
                x = mesh2Box.SelectedIndex;
                if (nudOpen)
                {
                    mesh2IndexLabel.Text = "N/A";
                    group2Label.Text = externalMesh.GroupCount.ToString();
                    groups2Label.Text = "N/A";
                    mat2Label.Text = externalMesh.Material;
                    formatByte2Label.Text = externalMesh.MeshFormat;
                }
                else if (xfbin2Open)
                {
                    mesh2IndexLabel.Text = meshList2[x].MeshIndex.ToString();
                    group2Label.Text = meshList2[x].GroupCount.ToString();
                    foreach (var group in meshList2[x].GroupBytes)
                    {
                        groupsText = groupsText + BitConverter.ToString(BitConverter.GetBytes(group.EndByte)).Substring(0, 2) + ", ";
                    }
                    groups2Label.Text = groupsText.Remove(groupsText.Length - 2);
                    mat2Label.Text = meshList2[x].Material;
                    formatByte2Label.Text = meshList2[x].MeshFormat;
                }
            }
        }
        public void XfbinClose(int xfbinNo)
        {
            BoneIDs.Clear();
            if (xfbinNo == 1)
            {
                mesh1Box.Items.Clear();
                xfbin1Box.Text = "";
                mesh1IndexLabel.Text = "";
                group1Label.Text = "";
                groups1Label.Text = "";
                mat1Label.Text = "";
                formatByte1Label.Text = "";
                meshCount1 = 0;
                if (xfbin1Open)
                {
                    file1Bytes.Clear();
                    meshList1.Clear();
                    xfbin1Groups.Clear();
                    groupsBox.Items.Clear();
                }
                xfbin1Open = false;
            }
            else if (xfbinNo == 2)
            {
                mesh2Box.Items.Clear();
                xfbin2Box.Text = "";
                mesh2IndexLabel.Text = "";
                group2Label.Text = "";
                groups2Label.Text = "";
                mat2Label.Text = "";
                formatByte2Label.Text = "";
                meshCount2 = 0;
                if (xfbin2Open)
                {
                    file2Bytes.Clear();
                    meshList2.Clear();
                    xfbin2Groups.Clear();
                }
                xfbin2Open = false;
                nudOpen = false;
            }
        }
        private void ExportNud1_Click(object sender, EventArgs e)
        {
            ExportNud(file1Bytes, meshList1, mesh1Box.SelectedIndex);
        }
        private void ExportNud2_Click(object sender, EventArgs e)
        {
            ExportNud(file1Bytes, meshList2, mesh2Box.SelectedIndex);
        }
        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            FileInfo fileName = new FileInfo(xfbin1Path);
            if (nudOpen) ReplaceExternalMesh(mesh1Box.SelectedIndex);
            else ReplaceMesh(mesh1Box.SelectedIndex, mesh2Box.SelectedIndex);
            File.WriteAllBytes(fileName.Name, file1Bytes.ToArray());
            MessageBox.Show($"File saved as \"" + fileName.Name + "\" in the program's directory.", $"Success");
        }
        private void Bones1Button_Click(object sender, EventArgs e)
        {
            if (openBones1Dialog.ShowDialog() == DialogResult.OK)
            {
                LoadBones(1, openBones1Dialog.FileName);
                exportB1Button.Enabled = true;
            }
        }
        private void ExportB1Button_Click(object sender, EventArgs e)
        {
            if (BoneNames.Any())
            {
                File.WriteAllLines(modelName + "_bones.txt", BoneNames);
                MessageBox.Show($"File saved as \"" + modelName + "_bones.txt" + "\" in the program's directory.", $"Success");
            }
        }
        private void XfbinBox_DragOver(object sender, DragEventArgs e)
        {
            dragFilePath = "";
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                string format = Path.GetExtension(files[0]);
                if (files.Length == 1)
                {
                    if (format == ".xfbin" || format == ".bak" && files[0].Contains(".xfbin"))
                    {
                        e.Effect = DragDropEffects.Copy;
                        dragFilePath = files[0];
                    }
                    else if ((sender as TextBox).Name == "xfbin2Box" && format == ".nud")
                    {
                        e.Effect = DragDropEffects.Copy;
                        dragFilePath = files[0];
                    }
                }
            }
        }
        private void XfbinBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if ((sender as TextBox).Name == "xfbin2Box")
                {
                    XfbinClose(2);
                    xfbin2Path = dragFilePath;
                    FileInfo file = new FileInfo(xfbin2Path);
                    if (file.Extension == ".nud")
                    {
                        nudPath = xfbin2Path;
                        nudOpen = true;
                        xfbin2Box.Text = nudPath;
                        externalMesh = LoadNud(File.ReadAllBytes(nudPath), 0, false);
                        mesh2Box.Items.Add(externalMesh.MeshName);
                    }
                    else
                    {
                        if (XfbinOpen(2, xfbin2Path))
                        {
                            xfbin2Box.Text = xfbin2Path;
                            foreach (var nameInList in meshList2)
                            {
                                mesh2Box.Items.Add(nameInList.MeshName);
                            }
                        }
                    }
                    mesh2Box.SelectedIndex = 0;
                    mesh2Box.Focus();
                    EnableButtons();
                }
                else
                {
                    XfbinClose(1);
                    xfbin1Path = dragFilePath;
                    if (XfbinOpen(1, xfbin1Path))
                    {
                        xfbin1Box.Text = dragFilePath;
                        foreach (var nameInList in meshList1)
                        {
                            mesh1Box.Items.Add(nameInList.MeshName);
                        }
                        foreach (var group in xfbin1Groups)
                        {
                            ListViewItem item = new ListViewItem(group.Name);
                            item.SubItems.Add(BitConverter.ToString(BitConverter.GetBytes(group.EndByte)).Substring(0, 2));
                            groupsBox.Items.Add(item);
                        }
                        mesh1Box.SelectedIndex = 0;
                        mesh1Box.Focus();
                        EnableButtons();
                    }
                }
            }
        }
    }
}
