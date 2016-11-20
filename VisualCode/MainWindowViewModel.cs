using System.Windows;
using VisualCode.Core;

namespace VisualCode
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ClassDiagramView classDiagramView;

        public MainWindowViewModel()
        {
            PopulateData();
        }

        public ClassDiagramView ClassDiagramView
        {
            get
            {
                return classDiagramView;
            }
            set
            {
                classDiagramView = value;

                OnPropertyChanged("ClassDiagramView");
            }
        }

        private void PopulateData()
        {
            ClassDiagramView = new ClassDiagramView();
            var class1 = CreateClass("Class 1", new Point(25, 25));
            var class2 = CreateClass("Class 2", new Point(250, 25));
        }

        private ClassViewModel CreateClass(string name, Point location)
        {
            var classViewModel = new ClassViewModel(name);
            classViewModel.X = location.X;
            classViewModel.Y = location.Y;
            return classViewModel;
        }
    }
}