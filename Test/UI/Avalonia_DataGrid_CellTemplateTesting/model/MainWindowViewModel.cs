using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;

namespace Avalonia_DataGrid_CellTemplateTesting.model
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        public ReactiveCommand<Unit, Unit> onFilterPeopleButtonClickCommand { get; }
        public ReactiveCommand<Unit, Unit> onRandomizePeopleCommand { get; }

        public ObservableCollection<Person> People
        {
            get { return GetValue(() => People); }
            set { SetValue(() => People, value);}
        }

        public string Filter
        {
            get { return GetValue(() => Filter); }
            set { SetValue(() => Filter, value); }
        }

        private List<Person> allPeople;

        public MainWindowViewModel()
        {
            this.People = new ObservableCollection<Person>();
            this.Filter = ""; // can't start out null
            
            /* documentation on how reactive commands work
               + see: https://avaloniaui.net/docs/binding/binding-to-commands
            */
            this.onFilterPeopleButtonClickCommand = ReactiveCommand.Create(onFilterPeopleButtonClick);
            this.onRandomizePeopleCommand = ReactiveCommand.Create(onRandomizePeople);
        }

        private void onFilterPeopleButtonClick()
        {
            if (this.allPeople == null)
            {
                this.allPeople = new List<Person>();
            }
            if (this.allPeople.Count == 0)
            {
                // copy everything into allPeople
                this.allPeople.AddRange(this.People);
            }
            
            this.People.Clear();

            var filtered = from l in allPeople
                where l.Firstname.Contains(Filter, StringComparison.OrdinalIgnoreCase) ||
                      l.Lastname.Contains(Filter, StringComparison.OrdinalIgnoreCase)
                select l;
            
            // now apply that back to Letters
            foreach (var l in filtered)
            {
                this.People.Add(l);
            }
        }

        private void onRandomizePeople()
        {
            this.People.Clear();
            
            // generate names that we can filter
            var firstNames = new[]
            {
                "Sandy", "Paula", "Barbara", "Nathan", "Nathaniel", "Brian", "Jordan", "Cynthia", "George", "Peter",
                "James", "John", "Blake", "Paul"
            };
            var lastNames = new[]
            {
                "Kapales", "Johnson", "Harris", "Hill", "Collier", "Sampson", "Skywalker", "McCarthy", "Joseph",
                "Miracle", "DeVos", "Rubar", "Pie", "Sky", "Bubblegum"
            };
                
            // form 100 random names
            var r = new System.Random();
            for (int i = 0; i < 100; ++i)
            {
                var person = new model.Person()
                {
                    Firstname = firstNames[r.Next(0, firstNames.Length - 1)],
                    Lastname = lastNames[r.Next(0, lastNames.Length - 1)]
                };
                this.People.Add(person);
            }
        }
        
        
        
    }
}