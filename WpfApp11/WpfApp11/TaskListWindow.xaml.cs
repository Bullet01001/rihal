using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace YourNamespace
{
    public partial class TaskListWindow : Window
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }
        private readonly ICollectionView tasksView;

        public TaskListWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskItem>
            {
                new TaskItem { TaskName = "Task 1", IsCompleted = false },
                new TaskItem { TaskName = "Task 2", IsCompleted = true },
                
            };

            tasksView = CollectionViewSource.GetDefaultView(Tasks);
            lstTasks.ItemsSource = tasksView;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string newTaskName = txtNewTask.Text;
            if (!string.IsNullOrEmpty(newTaskName))
            {
                Tasks.Add(new TaskItem { TaskName = newTaskName, IsCompleted = false });
                txtNewTask.Clear();
            }
        }
    }

    public class TaskItem : INotifyPropertyChanged
    {
        private string taskName;
        private bool isCompleted;

        public string TaskName
        {
            get { return taskName; }
            set
            {
                taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }

        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
