﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Analysis
{
    public partial class AssociatedAtrributionForm : Form
    {
        public InteractiveAgent agent = InteractiveAgent.getInstance();
        SchemeModel model = SchemeModel.getInstance();
        int anoAttributeId;
        public AssociatedAtrributionForm()
        {
            InitializeComponent();
        }

        //*交互*/加载时，根据主界面请求，取出同一个成员下的全部属性，向AttributesListBox里添加属性列表………………………………………………………………………………………………
        private void AssociatedAtrributionForm_Load(object sender, EventArgs e)
        {
            List<String> temlist = getMemBerAttributes(null);
            for (int i = 0; i < temlist.Count; i++)
            {
                this.attributesListBox.Items.Add(temlist[i]);
            }
        }


        //从model里取某个成员的所有属性………………………………………………………………………………………………………………………………目前假的
        private List<String> getMemBerAttributes(List<string> list)
        {
            //对成员属性get，并逻辑处理
            List<string> temlist = new List<string>();
            temlist.Add("属性1");
            temlist.Add("属性2");
            temlist.Add("属性3");
            return temlist;
        }

        //添加关联属性事件---------------------------------
        private void addButton_Click(object sender, EventArgs e)
        {
            if (this.attributesListBox.SelectedItems.Count == 0)
            {
                return;
            }
            else if (this.chosenAttributeListBox.Items.Count == 1)
            {
                MessageBox.Show("只可选择一个关联属性", "错误", MessageBoxButtons.OK);
                return;
            }
            else
            {
                chosenAttributeListBox.Items.Add(this.attributesListBox.SelectedItem);
                attributesListBox.Items.Remove(attributesListBox.SelectedItem);
            }
        }

        //删除属性按钮------------------------------------
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (this.chosenAttributeListBox.SelectedItems.Count == 0)
            {
                return;
            }
            else
            {
                attributesListBox.Items.Add(chosenAttributeListBox.SelectedItem);
                chosenAttributeListBox.Items.Remove(chosenAttributeListBox.SelectedItem);
            }
        }

        //确定，model里调用set关联分析的两个属性，并开启关联分析窗口  --------------------------------
        private void comfirmButton_Click(object sender, EventArgs e)
        {
            string anoAttribute = "";
            if (chosenAttributeListBox.Items.Count <= 0)
            {
                MessageBox.Show("请选择另一个关联属性", "错误", MessageBoxButtons.OK);
                return;
            }
            else
            {
                for (int i = 0; i < chosenAttributeListBox.Items.Count; i++)
                {
                    anoAttribute = chosenAttributeListBox.Items[i].ToString();//关联属性的属性名
                    //记录属性id，数据库查找………………………………………………………………………………………………………………………………………………………………//
                }
                this.DialogResult = DialogResult.OK;
                model.setAssociatedAttibuteName(anoAttribute);         //model存储关联的属性
                model.setAssociatedAttibuteId("3");            //…………………………………………………………………………………………………………………………假的
            }

        }

    }
}
