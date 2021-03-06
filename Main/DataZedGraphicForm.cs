﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Analysis {
    public partial class DataZedGraphicForm : WeifenLuo.WinFormsUI.Docking.DockContent {

        public SchemeModel model = SchemeModel.getInstance();
        //分别存储,方案名.方案成员,方案属性,方案的第几次
        public List<string> schememessage;
        public List<int> runtimessage;


        public DataZedGraphicForm() {
            InitializeComponent();
            // InitDraw();

        }

        public void InitDraw() {

            CreateGraph_static(this.zedGraphControl1);
            this.zedGraphControl1.GraphPane.CurveList.Clear();
            this.zedGraphControl1.GraphPane.GraphObjList.Clear();
            ChooseScheme();

            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();//刷新
        }


        //传递过来的信息
        public void SchemeMessage() {
            schememessage = model.GetLateNameStatus();


        }
        public void runtimesage() {
            runtimessage = model.GetHistoryrun();
            if (schememessage.Count == 3) {
                InitDraw();
            }

        }


        //根据传来的信息进行方案的选择
        public void ChooseScheme() {

            if (schememessage[2].Equals("(005)属性1")) {//
                if (runtimessage == null)
                    return;
                for (int i = 0; i < runtimessage.Count; i++) {
                    if (runtimessage[i] == 3) {
                        TestResultForm2(TestData());
                    }
                    if (runtimessage[i] == 2) {
                        TestResultForm(TestData2());
                    }
                }


            }

            else if (schememessage[2].Equals("(006)属性2")) {
                TestResultForm(TestData3());

            }

        }

        //信息的初始化
        public void CreateGraph_static(ZedGraphControl zedgraphcontrol) {

            //#region 现实特征设置

            GraphPane myPane = zedgraphcontrol.GraphPane;

            //// Change the color of the title 改变标题的颜色
            //myPane.Title.FontSpec.FontColor = Color.Green;

            //// Add gridlines to the plot, and make them gray 网格线添加到情节,灰色
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.Gray;
            myPane.YAxis.MajorGrid.Color = Color.Gray;

            //myPane.XAxis.Scale.Format = "yyyy-MM-dd  HH:mm:ss";   //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") 
            //myPane.XAxis.Scale.Max = 5;
            //myPane.XAxis.Scale.MinorStep = 0.1;

            myPane.XAxis.Scale.FontSpec.Angle = 75; //横坐标字体角度
            zedgraphcontrol.GraphPane.Title.FontSpec.Size = 20;
            zedgraphcontrol.AutoScroll = true;
            zedgraphcontrol.GraphPane.XAxis.Type = ZedGraph.AxisType.DateAsOrdinal; //X轴属性类型
            zedgraphcontrol.PanModifierKeys = Keys.Shift;//移动坐标图



            zedgraphcontrol.GraphPane.XAxis.MajorTic.PenWidth = 8.0F;


            //myPane.XAxis.Scale.FontSpec.Size = 14;  //字号 ,最好不要设置

            //myPane.XAxis.Scale.FontSpec.IsBold = true;     //是不是粗体
            //myPane.XAxis.Scale.FontSpec.Border.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            //#endregion

        }
        public List<int> ExchangeColor() {

            List<int> listRGB = new List<int>();

            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
            B = (B > 255) ? 255 : B;
            listRGB.Add(R);
            listRGB.Add(G);
            listRGB.Add(B);
            return listRGB;


        }

    
        public PointPairList TestData() {
            double x, y1, y2;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            for (int i = 0; i < 36; i++) {
                x = (double)i + 5;
                y1 = 1.5 + Math.Sin((double)i * 0.2);
                y2 = 3.0 * (1.5 + Math.Sin((double)i * 0.2));
                list1.Add(x, y1);
                list2.Add(x, y2);
            }
            return list1;

        }
        public PointPairList TestData2() {
            double x, y1, y2;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            for (int i = 0; i < 36; i++) {
                x = (double)i + 5;
                y1 = 1.5 + Math.Sin((double)i * 0.2);
                y2 = 3.0 * (1.5 + Math.Sin((double)i * 0.2));
                list1.Add(x, y1);
                list2.Add(x, y2);
            }
            return list2;

        }
        public PointPairList TestData3() {
            double x, y1, y2;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            for (int i = 0; i < 36; i++) {
                x = (double)i + 5;
                y1 = 1.5 + Math.Sin((double)i * 0.2);
                y2 = 4.0 * (1.5 + Math.Sin((double)i * 0.2));
                list1.Add(x, y1);
                list2.Add(x, y2);
            }
            return list2;

        }

        public void TestResultForm(PointPairList list1) {


            GraphPane my = this.zedGraphControl1.GraphPane;
            List<int> list = new List<int>();
            list = ExchangeColor();

            int R = list[0];
            int G = list[1];
            int B = list[2];

            LineItem myCurve;



            myCurve = my.AddCurve("对比数据", list1, Color.FromArgb(R, G, B), SymbolType.Circle);
            myCurve.Symbol.Fill = new Fill(Color.FromArgb(R, G, B));


        }
        public void TestResultForm2(PointPairList list1) {
            GraphPane myPane = this.zedGraphControl1.GraphPane;
            ZedGraphControl zedgraphcontrol = this.zedGraphControl1;
            LineItem myCurve;//第一条折线

            //设置一个数组

            zedgraphcontrol.GraphPane.Title.Text = "参数折线图";
            zedgraphcontrol.GraphPane.XAxis.Title.Text = "步长";
            zedgraphcontrol.GraphPane.YAxis.Title.Text = "数量";

            myCurve = myPane.AddCurve("对比数据", list1, Color.DarkGreen, SymbolType.Circle);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("调用");
            model.setStartStep(this.startStepTextBox.Text);
            model.setEndStep(this.endStepTextBox.Text);
        }
    }

}

