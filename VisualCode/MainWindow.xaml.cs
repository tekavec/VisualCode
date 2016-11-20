using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VisualCode
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                e.Effects = e.KeyStates == DragDropKeyStates.ControlKey ? DragDropEffects.Copy : DragDropEffects.Move;
            }
        }

        private void panel_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled) return;
            var panel = (Panel)sender;
            var element = (UIElement)e.Data.GetData("Object");

            if (panel == null || element == null) return;
            var parent = (Panel)VisualTreeHelper.GetParent(element);

            if (parent == null) return;
            if (e.KeyStates == DragDropKeyStates.ControlKey &&
                e.AllowedEffects.HasFlag(DragDropEffects.Copy))
            {
                var circle = new ClassView((ClassView)element);
                panel.Children.Add(circle);
                e.Effects = DragDropEffects.Copy;
            }
            else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
            {
                parent.Children.Remove(element);
                panel.Children.Add(element);
                e.Effects = DragDropEffects.Move;
            }
        }
    }
}