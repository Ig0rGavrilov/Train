using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Train
{
    class Locomotive
    {
        Person person;
        Engine engine;

        public Locomotive()
        {
            
        }
        public Locomotive(Person person, Engine engine)
        {
            
        }
        public override string ToString()
        {
            
            return $"Locomotive";
        }
    }

    class Person
    {
        string firstName;
        string lastName;

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }

        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public override string ToString()
        {
            return $"First name: {firstName}, Last name: {lastName}";
        }
    }

    class Engine
    {
        string type;
        public string Type { get { return type; } set { type = value; } }

        public Engine(string type)
        {
            this.type = type;
        }
        public override string ToString()
        {
            return $"Engine type - {type}";
        }
    }

    class Chair
    {
        bool nearWindow;
        int number;
        bool reserved;
        public bool NearWindow { get { return nearWindow; } set { nearWindow = value; } }
        public int Number { get {return number; } set { number = value; } }
        public bool Reserved { get { return reserved; } set { reserved = value; } }

        public Chair(int number, bool nearWindow)
        {
            this.number = number;
            this.nearWindow=nearWindow;
        }
    }

    class Bed
    {
        int number;
        bool reserved;
        public int Number { get { return number; } set { number = value; } }
        public bool Reserved { get { return reserved; } set { reserved = value; } }
    }

    class Door
    {
        double height;
        double width;

        public double Height { get { return height; } set { height = value; } }
        public double Width { get { return width; } set { width = value; } }
        public Door(double height, double width)
        {
            this.height = height;
            this.width = width;
        }

    }

    class Wagon
    {

    }
    class PersonalWagon: Wagon
    {
        public List<Door> doors;
        public List<Chair> sits = new List<Chair>();
        public int numberOfChairs;

        public PersonalWagon(int numberOfChairs)
        {
            this.numberOfChairs = numberOfChairs;

            // Generate  sits
            for (int i=0; i<numberOfChairs; i++)
            {
                sits.Add(new Chair(i, true));
                sits[i].Reserved = false;
            }
        }
     
    }

    class EconomyWagon : PersonalWagon
    {
        public EconomyWagon(int numberOfChairs) : base (numberOfChairs)
        {
            //base.numberofchairs = numberofchairs;
        }

        public override string ToString()
        {
            return $"EconomyWagon: Number of chairs:{numberOfChairs}";
        }
    }

    class BusinessWagon: PersonalWagon
    {
        Person steward;
        public  Person Steward { get { return steward; } set { steward = value; } }

        public BusinessWagon(Person steward, int numberOfChairs) : base (numberOfChairs) 
        {
            this.steward = steward;
            
        }
        public override string ToString()
        {
            return $"BusinessWagon: Number of chairs:{numberOfChairs}, steward {steward.ToString()}";
        }
    }
    class NightWagon: PersonalWagon
    {
        Bed[] beds;
        int numberOfBeds;
        public Bed Beds { get { return Beds; } set { Beds = value; } }

       public NightWagon (int  numberOfChairs, int numberOfBeds) : base(numberOfChairs)
        {
            this.numberOfBeds=numberOfBeds;
        }

        public override string ToString()
        {
            return $"NightWagon: Number of chairs:{numberOfChairs}, number of Beds:{numberOfBeds} ";
        }
    }
    class Hopper : Wagon
    {
        double loadingCapacity;
        public double LoadingCapacity { get{ return loadingCapacity;  } set { loadingCapacity = value; } }

        public Hopper(double tonnage)
        {
            this.loadingCapacity = tonnage;
        }
        public override string ToString()
        {
            return $"Hopper: loading:{loadingCapacity}";
        }

    }

    class Train
    {
        Locomotive locomotive;
        List<Wagon> wagons;

        public Train()
        {

        }
        public Train(Locomotive locomotive)
        {
            this.locomotive = locomotive;
        }

        public Train(Locomotive locomotive, List<Wagon> wagons)
        {
            this.locomotive = locomotive;
            this.wagons = wagons;
        }

        public void ConnectWagon(Wagon wagon)
        {
            wagons.Add(wagon);
        }
        public void DisonnectWagon(Wagon wagon)
        {
            wagons.Remove(wagon);
        }

        public void ReserveChair(int wagonNumber, int chairNumber)
        {
            //Check wagon number 
            if (wagons.Count <= wagonNumber)
            {
                Console.WriteLine($"wagon number {wagonNumber} too big ");
            }
            else
            {
                Wagon wagon = wagons[wagonNumber];

                if (wagon is PersonalWagon)
                {
                 
                 if (((PersonalWagon)wagon).sits.Count>=chairNumber)
                    {
                        //Check if sits is reserved before
                        //Check if Reserved
                        if (((PersonalWagon)wagon).sits[chairNumber].Reserved == true)
                        {
                            Console.WriteLine($"The seat {chairNumber} in wagon {wagonNumber} was reserved before , choose another");
                        }

                        // Reservation
                        else
                        {
                            //((PersonalWagon)wagon).sits.Add(new Chair(chairNumber, true)); // All times near window

                            ((PersonalWagon)wagon).sits[chairNumber].Reserved = true;
                            Console.WriteLine($"The seat {chairNumber}  was reserved in wagon {wagonNumber}");
                        }
                    }
                 else
                    {
                        Console.WriteLine($"ChairNumber {chairNumber} is exceed, max number {((PersonalWagon)wagon).sits.Count} ");
                    }                    
                }
                else
                {
                    Console.WriteLine("There is not seats / Hopper ");
                }
            }                        
        }

        public void ListReservedChairs()
        {

            foreach (Wagon wagon in wagons)
            {
                if (wagon is PersonalWagon)
                {
                    PersonalWagon personalWagon = (PersonalWagon)wagon;
                    foreach(Chair chair in personalWagon.sits)
                    {
                        if (chair.Reserved==true)
                        {
                            Console.WriteLine($"The seat number - {chair.Number} wagon number {wagons.IndexOf(wagon)}");
                        }                        
                    }                    
                }
            }
            
        }
        public override string ToString()
        {
            string train="";
            train = train + locomotive.ToString() + "\n";
            for (int i=0; i<wagons.Count; i++)
            {
                
                train += "W:"+(i)+" "+wagons[i].ToString() + "\n";
            }
            return train;
        }



    }
    internal class Program
    {
        static void Main(string[] args)
        {


            

            //Vytvořte lokomotivu (diesel se strojvedoucím Karlem Novákem). 
            Person driver = new Person("Karel", "Novak");            
            Engine engine = new Engine("Diesel");            
            Locomotive locomotive = new Locomotive(driver, engine);

            //Vytvořte 3 osobní vagony (jeden z toho bude Business, stewardka Lenka Kozáková),
            //jeden spací a  jeden Hopper

            BusinessWagon businessWagon = new BusinessWagon(new Person("Lenka", "Kozakova"), 40);            
            NightWagon nightWagon = new NightWagon(25, 15);
            Hopper hopper = new Hopper(39);

            List<Wagon> wagons = new List<Wagon>();
            wagons.Add(businessWagon);
            wagons.Add(nightWagon); 
            wagons.Add(hopper);

            Train trainFirst = new Train(locomotive, wagons );

            //vytvořte mu další vagon (ještě jeden Hopper) a připojte.  
            Hopper hopper1 = new Hopper(44);            
            trainFirst.ConnectWagon(hopper1);
            Console.WriteLine("First Train -----------------");
            Console.WriteLine(trainFirst.ToString());

            // Disconnect
            trainFirst.DisonnectWagon(hopper1);

            // Second train
            
            List<Wagon> wagons2 = new List<Wagon>();
            EconomyWagon economyWagon1 = new EconomyWagon(50);          
            EconomyWagon economyWagon2 = new EconomyWagon(50);
            NightWagon nightWagon3 = new NightWagon(25, 15);
            Hopper hopper4 = new Hopper(39);
            BusinessWagon businessWagon5 = new BusinessWagon(new Person("Lenka", "Novakova"), 40);
            wagons2.Add(economyWagon1);
            wagons2.Add(economyWagon2);
            wagons2.Add(nightWagon3);
            wagons2.Add(hopper4);
            wagons2.Add(businessWagon5);

            Train trainSecond = new Train(new Locomotive(new Person("Marek", "Behal"), new Engine("Steam")), wagons2);
            Console.WriteLine("Second Train -----------------");
            Console.WriteLine(trainSecond.ToString());


            int w = 2;
            int s = 1;
            Console.WriteLine($"Reservation Train 2--- Wagon {w} ----- Seat {s}----------");
            trainSecond.ReserveChair(w, s);  // w - wagon - s - chair

            // Reserve same chair as before
            trainSecond.ReserveChair(w, s);  // w - wagon - s - chair

            // Reserve outside chairNumber
            w = 2;
            s = 55;
            trainSecond.ReserveChair(w, s);  // w - wagon - s - chair



            //Console.WriteLine(economyWagon1.numberOfChairs);
            Console.WriteLine();
            Console.WriteLine("List Reserved-------------------------------------------");
            trainSecond.ListReservedChairs();
        }
    }
}