using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Numbers Converter";

            foreach (var i in LayoutRoot.Children)
            {
                if (i is Button)
                {
                    ((Button)i).Click += Button_Click;
                }
            }
        }
        private String TransformNumber(String number, int typeFrom, int typeTo)
        {
            string result = null;

            bool IsInputValid(String input, int @base)
            {
                try
                {
                    Convert.ToInt64(input, @base);
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }

            if (IsInputValid(number, typeFrom))
            {
                long @decimal = 0;
                try
                {
                    @decimal = Convert.ToInt64(number, typeFrom);
                }
                catch (StackOverflowException)
                {
                    return null;
                }
                finally
                {
                    result = Convert.ToString(@decimal, typeTo);
                }
            }
            

            return result;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string convertText = (string)((Button)e.OriginalSource).Content;

            int typeFrom = Convert.ToInt32(convertText.Substring(0, ((Func<int>) ( () => { return convertText.IndexOf('-'); } ))()));
            int typeTo = Convert.ToInt32(convertText.Substring(((Func<int>)(() => { return convertText.IndexOf('>') + 1; }))()));

            string result = TransformNumber(Number.Text, typeFrom, typeTo);

            Number.Text = result ?? Number.Text;
        }
    }
}
