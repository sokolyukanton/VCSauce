using System;
using System.Windows;
using System.Windows.Forms;
using VCSauce.Data.Managers;
using MessageBox = System.Windows.MessageBox;

namespace VCSauce.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RepositoryManager manager=new RepositoryManager();

        StorageManager storageManager=new StorageManager();

        public MainWindow()
        {
            InitializeComponent();
            //FolderBrowserDialog d = new FolderBrowserDialog();
            //d.ShowDialog();
            //MessageBox.Show(string.Join(Environment.NewLine, storageManager.GetFilesFromDirectory(d.SelectedPath)));
            manager.CreateRepository(@"I:\ВУЗ\Диплом", @"I:\ВУЗ\Диплом\Storage", "Name");
        }
    }
}
