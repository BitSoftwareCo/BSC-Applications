using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BSC_Applications.Core.Applications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Phone_Book
    {
        private string startMessage = "Enter a Name and Number then click \"Add\" to add a Number.";

        public Phone_Book()
        {
            this.InitializeComponent();

            Message.Text = startMessage;

            if (lib.Var.phoneBookContent.Count != 0)
            {
                for (int i = 0; i < lib.Var.phoneBookContent.Count; i++)
                {
                    string item = lib.Var.phoneBookContent[i];
                    List.Items.Add(item);
                }
                Message.Text = "";
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "" && Number.Text != "")
            {
                Message.Text = "";
                string item = $"{Name.Text} - {Number.Text}";
                List.Items.Add(item);
                lib.Var.phoneBookContent.Add(item);

                Name.Text = "";
                Number.Text = "";
            }
        }
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("BSC OBJ File", new List<string>() { ".bof" });
            savePicker.FileTypeChoices.Add("BA Phone Book", new List<string>() { ".bpb" });
            savePicker.SuggestedFileName = "Untitled Phone Book";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                string text = "";
                if (file.Path.Contains(".bpb"))
                    text = SaveBPB(text);
                else
                    text = SaveBOF(text);

                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, text);
                await CachedFileManager.CompleteUpdatesAsync(file);
            }
        }
        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker open = new FileOpenPicker();
            open.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".bof");
            open.FileTypeFilter.Add(".bpb");

            StorageFile file = await open.PickSingleFileAsync();
            if (file != null)
            {
                Message.Text = "";

                List.Items.Clear();

                string text = await FileIO.ReadTextAsync(file);
                if (file.Path.Contains(".bpb"))
                    ReadBPB(text);
                else
                    ReadBOF(text);
            }
        }
        private async void New_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Are you sure you want to create a new Phone Book?",
                Content = "Creating a new Phone Book will delete any unsaved numbers.",
                PrimaryButtonText = "New",
                SecondaryButtonText = "Cancel"
            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                Message.Text = startMessage;

                List.Items.Clear();
                lib.Var.phoneBookContent.Clear();
            }
        }
        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#phone-book"));
        }

        private string SaveBPB(string text)
        {
            for (int i = 0; i < List.Items.Count; i++)
            {
                text += $"{List.Items[i]}&";
            }
            text += "␀";
            return text;
        }
        private string SaveBOF(string text)
        {
            List<object> items = new List<object>();
            for (int i = 0; i < List.Items.Count; i++)
            {
                items.Add(List.Items[i]);
            }
            return (string)lib.FileType.WriteBOF(items.ToArray());
        }

        private void ReadBPB(string text)
        {
            string[] numbers = text.Split("&");
            for (int i = 0; i < numbers.Length; i++)
            {
                if (!numbers[i].Contains("␀"))
                {
                    string item = numbers[i];
                    List.Items.Add(item);
                    lib.Var.phoneBookContent.Add(item);
                }
            }
        }
        private void ReadBOF(string text)
        {
            string[] numbers = (string[])lib.FileType.ReadBOF(text);
            for (int i = 0; i < numbers.Length; i++)
            {
                string item = numbers[i];
                List.Items.Add(item);
                lib.Var.phoneBookContent.Add(item);
            }
        }
    }

}