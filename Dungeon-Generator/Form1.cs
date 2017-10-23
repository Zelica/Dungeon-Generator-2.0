using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dungeon_Generator
{
    public partial class Form1 : Form
    {

        int NumberOfRooms = 0;
        int NumberOfCorridors = 0;
        int RoomXCoordinate = 0;
        int RoomYCoordinate = 0;
        int Minimum = 0;
        int Maximum = 0;
        int CorridorsToRoom = 0;
        int RoomDirection = 0;
        int RoomNumber = 0;

        // antal celler ud ad x og y aksen
        int x = 10;
        int y = 10;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            NumberOfRooms = int.Parse(numericUpDown1.Text);

            NumberOfCorridors = NumberOfRooms - 1;
            RoomNumber = 0;

            // Der laves et kordinatsystem af arrays in arrays hvor
            // første array angiver x kordinatet 
            // anden array angiver y kordinatet 
            // tredje array skal bruges til at glemme values i
            //    første (0) til fjere (3) bool er om der er en gang til feltet i rækkefølgen N, Ø, S, V
            bool[][][] Celle = new bool[x][][];
            for (int i = 0; i < x; i++)
            {
                Celle[i] = new bool[y][];

                for (int j = 0; j < y; j++)
                {
                    Celle[i][j] = new bool[5];

                    for (int k = 0; k < 5; k++)
                    {
                        Celle[i][j][k] = false;
                    }
                }        
            }


            // liste over celler med gange og uden rum hvor det angives [nummeret på rummet][x og y kordinater (0 er x og 1 er y)]
            int[][] ListOfCellsWithCorridor = new int[NumberOfRooms - 1][];
            for (int i = 0; i < NumberOfRooms; i++)
            {
                ListOfCellsWithCorridor[i] = new int [2];

                for (int j = 0; j < 2; j++)
                {
                    ListOfCellsWithCorridor[i][j] = 0;
                }
            }


            // Cellen for det første rum bliver valgt
            RoomXCoordinate = x / 2 - 1;
            RoomYCoordinate = y / 2 - 1;

            // Det bliver gemt at der er et rum på feltet
            Celle[RoomXCoordinate][RoomYCoordinate][4] = true;

            // Visuelt mangler her

            // Hvor mange gange ud over dem der alderede er der kan være fra rummet
            // Siden denne kun bruges til det første rum, skal der ikke tages stilling til alderede placerede gange
            if (NumberOfCorridors > 4)
            {
                Minimum = 1;
                Maximum = 4;
            }   
            else if (0 < NumberOfCorridors && NumberOfCorridors <= 4)
            {
                Minimum = 1;
                Maximum = NumberOfCorridors;
            }
            else
            {
                Minimum = 0;
                Maximum = 0;
            }

            // på grund af at det maximale nummer ikke bliver taget med når man generere et tilfældigt nummer pluses der med 1
            Random random1 = new Random();
            CorridorsToRoom = random1.Next(Minimum, Maximum + 1);

            for (int i = 0; i < CorridorsToRoom;)
            {
                Random random2 = new Random();
                RoomDirection = random2.Next(0, 5);

                if (Celle[RoomXCoordinate][RoomYCoordinate][RoomDirection] == false)
                {
                    Celle[RoomXCoordinate][RoomYCoordinate][RoomDirection] = true;

                    //    Det bliver fortalt til cellen hvor gangen går til, at der er en gang
                    switch (RoomDirection)
                    {
                        case 0:
                            Celle[RoomXCoordinate][RoomYCoordinate + 1][2] = true;
                            ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate;
                            ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate + 1;
                            break;
                        case 1:
                            Celle[RoomXCoordinate + 1][RoomYCoordinate][3] = true;
                            ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate + 1;
                            ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate;
                            break;
                        case 2:
                            Celle[RoomXCoordinate][RoomYCoordinate - 1][0] = true;
                            ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate;
                            ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate - 1;
                            break;
                        case 3:
                            Celle[RoomXCoordinate - 1][RoomYCoordinate][1] = true;
                            ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate - 1;
                            ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate;
                            break;
                        default:
                            MessageBox.Show("Noget gik galt");
                            break;
                    }


                    // visuelle ting burde ske her


                }
                else
                {
                    i = i - 1;
                }
            }

            NumberOfRooms = NumberOfRooms - 1;


            // Under bliver resten af rummene genereret 
            for (int i = 0; i < NumberOfRooms; i++)
            {
                
                
                
                // Det bliver gemt at der er et rum på feltet
                Celle[RoomXCoordinate][RoomYCoordinate][0] = true;

                // Visuelt mangler her

                // Hvor mange gange ud over dem der alderede er der kan være fra rummet
                // Siden denne kun bruges til det første rum, skal der ikke tages stilling til alderede placerede gange
                if (NumberOfCorridors > 4)
                {
                    Minimum = 1;
                    Maximum = 4;
                }
                else if (0 < NumberOfCorridors && NumberOfCorridors <= 4)
                {
                    Minimum = 1;
                    Maximum = NumberOfCorridors;
                }
                else
                {
                    Minimum = 0;
                    Maximum = 0;
                }

                // på grund af at det maximale nummer ikke bliver taget med når man generere et tilfældigt nummer pluses der med 1
                Random random3 = new Random();
                CorridorsToRoom = random3.Next(Minimum, Maximum + 1);

                for (int j = 0; j < CorridorsToRoom;)
                {
                    Random random2 = new Random();
                    RoomDirection = random2.Next(0, 5);

                    if (Celle[RoomXCoordinate][RoomYCoordinate][RoomDirection] == false)
                    {
                        Celle[RoomXCoordinate][RoomYCoordinate][RoomDirection] = true;

                        //    Det bliver fortalt til cellen hvor gangen går til, at der er en gang
                        switch (RoomDirection)
                        {
                            case 0:
                                Celle[RoomXCoordinate][RoomYCoordinate + 1][2] = true;
                                break;
                            case 1:
                                Celle[RoomXCoordinate + 1][RoomYCoordinate][3] = true;
                                break;
                            case 2:
                                Celle[RoomXCoordinate][RoomYCoordinate - 1][0] = true;
                                break;
                            case 3:
                                Celle[RoomXCoordinate - 1][RoomYCoordinate][1] = true;
                                break;
                            default:
                                MessageBox.Show("Noget gik galt");
                                break;


                                // visuelle ting burde ske her

                        }
                    }
                    else
                    {
                        i = i - 1;
                    }
                }

                NumberOfRooms = NumberOfRooms - 1;
            }
        }
    }
}
