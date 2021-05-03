using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.ItemsControl.SimpleTesT.AddItemWithButton.models;
using ReactiveUI;

namespace Avalonia.ItemsControl.SimpleTesT.AddItemWithButton.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<models.MyItemType> MyListofStuff { get; set; }
        public models.MyItemType newItem { get; set; }
        public ReactiveCommand<Unit, Unit> AddNewItemToMyListOfStuffCommand { get; }

        public MainWindowViewModel()
        {
            this.MyListofStuff = new ObservableCollection<MyItemType>();
            this.newItem = new models.MyItemType();

            // documentation on how reactive commands work
            //   + see: https://avaloniaui.net/docs/binding/binding-to-commands
            this.AddNewItemToMyListOfStuffCommand = ReactiveCommand.Create(AddNewItemToMyListOfStuff);
        }

        void AddNewItemToMyListOfStuff()
        {
            this.MyListofStuff.Add(new models.MyItemType()
            {
                Name = this.newItem.Name
            });
            this.newItem.Name = "";
        }
    }
}