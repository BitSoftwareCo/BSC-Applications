using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BSC_Applications.Page.Applications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Phone_Book
    {
        public Phone_Book()
        {
            this.InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != null && Number.Text != null)
            {
                string item = $"{Name.Text} - {Number.Text}";
                List.Items.Add(item);
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("BA Phone Book", new List<string>() { ".bpb" });
            savePicker.SuggestedFileName = "Untitled Phone Book";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                string text = "";
                for (int i = 0; i < List.Items.Count; i++)
                {
                    text += $"{List.Items[i]}&";
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
            open.FileTypeFilter.Add(".bpb");

            StorageFile file = await open.PickSingleFileAsync();
            if (file != null)
            {
                string text = await FileIO.ReadTextAsync(file);
                string[] numbers = text.Split("&");
                for(int i = 0; i < numbers.Length; i++)
                {
                    if (!numbers[i].Contains("␀"))
                    {
                        string item = numbers[i];
                        List.Items.Add(item);
                    }
                }
            }
        }

        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#phone-book");
            await Launcher.LaunchUriAsync(uri);
        }
    }

}
