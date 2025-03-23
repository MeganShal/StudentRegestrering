using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StudentRegestrering
{
    class Start
    {
        private StudentManagment managment;              

        public Start()
        {
            managment = new StudentManagment(this);
        }

        public void RunStart()
        {         
            Console.Clear();
            MenyInfo();
            managment.CheckInput();

            switch (managment.input)
            {
                case 1:
                    managment.AddStudent();
                    break;

                case 2:
                    managment.ChangeStudent();
                    break;

                case 3:
                    managment.ListaAllaStudenter();
                    break;

                case 4:
                    Console.WriteLine("\nExiting...");
                    break;

                default:
                    Console.WriteLine("\nValet du gjorde hittades inte vänligen försök igen med en existerande val, du kan välja mellan 1,2,3 eller 4!");
                    managment.ContinueMessage();
                    break;
            }
            
        }

        private void MenyInfo()
        {
            Console.WriteLine("Hej och vällkommen till din skolas registrerings program där vi hanterar studenter");
            Console.WriteLine("Vad är du intresserad av idag\n");
            Console.WriteLine("1 för att registera en ny student");
            Console.WriteLine("2 för att ändra information om en befinande student");
            Console.WriteLine("3 för att ta fram en lista av alla registrerade studenter eller för att radera en student");
            Console.WriteLine("4 för att stänga av applicationen!");
        }
        

    }
}
