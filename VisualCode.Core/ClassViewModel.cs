namespace VisualCode.Core
{
    public class ClassViewModel : ViewModelBase
    {
        private string name;

        public ClassViewModel()
        {
        }

        public ClassViewModel(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                {
                    return;
                }

                name = value;

                OnPropertyChanged("Name");
            }
        }

        public double X { get; set; }

        public double Y { get; set; }
    }
}