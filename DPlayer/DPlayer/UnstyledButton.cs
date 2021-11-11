namespace DPlayer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class UnstyledButton : UserControl
    {
        private IContainer components = null;
        private DPlayerButtonTypes dPlayerButtonType;
        private bool mouseIsDown = false;

        public UnstyledButton()
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

        private void DrawBeginningOfTrack(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            graphic.DrawLine(pen, (int) (7 + drawingOffset), (int) (4 + drawingOffset), (int) (7 + drawingOffset), (int) (10 + drawingOffset));
            Point[] points1 = new Point[] { new Point(13 + drawingOffset, 4 + drawingOffset), new Point(10 + drawingOffset, 7 + drawingOffset), new Point(13 + drawingOffset, 10 + drawingOffset) };
            graphic.DrawLines(pen, points1);
            Point[] points2 = new Point[] { new Point(0x12 + drawingOffset, 4 + drawingOffset), new Point(15 + drawingOffset, 7 + drawingOffset), new Point(0x12 + drawingOffset, 10 + drawingOffset) };
            graphic.DrawLines(pen, points2);
        }

        private void DrawBorder(Graphics graphics)
        {
            Pen pen = new Pen(SystemColors.ButtonFace);
            Rectangle r = base.ClientRectangle;
            r.Inflate(-1, -1);
            if (this.Focused)
            {
                graphics.DrawRectangle(pen, r);
            }
            else
            {
                graphics.DrawLines(pen, this.GetBorderPoints(r));
            }
        }

        private void DrawButtonType(Graphics graphics, Color color, int drawingOffset)
        {
            switch (this.dPlayerButtonType)
            {
                case DPlayerButtonTypes.BeginningOfTrack:
                    this.DrawBeginningOfTrack(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.Close:
                    this.DrawClose(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.EndOfTrack:
                    this.DrawEndOfTrack(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.FastForward:
                    this.DrawFastForward(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.FastRewind:
                    this.DrawFastRewind(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.Maximize:
                    this.DrawMaximize(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.Minimize:
                    this.DrawMinimize(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.Pause:
                    this.DrawPause(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.Play:
                    this.DrawPlay(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.Restore:
                    this.DrawRestore(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.SpeakerMuted:
                    this.DrawSpeakerMuted(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.SpeakerUnmuted:
                    this.DrawSpeakerUnmuted(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.Stop:
                    this.DrawStop(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.VolumeDown:
                    this.DrawVolumeDown(graphics, color, drawingOffset);
                    break;

                case DPlayerButtonTypes.VolumeUp:
                    this.DrawVolumeUp(graphics, color, drawingOffset);
                    break;
            }
        }

        private void DrawClose(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            Point[] points1 = new Point[] { new Point(8 + drawingOffset, 4 + drawingOffset), new Point(8 + drawingOffset, 6 + drawingOffset), new Point(14 + drawingOffset, 11 + drawingOffset), new Point(14 + drawingOffset, 13 + drawingOffset) };
            Point[] points2 = new Point[] { new Point(14 + drawingOffset, 4 + drawingOffset), new Point(14 + drawingOffset, 6 + drawingOffset), new Point(8 + drawingOffset, 11 + drawingOffset), new Point(8 + drawingOffset, 13 + drawingOffset) };
            graphic.DrawLines(pen, points1);
            graphic.DrawLines(pen, points2);
        }

        private void DrawEndOfTrack(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            graphic.DrawLine(pen, (int) (0x12 + drawingOffset), (int) (4 + drawingOffset), (int) (0x12 + drawingOffset), (int) (10 + drawingOffset));
            Point[] points1 = new Point[] { new Point(7 + drawingOffset, 4 + drawingOffset), new Point(10 + drawingOffset, 7 + drawingOffset), new Point(7 + drawingOffset, 10 + drawingOffset) };
            graphic.DrawLines(pen, points1);
            Point[] points2 = new Point[] { new Point(12 + drawingOffset, 4 + drawingOffset), new Point(15 + drawingOffset, 7 + drawingOffset), new Point(12 + drawingOffset, 10 + drawingOffset) };
            graphic.DrawLines(pen, points2);
        }

        private void DrawFastForward(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            Point[] points1 = new Point[] { new Point(7 + drawingOffset, 4 + drawingOffset), new Point(10 + drawingOffset, 7 + drawingOffset), new Point(7 + drawingOffset, 10 + drawingOffset) };
            graphic.DrawLines(pen, points1);
            Point[] points2 = new Point[] { new Point(12 + drawingOffset, 4 + drawingOffset), new Point(15 + drawingOffset, 7 + drawingOffset), new Point(12 + drawingOffset, 10 + drawingOffset) };
            graphic.DrawLines(pen, points2);
        }

        private void DrawFastRewind(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            Point[] points1 = new Point[] { new Point(10 + drawingOffset, 4 + drawingOffset), new Point(7 + drawingOffset, 7 + drawingOffset), new Point(10 + drawingOffset, 10 + drawingOffset) };
            graphic.DrawLines(pen, points1);
            Point[] points2 = new Point[] { new Point(15 + drawingOffset, 4 + drawingOffset), new Point(12 + drawingOffset, 7 + drawingOffset), new Point(15 + drawingOffset, 10 + drawingOffset) };
            graphic.DrawLines(pen, points2);
        }

        private void DrawMaximize(Graphics graphic, Color color, int drawingOffset)
        {
            graphic.DrawLine(new Pen(color, 1f), 12, 8, 0x16, 8);
            graphic.DrawRectangle(new Pen(color, 1f), 12, 9, 10, 8);
        }

        private void DrawMinimize(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            graphic.DrawLine(pen, (int) (6 + drawingOffset), (int) (13 + drawingOffset), (int) (0x10 + drawingOffset), (int) (13 + drawingOffset));
        }

        private void DrawPause(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            graphic.DrawLine(pen, (int) (15 + drawingOffset), (int) (4 + drawingOffset), (int) (15 + drawingOffset), (int) (10 + drawingOffset));
            graphic.DrawLine(pen, (int) (10 + drawingOffset), (int) (4 + drawingOffset), (int) (10 + drawingOffset), (int) (10 + drawingOffset));
        }

        private void DrawPlay(Graphics graphic, Color color, int drawingOffset)
        {
            SolidBrush brush = new SolidBrush(color);
            Point[] points = new Point[] { new Point(8 + drawingOffset, 3 + drawingOffset), new Point(15 + drawingOffset, 7 + drawingOffset), new Point(8 + drawingOffset, 11 + drawingOffset), new Point(8 + drawingOffset, 3 + drawingOffset) };
            graphic.FillPolygon(brush, points);
        }

        private void DrawRestore(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            Point[] points = new Point[] { new Point(9 + drawingOffset, 4 + drawingOffset), new Point(0x11 + drawingOffset, 4 + drawingOffset), new Point(0x11 + drawingOffset, 10 + drawingOffset), new Point(13 + drawingOffset, 10 + drawingOffset) };
            graphic.DrawLine(pen, (int) (9 + drawingOffset), (int) (3 + drawingOffset), (int) (0x11 + drawingOffset), (int) (3 + drawingOffset));
            graphic.DrawLines(pen, points);
            graphic.DrawLine(pen, (int) (9 + drawingOffset), (int) (4 + drawingOffset), (int) (9 + drawingOffset), (int) (5 + drawingOffset));
            graphic.DrawLine(pen, (int) (5 + drawingOffset), (int) (7 + drawingOffset), (int) (13 + drawingOffset), (int) (7 + drawingOffset));
            graphic.DrawRectangle(pen, 5 + drawingOffset, 8 + drawingOffset, 8, 6);
        }

        private void DrawSpeakerMuted(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            SolidBrush brush = new SolidBrush(color);
            graphic.FillRectangle(brush, 3 + drawingOffset, 3 + drawingOffset, 2, 9);
            Point[] points1 = new Point[] { new Point(5 + drawingOffset, 6 + drawingOffset), new Point(12 + drawingOffset, 3 + drawingOffset), new Point(12 + drawingOffset, 11 + drawingOffset), new Point(5 + drawingOffset, 8 + drawingOffset) };
            graphic.DrawLines(pen, points1);
        }

        private void DrawSpeakerUnmuted(Graphics graphic, Color color, int drawingOffset)
        {
            this.DrawSpeakerMuted(graphic, color, drawingOffset);
            this.DrawUnmuted(graphic, color, drawingOffset);
        }

        private void DrawStop(Graphics graphic, Color color, int drawingOffset)
        {
            SolidBrush brush = new SolidBrush(color);
            graphic.FillRectangle(brush, 9 + drawingOffset, 4 + drawingOffset, 7, 7);
        }

        private void DrawUnmuted(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            graphic.DrawLine(pen, (int) (15 + drawingOffset), (int) (5 + drawingOffset), (int) (20 + drawingOffset), (int) (3 + drawingOffset));
            graphic.DrawLine(pen, (int) (15 + drawingOffset), (int) (7 + drawingOffset), (int) (20 + drawingOffset), (int) (7 + drawingOffset));
            graphic.DrawLine(pen, (int) (15 + drawingOffset), (int) (9 + drawingOffset), (int) (20 + drawingOffset), (int) (11 + drawingOffset));
        }

        private void DrawVolumeDown(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            Point[] myTempVariable = new Point[] { new Point(8 + drawingOffset, 4 + drawingOffset), new Point(11 + drawingOffset, 7 + drawingOffset), new Point(14 + drawingOffset, 4 + drawingOffset) };
            Point[] points1 = myTempVariable;
            graphic.DrawLines(pen, points1);
            myTempVariable = new Point[] { new Point(8 + drawingOffset, 9 + drawingOffset), new Point(11 + drawingOffset, 12 + drawingOffset), new Point(14 + drawingOffset, 9 + drawingOffset) };
            Point[] points2 = myTempVariable;
            graphic.DrawLines(pen, points2);
        }

        private void DrawVolumeUp(Graphics graphic, Color color, int drawingOffset)
        {
            Pen pen = new Pen(color, 1f);
            Point[] myTempVariable = new Point[] { new Point(7 + drawingOffset, 7 + drawingOffset), new Point(10 + drawingOffset, 4 + drawingOffset), new Point(13 + drawingOffset, 7 + drawingOffset) };
            Point[] points1 = myTempVariable;
            graphic.DrawLines(pen, points1);
            myTempVariable = new Point[] { new Point(7 + drawingOffset, 12 + drawingOffset), new Point(10 + drawingOffset, 9 + drawingOffset), new Point(13 + drawingOffset, 12 + drawingOffset) };
            Point[] points2 = myTempVariable;
            graphic.DrawLines(pen, points2);
        }

        private Point[] GetBorderPoints(Rectangle r)
        {
            return new Point[] { new Point(r.Left, r.Height - 1), new Point(r.Left, r.Top), new Point(r.Width, r.Top) };
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.Transparent;
            this.ForeColor = SystemColors.ButtonFace;
            base.Name = "UnstyledButton";
            base.Size = new Size(60, 0x2c);
            base.Enter += new EventHandler(this.UnstyledButton_Enter);
            base.Leave += new EventHandler(this.UnstyledButton_Leave);
            base.MouseEnter += new EventHandler(this.UnstyledButton_MouseEnter);
            base.ResumeLayout(false);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.mouseIsDown = true;
            base.OnMouseDown(e);
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.mouseIsDown = false;
            base.OnMouseLeave(e);
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.mouseIsDown = false;
            base.OnMouseUp(e);
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle r = base.ClientRectangle;
            int drawingOffset = 4;
            if (this.mouseIsDown)
            {
                ControlPaint.DrawBorder3D(e.Graphics, r, Border3DStyle.Sunken);
            }
            else
            {
                this.DrawBorder(e.Graphics);
            }
            Pen pen = new Pen(this.BackColor, 1f);
            if (this.Focused)
            {
                Color b = ControlPaint.LightLight(this.BackColor);
                pen.Color = SystemColors.ButtonFace;
                pen.DashStyle = DashStyle.Dot;
                r.Inflate(drawingOffset * -1, drawingOffset * -1);
                e.Graphics.DrawRectangle(pen, r);
            }
            this.DrawButtonType(e.Graphics, SystemColors.ButtonFace, drawingOffset);
        }

        internal void PerformClick()
        {
            base.OnClick(new EventArgs());
        }

        private void UnstyledButton_Enter(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        private void UnstyledButton_Leave(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        private void UnstyledButton_MouseEnter(object sender, EventArgs e)
        {
            base.Focus();
        }

        public DPlayerButtonTypes DPlayerButtonType
        {
            get
            {
                return this.dPlayerButtonType;
            }
            set
            {
                this.dPlayerButtonType = value;
                base.Invalidate();
            }
        }
    }
}

