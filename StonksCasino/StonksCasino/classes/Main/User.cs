using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Main
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string _name;

        public string MyName
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private int _tokens;

        public int MyTokens
        {
            get { return _tokens; }
            set { _tokens = value; OnPropertyChanged(); }
        }
        public User(string Name, int Tokens)
        {
            MyName = Name;
            MyTokens = Tokens;
        }

    }
}
