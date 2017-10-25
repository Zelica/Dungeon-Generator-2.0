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

        //Skal forklares (hvad er hvad)
        int NumberOfRoomsLeft = 0;
        int NumberOfCorridors = 0;
        int RoomXCoordinate = 0;
        int RoomYCoordinate = 0;
        int Minimum = 0;
        int Maximum = 0;
        int NewCorridors = 0;
        int RoomDirection = 0;
        int RoomNumber = 0;
        int RandomCellWithCorridor = 0;
        int ListMaximum = 0;
        int CellsWithCorridorTo = 0;
        int NumberOfCorridorsToRoom = 0;
        bool Room = false;

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

            NumberOfRoomsLeft = int.Parse(numericUpDown1.Text);

            RoomNumber = 0;
            CellsWithCorridorTo = 0;

            // Der laves et kordinatsystem af arrays in arrays hvor
            // første array angiver x kordinatet 
            // anden array angiver y kordinatet 
            // tredje array skal bruges til at glemme values i
            //    første (0) til fjere (3) bool er om der er en gang til feltet i rækkefølgen N, Ø, S, V
            //    femte (4) bool er til hvorvidt der er en gang til cellen
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

            // formelen under skal nok forklares eller ændres 
            ListMaximum = Convert.ToInt32(Math.Floor(2 * (NumberOfRoomsLeft - 1 * Math.Sqrt(NumberOfRoomsLeft))));

            // liste over celler med gange og uden rum hvor det angives [nummeret på rummet][x og y kordinater (0 er x og 1 er y)]
            int[][] ListOfCellsWithCorridor = new int[NumberOfRoomsLeft - 1][];
            for (int i = 0; i <= ListMaximum; i++)
            {
                ListOfCellsWithCorridor[i] = new int [2];

                for (int j = 0; j < 2; j++)
                {
                    ListOfCellsWithCorridor[i][j] = -1;
                }
            }


            // Cellen for det første rum bliver valgt
            RoomXCoordinate = x / 2 - 1;
            RoomYCoordinate = y / 2 - 1;

            NumberOfRoomsLeft = NumberOfRoomsLeft - 1;


            // Visuelt mangler her


            NumberOfCorridors = NumberOfRoomsLeft - CellsWithCorridorTo;

            // Hvor mange gange der kan være fra rummet
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
                Celle[RoomXCoordinate][RoomYCoordinate][4] = true;
            }
            else
            {
                Minimum = 0;
                Maximum = 0;
            }

            // på grund af at det maximale nummer ikke bliver taget med når man generere et tilfældigt nummer pluses der med 1
            Random random1 = new Random();
            NewCorridors = random1.Next(Minimum, Maximum + 1);

            CellsWithCorridorTo = NewCorridors;

            for (int i = 0; i < NewCorridors;)
            {
                Random random2 = new Random();
                RoomDirection = random2.Next(0, 5);

                if (Celle[RoomXCoordinate][RoomYCoordinate][RoomDirection] == false)
                {
                    Celle[RoomXCoordinate][RoomYCoordinate][RoomDirection] = true;

                    // Det bliver fortalt til cellen hvor gangen går til, at der er en gang
                    // Og cellen hvor gangen går til tilføjes til en liste
                    switch (RoomDirection)
                    {
                        case 0:
                            Celle[RoomXCoordinate][RoomYCoordinate + 1][2] = true;

                            if (Celle[RoomXCoordinate][RoomYCoordinate + 1][4] == false)
                            {
                                CellsWithCorridorTo = CellsWithCorridorTo + 1;
                            }

                            Celle[RoomXCoordinate][RoomYCoordinate + 1][4] = true;
                            ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate;
                            ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate + 1;
                            break;
                        case 1:
                            Celle[RoomXCoordinate + 1][RoomYCoordinate][3] = true;

                            if (Celle[RoomXCoordinate + 1][RoomYCoordinate][4] == false)
                            {
                                CellsWithCorridorTo = CellsWithCorridorTo + 1;
                            }

                            Celle[RoomXCoordinate + 1][RoomYCoordinate][4] = true;
                            ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate + 1;
                            ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate;
                            break;
                        case 2:
                            Celle[RoomXCoordinate][RoomYCoordinate - 1][0] = true;

                            if (Celle[RoomXCoordinate][RoomYCoordinate - 1][4] == false)
                            {
                                CellsWithCorridorTo = CellsWithCorridorTo + 1;
                            }

                            Celle[RoomXCoordinate][RoomYCoordinate - 1][4] = true;
                            ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate;
                            ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate - 1;
                            break;
                        case 3:
                            Celle[RoomXCoordinate - 1][RoomYCoordinate][1] = true;

                            if (Celle[RoomXCoordinate - 1][RoomYCoordinate][4] == false)
                            {
                                CellsWithCorridorTo = CellsWithCorridorTo + 1;
                            }

                            Celle[RoomXCoordinate - 1][RoomYCoordinate][4] = true;
                            ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate - 1;
                            ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate;
                            break;
                        default:
                            MessageBox.Show("Noget gik galt");
                            break;
                    }

                    RoomNumber = RoomNumber + 1;


                    // visuelle ting burde ske her


                }
                else
                {
                    i = i - 1;
                }
            }


            // Under bliver resten af rummene genereret 
            for (int i = 0; i < NumberOfRoomsLeft; i++)
            {
                // Det næste rums placering bliver valgt
                do
                {
                    Random random3 = new Random();
                    RandomCellWithCorridor = random3.Next(0, ListMaximum + 1);

                    if (ListOfCellsWithCorridor[RandomCellWithCorridor][0] == -1 && ListOfCellsWithCorridor[RandomCellWithCorridor][1] == -1)
                    {
                        Room = false;
                    }
                    else
                    {
                        Room = true;
                    }

                }
                while (!Room);
                // siden loopet bliver ved med at køre så lange statmentet er False 
                // og vi gerne vil havde det ttil at køre indtil det er sandt, sættes et udråbstegn

                RoomXCoordinate = ListOfCellsWithCorridor[RandomCellWithCorridor][0];
                RoomYCoordinate = ListOfCellsWithCorridor[RandomCellWithCorridor][1];

                //Kordinaterne på listen sættes til -1 igen da cellen hvor der nu er et rum ikke skal vælges mere end en gang
                ListOfCellsWithCorridor[RandomCellWithCorridor][0] = -1;
                ListOfCellsWithCorridor[RandomCellWithCorridor][1] = -1;

                CellsWithCorridorTo = CellsWithCorridorTo - 1;

                NumberOfRoomsLeft = NumberOfRoomsLeft - 1;


                // Visuelt mangler her


                NumberOfCorridors = NumberOfRoomsLeft - CellsWithCorridorTo;

                for (int j = 0; j <= 3; j++)
                {
                    if(Celle[RoomXCoordinate][RoomYCoordinate][j])
                    {
                        NumberOfCorridorsToRoom = NumberOfCorridorsToRoom + 1;
                    }
                }

                // Hvor mange gange der kan være fra rummet
                // Der bliver 
                if (NumberOfCorridors >= 4)
                {
                    Minimum = 1;
                    Maximum = 4 - NumberOfCorridorsToRoom;
                }
                else if (0 < NumberOfCorridors && NumberOfCorridors < 4)
                {
                    Minimum = 1;
                    Maximum = Math.Min(NumberOfCorridors, 4 - NumberOfCorridorsToRoom);
                }
                else
                {
                    Minimum = 0;
                    Maximum = 0;
                }

                // på grund af at det maximale nummer ikke bliver taget med når man generere et tilfældigt nummer pluses der med 1
                Random random4 = new Random();
                NewCorridors = random4.Next(Minimum, Maximum + 1);

                NumberOfCorridors = NumberOfCorridors - NewCorridors;

                for (int j = 0; j < NewCorridors;)
                {
                    Random random5 = new Random();
                    RoomDirection = random5.Next(0, 5);

                    if (Celle[RoomXCoordinate][RoomYCoordinate][RoomDirection] == false)
                    {
                        Celle[RoomXCoordinate][RoomYCoordinate][RoomDirection] = true;

                        // Det bliver fortalt til cellen hvor gangen går til, at der er en gang
                        // Og cellen hvor gangen går til tilføjes til en liste
                        switch (RoomDirection)
                        {
                            case 0:
                                Celle[RoomXCoordinate][RoomYCoordinate + 1][2] = true;

                                if (Celle[RoomXCoordinate][RoomYCoordinate + 1][4] == false)
                                {
                                    CellsWithCorridorTo = CellsWithCorridorTo + 1;
                                    ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate;
                                    ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate + 1;
                                }

                                Celle[RoomXCoordinate][RoomYCoordinate + 1][4] = true;
                                break;
                            case 1:
                                Celle[RoomXCoordinate + 1][RoomYCoordinate][3] = true;

                                if (Celle[RoomXCoordinate + 1][RoomYCoordinate][4] == false)
                                {
                                    CellsWithCorridorTo = CellsWithCorridorTo + 1;
                                    ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate + 1;
                                    ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate;
                                }

                                Celle[RoomXCoordinate + 1][RoomYCoordinate][4] = true;
                                break;
                            case 2:
                                Celle[RoomXCoordinate][RoomYCoordinate - 1][0] = true;

                                if (Celle[RoomXCoordinate][RoomYCoordinate - 1][4] == false)
                                {
                                    CellsWithCorridorTo = CellsWithCorridorTo + 1;
                                    ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate;
                                    ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate - 1;
                                }

                                Celle[RoomXCoordinate][RoomYCoordinate - 1][4] = true;
                                break;
                            case 3:
                                Celle[RoomXCoordinate - 1][RoomYCoordinate][1] = true;

                                if (Celle[RoomXCoordinate - 1][RoomYCoordinate][4] == false)
                                {
                                    CellsWithCorridorTo = CellsWithCorridorTo + 1;
                                    ListOfCellsWithCorridor[RoomNumber][0] = RoomXCoordinate - 1;
                                    ListOfCellsWithCorridor[RoomNumber][1] = RoomYCoordinate;
                                }

                                Celle[RoomXCoordinate - 1][RoomYCoordinate][4] = true;
                                break;
                            default:
                                MessageBox.Show("Something went wrong");
                                break;
                        }

                        RoomNumber = RoomNumber + 1;


                        // visuelle ting burde ske her


                    }
                    else
                    {
                        i = i - 1;
                    }

                    // visuelle ting her?
                }
            }
        }
    }
}
