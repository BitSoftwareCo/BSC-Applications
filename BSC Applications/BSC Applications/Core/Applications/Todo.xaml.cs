﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BSC_Applications.Core.Applications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Todo
    {
        private string startMessage = "Enter a Task and click \"New\" to add a Task.";

        public Todo()
        {
            this.InitializeComponent();

            Message.Text = startMessage;

            if (lib.Var.todoContent.Count != 0)
            {
                for (int i = 0; i < lib.Var.todoContent.Count; i++)
                {
                    CheckBox item = new CheckBox();
                    item.Content = lib.Var.todoContent[i];
                    List.Items.Add(item);
                }
                Message.Text = "";
            }
        }

        private void Task_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            add(sender, e);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            add(sender, e);
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
                Message.Text = "";

                List.Items.Clear();

                string text = await FileIO.ReadTextAsync(file);
                if (file.Path.Contains(".todo"))
                    OpenTODO(text);
                else
                    OpenBOF(text);
            }
        }
        private async void New_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Are you sure you want to create a new Todo List?",
                Content = "Creating a new Todo List will delete any unsaved tasks.",
                PrimaryButtonText = "New",
                SecondaryButtonText = "Cancel"
            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                Message.Text = startMessage;

                List.Items.Clear();
                lib.Var.todoContent.Clear();
            }
        }
        private async void Help_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://bitsoftwareco.github.io/docs/BSC-Applications.html#todo"));
        }

        private void add(object sender, object e)
        {
            string element = sender.ToString();
            if (element.Contains("AppBarButton"))
            {
                if (Task.Text != "")
                {
                    Message.Text = "";
                    CheckBox item = new CheckBox();
                    item.Content = Task.Text;

                    List.Items.Add(item);
                    lib.Var.todoContent.Add((string)item.Content);

                    Task.Text = "";
                }
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
                    lib.Var.todoContent.Add((string)item.Content);
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
                lib.Var.todoContent.Add((string)item.Content);
            }
        }
    }
}