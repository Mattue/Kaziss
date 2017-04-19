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

        static void print_freqensys(List<Letter> letter_1, List<Letter> letter_2)
        {
            for (int i = 0; i < letter_1.Count; i++)
            {
                Console.Write(letter_1[i].name + " " + Math.Round(letter_1[i].freqency * 100) + "%  | ");
                Console.WriteLine(letter_2[i].name + " " + Math.Round(letter_2[i].freqency * 100) + "%");
            }
        }

        static double koziss_method(List<Letter> letters, int count)
        {
            double index = 0;

            foreach (Letter letter in letters)
            {
                index = index + Math.Pow(letter.freqency, 2);
            }

            Console.WriteLine(index);
            return index;
        }

        static void read_only_count(List<Letter> letters, int count, string file_name)
        {

            foreach (Letter letter in letters)
            {
                letter.freqency = 0;
            }

            StreamReader reader;
            reader = new StreamReader(file_name + ".txt");

            char my_char;
            int cycle_count = 0;
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
        }

        static string find_and_replace(int start_index, int count, char a, char b, string line)
        {
            for (int i = start_index; i < line.Length; i += count)
            {
                if (line[i] == b)
                {
                    line = line.Remove(i, 1);
                    line = line.Insert(i, Convert.ToString(a));
                }
            }

            return line;
        }

        static void frequency_recount(int start_index, int count, List<Letter> letters)
        {
            StreamReader reader;
            reader = new StreamReader("fixed.txt");

            string text_line = reader.ReadLine();

            reader.Close();

            string cuted_line = "";

            for (int i = start_index; i < text_line.Length; i += count)
            {
                cuted_line += text_line[i];
            }

            for (int i = 0; i < cuted_line.Length; i++)
            {
                letters.Find(x => x.name == cuted_line[i]).freqency++;
            }

            foreach (Letter letter in letters)
            {
                letter.freqency = letter.freqency / cuted_line.Length;
            }
        }

        static string move_letters_by_index(int move_index, List<Letter> letters_clean, string line, int start_index, int count)
        {
            //string test_line = "абвгдежзийклмнопрстуфхцчшщъыьэюя";

            for (int i = start_index; i < line.Length; i += count)
            {
                int test_index = letters_clean.IndexOf(letters_clean.Find(x => x.name == line[i]));
                line = line.Remove(i, 1);
                test_index += move_index;
                if (test_index < 0)
                {
                    test_index += 32;
                }
                if (test_index >= 32)
                {
                    test_index -= 32;
                }
                line = line.Insert(i, Convert.ToString(letters_clean[test_index].name));
            }

            return line;
        }

        static void Main(string[] args)
        {
            StreamReader reader;
            reader = new StreamReader("fixed.txt");

            string text_line = reader.ReadLine();

            reader.Close();

            List<Letter> letters = new List<Letter> { };
            List<Letter> letters_clean = new List<Letter> { };

            create_list(letters);
            create_list(letters_clean);

            fill_frequency("file", letters_clean);
            fill_frequency("fixed", letters);

            print_freqensys(letters_clean, letters);

            double index_2 = koziss_method(letters, 2);

            //длина ключа
            int zer = 2;

            while(Convert.ToString(index_2)[3] != '5' && zer <= 2000)
            {
                Console.WriteLine("key :" + zer);

                read_only_count(letters, zer, "fixed");

                index_2 = koziss_method(letters, 2);

                zer++;
            }

            zer--;

            //точка вхождения
            int start_index = 0;

            int show_index = 1;

            //print_freqensys(letters_clean, letters);

            for (int i = 0; i < letters_clean.Count; i++)
            {
                Console.WriteLine(letters_clean[i].name + " " + String.Format("{0:0.000}", letters_clean[i].freqency) + " | " + letters[i].name + " " + String.Format("{0:0.000}", letters[i].freqency));
            }

            Console.WriteLine("Начинаем с : " + show_index + " буквы");
            Console.WriteLine("Длина ключа : " + zer);

            Console.WriteLine("1. Печать таблицы");
            Console.WriteLine("2. Печать текста по длине ключа");
            Console.WriteLine("3. Сдвинуть буквы");
            Console.WriteLine("4. Изменить точку вхождения");
            Console.WriteLine("5. Печать расшифрованого текста");
            Console.WriteLine("9. Выход");

            int insert_value = 0;

            while ((insert_value = Convert.ToInt32(Console.ReadLine())) != 9)
            {
                switch(insert_value)
                {
                    case 1:
                        {
                            Console.WriteLine("1");
                            for (int i = 0; i < letters_clean.Count; i++)
                            {
                                Console.WriteLine(letters_clean[i].name + " " + String.Format("{0:0.000}", letters_clean[i].freqency) + " | " + letters[i].name + " " + String.Format("{0:0.000}", letters[i].freqency));
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("2");
                            for (int i = start_index; i <= text_line.Length; i += zer)
                            {
                                Console.Write(text_line[i]);
                            }
                            Console.WriteLine();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("3");

                            Console.Write("На какое значение: ");
                            int move_index = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Сдвиг на " + move_index + " букв");

                            text_line = move_letters_by_index(move_index, letters_clean, text_line, start_index, zer);

                            /*Console.Write("Какую буву: ");
                            char b = Convert.ToChar(Console.ReadLine());
                            Console.Write("На какую буву: ");
                            char a = Convert.ToChar(Console.ReadLine());
                            text_line = find_and_replace(start_index, zer, a, b, text_line);*/

                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("4");
                            if (start_index == zer)
                            {
                                start_index = 0;
                                show_index = 1;
                            }
                            else
                            {
                                start_index++;
                                show_index++;
                            }
                            frequency_recount(start_index, zer, letters);
                            Console.WriteLine("Начинаем с : " + show_index + " буквы");
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine(text_line);
                            break;
                        }
                    case -2:
                        {
                            Console.WriteLine("-2");
                            break;
                        }
                    default:
                        break;
                }

                Console.WriteLine("Начинаем с : " + show_index + " буквы");
                Console.WriteLine("Длина ключа : " + zer);

                Console.WriteLine("1. Печать таблицы");
                Console.WriteLine("2. Печать текста по длине ключа");
                Console.WriteLine("3. Сдвинуть буквы");
                Console.WriteLine("4. Изменить точку вхождения");
                Console.WriteLine("5. Печать расшифрованого текста");
                Console.WriteLine("9. Выход");
            }

            Console.ReadLine();
        }
    }
}
