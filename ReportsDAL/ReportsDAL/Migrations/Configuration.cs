namespace ReportsDAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ReportsDAL.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ReportsDAL.EF.ReportEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReportsDAL.EF.ReportEntities context)
        {

            var executors = new List<Executor>
            {
                new Executor {FirstName = "Світлана", LastName = "Алексєєва", Patronymic = "Василівна",
                    PassportNumber ="AO453445", PassportIssuedBy = "Ленінським РУВД м. Дніпро",
                    PassportDateOfIssue = new DateTime(1975, 3, 4), TIN = "234787639874", Entrepreneur=false,
                    Birthday = new DateTime(1963, 5, 3)},

                new Executor {FirstName = "Петро", LastName = "Куркуль", Patronymic = "Іванович",
                    PassportNumber ="AO157437", PassportIssuedBy = "Ленінським РУВД м. Дніпро",
                    PassportDateOfIssue = new DateTime(1985, 7, 15), TIN = "234663343885", Entrepreneur=false,
                    Birthday = new DateTime(1975, 11, 25)},

                new Executor {FirstName = "Леонід", LastName = "Добрій", Patronymic = "Федорович",
                    PassportNumber ="AO378347", PassportIssuedBy = "Ленінським РУВД м. Дніпро",
                    PassportDateOfIssue = new DateTime(1972, 8, 6), TIN = "345345345344", Entrepreneur=false,
                    Birthday = new DateTime(1967, 3, 18)},

                new Executor {FirstName = "Валентина", LastName = "Краснопільска", Patronymic = "Володимирівна",
                    PassportNumber ="AO148935", PassportIssuedBy = "Ленінським РУВД м. Дніпро",
                    PassportDateOfIssue = new DateTime(1979, 2, 16), TIN = "234353453364", Entrepreneur=false,
                    Birthday = new DateTime(1975, 7, 15)},

                new Executor {FirstName = "Артем", LastName = "Зозуля", Patronymic = "Ігорович",
                    PassportNumber ="AO938025", PassportIssuedBy = "Ленінським РУВД м. Дніпро",
                    PassportDateOfIssue = new DateTime(1996, 12, 29), TIN = "234458537369",Entrepreneur=true,
                    Birthday = new DateTime(1975, 7, 15)},

                new Executor {FirstName = "Олег", LastName = "Аксютенко", Patronymic = "Володимирович",
                    PassportNumber ="AO168340", PassportIssuedBy = "Ленінським РУВД м. Дніпро",
                    PassportDateOfIssue = new DateTime(1986, 3, 4), TIN = "234787639874",Entrepreneur=true,
                    Birthday = new DateTime(1975, 7, 15)},

                new Executor {FirstName = "Микита", LastName = "Гришанов", Patronymic = "Ігорович",
                    PassportNumber ="AO937348", PassportIssuedBy = "Ленінським РУВД м. Дніпро",
                    PassportDateOfIssue = new DateTime(1992, 6, 18), TIN = "274574566674",Entrepreneur=false,
                    Birthday = new DateTime(1985, 7, 15)},

                new Executor {FirstName = "Михайло", LastName = "Дроздов", Patronymic = "Артемович",
                    PassportNumber ="AO452745", PassportIssuedBy = "Ленінським РУВД м. Дніпро",
                    PassportDateOfIssue = new DateTime(1975, 12, 6), TIN = "453453477349", Entrepreneur=true,
                    Birthday = new DateTime(1985, 7, 15)}
            };
            context.Executors.AddOrUpdate(x => new { x.FirstName, x.LastName }, executors.ToArray());

            var grantAgreements = new List<GrantAgreement>
            {
                new GrantAgreement { Number = "023-GF-20", Name = "Зменшення ризику інфікування ТБ, " +
                "шляхом впровадження ефективної моделей соціального супроводу хворих на чутливий ТБ у Дніпропетровській області",
                From = new DateTime(2019, 5, 6), To = new DateTime(2020, 7, 14)}
            };
            context.GrantAgreements.AddOrUpdate(x => new { x.Number }, grantAgreements.ToArray());

            var directions = new List<Direction>
            {
                new Direction { Number = "21M", Name = "Забезпечення ДОТ та психосоціального супроводу " +
                "клієнтів з чутливим ТБ (включаючи ВІЛ/ТБ) на амбулаторному етапі лікування"},

                new Direction { Number = "22M", Name = "Забезпечення ДОТ та психосоціального супроводу" +
                " клієнтів з МР ТБ/РР ТБ (включаючи ВІЛ/ТБ) на амбулаторному етапі"}

            };
            context.Directions.AddOrUpdate(x => new { x.Number }, directions.ToArray());


            var services = new List<Service>
            {
                new Service { Name = "Засвідчення факту прийому Клієнтом протитуберкульозних препаратів",
                    Price = 20f, Type = "Звичайна", Direction = directions[0]},

                new Service { Name = "Консультація щодо виконання  індивідуального плану лікування туберкульозу",
                    Price = 120f, Type = "Звичайна", Direction = directions[0]},

                new Service { Name = "Консультація щодо формування прихильності або підтримки прихильності до лікування туберкульозу",
                    Price = 120f, Type = "Звичайна", Direction = directions[0]},

                new Service { Name = "Засвідчення факту проходження діагностичних процедур після закінчення курсу лікування",
                    Price = 150f, Type = "Звичайна", Direction = directions[0]},

                new Service { Name = "Засвідчення факту закінчення Клієнтом прийому запланованого курсу лікування туберкульозу",
                    Price = 300f, Type = "Звичайна", Direction = directions[0]},

                new Service { Name = "Засвідчення факту прийому Клієнтом протитуберкульозних препаратів",
                    Price = 25f, Type = "Звичайна", Direction = directions[1]},

                new Service { Name = "Консультація щодо виконання  індивідуального плану лікування туберкульозу",
                    Price = 100f, Type = "Звичайна", Direction = directions[1]},

                new Service { Name = "Консультація щодо формування прихильності або підтримки прихильності до лікування туберкульозу",
                    Price = 150f, Type = "Звичайна", Direction = directions[1]},

                new Service { Name = "Засвідчення факту проходження діагностичних процедур після закінчення курсу лікування",
                    Price = 133f, Type = "Звичайна", Direction = directions[1]},

                new Service { Name = "Засвідчення факту закінчення Клієнтом прийому запланованого курсу лікування туберкульозу",
                    Price = 285f, Type = "Звичайна", Direction = directions[1]}
            };
            context.Services.AddOrUpdate(x => new { x.Name }, services.ToArray());

            var contracts = new List<Contract>
            {
                new Contract{ Number = 40, From = new DateTime(2019, 1, 1), To = new DateTime(2020, 1, 1),
                    GrantAgreements = new System.Collections.ObjectModel.ObservableCollection<GrantAgreement>(){grantAgreements[0]},
                Executor = executors[0]},

                new Contract{ Number = 41, From = new DateTime(2019, 1, 1), To = new DateTime(2020, 1, 1),
                    GrantAgreements = new System.Collections.ObjectModel.ObservableCollection<GrantAgreement>(){grantAgreements[0]},
                Executor = executors[1]},

                new Contract{ Number = 42, From = new DateTime(2019, 1, 1), To = new DateTime(2020, 1, 1),
                    GrantAgreements = new System.Collections.ObjectModel.ObservableCollection<GrantAgreement>(){grantAgreements[0]},
                Executor = executors[2]},

                new Contract{ Number = 43, From = new DateTime(2019, 1, 1), To = new DateTime(2020, 1, 1),
                    GrantAgreements = new System.Collections.ObjectModel.ObservableCollection<GrantAgreement>(){grantAgreements[0]},
                Executor = executors[3]},

                new Contract{ Number = 44, From = new DateTime(2019, 1, 1), To = new DateTime(2020, 1, 1),
                    GrantAgreements = new System.Collections.ObjectModel.ObservableCollection<GrantAgreement>(){grantAgreements[0]},
                Executor = executors[4]},

                new Contract{ Number = 45, From = new DateTime(2019, 1, 1), To = new DateTime(2020, 1, 1),
                    GrantAgreements = new System.Collections.ObjectModel.ObservableCollection<GrantAgreement>(){grantAgreements[0]},
                Executor = executors[5]},

                new Contract{ Number = 46, From = new DateTime(2019, 1, 1), To = new DateTime(2020, 1, 1),
                    GrantAgreements = new System.Collections.ObjectModel.ObservableCollection<GrantAgreement>(){grantAgreements[0]},
                Executor = executors[6]},
            };
            context.Contracts.AddOrUpdate(x => new { x.Number }, contracts.ToArray());
            
            

            var clients = new List<Client>
            {
                new Client { Code = "idglr02934", Executor = executors[0]},
                new Client { Code = "kdmgd34124", Executor = executors[0]},
                new Client { Code = "wmefk34144", Executor = executors[0]}
            };
            context.Clients.AddOrUpdate(x => new { x.Id }, clients.ToArray());


            var amountOfCreatedReports = new List<AmountOfCreatedReports>
            {
                new AmountOfCreatedReports{ Contract = contracts[0], GrantAgreement = grantAgreements[0], Amount = 3 },

                new AmountOfCreatedReports{ Contract = contracts[0], GrantAgreement = grantAgreements[0], Amount = 5 }
            };
            context.AmountOfCreatedReports.AddOrUpdate(x => new { x.Id }, amountOfCreatedReports.ToArray());
        }
    }
}
