namespace EpsonModule {
    partial class EpsonStatusForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpsonStatusForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.@__progressRBStatus = new ExtendedDotNET.Controls.Progress.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.@__progressVisStatus = new ExtendedDotNET.Controls.Progress.ProgressBar();
            this.@__btNewInsp = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.@__progressVibStatus = new ExtendedDotNET.Controls.Progress.ProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._posx = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this._posy = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this._posz = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this._posu = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.@__progressRBStatus);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // __progressRBStatus
            // 
            this.@__progressRBStatus.BarOffset = 1;
            this.@__progressRBStatus.Caption = "Desligado do Robot";
            this.@__progressRBStatus.CaptionColor = System.Drawing.Color.Black;
            this.@__progressRBStatus.CaptionMode = ExtendedDotNET.Controls.Progress.ProgressCaptionMode.Custom;
            this.@__progressRBStatus.CaptionShadowColor = System.Drawing.Color.White;
            this.@__progressRBStatus.ChangeByMouse = false;
            this.@__progressRBStatus.DashSpace = 2;
            this.@__progressRBStatus.DashWidth = 5;
            resources.ApplyResources(this.@__progressRBStatus, "__progressRBStatus");
            this.@__progressRBStatus.Edge = ExtendedDotNET.Controls.Progress.ProgressBarEdge.Rounded;
            this.@__progressRBStatus.EdgeColor = System.Drawing.Color.Gray;
            this.@__progressRBStatus.EdgeLightColor = System.Drawing.Color.LightGray;
            this.@__progressRBStatus.EdgeWidth = 1;
            this.@__progressRBStatus.FloodPercentage = 0.2F;
            this.@__progressRBStatus.FloodStyle = ExtendedDotNET.Controls.Progress.ProgressFloodStyle.Standard;
            this.@__progressRBStatus.Invert = false;
            this.@__progressRBStatus.MainColor = System.Drawing.Color.DarkSeaGreen;
            this.@__progressRBStatus.Maximum = 100;
            this.@__progressRBStatus.Minimum = 0;
            this.@__progressRBStatus.Name = "__progressRBStatus";
            this.@__progressRBStatus.Orientation = ExtendedDotNET.Controls.Progress.ProgressBarDirection.Horizontal;
            this.@__progressRBStatus.ProgressBackColor = System.Drawing.Color.WhiteSmoke;
            this.@__progressRBStatus.ProgressBarStyle = ExtendedDotNET.Controls.Progress.ProgressStyle.Solid;
            this.@__progressRBStatus.SecondColor = System.Drawing.Color.White;
            this.@__progressRBStatus.Shadow = true;
            this.@__progressRBStatus.ShadowOffset = 1;
            this.@__progressRBStatus.Step = 1;
            this.@__progressRBStatus.TextAntialias = true;
            this.@__progressRBStatus.Value = 0;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.@__progressVisStatus);
            this.groupBox2.Controls.Add(this.@__btNewInsp);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // __progressVisStatus
            // 
            this.@__progressVisStatus.BarOffset = 1;
            this.@__progressVisStatus.Caption = "";
            this.@__progressVisStatus.CaptionColor = System.Drawing.Color.Black;
            this.@__progressVisStatus.CaptionMode = ExtendedDotNET.Controls.Progress.ProgressCaptionMode.Custom;
            this.@__progressVisStatus.CaptionShadowColor = System.Drawing.Color.White;
            this.@__progressVisStatus.ChangeByMouse = false;
            this.@__progressVisStatus.DashSpace = 2;
            this.@__progressVisStatus.DashWidth = 5;
            resources.ApplyResources(this.@__progressVisStatus, "__progressVisStatus");
            this.@__progressVisStatus.Edge = ExtendedDotNET.Controls.Progress.ProgressBarEdge.Rounded;
            this.@__progressVisStatus.EdgeColor = System.Drawing.Color.Gray;
            this.@__progressVisStatus.EdgeLightColor = System.Drawing.Color.LightGray;
            this.@__progressVisStatus.EdgeWidth = 1;
            this.@__progressVisStatus.FloodPercentage = 0.2F;
            this.@__progressVisStatus.FloodStyle = ExtendedDotNET.Controls.Progress.ProgressFloodStyle.Standard;
            this.@__progressVisStatus.Invert = false;
            this.@__progressVisStatus.MainColor = System.Drawing.Color.DarkSeaGreen;
            this.@__progressVisStatus.Maximum = 100;
            this.@__progressVisStatus.Minimum = 0;
            this.@__progressVisStatus.Name = "__progressVisStatus";
            this.@__progressVisStatus.Orientation = ExtendedDotNET.Controls.Progress.ProgressBarDirection.Horizontal;
            this.@__progressVisStatus.ProgressBackColor = System.Drawing.Color.WhiteSmoke;
            this.@__progressVisStatus.ProgressBarStyle = ExtendedDotNET.Controls.Progress.ProgressStyle.Solid;
            this.@__progressVisStatus.SecondColor = System.Drawing.Color.White;
            this.@__progressVisStatus.Shadow = true;
            this.@__progressVisStatus.ShadowOffset = 1;
            this.@__progressVisStatus.Step = 1;
            this.@__progressVisStatus.TextAntialias = true;
            this.@__progressVisStatus.Value = 0;
            // 
            // __btNewInsp
            // 
            resources.ApplyResources(this.@__btNewInsp, "__btNewInsp");
            this.@__btNewInsp.Name = "__btNewInsp";
            this.@__btNewInsp.UseVisualStyleBackColor = true;
            this.@__btNewInsp.Click += new System.EventHandler(this.@__btNewInsp_Click_1);
            // 
            // groupBox3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox3, 2);
            this.groupBox3.Controls.Add(this.@__progressVibStatus);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // __progressVibStatus
            // 
            this.@__progressVibStatus.BarOffset = 1;
            this.@__progressVibStatus.Caption = "";
            this.@__progressVibStatus.CaptionColor = System.Drawing.Color.Black;
            this.@__progressVibStatus.CaptionMode = ExtendedDotNET.Controls.Progress.ProgressCaptionMode.Custom;
            this.@__progressVibStatus.CaptionShadowColor = System.Drawing.Color.White;
            this.@__progressVibStatus.ChangeByMouse = false;
            this.@__progressVibStatus.DashSpace = 2;
            this.@__progressVibStatus.DashWidth = 5;
            resources.ApplyResources(this.@__progressVibStatus, "__progressVibStatus");
            this.@__progressVibStatus.Edge = ExtendedDotNET.Controls.Progress.ProgressBarEdge.Rounded;
            this.@__progressVibStatus.EdgeColor = System.Drawing.Color.Gray;
            this.@__progressVibStatus.EdgeLightColor = System.Drawing.Color.LightGray;
            this.@__progressVibStatus.EdgeWidth = 1;
            this.@__progressVibStatus.FloodPercentage = 0.2F;
            this.@__progressVibStatus.FloodStyle = ExtendedDotNET.Controls.Progress.ProgressFloodStyle.Standard;
            this.@__progressVibStatus.Invert = false;
            this.@__progressVibStatus.MainColor = System.Drawing.Color.DarkSeaGreen;
            this.@__progressVibStatus.Maximum = 100;
            this.@__progressVibStatus.Minimum = 0;
            this.@__progressVibStatus.Name = "__progressVibStatus";
            this.@__progressVibStatus.Orientation = ExtendedDotNET.Controls.Progress.ProgressBarDirection.Horizontal;
            this.@__progressVibStatus.ProgressBackColor = System.Drawing.Color.WhiteSmoke;
            this.@__progressVibStatus.ProgressBarStyle = ExtendedDotNET.Controls.Progress.ProgressStyle.Solid;
            this.@__progressVibStatus.SecondColor = System.Drawing.Color.White;
            this.@__progressVibStatus.Shadow = true;
            this.@__progressVibStatus.ShadowOffset = 1;
            this.@__progressVibStatus.Step = 1;
            this.@__progressVibStatus.TextAntialias = true;
            this.@__progressVibStatus.Value = 0;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._posx,
            this.toolStripStatusLabel2,
            this._posy,
            this.toolStripStatusLabel4,
            this._posz,
            this.toolStripStatusLabel3,
            this._posu});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // _posx
            // 
            resources.ApplyResources(this._posx, "_posx");
            this._posx.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._posx.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._posx.Name = "_posx";
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // _posy
            // 
            resources.ApplyResources(this._posy, "_posy");
            this._posy.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._posy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._posy.Name = "_posy";
            // 
            // toolStripStatusLabel4
            // 
            resources.ApplyResources(this.toolStripStatusLabel4, "toolStripStatusLabel4");
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            // 
            // _posz
            // 
            resources.ApplyResources(this._posz, "_posz");
            this._posz.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._posz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._posz.Name = "_posz";
            // 
            // toolStripStatusLabel3
            // 
            resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            // 
            // _posu
            // 
            resources.ApplyResources(this._posu, "_posu");
            this._posu.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this._posu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._posu.Name = "_posu";
            // 
            // EpsonStatusForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EpsonStatusForm";
            this.Load += new System.EventHandler(this.EpsonStatusForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public ExtendedDotNET.Controls.Progress.ProgressBar __progressRBStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        public ExtendedDotNET.Controls.Progress.ProgressBar __progressVibStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Button __btNewInsp;
        public ExtendedDotNET.Controls.Progress.ProgressBar __progressVisStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel _posx;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        public System.Windows.Forms.ToolStripStatusLabel _posy;
        public System.Windows.Forms.ToolStripStatusLabel _posz;
        public System.Windows.Forms.ToolStripStatusLabel _posu;


    }
}