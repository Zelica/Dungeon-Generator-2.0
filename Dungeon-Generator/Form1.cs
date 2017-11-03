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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Hvor mange rum er der mangler at blive genereret
            int NumberOfRoomsLeft = 0;

            // brug til at finde ud af hvor mange korridore der kan placeres ud fra et rum
            int NumberOfCorridors = 0;

            // Kordinaterne til et rum
            int RoomXCoordinate = 0;
            int RoomYCoordinate = 0;

            // Minimum og maximum af korridore der må være ud fra et rum
            int Minimum = 0;
            int Maximum = 0;

            // Antalet af korridore der bliver genereret ud fra et rum
            int NewCorridors = 0;

            // Retningen Korridoren bliver genereret i ud fra rummet
            int Direction = 0;

            // Brug til liste over celler med korridore til uden et rum
            int CellNumber = 0;

            // Bruges til at finde et tilfældig cell med en korridor til hvor der så kan placeres et rum
            int RandomCellWithCorridor = 0;

            // Antallet af punkter på listen over celler med korridore til uden et rum
            int ListMaximum = 0;

            // Antal celler med minimum en korridor til
            // Skal bruges til at beregne hvor mange korridore der må være ud fra et rum
            int CellsWithCorridorTo = 0;

            // Antal korridorer det går til det rum der lige er sat
            // Skal bruges til at beregne hvor mange korridore der må være ud fra et rum
            int NumberOfCorridorsToRoom = 0;

            // Hvor mange af siderne i et rum der er ud til kanten af kordinatsystemet
            int NumberOfSidesToEdge = 0;

            // Hvad man skal ændre på i korrdinasættet fra det nuværende rum for at få det rum en korridor går til
            int YModdifire = 0;
            int XModdifire = 0;

            // Den retning en korridor vil gå til en celle eller et rum 
            int DirectionToRoom = 0;

            // Antallet af korridore placeret alt i alt
            int CorridorsPlaced = 0;

            // placeringen af det første rum forgår anderledes derfor bruges denne til at teste det
            bool FirstRoom = true;

            // Den string der bliver vist i tekstboksen til sidst
            string Display = "";

            // antal celler ud ad x og y aksen
            int x = 50;
            int y = 2;

            NumberOfRoomsLeft = int.Parse(numericUpDown1.Text);

            // Der laves et kordinatsystem af arrays in arrays hvor
            // første array angiver x kordinatet 
            // anden array angiver y kordinatet 
            // tredje array skal bruges til at glemme values i
            //    første (0) til fjere (3) bool er om der er en gang til feltet i rækkefølgen N, Ø, S, V
            //    femte (4) bool er til hvorvidt der er en gang til cellen
            //    sjette (5) er hvorvidt der er et rum
            bool[][][] Celle = new bool[x][][];
            for (int i = 0; i < x; i++)
            {
                Celle[i] = new bool[y][];

                for (int j = 0; j < y; j++)
                {
                    Celle[i][j] = new bool[6];

                    for (int k = 0; k < 6; k++)
                    {
                        Celle[i][j][k] = false;
                    }
                }        
            }

            // formelen under beregner det maksimale antal korridore der kan placeres ud fra antalet at rum 
            ListMaximum = Convert.ToInt32(Math.Floor(2 * (NumberOfRoomsLeft - 1 * Math.Sqrt(NumberOfRoomsLeft))));

            // liste over celler med gange og uden rum hvor det angives [nummeret på rummet][x og y kordinater (0 er x og 1 er y)]
            int[][] ListOfCellsWithCorridor = new int[ListMaximum][];
            for (int i = 0; i < ListMaximum; i++)
            {
                ListOfCellsWithCorridor[i] = new int [2];

                for (int j = 0; j < 2; j++)
                {
                    ListOfCellsWithCorridor[i][j] = -1;
                }
            }

            // Under bliver resten af rummene genereret 
            for (int i = 0; i < NumberOfRoomsLeft;)
            {
                if (FirstRoom == true)
                {
                    // Cellen for det første rum bliver valgt
                    RoomXCoordinate = x / 2 - 1;
                    RoomYCoordinate = y / 2 - 1;

                    FirstRoom = false;
                }
                else
                {
                    // Alle de andre rums placering bliver valgt
                    do
                    {
                        Random random1 = new Random();
                        RandomCellWithCorridor = random1.Next(0, Math.Min(ListMaximum, CorridorsPlaced));
                    }
                    while (ListOfCellsWithCorridor[RandomCellWithCorridor][0] == -1);
                    // dette loop vil blive ved med at køre indtil at der er fundet et punkt på listen hvor der står noget

                    RoomXCoordinate = ListOfCellsWithCorridor[RandomCellWithCorridor][0];
                    RoomYCoordinate = ListOfCellsWithCorridor[RandomCellWithCorridor][1];

                    // Kordinaterne på listen sættes til -1 igen da cellen hvor der nu er et rum ikke skal vælges mere end en gang
                    ListOfCellsWithCorridor[RandomCellWithCorridor][0] = -1;
                    ListOfCellsWithCorridor[RandomCellWithCorridor][1] = -1;

                    CellsWithCorridorTo = CellsWithCorridorTo - 1;
                }

                // Der gemmes at der er et rum
                Celle[RoomXCoordinate][RoomYCoordinate][5] = true;

                NumberOfRoomsLeft = NumberOfRoomsLeft - 1;

                NumberOfCorridors = NumberOfRoomsLeft - CellsWithCorridorTo;

                // varabler sættes til 0 igen
                NumberOfCorridorsToRoom = 0;
                NumberOfSidesToEdge = 0;

                for (int j = 0; j <= 3; j++)
                {
                    if (Celle[RoomXCoordinate][RoomYCoordinate][j])
                    {
                        NumberOfCorridorsToRoom = NumberOfCorridorsToRoom + 1;
                    }
                }

                // Under bliver der taget højde for hvis rummet der er placeret er ved siden af kanten
                if (RoomXCoordinate == x - 1 || RoomXCoordinate == 0)
                {
                    NumberOfSidesToEdge++;
                }

                if (RoomYCoordinate == y - 1 || RoomYCoordinate == 0)
                {
                    NumberOfSidesToEdge++;
                }

                // Antalet af gange der kan placeres findes
                if (NumberOfCorridors >= 4)
                {
                    Minimum = 1;
                    Maximum = Math.Min(4 - NumberOfCorridorsToRoom, 4 - NumberOfSidesToEdge);
                }
                else if (0 < NumberOfCorridors && NumberOfCorridors < 4)
                {
                    Minimum = 1;
                    Maximum = Math.Min(NumberOfCorridors, Math.Min(4 - NumberOfCorridorsToRoom, 4 - NumberOfSidesToEdge));
                    Celle[RoomXCoordinate][RoomYCoordinate][4] = true;
                }
                else
                {
                    Minimum = 0;
                    Maximum = 0;
                }

                // på grund af at det maximale nummer ikke bliver taget med når man generere et tilfældigt nummer pluses der med 1
                Random random2 = new Random();
                NewCorridors = random2.Next(Minimum, Maximum + 1);

                CorridorsPlaced = CorridorsPlaced + NewCorridors;

                for (int j = 0; j < NewCorridors; j++)
                {
                    Random random3 = new Random();
                    Direction = random3.Next(0, 4);

                    if (Celle[RoomXCoordinate][RoomYCoordinate][Direction] == false)
                    {
                        Celle[RoomXCoordinate][RoomYCoordinate][Direction] = true;

                        // Det bliver fortalt til cellen hvor gangen går til, at der er en gang
                        // Og cellen hvor gangen går til tilføjes til en liste
                        switch (Direction)
                        {
                            case 0:
                                XModdifire = 0;
                                YModdifire = 1;
                                DirectionToRoom = 2;
                                break;

                            case 1:
                                XModdifire = 1;
                                YModdifire = 0;
                                DirectionToRoom = 3;
                                break;

                            case 2:
                                XModdifire = 0;
                                YModdifire = -1;
                                DirectionToRoom = 0;
                                break;

                            case 3:
                                XModdifire = -1;
                                YModdifire = 0;
                                DirectionToRoom = 1;
                                break;

                            default:
                                MessageBox.Show("Something went wrong");
                                break;
                        }

                        try
                        {
                            Celle[RoomXCoordinate + XModdifire][RoomYCoordinate + YModdifire][DirectionToRoom] = true;
                            if (Celle[RoomXCoordinate + XModdifire][RoomYCoordinate + YModdifire][4] == false)
                            {
                                CellsWithCorridorTo = CellsWithCorridorTo + 1;
                                ListOfCellsWithCorridor[CellNumber][0] = RoomXCoordinate + XModdifire;
                                ListOfCellsWithCorridor[CellNumber][1] = RoomYCoordinate + YModdifire;
                            }
                            Celle[RoomXCoordinate + XModdifire][RoomYCoordinate + YModdifire][4] = true;

                            CellNumber = CellNumber + 1;
                        }
                        catch
                        {
                            j = j - 1;
                        }

                    }
                    else
                    {
                        j = j - 1;
                    }
                }
            }

            // Selve dugeonen bliver vist som tekst
            Display = "";
            
            for (int i = y - 1; i > 0; i-- )
            {
                for (int j = 0; j < x; j++)
                {
                    if (Celle[j][i][5] == true)
                    {
                        Display = Display + "◻";
                        if (Celle[j][i][1] == true)
                        {
                            Display = Display + "═";
                        }
                        else
                        {
                            Display = Display + "      ";
                        }
                    }
                    else
                    {
                        Display = Display + "                ";
                    }
                }
                Display = Display + "\r\n";
                for (int j = 0; j < x; j++)
                {
                    if (Celle[j][i][2] == true)
                    {
                        Display = Display + " ║         ";
                    }
                    else
                    {
                        Display = Display + "                ";
                    }
                }
                Display = Display + "\r\n";
            }

            textBox2.Text = Display;

        }
    }
}
