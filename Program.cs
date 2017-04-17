using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kaziss
{
    class Letter
    {
        public char name;
        public double freqency;
    }

    class Program
    {
        static void fill_frequency(string file_name, List<Letter> letters)
        {
            StreamReader reader;
            reader = new StreamReader(file_name + ".txt");

            char my_char;
            int text_count = 0;

            while (!reader.EndOfStream)
            {
                my_char = (char)reader.Read();

                letters.Find(x => x.name == my_char).freqency++;

                text_count++;
            }

            reader.Close();

            foreach (Letter letter in letters)
            {
                letter.freqency = letter.freqency / text_count;
            }
        }

        static void create_list(List<Letter> letters)
        {
            int i = 0;

            for (char c = 'а'; c <= 'я'; c++)
            {
                letters.Add(new Letter());
                letters[i].name = c;
                letters[i].freqency = 0;
                i++;
            }
        }

        static double koziss_method(List<Letter> letters, int count)
        {
            double index = 0;

            foreach(Letter letter in letters)
            {
                index = index + Math.Pow(letter.freqency, 2);
            }

            Console.WriteLine(index);
            return index;
        }

        static void read_only_count(List<Letter> letters, int count, string file_name)
        {
            StreamReader reader;
            reader = new StreamReader(file_name + ".txt");

            char my_char;
            int cycle_count = 1;
            int text_count = 1;


            while (!reader.EndOfStream)
            {
                if (cycle_count % count != 0)
                {
                    cycle_count++;
                    my_char = (char)reader.Read();
                    continue;
                }

                my_char = (char)reader.Read();

                letters.Find(x => x.name == my_char).freqency++;

                text_count++;
                cycle_count++;
            }

            reader.Close();

            foreach (Letter letter in letters)
            {
                letter.freqency = letter.freqency / text_count;
            }

            Console.WriteLine("text_count " + text_count);
        }

        static void Main(string[] args)
        {
            List<Letter> letters = new List<Letter> { };
            List<Letter> letters_clean = new List<Letter> { };

            create_list(letters);
            create_list(letters_clean);

            fill_frequency("file", letters_clean);
            fill_frequency("fixed", letters);

            for (int i = 0; i < letters_clean.Count; i++)
            {
                Console.Write(letters_clean[i].name + " " + Math.Round(letters_clean[i].freqency*100) + "%  | ");
                Console.WriteLine(letters[i].name + " " + Math.Round(letters[i].freqency*100) + "%");
            }

            koziss_method(letters, 2);

            //read_only_count(letters, 21, "fixed");

            for (int i = 0; i < letters_clean.Count; i++)
            {
                Console.Write(letters_clean[i].name + " " + Math.Round(letters_clean[i].freqency * 100) + "%  | ");
                Console.WriteLine(letters[i].name + " " + Math.Round(letters[i].freqency * 100) + "%");
            }

            double index_2 = koziss_method(letters, 2);

            int zer = 3;

            while(Convert.ToString(index_2)[3] != '5' && zer <= 2000)
            {
                Console.WriteLine("key :" + zer);

                read_only_count(letters, zer, "fixed");

                /*for (int i = 0; i < letters_clean.Count; i++)
                {
                    //Console.Write(letters_clean[i].name + " " + Math.Round(letters_clean[i].freqency * 100) + "%  | ");
                    //Console.WriteLine(letters[i].name + " " + Math.Round(letters[i].freqency * 100) + "%");
                }*/

                index_2 = koziss_method(letters, 2);

                zer++;
            }

            Console.ReadLine();
        }
    }
}
