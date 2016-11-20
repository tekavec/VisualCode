using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace VisualCode
{
    public partial class ClassView
    {
        private Brush previousFill;

        public ClassView()
        {
            InitializeComponent();
        }

        public ClassView(ClassView classView)
        {
            InitializeComponent();
            ClassViewUi.Height = classView.ClassViewUi.Height;
            ClassViewUi.Width = classView.ClassViewUi.Width;
            ClassViewUi.Fill = classView.ClassViewUi.Fill;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var data = new DataObject();
            data.SetData(DataFormats.StringFormat, ClassViewUi.Fill.ToString());
            data.SetData("Double", ClassViewUi.Width);
            data.SetData("Double", ClassViewUi.Height);
            data.SetData("Object", this);
            DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                Mouse.SetCursor(Cursors.Cross);
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                Mouse.SetCursor(Cursors.Pen);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                var dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                var converter = new BrushConverter();
                if (dataString != null && converter.IsValid(dataString))
                {
                    var newFill = (Brush)converter.ConvertFromString(dataString);
                    ClassViewUi.Fill = newFill;
                    e.Effects = e.KeyStates.HasFlag(DragDropKeyStates.ControlKey) ? DragDropEffects.Copy : DragDropEffects.Move;
                }
            }
            e.Handled = true;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            e.Effects = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                var dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                var converter = new BrushConverter();
                if (dataString != null && converter.IsValid(dataString))
                {
                    e.Effects = e.KeyStates.HasFlag(DragDropKeyStates.ControlKey) ? DragDropEffects.Copy : DragDropEffects.Move;
                }
            }
            e.Handled = true;
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            previousFill = ClassViewUi.Fill;
            if (!e.Data.GetDataPresent(DataFormats.StringFormat)) return;
            var dataString = (string)e.Data.GetData(DataFormats.StringFormat);
            var converter = new BrushConverter();
            if (dataString == null || !converter.IsValid(dataString)) return;
            var newFill = (Brush)converter.ConvertFromString(dataString);
            ClassViewUi.Fill = newFill;
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);
            ClassViewUi.Fill = previousFill;
        }
    }
}