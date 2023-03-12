using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] sudoku = new int[9,9];
            List<int>[] numPool = new List<int>[4]; //0 - Horizontal, 1 vertical, 2 square, 3 finalChoice
            int[,] boxMinMax = new int[2,2];


            int attempt = 0;
            bool notSuccess = true;
            bool attemptFail = false;
            Random rnd = new Random();

            //for(int x = 0; x < 9; x++)
            //    Console.WriteLine(x / 3);

            while(notSuccess)
            {
                attempt++;
                attemptFail = false;
                //Console.WriteLine("Beginning attempt {0}", attempt);
                // reset board
                for (int a = 0; a < sudoku.GetLength(0); a++) // row
                {
                    for (int b = 0; b < sudoku.GetLength(1); b++) // column
                    {
                        sudoku[a, b] = 0;
                    }
                }

                #region Trial
                for (int a = 0; a < sudoku.GetLength(0); a++) // row
                {
                    for (int b = 0; b < sudoku.GetLength(1); b++) // column
                    {
                        // reset numPools
                        numPool = new List<int>[4];
                        for (int c = 0; c < numPool.Length - 1; c++)
                        {
                            numPool[c] = new List<int>();
                            for (int d = 1; d < 10; d++)
                            {
                                numPool[c].Add(d);
                            }
                        }

                        // check horizontal
                        for (int c = 0; c < sudoku.GetLength(0); c++)
                        {
                            if (c == b)
                            {
                                continue;
                            }
                            else
                            {
                                if (numPool[0].Contains(sudoku[a, c]))
                                    numPool[0].Remove(sudoku[a, c]);
                            }
                        }
                        // check vertical
                        for (int c = 0; c < sudoku.GetLength(1); c++)
                        {
                            if (c == a)
                            {
                                continue;
                            }
                            else
                            {
                                if (numPool[1].Contains(sudoku[c, b]))
                                    numPool[1].Remove(sudoku[c, b]);
                            }
                        }
                        // check box
                        boxMinMax[0, 0] = (a / 3) * 3;
                        boxMinMax[0, 1] = boxMinMax[0, 0] + 2;
                        boxMinMax[1, 0] = (b / 3) * 3;
                        boxMinMax[1, 1] = boxMinMax[1, 0] + 2;
                        for (int c = boxMinMax[0, 0]; c <= boxMinMax[0, 1]; c++)
                        {
                            for (int d = boxMinMax[1, 0]; d <= boxMinMax[1, 1]; d++)
                            {
                                if (c == a && d == b)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (numPool[2].Contains(sudoku[c, d]))
                                        numPool[2].Remove(sudoku[c, d]);
                                }
                            }
                        }

                        // consolidate pools
                        numPool[3] = new List<int>();
                        for (int c = 1; c < 10; c++)
                        {
                            if (numPool[0].Contains(c) && numPool[1].Contains(c) && numPool[2].Contains(c))
                                numPool[3].Add(c);
                        }

                        if (numPool[3].Count > 0)
                        {
                            sudoku[a, b] = numPool[3][rnd.Next(0, numPool[3].Count)];
                        }
                        else
                            attemptFail = true;

                        if (attemptFail)
                            break;
                    }
                    if (attemptFail)
                        break;
                }
                #endregion

                #region Trial2
                //for (int a = 0; a < sudoku.GetLength(0); a++)
                //{
                //    int b = a;
                //    int c = 0;
                //    do
                //    {
                //        // reset numPools
                //        numPool = new List<int>[4];
                //        for (int d = 0; d < numPool.Length - 1; d++)
                //        {
                //            numPool[d] = new List<int>();
                //            for (int e = 1; e < 10; e++)
                //            {
                //                numPool[d].Add(e);
                //            }
                //        }

                //        // check horizontal
                //        for (int d = 0; d < sudoku.GetLength(0); d++)
                //        {
                //            if (d == b)
                //            {
                //                continue;
                //            }
                //            else
                //            {
                //                if (numPool[0].Contains(sudoku[b, d]))
                //                    numPool[0].Remove(sudoku[b, d]);
                //            }
                //        }
                //        // check vertical
                //        for (int d = 0; d < sudoku.GetLength(1); d++)
                //        {
                //            if (d == c)
                //            {
                //                continue;
                //            }
                //            else
                //            {
                //                if (numPool[1].Contains(sudoku[d, c]))
                //                    numPool[1].Remove(sudoku[d, c]);
                //            }
                //        }
                //        // check box
                //        boxMinMax[0, 0] = (a / 3) * 3;
                //        boxMinMax[0, 1] = boxMinMax[0, 0] + 2;
                //        boxMinMax[1, 0] = (b / 3) * 3;
                //        boxMinMax[1, 1] = boxMinMax[1, 0] + 2;
                //        for (int d = boxMinMax[0, 0]; d <= boxMinMax[0, 1]; d++)
                //        {
                //            for (int e = boxMinMax[1, 0]; e <= boxMinMax[1, 1]; e++)
                //            {
                //                if (d == a && e == b)
                //                {
                //                    continue;
                //                }
                //                else
                //                {
                //                    if (numPool[2].Contains(sudoku[c, d]))
                //                        numPool[2].Remove(sudoku[c, d]);
                //                }
                //            }
                //        }

                //        // consolidate pools
                //        numPool[3] = new List<int>();
                //        for (int d = 1; d < 10; d++)
                //        {
                //            if (numPool[0].Contains(d) && numPool[1].Contains(d) && numPool[2].Contains(d))
                //                numPool[3].Add(d);
                //        }

                //        if (numPool[3].Count > 0)
                //        {
                //            sudoku[b, c] = numPool[3][rnd.Next(0, numPool[3].Count)];
                //        }

                //        if (b - 1 >= 0)
                //        {
                //            b--;
                //            c++;
                //        }
                //        else
                //        {
                //            break;
                //        }

                //    } while (true);
                //} 
                #endregion

                if (!attemptFail)
                {
                    notSuccess = false;
                }
            }

            Console.WriteLine("Program took {0} attempts to generate sudoku board!", attempt);
            for(int a = 0; a < sudoku.GetLength(0); a++)
            {
                for(int b = 0; b < sudoku.GetLength(1); b++)
                {
                    Console.Write(sudoku[a, b] + "\t");
                    if (b == 2 || b == 5)
                        Console.Write("|\t");
                }
                if(a == 2 || a == 5)
                {
                    Console.WriteLine("\n------------------------+-------------------------------+-------------------------");
                }
                else
                    Console.WriteLine();
            }

            Console.ReadKey();

        }
    }
}
