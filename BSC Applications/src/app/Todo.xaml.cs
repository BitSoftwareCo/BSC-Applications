using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BSC_Applications.src.app
{

    public sealed partial class Todo
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
        private bool temp;

        private int selectedIndex = -1;
        private List<string> todoList = new List<string>();

        public Todo()
        {
            temp = Boolean.Parse(roamingSettings.Values["temporaryContent"].ToString());

            this.InitializeComponent();

            todoList = lib.Var.todoContent;
            if (todoList.Count != 0 && temp)
            {
                for (int i = 0; i < todoList.Count; i++)
                {
                    CheckBox item = new CheckBox();
                    item.Content = lib.Var.todoContent[i];
                    List.Items.Add(item);
                }
                Message.Visibility = Visibility.Collapsed;
                Remove.IsEnabled = true;
            }

            MainPage.nav.ItemInvoked += Nav_ItemInvoked;

            new lib.Events("Todo Loaded", 0);
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedIndex = List.SelectedIndex;
        }

        private void Task_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter) add();
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex != -1)
            {
                List.Items.RemoveAt(selectedIndex);

                if (List.Items.Count == 0)
                {
                    Message.Visibility = Visibility.Visible;
                    Remove.IsEnabled = false;
                }
            }
        }
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("BSC OBJ File", new List<string>() { ".bof" });
            savePicker.FileTypeChoices.Add("BSC Todo File", new List<string>() { ".todo" });
            savePicker.SuggestedFileName = "Untitled Todo List";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                string text = "";
                if (file.Path.Contains(".todo"))
                    text = SaveTODO(text);
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
            open.FileTypeFilter.Add(".todo");

            StorageFile file = await open.PickSingleFileAsync();
            if (file != null)
            {
                Message.Visibility = Visibility.Collapsed;

                List.Items.Clear();

                string text = await FileIO.ReadTextAsync(file);
                if (file.Path.Contains(".todo"))
                    OpenTODO(text);
                else
                    OpenBOF(text);

                if (List.Items.Count == 0)
                {
                    Remove.IsEnabled = false;
                }
                else Remove.IsEnabled = true;
            }
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = (Button)sender;
            if (senderButton.Name == "NewFlyout_New") 
            {
                Message.Visibility = Visibility.Visible;
                Remove.IsEnabled = false;

                List.Items.Clear();
                todoList.Clear();
            }
            NewFlyout.Hide();
        }

        private void add()
        {
            Remove.IsEnabled = true;
            if (Task.Text != "")
            {
                Message.Visibility = Visibility.Collapsed;
                CheckBox item = new CheckBox();
                item.Content = Task.Text;

                List.Items.Add(item);
                todoList.Add((string)item.Content);

                Task.Text = "";
            }
        }

        private string SaveTODO(string text)
        {
            for (int i = 0; i < List.Items.Count; i++)
            {
                CheckBox item = (CheckBox)List.Items[i];
                if ((bool)!item.IsChecked)
                    text += $"{item.Content}&";
            }
            text += "␀";
            return text;
        }
        private string SaveBOF(string text)
        {
            List<object> items = new List<object>();
            for (int i = 0; i < List.Items.Count; i++)
            {
                CheckBox item = (CheckBox)List.Items[i];
                if ((bool)!item.IsChecked)
                    items.Add(item.Content);
            }
            return (string)lib.FileType.WriteBOF(items.ToArray());
        }

        private void OpenTODO(string text)
        {
            string[] items = text.Split("&");
            for (int i = 0; i < items.Length; i++)
            {
                if (!items[i].Contains("␀"))
                {
                    CheckBox item = new CheckBox();
                    item.Content = items[i];
                    List.Items.Add(item);
                    todoList.Add((string)item.Content);
                }
            }
        }
        private void OpenBOF(string text)
        {
            string[] items = (string[])lib.FileType.ReadBOF(text);
            for (int i = 0; i < items.Length; i++)
            {
                CheckBox item = new CheckBox();
                item.Content = items[i];
                List.Items.Add(item);
                todoList.Add((string)item.Content);
            }
        }

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (temp)
                lib.Var.todoContent = todoList;
        }
    }
}