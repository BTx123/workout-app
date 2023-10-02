﻿namespace WorkoutApp.Controls;

public class SettingsSearchHandler : SearchHandler
{
    public IList<string> SearchStrings { get; set; } = new List<string>();

    // public Type SelectedItemNavigationTarget { get; set; }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = SearchStrings
                .Where(setting => setting.ToLower().Contains(newValue.ToLower()))
                .ToList();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        // // Let the animation complete
        // await Task.Delay(1000);
        //
        // var state = (App.Current.MainPage as Shell).CurrentState;
        // // The following route works because route names are unique in this app.
        // await Shell.Current.GoToAsync($"{GetNavigationTarget()}?name={((Animal)item).Name}");
    }

    string GetNavigationTarget()
    {
        return string.Empty;
        // return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
    }
}