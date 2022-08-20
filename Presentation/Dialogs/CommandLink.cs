﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Scover.WinClean.Presentation.Dialogs;

public class CommandLink : INotifyPropertyChanged
{
    private bool _isEnabled = true;
    private string _note = string.Empty;
    private string _text = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            OnPropertyChanged();
        }
    }

    public string Note
    {
        get => _note;
        set
        {
            _note = value;
            OnPropertyChanged();
        }
    }

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string callerMemberName = "") => PropertyChanged?.Invoke(this, new(callerMemberName));
}