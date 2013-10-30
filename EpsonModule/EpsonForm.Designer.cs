namespace EpsonModule {
    partial class EpsonMainForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpsonMainForm));
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.@__MenuBar = new System.Windows.Forms.ToolStrip();
            this.@__toolStripDisconnected = new System.Windows.Forms.ToolStripButton();
            this.@__toolStripConnected = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.@__btStart = new System.Windows.Forms.ToolStripButton();
            this.@__btStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.@__dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.@__timerParagem = new System.Windows.Forms.Timer(this.components);
            this.@__MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // __MenuBar
            // 
            resources.ApplyResources(this.@__MenuBar, "__MenuBar");
            this.@__MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__toolStripDisconnected,
            this.@__toolStripConnected,
            this.toolStripSeparator1,
            this.@__btStart,
            this.@__btStop,
            this.toolStripSeparator4});
            this.@__MenuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.@__MenuBar.Name = "__MenuBar";
            this.@__MenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // __toolStripDisconnected
            // 
            resources.ApplyResources(this.@__toolStripDisconnected, "__toolStripDisconnected");
            this.@__toolStripDisconnected.Name = "__toolStripDisconnected";
            // 
            // __toolStripConnected
            // 
            resources.ApplyResources(this.@__toolStripConnected, "__toolStripConnected");
            this.@__toolStripConnected.Name = "__toolStripConnected";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // __btStart
            // 
            resources.ApplyResources(this.@__btStart, "__btStart");
            this.@__btStart.Name = "__btStart";
            this.@__btStart.Click += new System.EventHandler(this.@__btInit_Click);
            // 
            // __btStop
            // 
            resources.ApplyResources(this.@__btStop, "__btStop");
            this.@__btStop.Name = "__btStop";
            this.@__btStop.Click += new System.EventHandler(this.@__btStop_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // __dockPanel1
            // 
            this.@__dockPanel1.ActiveAutoHideContent = null;
            resources.ApplyResources(this.@__dockPanel1, "__dockPanel1");
            this.@__dockPanel1.DockBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.@__dockPanel1.DockBottomPortion = 150D;
            this.@__dockPanel1.DockLeftPortion = 200D;
            this.@__dockPanel1.DockRightPortion = 200D;
            this.@__dockPanel1.DockTopPortion = 150D;
            this.@__dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.@__dockPanel1.Name = "__dockPanel1";
            this.@__dockPanel1.RightToLeftLayout = true;
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.@__dockPanel1.Skin = dockPanelSkin1;
            // 
            // __timerParagem
            // 
            this.@__timerParagem.Interval = 250;
            this.@__timerParagem.Tick += new System.EventHandler(this.@__timerParagem_Tick);
            // 
            // EpsonMainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.@__dockPanel1);
            this.Controls.Add(this.@__MenuBar);
            this.Name = "EpsonMainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EpsonMainForm_FormClosing);
            this.Load += new System.EventHandler(this.EpsonMainForm_Load);
            this.@__MenuBar.ResumeLayout(false);
            this.@__MenuBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip __MenuBar;
        private System.Windows.Forms.ToolStripButton __btStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private WeifenLuo.WinFormsUI.Docking.DockPanel __dockPanel1;
        private System.Windows.Forms.ToolStripButton __btStart;
        private System.Windows.Forms.ToolStripButton __toolStripDisconnected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton __toolStripConnected;
        private System.Windows.Forms.Timer __timerParagem;
    }
}