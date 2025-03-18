using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using LinearListApp.Models;

namespace LinearListApp.ViewModels
{
    public class LinearListViewModel : ViewModelBase
    {
        private string _currentItem = "Нет элементов";
        private string _status = "Список пуст";


        public string CurrentItem
        {
            get => _currentItem;
            set => this.RaiseAndSetIfChanged(ref _currentItem, value);
        }

        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public ObservableCollection<string> Items { get; } = new();
        private LinearList<string> _linearList = new();

        public ReactiveCommand<string, Unit> AddItem { get; }
        public ReactiveCommand<string, Unit> RemoveItem { get; }
        public ReactiveCommand<Unit, Unit> MoveNext { get; }
        public ReactiveCommand<Unit, Unit> Reset { get; }
        public ReactiveCommand<Unit, Unit> ClearList { get; }

        public LinearListViewModel()
        {
            AddItem = ReactiveCommand.Create<string>(AddItemAction);
            RemoveItem = ReactiveCommand.Create<string>(RemoveItemAction);
            MoveNext = ReactiveCommand.Create(MoveNextAction);
            Reset = ReactiveCommand.Create(ResetAction);
            ClearList = ReactiveCommand.Create(ClearListAction);
        }

        private void AddItemAction(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                Status = "Ошибка: нельзя добавить пустой элемент!";
                return;
            }
            _linearList.Add(item);
            Items.Add(item);
           
            UpdateStatus();
        }

        private void RemoveItemAction(string item)
        {
            if (_linearList.Remove(item))
                Items.Remove(item);
            UpdateStatus();
        }

        private void MoveNextAction()
        {
            if (_linearList.MoveNext())
                CurrentItem = _linearList.CurrentItem ?? "Нет элементов";
            else
                Status = "Достигнут конец списка";
        }

        private void ResetAction()
        {
            _linearList.Reset();
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            CurrentItem = _linearList.CurrentItem ?? "Нет элементов";
            Status = _linearList.IsEmpty ? "Список пуст" : $"Количество элементов: {_linearList.Count}";
        }

        private void ClearListAction()
        {
            _linearList = new LinearList<string>();
            Items.Clear();
            UpdateStatus();
        }
    }
}
