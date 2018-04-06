using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Core.Annotations;

namespace Core
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private int _selectedIndex;
        private int _selectedIndex1;

        public event PropertyChangedEventHandler PropertyChanged;


        public MainViewModel()
        {
            VerifyDataCollection = new ObservableCollection<AccountData>();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Login
        {
            get => _login;
            set
            {
                if (value == _login) return;
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AccountData> VerifyDataCollection { get; }

        public AccountData SelectedItem
        {
            get => null;
            set
            {
                Debug.WriteLine(value?.GetHashCode());
                if (value == null) return;
                SelectedIndex = -1;
                VerifyDataCollection.Remove(value); 
            }
        }

        public int SelectedIndex
        {
            get => -1;
            set
            {
                if (value == _selectedIndex1) return;
                _selectedIndex1 = value;
                OnPropertyChanged();
            }
        }

        public ICommand VerifyDataCommand => new Command(() =>
        {
            var account = new AccountData(_login, _password);
            if (_login == "a" && _password == "b")
            {
                account.Verified = true;
            }
            else
            {
                account.Verified = false;
            }

            VerifyDataCollection.Add(account);
        });
    }

    public class AccountData
    {
        public string Login { get; }
        public string Password { get; }
        public bool Verified { get; set; }

        public AccountData(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            var valid = Verified ? "valid" : "invalid";
            return $"{Login} and {Password} are {valid}";
        }
    }

    public class Command : ICommand
    {
        private readonly Action _action;

        public Command(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
                return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
       
        }

        public event EventHandler CanExecuteChanged;


    }
}
