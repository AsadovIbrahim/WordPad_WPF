using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordPad_WPF
{
    public partial class MainWindow : Window
    {
        string alltext;
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            richtextBox.SelectAll();
            if (richtextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty) is FontWeight fontWeight &&
                fontWeight.Equals(FontWeights.Bold))
            {
                richtextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                richtextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }

        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            richtextBox.SelectAll();
            if (richtextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty) is FontStyle fontStyle && fontStyle.Equals(FontStyles.Italic)){
                richtextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                richtextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }
        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            richtextBox.SelectAll();
            if (richtextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) is TextDecorationCollection textDecorations &&
        textDecorations.Equals(TextDecorations.Underline))
            {
                richtextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,null);
            }
            else
            {
                richtextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void SelectAll_ButtonClick(object sender, RoutedEventArgs e)
        {
            richtextBox.SelectAll();
        }

        private void Paste_ButtonClick(object sender, RoutedEventArgs e)
        {
            richtextBox.Paste();
        }

        private void Copy_ButtonClick(object sender, RoutedEventArgs e)
        {
            richtextBox.Copy();
        }

        private void Cut_ButtonClick(object sender, RoutedEventArgs e)
        {
            richtextBox.Cut();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void Save_ButtonClick(object sender, RoutedEventArgs e)
        {
            
        }
        private void SaveAs_ButtonClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; 
            if(saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    string contentToSave = new TextRange(richtextBox.Document.ContentStart, richtextBox.Document.ContentEnd).Text;
                    File.WriteAllText(filePath, contentToSave);

                    System.Windows.MessageBox.Show("File Saved Succesfully!");

                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }

            }
        }
        private void Load_ButtonClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "All files|*.*|Text Files|*.txt";
            if(openFileDialog.ShowDialog() == true)
            {
               richtextBox.Selection.Text= File.ReadAllText(openFileDialog.FileName);


            }
        }



        private void ForGround_ButtonCLick(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Media.Color selectedColor = System.Windows.Media.Color.FromArgb(
                    colorDialog.Color.A,
                    colorDialog.Color.R,
                    colorDialog.Color.G,
                    colorDialog.Color.B);

                SolidColorBrush brush = new SolidColorBrush(selectedColor);
                richtextBox.Foreground = brush;
            }
        }

        private void Background_ButtonClick(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Media.Color selectedColor = System.Windows.Media.Color.FromArgb(
                    colorDialog.Color.A,
                    colorDialog.Color.R,
                    colorDialog.Color.G,
                    colorDialog.Color.B);

                SolidColorBrush brush = new SolidColorBrush(selectedColor);
                richtextBox.Background = brush;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (richtextBox.FontFamily != null) {
                richtextBox.FontFamily = new FontFamily(((System.Windows.Controls.ComboBox)sender).SelectedItem.ToString());
            }
        } 
        private void sizez_ButtonClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            for (int i = 1; i < 72; i++)
            {

                Sizes.Items.Add(i.ToString());
                
            }
            Sizes.SelectedItem = richtextBox.FontSize;
        }

        private void ComboBox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            if (richtextBox.FontFamily != null)
            {
                double newSize;
                if (double.TryParse(Sizes.SelectedItem.ToString(), out newSize))
                {
                    richtextBox.FontSize = newSize;
                }
            }
        }

        private void richtextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkButton.IsChecked == true)
            {
                alltext = richtextBox.Selection.Text;
            }
        }

       

        private void fontFamily_ButtonClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            FontFamily[] fontFamilies = Fonts.SystemFontFamilies.ToArray();
            comboBoxFontFamily.ItemsSource = fontFamilies;

        }
    }
}
