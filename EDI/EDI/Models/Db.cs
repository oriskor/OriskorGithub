using System;
using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;


namespace EDI.Models
{   
    public static class Db
    {
        public static IList<Country> Countries = new List<Country>();
        public static IList<Chef> Chefs = new List<Chef>();
        public static IList<Meal> Meals = new List<Meal>();
        public static IList<Category> Categories = new List<Category>();
        public static IList<Dinner> Dinners = new List<Dinner>();
        public static IList<HeaderDetailInformation> HeaderDetailInformationDinners = new List<HeaderDetailInformation>();
        public static IList<Lunch> Lunches = new List<Lunch>();
        public static IList<Purchase> Purchases = new List<Purchase>();
        public static IList<Message> Messages = new List<Message>();
        public static IList<Spreadsheet> Spreadsheets = new List<Spreadsheet>();
        public static IList<Restaurant> Restaurants = new List<Restaurant>();
        public static IList<RestaurantAddress> RestaurantAddresses = new List<RestaurantAddress>();
        public static IList<TreeNode> TreeNodes = new List<TreeNode>();
        public static IList<Meeting> Meetings = new List<Meeting>();

        public static int Gid = 151;

        public static object Set<T>()
        {
            if (typeof(T) == typeof(Country)) return Countries;
            if (typeof(T) == typeof(Chef)) return Chefs;
            if (typeof(T) == typeof(Meal)) return Meals;
            if (typeof(T) == typeof(Dinner)) return Dinners;
            if (typeof(T) == typeof(HeaderDetailInformation)) return HeaderDetailInformationDinners;
            if (typeof(T) == typeof(Category)) return Categories;
            if (typeof(T) == typeof(Lunch)) return Lunches;
            if (typeof(T) == typeof(Purchase)) return Purchases;
            if (typeof(T) == typeof(Message)) return Messages;
            if (typeof(T) == typeof(Spreadsheet)) return Spreadsheets;
            if (typeof(T) == typeof(Restaurant)) return Restaurants;
            if (typeof(T) == typeof(RestaurantAddress)) return RestaurantAddresses;
            if (typeof(T) == typeof(TreeNode)) return TreeNodes;
            if (typeof(T) == typeof(Meeting)) return Meetings;

            return null;
        }

        public static T Insert<T>(T o) where T : Entity
        {
            o.Id = Gid += 2;
            ((IList<T>)Set<T>()).Add(o);
            return o;
        }

        public static T Get<T>(int? id) where T : Entity
        {
            var entity = ((IList<T>)Set<T>()).SingleOrDefault(o => o.Id == id);
            if (entity == null) throw new Exception("this item doesn't exist anymore");
            return entity;
        }

        public static void Update<T>(T o) where T : Entity
        {
            var t = Get<T>(o.Id);
            t.InjectFrom(o);
        }

        public static void Delete<T>(int id) where T : Entity
        {
            ((IList<T>)Set<T>()).Remove(Get<T>(id));
        }

        static Db()
        {
            Insert(new Category { Name = "Fruits" });
            Insert(new Category { Name = "Legumes" });
            Insert(new Category { Name = "Vegetables" });
            Insert(new Category { Name = "Nuts" });
            Insert(new Category { Name = "Grains" });

            Insert(new Meal { Name = "Mango", Category = Categories[0], Description = "The mango is a fleshy stone fruit belonging to the genus Mangifera" });
            Insert(new Meal { Name = "Apple", Category = Categories[0], Description = "The apple is the pomaceous fruit of the apple tree" });
            Insert(new Meal { Name = "Papaya", Category = Categories[0], Description = "The papaya is a large tree-like plant, with a single stem growing from 5 to 10 metres" });
            Insert(new Meal { Name = "Banana", Category = Categories[0], Description = "Bananas come in a variety of sizes and colors when ripe, including yellow, purple, and red." });
            Insert(new Meal { Name = "Cherry", Category = Categories[0], Description = "The cherry is the fruit of many plants of the genus Prunus, and is a fleshy stone fruit" });

            Insert(new Meal { Name = "Tomato", Category = Categories[1], Description = "The tomato fruit is consumed in diverse ways, including raw, as an ingredient in many dishes and sauces" });
            Insert(new Meal { Name = "Potato", Category = Categories[1], Description = "A potato is a starchy edible tuber native to South America and cultivated all over the world" });
            Insert(new Meal { Name = "Cucumber", Category = Categories[1], Description = "Like the tomato, the cucumber is a fruit. Botanically speaking, a fruit is the mature ovary of a flowering plant" });
            Insert(new Meal { Name = "Onion", Category = Categories[1], Description = " It is possible to chop the greens into small pieces for salads and as a garnish" });
            Insert(new Meal { Name = "Carrot", Category = Categories[1], Description = "The rich orange color of carrots comes from beta carotene, which also happens to be very good for optical health" });

            Insert(new Meal { Name = "Celery", Category = Categories[2], Description = "Although originally cultivated for its perceived medicinal qualities, celery has since made the jump into the daily diets" });
            Insert(new Meal { Name = "Broccoli", Category = Categories[2], Description = "Sometimes broccoli is compared to tiny trees" });
            Insert(new Meal { Name = "Artichoke", Category = Categories[2], Description = "The globe artichoke enjoys a long history of both lore and cooking preparation" });
            Insert(new Meal { Name = "Cauliflower", Category = Categories[2], Description = "As a general rule, the head is white, but variants of cauliflower come in purple and green as well" });
            Insert(new Meal { Name = "Lettuce", Category = Categories[2], Description = "Leaf lettuce is often very lightweight and ruffly, with a wrinkly surface and a soft, almost velvety texture" });

            Insert(new Meal { Name = "Hazelnuts", Category = Categories[3], Description = "Hazelnuts are produced by hazel trees" });
            Insert(new Meal { Name = "Chestnuts", Category = Categories[3], Description = "They have creamy white sweet flesh which appears in a number of cuisines" });
            Insert(new Meal { Name = "Walnuts", Category = Categories[3], Description = "A walnut is a seed from a tree in the genus Juglans" });
            Insert(new Meal { Name = "Almonds", Category = Categories[3], Description = "Almonds come in two varieties, sweet and bitter" });
            Insert(new Meal { Name = "Mongongo", Category = Categories[3], Description = "In addition to producing a highly useful lightweight, durable wood, the mongongo tree also yields a distinctive fruit" });

            Insert(new Meal { Name = "Rice", Category = Categories[4], Description = "Rice is a keystone of the grass family that produces a vast number of grains " });
            Insert(new Meal { Name = "Wheat", Category = Categories[4], Description = "Wheat is a type of grass grown all over the world for its highly nutritious and useful grain." });
            Insert(new Meal { Name = "Buckwheat", Category = Categories[4], Description = "Despite the name, buckwheat is not related to wheat" });
            Insert(new Meal { Name = "Oats", Category = Categories[4], Description = "They have been in cultivation for over 4,000 years" });
            Insert(new Meal { Name = "Barley", Category = Categories[4], Description = " a source of fermentable material for beer and certain distilled beverages, and as a component of various health foods" });

            var countries = new[]
            {
                "Elwynn Forest","Stormwind", "Loch Modan", "Westfall",
                "Ironforge", "Orgrimmar","Feralas", "Darnassus", "Teldrassil",
                "Winterspring", "Goldshire", "Sylvanaar","Redridge Mountains", "StoneCutter", "Farringdon",
                "Regent", "Piccadilly","Hatton Garden"
            };

            foreach (var country in countries)
            {
                Insert(new Country { Name = country });
            }

            Insert(new Chef { FirstName = "Naked", LastName = "Chef", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Chef", LastName = "Chef", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Athene", LastName = "Broccoli", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Omu", LastName = "Man", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Pepper", LastName = "Tomato", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Deezze", LastName = "Leysen", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Jeea", LastName = "Derveaux", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Phoebe", LastName = "Phoebes", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Sequeiro", LastName = "Olano", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Hercules", LastName = "Oat", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Hermes", LastName = "Graps", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Posh", LastName = "Sicy", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Hestia", LastName = "Cook", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Demeter", LastName = "Harvest", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Hyperion", LastName = "Light", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Gaia", LastName = "Earth", Country = Rnd(Countries) });
            Insert(new Chef { FirstName = "Chronos", LastName = "Timpus", Country = Rnd(Countries) });

            var dinners = new[] {"hot chocolate","believe in miracles", "out and about", "dinner tonight","watch tv","last night", "piece of toast","great","a little","surf and turf",
                "coffee for me","dinner with friends","eating at the pub","cooking in the garden","ninja dinner",
                "broccoli power","eating out","Awesome dinner","Uber dinner","Fruity dish","Nice dish","Nerds eating","Pros eating",
                "hungry men","snacks and movie","fruit salad","Morning meal","Morning Tea","Cookies and milk"};

            foreach (var o in dinners)
            {
                Insert(new Dinner
                {
                    Country = Rnd(Countries),
                    Chef = Rnd(Chefs),
                    Meals = RndMeals(),
                    Date = RndDate(),
                    Name = o,
                    BonusMeal = Rnd(Meals),
                    Organic = R.Next(10) > 3
                });
            }


            var ppl = new[]{
                "James","Jeremy","Richard","Penny","Sheldon","Leonard","Zazzles","Amy","Howard","Rajesh","Robert","Phil","Scott","Bernadette",
                "Russell","Timmy","Jeff","Adam","Jennifer","Audrey","Claudia","Brenda", "Tracy","Allison","Tom","Jerry","Oliver","Justin",
                "Jamie", "Tania", "Chiren", "Reese", "Dean", "Ian", "Paul", "Gabrielle", "Michelle","Hillary", "Jason","Joshua","Benjamin",
                "Ryan", "Henry", "Lucas", "Jake", "Luke", "Lewis", "Tyler", "David", "Patrick", "Evan", "Sam", "Sebastian", "Ben",
                "William", "Harry", "Daniel", "Jacob", "Sophie", "Emily", "Jessica", "Ella", "Mia", "Emma", "Megan", "Caitlin", "Amber",
                "Evelyn", "Lauren", "Nicole", "Paige", "Eve", "Iris", "Gracie", "Sarah", "Holly", "Elizabeth", "Rachel", "Courtney", "Owen",
                "Ruby", "Hannah", "Michael", "Gwen", "Morgan", "Carter", "Dolores", "Earl", "Fabiano", "Fernando",
                "Mario","Luigi", "Vincenzo", "Jonathan", "Nathan", "John", "Mary", "Maria", "Helena","Linda", "Lucy", "Francesco", "Sergio",
                "Katarina", "Raquel", "Vanessa", "Miguel", "Pedro", "Carlos"
            };

            var colors = new[] { "#5484ED", "#A4BDFC", "#7AE7BF", "#51B749", "#FBD75B", "#FFB878", "#FF887C", "#DC2127", "#DBADFF", "#E1E1E1" };

            var foods = new[] {"Banana","Cheesecake","Hot Beverage", "Brisket", "Oat meal", "French toast", "Pizza", "Apple Pie", "Shepherd's pie",
                "Salad", "Sushi"};
            var locations = new[] { "Home", "University", "Restaurant", "Visit", "Diner", "Central Perk", "Tavern" };

            var restaurantNames = new[] { "McDowell's", "The Rolling Donut", "Big Kahuna Burger", "City Wok", "Cluckin' Bell", "Central Perk", "Island Diner", "Dream Cafe",
                "Cleveland's Deli", "Don't Drop Inn", "The Happy Sumo", "The Hungry Hun", "Krusty Burger", "Luigi's", "Big T Burgers and Fries", "Pizza on a stick" };

            foreach (var restaurantName in restaurantNames.Reverse())
            {
                var restaurant = Insert(new Restaurant { Name = restaurantName, IsCreated = true });
                Insert(new RestaurantAddress { RestaurantId = restaurant.Id, Line1 = Rnd(ppl) + " square " + R.Next(10, 15) });
                Insert(new RestaurantAddress { RestaurantId = restaurant.Id, Line1 = Rnd(ppl) + " street " + R.Next(10, 35) });
            }

            IDictionary<string, List<string>> personFood = ppl.ToDictionary(o => o, o => new List<string>());
            IDictionary<string, int> personTimes = ppl.ToDictionary(o => o, o => 0);
            var prices = new[] { 10, 20, 30, 50, 70, 23, 45, 100, 21, 79, 39, 18 };

            for (var i = 0; i < 750; i++)
            {
                var maxLunches = 750 / ppl.Length;
                var pplc = 750 / ppl.Length;

                var person = Rnd(ppl);
                if (Lunches.Count(o => o.Person == person) >= pplc) person = Rnd(ppl);

                if (personTimes[person] >= maxLunches)
                {
                    person = Rnd(ppl);
                }
                personTimes[person]++;

                string food;

                if (personFood[person].Count == 3)
                {
                    food = Rnd(personFood[person]);
                }
                else
                {
                    food = Rnd(foods);
                    personFood[person].Add(food);
                }

                Insert(new Lunch
                {
                    Date = RndDate(),
                    Food = food,
                    Person = person,
                    Location = Rnd(locations),
                    Price = Rnd(prices),
                    Country = Rnd(Countries),
                    Chef = Rnd(Chefs),
                    Organic = R.Next(10) < 7
                });
            }

            var startDate = DateTime.UtcNow.AddDays(-30).Date;
            var endDate = DateTime.UtcNow.AddDays(27);
            var eventDurations = new[] { 15, 20, 30, 45, 60, 120, 450, 30, 15, 45, 60 };
            var lateDurations = new[] { 15, 20, 30, 45, 60, 30, 15 };

            while (startDate < endDate)
            {
                var eventsPerDay = R.Next(2, 4);
                if (startDate.DayOfWeek == DayOfWeek.Sunday) eventsPerDay = Math.Min(eventsPerDay, 1);

                for (var i = 0; i < eventsPerDay; i++)
                {
                    var ev = Insert(new Meeting { Start = startDate, Location = Rnd(locations) });
                    ev.Start = ev.Start.AddHours(R.Next(24));

                    if (R.Next(50) == 1)
                    {
                        ev.AllDay = true;
                        ev.End = ev.Start;
                    }
                    else
                    {
                        ev.End = ev.Start.AddMinutes(Rnd(ev.Start.Hour > 17 ? lateDurations : eventDurations));
                    }

                    ev.Title = "meeting at " + ev.Location;
                    ev.Color = Rnd(colors);
                }

                startDate = startDate.AddDays(1);
            }

            for (var i = 0; i < 70; i++)
            {
                Insert(new Purchase
                {
                    Date = RndDate(),
                    Customer = Rnd(ppl),
                    Product = Rnd(foods),
                    Quantity = Rnd(new[] { 1, 2, 3, 5, 7 })
                });
            }

            for (var i = 0; i < 100; i++)
            {
                Insert(new Message
                {
                    From = Rnd(ppl),
                    Subject = Rnd(foods),
                    DateReceived = RndDate(),
                    Body = RndMessage(new[] { ppl, foods, new[] { "bla bla bla", "and", "or", "it", "for", "something" } }),
                    IsRead = true
                });
            }

            foreach (var message in Messages.OrderByDescending(o => o.DateReceived).Take(5))
            {
                message.IsRead = false;
            }

            for (int i = 0; i < 97; i++)
            {
                Insert(new Spreadsheet
                {
                    Name = Rnd(ppl),
                    Location = Rnd(countries),
                    Meal = Rnd(Meals).Name
                });
            }

            for (int i = 0; i < 90; i++)
            {
                var node = Insert(new TreeNode());
                node.Name = "main node " + node.Id;
                FillNode(node, 0, R.Next(2, 5));
            }
        }

        private static readonly Random R = new Random();

        private static void FillNode(TreeNode parent, int depth, int maxDepth)
        {
            var maxNodes = (int)Math.Max(5 - depth * 0.7, 1);
            var nodeCount = R.Next(1, maxNodes);
            var leafNames = Meals.Where(o => o.Category.Name == "Fruits").Select(o => o.Name + " fruit").ToList();
            leafNames.Add("green leaf");
            leafNames.Add("leaf");

            for (int i = 0; i < nodeCount; i++)
            {
                if (depth >= maxDepth || R.Next(0, 2) == 0)
                {
                    var leaf = Insert(new TreeNode { Parent = parent });
                    leaf.Name = leaf.Name = Rnd(leafNames) + " " + leaf.Id;
                }
                else
                {
                    var node = Insert(new TreeNode { Parent = parent });
                    node.Name = "node " + node.Id;
                    FillNode(node, depth + 1, maxDepth);
                }
            }
        }

        private static string RndMessage(string[][] ph)
        {
            var s = "Hi \n ";
            for (int i = 0; i < R.Next(1000); i++)
            {
                s += " " + Rnd(Rnd(ph));
            }
            return s;
        }

        private static T Rnd<T>(ICollection<T> list)
        {
            return list.ToArray()[R.Next(0, list.Count)];
        }

        private static DateTime RndDate()
        {
            var day = 1 / 2 + R.Next((29) / 2 - 1 / 2);
            day = day * 2 + 1;
            return new DateTime(R.Next(2009, 2015), R.Next(1, 12), day);
        }

        private static IEnumerable<Meal> RndMeals()
        {
            var list = new List<Meal>();
            var x = R.Next(1, 4);
            for (int i = 0; i < x; i++)
            {
                list.Add(Meals[R.Next(Meals.Count - 1)]);
            }
            return list.Distinct();
        }
    }
}