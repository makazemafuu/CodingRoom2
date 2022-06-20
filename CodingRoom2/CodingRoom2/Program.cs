using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SelectionCandidat
{
    class Program
    {
        static void Main(string[] args)
        {
            string textFile = @"C:\Users\Utilisateur\OneDrive\Desktop\CNAM\CNAM Exercices\CodingRoom2Test\CodingRoom2Test\data-test2.txt";
            var data = new List<String>();
            string line;
            if (File.Exists(textFile))
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        data.Add(line);
                    }
                    file.Close();
                }
            }

            //Variables joueur & pièces.
            //La prochaine fois, change le noms des variables pour que se soit plus clair !

            int grille = Int32.Parse(data[0]);
            int coins = int.Parse(data[1]);
            string[] xy = data[2 + coins].Split(',');
            int coordX = int.Parse(xy[0]);
            int coordY = int.Parse(xy[1]);

            int[][] map = new int[grille][];
            int remainingCoins = coins;

            //Position du joueur après avoir compté ses pièces.
            xy = data[2 + coins].Split(',');

            for (int i = 0; i < grille; i++)
            {
                map[i] = new int[grille];
            }

            for (int i = 0; i < grille; i++)
            {
                for (int j = 0; j < grille; j++)
                {
                    map[i][j] = 0;
                }
            }

            //Pour mettre le 0,0 en bas à gauche et placer les pièces (x est mis en bas à gauche au lieu d'en haut à gauche).
            //On récupère les informations "data 2" par exemple, et grâce à grille - 1 - xy, on va reprendre ses informations pour les mettre en bas à gauche au lieu d'en haut à gauche.
            for (int i = 0; i < remainingCoins; i++)
            {
                xy = data[2 + i].Split(',');
                map[grille - 1 - int.Parse(xy[0])][int.Parse(xy[1])] = 2;
            }

            for (int i = 0; i < int.Parse(data[3 + coins]); i++)
            {
                MapBuild(grille, ref map, coordX, coordY, ref remainingCoins);

                //(char.Parse(data[4 + coins + i]) nous permet de récupérer les instructions tel qu'ils sont dans les paramètres de la List.
                if (char.Parse(data[4 + coins + i]) == 'u')
                {
                    coordY += 1;
                }
                if (char.Parse(data[4 + coins + i]) == 'l')
                {
                    coordX -= 1;
                }
                if (char.Parse(data[4 + coins + i]) == 'r')
                {
                    coordX += 1;
                }
                if (char.Parse(data[4 + coins + i]) == 'd')
                {
                    coordY -= 1;
                }
                if (coordX >= grille || coordX < 0 || coordY >= grille || coordY < 0)
                {
                    Console.WriteLine("out");
                    return;
                }


                DisplayMap(grille, map);

            }

            Console.Write(coins - remainingCoins);

        }

        //ref permet d'envoyer l'original au lieu d'une copie, ça envoie la référence de la variable (l'adresse).
        private static void MapBuild(int grille, ref int[][] map, int coordX, int coordY, ref int remainingCoins)
        {
            //le premier if permet au joueur de se déplacer sur une position où il y a une pièce et de remplacer donc le 2, par 1
            //puis une fois la fonction repassé dernière, cette position qui était 2 avant, puis 1, devient 0, pour que si le joeur
            //retourne sur celle-ci, iel ne reprenne pas de pièce.
            for (int i = 0; i <= grille - 1; i++)
            {
                for (int j = 0; j <= grille - 1; j++)
                {
                    if (grille - 1 - coordX == i && coordY == j)
                    {
                        if (map[i][j] == 2)
                            remainingCoins--;
                        map[i][j] = 1;
                    }
                    else if (map[i][j] == 2)
                        continue;
                    else
                        map[i][j] = 0;
                }
            }
        }

        //permet si jamais on veut voir avec VS que ça marche d'avoir un visuel (mais marche pas pour le moment)
        private static void DisplayMap(int grille, int[][] map)
        {
            for (int y = grille - 1; y >= 0; y--)
            {
                for (int x = 0; x <= grille - 1; x++)
                {
                    Console.Write($"{map[grille - 1 - x][y]} ");
                }
                Console.Write('\n');

            }
            Console.WriteLine("--------------");
        }
    }
}