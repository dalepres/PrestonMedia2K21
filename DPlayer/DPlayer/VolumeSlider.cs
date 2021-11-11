namespace DPlayer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class VolumeSlider : UserControl
    {
        private IContainer components = null;
        private TrackBar trackBar1;

        public VolumeSlider()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.trackBar1 = new TrackBar();
            this.trackBar1.BeginInit();
            base.SuspendLayout();
            this.trackBar1.Location = new Point(0x1c, 0x22);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = Orientation.Vertical;
            this.trackBar1.Size = new Size(0x2a, 0x5b);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickStyle = TickStyle.None;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.trackBar1);
            base.Name = "VolumeSlider";
            base.Size = new Size(0x11d, 0xe3);
            this.trackBar1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

