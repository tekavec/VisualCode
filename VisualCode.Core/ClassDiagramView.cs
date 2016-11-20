using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace VisualCode.Core
{
    public class ClassDiagramView : Control
    {

        public static readonly DependencyProperty ClassesSourceProperty =
            DependencyProperty.Register("ClassesSource", typeof(IEnumerable), typeof(ClassViewModel),
                new FrameworkPropertyMetadata(ClassesSource_PropertyChanged));


        public ObservableCollection<object> Classes
        {
            get
            {
                return (ObservableCollection<object>)GetValue(ClassesSourceProperty);
            }
            private set
            {
                SetValue(ClassesSourceProperty, value);
            }
        }

        private static void ClassesSource_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ClassDiagramView c = (ClassDiagramView)d;
            c.Classes.Clear();
            if (e.OldValue != null)
            {
                var notifyCollectionChanged = e.OldValue as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                {
                    notifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(c.ClassesSource_CollectionChanged);
                }
            }
            if (e.NewValue != null)
            {
                var enumerable = e.NewValue as IEnumerable;
                if (enumerable != null)
                {
                    foreach (object obj in enumerable)
                    {
                        c.Classes.Add(obj);
                    }
                }
                var notifyCollectionChanged = e.NewValue as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                {
                    notifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(c.ClassesSource_CollectionChanged);
                }
            }
        }

        private void ClassesSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Classes.Clear();
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (object obj in e.OldItems)
                    {
                        Classes.Remove(obj);
                    }
                }

                if (e.NewItems != null)
                {
                    foreach (object obj in e.NewItems)
                    {
                        Classes.Add(obj);
                    }
                }
            }
        }

    }
}