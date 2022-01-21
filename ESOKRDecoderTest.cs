using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings;

namespace ConsoleApp2
{
    class Program
    {
        static uint getData(uint input)
        {
            uint temp = input;
            if (temp >= 0xE4D880 && temp <= 0xE51680) //f(va>=0x6E00 && va <= 0xAC00)
            {
                temp += 0x3E00;
            }
            return temp;
        }
        static string getString(string input)
        {
            uint d = getData(Convert.ToUInt32(input, 16));
            byte[] dat = BitConverter.GetBytes(d);
            Array.Reverse(dat);
            string o = Encoding.UTF8.GetString(dat);
            return o;
        }
        static string getString(byte[] input)
        { 
            string o = Encoding.UTF8.GetString(input);
            return o;
        }
        static void Main(string[] args)
        {
            string path = @"C:\Users\infodba\Desktop\ESO Korean\sv.csv";
            List<string> lines = new List<string>();
            lines.AddRange(File.ReadLines(path, Encoding.UTF8));
            for (int i =0; i < 50; i++)
            {
                string text = lines[i].Split("\",\"", StringSplitOptions.None)[4] ;
                text = text.Substring(0, text.Length - 1);
                byte[] data = Encoding.UTF8.GetBytes(text);
                List<Byte> instByte = new List<byte>();
                int bitLeft = 0;
                int scope =0;
                while(scope < data.Length)
                {
                    if(data[scope] > 0xE0 && data[scope] <= 0xEF)
                    {
                        bitLeft = 2;
                    }
                    instByte.Add(data[scope]);
                    bitLeft--;
                    if(bitLeft < 0)
                    {
                        bitLeft = 0;
                        string v = "";
                        foreach(var b in instByte)
                        {
                            v += b.ToString("X2");
                        }
                        uint d = getData(Convert.ToUInt32(v, 16));
                        byte[] dat = BitConverter.GetBytes(d);
                        Array.Reverse(dat);
                        string o = Encoding.UTF8.GetString(dat);


                        string af = "";
                        foreach (var b in dat)
                        {
                            af += b.ToString("X2");
                        }

                        //Console.Write(o);
                        //Console.Write(String.Format("{1}[{2}:{0}]", af, o,v));
                        Console.Write(o);
                        instByte.Clear();
                    }
                    scope++;
                }
                Console.WriteLine();
                //foreach (var item in data)
                //{
                //    Console.Write("[");
                //    Console.Write(item.ToString("X2"));
                //    Console.Write("]");
                //}
                //for(int proc = 0; proc < (data.Length/3); proc++)
                //{
                //    string hexText = "";
                //    for(int pc = proc*3; pc < proc*3 + 3; pc++)
                //    {
                //        hexText+= data[pc].ToString("X2");
                //    }
                //    Console.Write(String.Format("{0}:{1}\n", getString(hexText), text[proc]));
                //}
                //Console.WriteLine();
            }
            while (true)
            {
                string input = Console.ReadLine(); //"E88AAC";//
                //byte[] dat = Encoding.UTF8.GetBytes(input);
                ////Array.Reverse(dat);
                //string o = Encoding.UTF8.GetString(dat);
                ////string o = Encoding.UTF8.GetString(new byte[] {232, 138,172});
                //string v = "";
                //foreach (var b in dat)
                //{
                //    v += b.ToString("X2");
                //}
                List<string> l = new List<string>();
                l.AddRange(File.ReadLines(path, Encoding.UTF8));
                List<string> outp = new List<string>();
                for (int i = 1; i < l.Count; i++)
                {
                    string o = "";
                    string text = l[i].Split("\",\"", StringSplitOptions.None)[4];
                    text = text.Substring(0, text.Length - 1);
                    byte[] encoded = Encoding.UTF8.GetBytes(text);
                    byte[] uni = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encoded);
                    string uniText = Encoding.Unicode.GetString(uni);
                    char[] c = uniText.ToCharArray();
                    foreach (var v in c)
                    {
                        uint va = Convert.ToUInt32(v);
                        if(va>=0x6E00 && va <= 0xAC00)
                        {
                            va += 0x3E00;
                        }
                        //Convert.ToUInt32(v).ToString("X2"))
                        byte[] utf = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, BitConverter.GetBytes(va));
                        utf = Array.FindAll(utf,e=>e!=0);
                        o += Encoding.UTF8.GetString(utf);
                        //Console.Write(Encoding.Unicode.GetString(BitConverter.GetBytes(va)));
                    }
                    outp.Add(o);
                    //Console.WriteLine();
                }
                File.WriteAllLines(@"C:\Users\infodba\Desktop\ESO Korean\test.txt", outp, Encoding.UTF8);
                //Console.WriteLine(new String(c));

                //File.WriteAllText(@"C:\Users\infodba\Desktop\ESO Korean\test2.txt", new String(c), Encoding.UTF8);
                //File.WriteAllText(@"C:\Users\infodba\Desktop\ESO Korean\test3.txt", new String(c), Encoding.Unicode);

            }
        }
    }
}
