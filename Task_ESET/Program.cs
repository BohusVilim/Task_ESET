using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using Task_ESET;
using Task_ESET.Models;

Helper helper = new Helper();
Parser parser = new Parser(helper);
Printer printer = new Printer(parser);

string exit = "n";

do
{
    var result = parser.Parse();
    List<InfectedFile> list = result.Item1;
    DateTime t = result.Item2;

    printer.Print(list);
    helper.TimeCatcher(t);

    Console.Write("\nPress y to exit the program, otherwise press any other key. Confirm with enter: ");
    exit = Console.ReadLine();
}

while(exit != "y");

