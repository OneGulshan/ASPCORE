using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Models
{
    public class Repositorys
    {
        public List<Country> Countries { get; set; }
        public List<State> States { get; set; }

        public Repositorys()
        {
            Countries = new List<Country>()
            {
                new Country{Id=1,Name="A"},
                new Country{Id=2,Name="B"},
            };
            States = new List<State>()
            {
                new State{Id=1,Name="S1",CountryId=1},
                new State{Id=2,Name="S2",CountryId=1},
                new State{Id=3,Name="S3",CountryId=1},
                new State{Id=4,Name="S4",CountryId=1},
                new State{Id=5,Name="S5",CountryId=2},
                new State{Id=6,Name="S6",CountryId=2},
            };
        }
    }
}
