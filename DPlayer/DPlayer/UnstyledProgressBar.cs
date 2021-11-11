namespace DPlayer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class UnstyledProgressBar : UserControl
    {
        private Color backColor;
        private Color borderColor;
        private IContainer components = null;
        private int marqueeAnimationSpeed = 0xbb8;
        private int maximum = 100;
        private int minimum = 0;
        private bool rightToLeftLayout;
        private int step = 1;
        private int value = 0;

        public UnstyledProgressBar()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private int GetProgress()
        {
            Rectangle rectToFill = new Rectangle(2, 2, base.Width - 4, base.Height - 4);
            double f1 = this.Value - this.minimum;
            double f2 = this.maximum - this.minimum;
            double f3 = rectToFill.Width * (f1 / f2);
            return (int) f3;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Window;
            base.Name = "UnstyledProgressBar";
            base.Size = new Size(0x210, 0x12);
            base.Paint += new PaintEventHandler(this.UnstyledProgressBar_Paint);
            base.ResumeLayout(false);
        }

        private void UnstyledProgressBar_Paint(object sender, PaintEventArgs e)
        {
            base.BackColor = this.BorderColor;
            Rectangle backgroundRectangle = new Rectangle(1, 1, base.Width - 2, base.Height - 2);
            SolidBrush backgroundBrush = new SolidBrush(this.BackColor);
            e.Graphics.FillRectangle(backgroundBrush, backgroundRectangle);
            Rectangle foregroundRectangle = new Rectangle(2, 2, this.GetProgress(), base.Height - 4);
            SolidBrush progressBrush = new SolidBrush(this.ForeColor);
            e.Graphics.FillRectangle(progressBrush, foregroundRectangle);
        }

        public new Color BackColor
        {
            get
            {
                return this.backColor;
            }
            set
            {
                this.backColor = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
            }
        }

        public int MarqueeAnimationSpeed
        {
            get
            {
                return this.marqueeAnimationSpeed;
            }
            set
            {
                this.marqueeAnimationSpeed = value;
            }
        }

        public int Maximum
        {
            get
            {
                return this.maximum;
            }
            set
            {
                this.maximum = value;
            }
        }

        public int Minimum
        {
            get
            {
                return this.minimum;
            }
            set
            {
                this.minimum = value;
            }
        }

        public bool RightToLeftLayout
        {
            get
            {
                return this.rightToLeftLayout;
            }
            set
            {
                this.rightToLeftLayout = value;
            }
        }

        public int Step
        {
            get
            {
                return this.step;
            }
            set
            {
                this.step = value;
            }
        }

        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                base.Invalidate();
            }
        }
    }
}

