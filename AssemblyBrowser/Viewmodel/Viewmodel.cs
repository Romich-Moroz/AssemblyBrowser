﻿using DisassemblerLib;
using System.ComponentModel;
using System.Windows.Input;

namespace AssemblyBrowser
{
    class Viewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Model model = new Model();

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public AssemblyInfo SelectedAssemblyInfo { get; set; }

        public ICommand SelectAssembly { get; }

        public Viewmodel()
        {
            this.SelectAssembly = new RelayCommand<object>((parameter) =>{ SelectedAssemblyInfo = model.GetAssemblyInfo() ?? SelectedAssemblyInfo; }, null);
        }
    }
}
