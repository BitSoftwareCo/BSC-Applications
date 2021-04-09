using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BSC_Applications.Page.Applications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Todo
    {
        public Todo()
        {
            this.InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Task.Text != "")
            {
                Message.Text = "";
                CheckBox item = new CheckBox();
                item.Content = Task.Text;

                List.Items.Add(item);
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("BSC Todo File", new List<string>() { ".todo" });
            savePicker.SuggestedFileName = "Untitled Todo List";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                string text = "";
                for (int i = 0; i < List.Items.Count; i++)
                {
                    CheckBox item = (CheckBox)List.Items[i];
                    if ((bool)!item.IsChecked)
                        text += $"{item.Content}&";
                }
                text += "␀";

                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, text);
                await CachedFileManager.CompleteUpdatesAsync(file);
            }
        }

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker open = new FileOpenPicker();
            open.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".todo");

            StorageFile file = await open.PickSingleFileAsync();
            if (file != null)
            {
                Message.Text = "";

                string text = await FileIO.ReadTextAsync(file);
                string[] items = text.Split("&");
                for (int i = 0; i < items.Length; i++)
                {
                    if (!items[i].Contains("␀"))
                    {
                        CheckBox item = new CheckBox();
                        item.Content = items[i];
                        List.Items.Add(item);
                    }
                }
            }
        }
    }
}
