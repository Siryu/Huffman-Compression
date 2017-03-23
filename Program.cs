using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman-Compression
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "Kelsey bo belsey bannana fanna fo felsey and the quick brown fox jummped over a lazy cow and some more letters and stuff";
            HuffmanTree compression = new HuffmanTree();
            compression.CreateTree(input);
            string compressedString = compression.Compress(input);

            Console.WriteLine("uncompressed size = " + input.Length * 8 + " bits");
            Console.WriteLine("compressed size   = " + compressedString.Length + " bits");
            Console.WriteLine();
            Console.WriteLine(compressedString);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(compression.Decompress(compressedString));
        }
    }
}
